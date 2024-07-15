﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

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
        }

        public bool loadCollider(string cell, Func<Point, PlayerController, bool> action)
        {
            if (store.ContainsKey(cell)) return false;

            store.Add(cell, action);

            return true;
        }

        public bool Collide(PlayerController player, Point location, string cell)
        {
            string[] keys = store.Keys.ToArray();

            Func<Point, PlayerController, bool> action = null;

            for (int i = 0; i < keys.Length; i++) { 
                if (keys[i].EndsWith(cell) || keys[i] == cell){
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
            grid.changeCell(position.X, position.Y, "empty");
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
            player.movePlayerTo(0, 0, true);
            
            return false;
        }

        public bool HiddenDeathPointCollider(Point position, PlayerController player) 
        {
            // TODO: restart level, hearts?
            MessageBox.Show("You got poisoned! Goodbye.", "Game over");
            form.Close();

            return true;
        }
    }
}
