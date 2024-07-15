using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;
using System.Reflection;
using System.Drawing.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace giwf2024
{
    class FontManager
    {
        private Form1 form;
        private PrivateFontCollection privateFontCollection = new PrivateFontCollection();

        public FontManager(Form1 form)
        {
            this.form = form;
        }

        public void LoadCustomFont()
        {
            privateFontCollection.AddFontFile(Path.Combine(Application.StartupPath, @"fonts\Silkscreen.ttf"));

            form.setStatusLabelFont(new Font(privateFontCollection.Families[0],12.0F));
        }
    }
}
