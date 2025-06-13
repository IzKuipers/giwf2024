using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace giwf2024
{
    class PlayerController
    {
        private Form1 form;
        public PictureBox _player;
        private TextureController textures;
        private Grid grid;
        private Levels levels;
        private CollisionManager collisionManager;
        public int coins = 0;
        public int maxCoins = 0;
        public bool hasYellowKey = false;
        public bool hasBlueKey = false;
        public bool hasGreenKey = false;
        public bool zapped = false;
        public string status = "";


        public Point position = Configuration.playerStartPosition;

        public PlayerController(Form1 form, TextureController textures, Grid grid, Levels levels)
        {
            Logging.Log("PlayerController", "Constructing");

            this.form = form;
            this.textures = textures;
            this.grid = grid;
            this.levels = levels;
            this.collisionManager = new CollisionManager(grid, levels, textures, form);

            this.Spawn();
        }

        public void Spawn()
        {
            Logging.Log("PlayerController", "Spawning player");
            PictureBox player = new PictureBox
            {
                Name = "player",
                Size = new Size(Configuration.cellSize, Configuration.cellSize),
                Image = textures.getTexture("player"),
            };

            this.form.Controls.Add(player);
            this._player = player;

            player.BringToFront();

            updatePosition();
        }

        public void updatePosition()
        {
            try
            {
                _player.Location = Utilities.translateGridPosition(position);
            }
            catch (Exception e)
            {
                Logging.Log("PlayerController.updatePosition", "Updating player position failed! " + e.Message);
            }
        }

        public void movePlayerRelatively(int xModifier, int yModifier)
        {
            Logging.Log("PlayerController.movePlayerRelatively", "xm=" + xModifier + " ym=" + yModifier);

            Point newPosition = new Point();

            newPosition.X = position.X + xModifier;
            newPosition.Y = position.Y + yModifier;

            bool canDo = checkBounds(newPosition);

            if (!canDo)
            {
                return;
            }

            position = newPosition;
            updatePosition();
        }

        public void movePlayerTo(int x, int y, bool force = false)
        {
            Logging.Log("PlayerController.movePlayerTo", "x=" + x + " y=" + y);

            Point newPosition = new Point(x, y);

            bool canDo = force || checkBounds(newPosition);

            if (!canDo)
            {
                return;
            }

            position = newPosition;
            updatePosition();
        }

        public bool checkBounds(Point position)
        {
            if (position.X < 0 || position.Y < 0) return false;
            if (position.X >= Configuration.gridWidth || position.Y >= Configuration.gridHeight) return false;

            string underlyingCell = grid.cellAt(position.X, position.Y);

            bool canDo = collisionManager.Collide(this, position, underlyingCell);

            if (!canDo) return false;

            return true;
        }

        public void updatePlayerVibe()
        {
            int percentage = (int)Math.Round((double)(100 * coins) / maxCoins);

            Image image;

            if (maxCoins == 0)
            {
                image = textures.getTexture("player_jim");
            }
            else if (percentage < 25)
            {
                image = textures.getTexture("player");
            }
            else if (percentage < 50)
            {
                image = textures.getTexture("player_neutral");
            }
            else if (percentage < 75)
            {
                image = textures.getTexture("player_happy");
            }
            else
            {
                image = textures.getTexture("player_jim");
            }

            _player.Image = image;
        }

        public void collideWithEverything()
        {
            for (int y = 0; y < grid.grid.Count; y++)
            {
                for (int x = 0; x < grid.grid[y].Count; x++)
                {
                    Point position = new Point(x, y);

                    string underlyingCell = grid.cellAt(position.X, position.Y);

                    if (underlyingCell == "#next") continue;

                    collisionManager.Collide(this, position, underlyingCell);
                }
            }
        }
    }
}
