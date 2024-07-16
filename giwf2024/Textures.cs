using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace giwf2024
{
    class TextureController
    {
        private readonly string[] TextureIds = 
        {
            "coin", 
            "player", 
            "deathpoint", 
            Configuration.emptyCell, 
            "wall",
            "hdeathpoint",
            "#next",
            "Kgreen",
            "Kblue", 
            "Kyellow", 
            "kgreen_", 
            "kblue_", 
            "kyellow_",
            "Igreen",
            "Iblue",
            "Iyellow",
            "ironbar",
            "player_neutral",
            "player_happy",
            "player_jim",
            "1teleporter",
            "2teleporter", 
            "3teleporter"
        };
        private ImageList textures;

        public TextureController(ImageList textures)
        {
            this.textures = textures;
        }

        public Image getTexture(string id)
        {
            return this.textures.Images[id];
        }

        public string shortIdToId(string id)
        {
            for (int i = 0; i < TextureIds.Length; i++)
            {
                if (TextureIds[i].StartsWith(id)) return TextureIds[i];
            }

            return null;
        }
    }
}
