using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Specialized;

namespace giwf2024
{
    class Grid
    {
        public List<List<string>> grid = new List<List<string>> { };
        public List<List<PictureBox>> controls = new List<List<PictureBox>> {};
        public TextureController textures;
        private Form1 form;

        public Grid(Form1 form, TextureController textures)
        {
            Logging.Log("Grid", "Constructing");
            this.form = form;
            this.textures = textures;

            populateGrid();
        }

        public void populateGrid() {
            Logging.Log("Grid.populateGrid", "Populating grid");
            grid = new List<List<string>> { };

            for (int y = 0; y < Configuration.gridHeight; y++) {
                List<string> row = new List<string> { };

                for (int x = 0; x < Configuration.gridWidth; x++) {
                    row.Add(Configuration.emptyCell);
                }

                grid.Add(row);
            }

            populateControls();
        }

        public void populateControls() {
            Logging.Log("Grid.populateControls", "Populating a fresh set of controls");

            for (int y = 0; y < controls.Count; y++) {
                for (int x = 0; x < controls[y].Count; x++) {
                    if (controls[y][x] == null) continue;

                    controls[y][x].Dispose();
                }
            }

            controls = new List<List<PictureBox>> { };

            for (int y = 0; y < grid.Count; y++)
            {
                List<PictureBox> row = new List<PictureBox> { };

                for (int x = 0; x < grid[y].Count; x++)
                {
                    if (grid[y][x] == Configuration.emptyCell) {
                        row.Add(null);
                        continue;
                    }

                    PictureBox cell = new PictureBox
                    {
                        Name = "entity_" + x + "_" + y,
                        Image = textures.getTexture(grid[y][x]),
                        Location = Utilities.translateGridPosition(new Point(x, y)),
                        Size = new Size(Configuration.cellSize,Configuration.cellSize),
                        
                    };

                    row.Add(cell);
                    form.Controls.Add(cell);
                }

                controls.Add(row);
            }
        }

        public void changeCell(int x, int y, string texture, bool render = true) {
            grid[y][x] = texture;

            PictureBox cell = controlAt(x, y);

            if (cell == null) return;

            if (texture == Configuration.emptyCell)
            {
                cell.Location = new Point(-20, -20);
                cell.Dispose();
                controls[y][x] = null;

                return;
            }

            cell.Image = textures.getTexture(grid[y][x]);
        }

        public PictureBox controlAt(int x, int y)
        {
            return controls[y][x];
        }

        public string cellAt(int x, int y) {
            return grid[y][x];
        }
    }
}
