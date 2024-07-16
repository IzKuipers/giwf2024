using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace giwf2024
{
    class Levels
    {
        public Form1 form;
        public Grid grid;
        public TextureController textures;
        public int Warnings = 0;
        public int CurrentLevel = 0;
        public int LevelCount = 3;
        public Point playerStartPosition = new Point(0, 0);

        public Levels(Form1 form, Grid grid, TextureController textures)
        {
            Logging.Log("Levels", "Constructing");

            this.form = form;
            this.grid = grid;
            this.textures = textures;
        }

        public void loadFile(string path)
        {
            Logging.Log("Levels.loadFile", "Attempting to load " + path);

            try
            {
                StreamReader reader = new StreamReader(path);

                string text = reader.ReadToEnd();

                List<List<string>> result = new List<List<string>> { };
                
                string[] rows = text.Split('\n');

                for (int y = 0; y < rows.Length; y++)
                {
                    List<string> row = new List<string> { };

                    string[] split = rows[y].Split(' ');

                    for (int x = 0; x < split.Length; x++)
                    {
                        row.Add(split[x]);

                    }

                    result.Add(row);
                }

                loadData(result);
            }
            catch (IOException e)
            {
                Logging.Log("Levels.loadFile", "Loading level failed!");
                DialogResult msg = MessageBox.Show("Failed to load level: " + e.Message, "Level load failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
        }

        private void loadData(List<List<string>> data)
        {
            form.Hide();
            Warnings = 0;
            Logging.Log("Levels.loadData", "Injecting level data into Grid");

            int width = data[0].Count - 1;
            Configuration.gridWidth = width;
            playerStartPosition = new Point(0, 0);
            grid.populateGrid();
            form.UpdateWindow();

            for (int y = 0; y < Configuration.gridHeight; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (data[y].Count <= x)
                    {
                        Logging.Log("Levels.loadData", "Warning " + (Warnings + 1) + ": Grid width was out of bounds for row " + y);

                        Warnings++;
                        break;
                    }

                    string cell = "" + data[y][x];

                    if (cell == "$$")
                    {
                        playerStartPosition = new Point(x, y);
                        continue;
                    }

                    string texture = textures.shortIdToId(cell);

                    if (texture == null)
                    {
                        Logging.Log("Levels.loadData", "Warning " + (Warnings + 1) + ": unrecognised cell byte '" + cell + "' at " + x + "x" + y + ", skipping");
                        Warnings++;

                        continue;
                    }

                    grid.changeCell(x, y, texture, false);
                }
            }

            grid.populateControls();
            form.updatePlayerCoinState();
            form.setPlayerPosition(playerStartPosition);
            Logging.Log("Levels.loadData", "Level load completed with " + Warnings + " warning(s).");
            form.ResumeLayout();
            form.Show();
        }

        public void nextLevel()
        {
            CurrentLevel++;

            if (CurrentLevel > LevelCount)
            {
                MessageBox.Show("Oh no! You've beaten the game. Take your victory, humble traveler!", "The end!");
                form.Close();

                return;
            }

            loadFile(@"levels\" + CurrentLevel + ".txt");
        }
    }
}
