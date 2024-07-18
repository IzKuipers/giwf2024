using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace giwf2024
{
    class CollisionManager
    {
        private Grid grid;
        private Levels levels;
        private TextureController textureController;
        private Form1 form;

        private Dictionary<string, Func<Point, PlayerController, bool>> store = new Dictionary<string, Func<Point, PlayerController, bool>>();

        public CollisionManager(Grid grid, Levels levels, TextureController textureController, Form1 form)
        {
            Logging.Log("CollisionManager", "Constructing");

            this.grid = grid;
            this.levels = levels;
            this.textureController = textureController;
            this.form = form;

            loadCollider("wall", (Point o, PlayerController p) => WallCollider(o, p));
            loadCollider("coin", (Point o, PlayerController p) => CoinCollider(o, p));
            loadCollider("Kgreen", (Point o, PlayerController p) => GreenKeyCollider(o, p));
            loadCollider("Kblue", (Point o, PlayerController p) => BlueKeyCollider(o, p));
            loadCollider("Kyellow", (Point o, PlayerController p) => YellowKeyCollider(o, p));
            loadCollider("#next", (Point o, PlayerController p) => NextLevelCollider(o, p));
            loadCollider("hdeathpoint", (Point o, PlayerController p) => HiddenDeathPointCollider(o, p));
            loadCollider("1teleporter", (Point o, PlayerController p) => FirstTeleporterCollider(o, p));
            loadCollider("2teleporter", (Point o, PlayerController p) => SecondTeleporterCollider(o, p));
            loadCollider("3teleporter", (Point o, PlayerController p) => ThirdTeleporterCollider(o, p));
            loadCollider("Igreen", (Point o, PlayerController p) => GreenIronBarCollider(o, p));
            loadCollider("Iblue", (Point o, PlayerController p) => BlueIronBarCollider(o, p));
            loadCollider("Iyellow", (Point o, PlayerController p) => YellowIronBarCollider(o, p));
            loadCollider("ironbar", (Point o, PlayerController p) => IronBarCollider(o, p));
            loadCollider("Lhstart", (Point o, PlayerController p) => LaserCollider(o, p));
            loadCollider("Lhend", (Point o, PlayerController p) => LaserCollider(o, p));
            loadCollider("lhend_", (Point o, PlayerController p) => LaserOffCollider(o, p));
            loadCollider("Lhmid", (Point o, PlayerController p) => LaserCollider(o, p));
            loadCollider("lhmid_", (Point o, PlayerController p) => LaserOffCollider(o, p));
            loadCollider("lhone_", (Point o, PlayerController p) => LaserOffCollider(o, p));
            loadCollider("Lhone", (Point o, PlayerController p) => LaserCollider(o, p));
            loadCollider("lhstart_", (Point o, PlayerController p) => LaserOffCollider(o, p));
            loadCollider("Lvstart", (Point o, PlayerController p) => LaserCollider(o, p));
            loadCollider("Lvend", (Point o, PlayerController p) => LaserCollider(o, p));
            loadCollider("lvend_", (Point o, PlayerController p) => LaserOffCollider(o, p));
            loadCollider("Lvmid", (Point o, PlayerController p) => LaserCollider(o, p));
            loadCollider("lvmid_", (Point o, PlayerController p) => LaserOffCollider(o, p));
            loadCollider("lvone_", (Point o, PlayerController p) => LaserOffCollider(o, p));
            loadCollider("Lvone", (Point o, PlayerController p) => LaserCollider(o, p));
            loadCollider("lvstart_", (Point o, PlayerController p) => LaserOffCollider(o, p));
        }

        public bool loadCollider(string cell, Func<Point, PlayerController, bool> action)
        {
            if (store.ContainsKey(cell)) return false;

            Logging.Log("CollisionManager.loadCollider", "Loading collider for cell '" + cell + "'");

            store.Add(cell, action);

            return true;
        }

        public bool Collide(PlayerController player, Point location, string cell)
        {
            string[] keys = store.Keys.ToArray();

            Func<Point, PlayerController, bool> action = null;

            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i].EndsWith(cell) || keys[i] == cell)
                {
                    action = store[keys[i]];
                }
            }

            if (action == null) return true;

            return action(location, player);
        }

        public bool WallCollider(Point position, PlayerController player)
        {
            return false;
        }

        public bool CoinCollider(Point position, PlayerController player)
        {
            player.status = "You found a coin!";
            player.coins += 1;
            grid.changeCell(position.X, position.Y, "...empty");
            player.updatePlayerVibe();

            return true;
        }

        public bool GreenKeyCollider(Point position, PlayerController player)
        {
            player.status = "You found a green key!";
            player.hasGreenKey = true;
            grid.changeCell(position.X, position.Y, "kgreen_");

            form.updateKeyDisplay();

            return true;
        }

        public bool BlueKeyCollider(Point position, PlayerController player)
        {
            player.status = "You found a blue key!";
            player.hasBlueKey = true;
            grid.changeCell(position.X, position.Y, "kblue_");

            form.updateKeyDisplay();

            return true;
        }

        public bool YellowKeyCollider(Point position, PlayerController player)
        {
            player.status = "You found a yellow key!";
            player.hasYellowKey = true;
            grid.changeCell(position.X, position.Y, "kyellow_");

            form.updateKeyDisplay();

            return true;
        }

        public bool NextLevelCollider(Point position, PlayerController player)
        {
            form.Update();
            System.Threading.Thread.Sleep(10);
            levels.nextLevel();
            form.Update();

            player.movePlayerTo(levels.playerStartPosition.X, levels.playerStartPosition.Y, true);

            return false;
        }

        public bool HiddenDeathPointCollider(Point position, PlayerController player)
        {
            // TODO: restart level, hearts?
            MessageBox.Show("You got poisoned! Goodbye.", "Game over");
            form.Close();

            return true;
        }

        public bool FirstTeleporterCollider(Point position, PlayerController player)
        {
            for (int y = 0; y < grid.grid.Count; y++)
            {
                for (int x = 0; x < grid.grid[y].Count; x++)
                {
                    if (position.X == x && position.Y == y) continue;
                    if (grid.grid[y][x] != "1teleporter") continue;

                    player.movePlayerTo(x, y, true);
                    player.status = "WOOSH!!";

                    return false;
                }
            }

            return true;
        }

        public bool SecondTeleporterCollider(Point position, PlayerController player)
        {
            for (int y = 0; y < grid.grid.Count; y++)
            {
                for (int x = 0; x < grid.grid[y].Count; x++)
                {
                    if (position.X == x && position.Y == y) continue;
                    if (grid.grid[y][x] != "2teleporter") continue;

                    player.movePlayerTo(x, y, true);
                    player.status = "WOOSH!!";

                    return false;
                }
            }

            return true;
        }

        public bool ThirdTeleporterCollider(Point position, PlayerController player)
        {
            for (int y = 0; y < grid.grid.Count; y++)
            {
                for (int x = 0; x < grid.grid[y].Count; x++)
                {
                    if (position.X == x && position.Y == y) continue;
                    if (grid.grid[y][x] != "3teleporter") continue;

                    player.movePlayerTo(x, y, true);
                    player.status = "WOOSH!!";

                    return false;
                }
            }

            return true;
        }

        public bool GreenIronBarCollider(Point position, PlayerController player)
        {
            if (player.hasGreenKey)
            {
                grid.changeCell(position.X, position.Y, "...empty");
                return true;
            }

            player.status = "You need a green key!";

            return false;
        }

        public bool BlueIronBarCollider(Point position, PlayerController player)
        {
            if (player.hasBlueKey)
            {
                grid.changeCell(position.X, position.Y, "...empty");
                return true;
            }

            player.status = "You need a blue key!";

            return false;
        }

        public bool YellowIronBarCollider(Point position, PlayerController player)
        {
            if (player.hasYellowKey)
            {
                grid.changeCell(position.X, position.Y, "...empty");
                return true;
            }

            player.status = "You need a yellow key!";

            return false;
        }

        public bool IronBarCollider(Point position, PlayerController player)
        {
            return false;
        }

        public bool LaserCollider(Point position, PlayerController player) {
            MessageBox.Show("You got zapped! Goodbye.", "Game over");
            try
            {
                form.Close(); 
            }
            catch { }

            Application.Exit();

            return false;
        }

        public bool LaserOffCollider(Point position, PlayerController player) {
            return true;
        }
    }
}
