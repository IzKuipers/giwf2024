﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace giwf2024
{
    public partial class TrickyForm : Form
    {
        public PictureBox _player;
        private PlayerController playerController;
        private Grid grid;
        private Levels levels;
        private TextureController textureController;
        private FontManager fontManager;
        private Thread backgroundThread;
        public Thread laserThread;

        public TrickyForm()
        {
            Logging.Log("TrickyForm", "Starting giwf2024...");

            InitializeComponent();

            fontManager = new FontManager(this);
            textureController = new TextureController(textures);
            grid = new Grid(this, textureController);
            levels = new Levels(this, grid, textureController);
            playerController = new PlayerController(this, textureController, grid, levels);
            _player = playerController._player;

            updateKeyDisplay();
            UpdateWindow();
            levels.nextLevel();

            backgroundThread = new Thread(new ThreadStart(eventLoop));
            backgroundThread.IsBackground = true;
            backgroundThread.Start();
            laserThread = new Thread(new ThreadStart(laserLoop));
            laserThread.IsBackground = true;
            laserThread.Start();
        }

        public void eventLoop() {
            int length = 0;

            while (true)
            {
                Thread.Sleep(1000);

                string[] lines = Logging.store.ToArray();

                if (length == lines.Length) continue;

                length = lines.Length;

                using (StreamWriter outputFile = new StreamWriter(Path.Combine(Environment.CurrentDirectory, "tricky.log")))
                {
                    foreach (string line in lines)
                        outputFile.WriteLine(line);
                }
            }
        }

        public void laserLoop() {
            while (true) {
                Thread.Sleep(1000);

                Point playerPosition = playerController.position;                

                for (int y = 0; y < grid.grid.Count; y++)
                {
                    for (int x = 0; x < grid.grid[y].Count; x++) 
                    {
                        
                        string cell = grid.grid[y][x];
                        string lower = cell.ToLower();

                        if (!lower.StartsWith("lv") && !lower.StartsWith("lh")) {
                            continue;
                        }

                        switch (cell[0]) { 
                            case 'L': // it's currently on
                                grid.changeCell(x, y, cell.Replace("L", "l") + "_");
                                break;
                            case 'l':
                                grid.changeCell(x, y, cell.Remove(cell.Length - 1).Replace("l", "L"));
                                break;  
                        }

                        
                    }
                }

                bool collided = !playerController.checkBounds(playerPosition);
                if (collided) laserThread.Abort();
            }
        }

        public void UpdateWindow()
        {
            Logging.Log("TrickyForm", "UpdateWindow");

            this.BackColor = Color.Black;

            int width = (Configuration.gridWidth * Configuration.cellSize) + (Configuration.windowPadding * 2);
            int height = (Configuration.gridHeight * Configuration.cellSize) + (Configuration.windowPadding * 2) + 40;

            this.Size = this.SizeFromClientSize(new Size(width, height));

            statusLabel.Text = "Hello, humble traveler...";
            statusLabel.Location = new Point(Configuration.windowPadding, height - 40);

            keyDisplay.Location = new Point(width - 60 - Configuration.windowPadding, height - 20 - Configuration.windowPadding - 20);



            fontManager.LoadCustomFont();

            Update();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Keys key = e.KeyCode;

            switch (key)
            {
                case Keys.W: playerController.movePlayerRelatively(0, -1); break;
                case Keys.A: playerController.movePlayerRelatively(-1, 0); break;
                case Keys.S: playerController.movePlayerRelatively(0, 1); break;
                case Keys.D: playerController.movePlayerRelatively(1, 0); break;
                case Keys.F2:
                    Update();
                    System.Threading.Thread.Sleep(10);
                    levels.nextLevel();
                    Update();
                    playerController.movePlayerTo(0, 0, true);
                    break;
                case Keys.F9:
                    playerController.collideWithEverything();
                    break;
                case Keys.R: UpdateWindow(); break;
            }
            Update();

            string currentCell = grid.cellAt(playerController.position.X, playerController.position.Y);
            statusLabel.Text = playerController.position.X + ", " + playerController.position.Y + " | Coins: " + playerController.coins + ", " + remainingCoins() + " remaining | " + playerController.status;
        }

        public int remainingCoins()
        {
            int result = 0;

            for (int y = 0; y < grid.grid.Count; y++)
            {
                for (int x = 0; x < grid.grid[y].Count; x++)
                {
                    if (grid.grid[y][x] == "coin") result++;
                }
            }

            return result;
        }

        public void setStatusLabelFont(Font font)
        {
            statusLabel.Font = font;
            statusLabel.Update();
            Update();
        }

        public void updateKeyDisplay()
        {
            greenKeyDisplay.Image = textureController.getTexture(playerController.hasGreenKey ? "Kgreen" : "kgreen_");
            blueKeyDisplay.Image = textureController.getTexture(playerController.hasBlueKey ? "Kblue" : "kblue_");
            yellowKeyDisplay.Image = textureController.getTexture(playerController.hasYellowKey ? "Kyellow" : "kyellow_");
        }

        public void updatePlayerCoinState()
        {
            playerController.coins = 0;
            playerController.maxCoins = remainingCoins();
            _player.Image = textures.Images["player"];
            CenterToScreen();
        }

        public void setPlayerPosition(Point position) {
            playerController.movePlayerTo(position.X, position.Y, true);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
