namespace giwf2024
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textures = new System.Windows.Forms.ImageList(this.components);
            this.statusLabel = new System.Windows.Forms.Label();
            this.keyDisplay = new System.Windows.Forms.Panel();
            this.yellowKeyDisplay = new System.Windows.Forms.PictureBox();
            this.blueKeyDisplay = new System.Windows.Forms.PictureBox();
            this.greenKeyDisplay = new System.Windows.Forms.PictureBox();
            this.keyDisplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.yellowKeyDisplay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blueKeyDisplay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.greenKeyDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // textures
            // 
            this.textures.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("textures.ImageStream")));
            this.textures.TransparentColor = System.Drawing.Color.Transparent;
            this.textures.Images.SetKeyName(0, "...empty");
            this.textures.Images.SetKeyName(1, "#next");
            this.textures.Images.SetKeyName(2, "deathpoint");
            this.textures.Images.SetKeyName(3, "wall");
            this.textures.Images.SetKeyName(4, "Kgreen");
            this.textures.Images.SetKeyName(5, "Kblue");
            this.textures.Images.SetKeyName(6, "Kyellow");
            this.textures.Images.SetKeyName(7, "player_final");
            this.textures.Images.SetKeyName(8, "kyellow_");
            this.textures.Images.SetKeyName(9, "kgreen_");
            this.textures.Images.SetKeyName(10, "kblue_");
            this.textures.Images.SetKeyName(11, "ironbar");
            this.textures.Images.SetKeyName(12, "Iyellow");
            this.textures.Images.SetKeyName(13, "Iblue");
            this.textures.Images.SetKeyName(14, "Igreen");
            this.textures.Images.SetKeyName(15, "player");
            this.textures.Images.SetKeyName(16, "player_happy");
            this.textures.Images.SetKeyName(17, "player_jim");
            this.textures.Images.SetKeyName(18, "player_neutral");
            this.textures.Images.SetKeyName(19, "coin");
            this.textures.Images.SetKeyName(20, "hdeathpoint");
            this.textures.Images.SetKeyName(21, "1teleporter");
            this.textures.Images.SetKeyName(22, "2teleporter");
            this.textures.Images.SetKeyName(23, "3teleporter");
            // 
            // statusLabel
            // 
            this.statusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.statusLabel.AutoSize = true;
            this.statusLabel.BackColor = System.Drawing.Color.Transparent;
            this.statusLabel.Location = new System.Drawing.Point(12, 217);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(35, 13);
            this.statusLabel.TabIndex = 0;
            this.statusLabel.Text = "label1";
            // 
            // keyDisplay
            // 
            this.keyDisplay.Controls.Add(this.yellowKeyDisplay);
            this.keyDisplay.Controls.Add(this.blueKeyDisplay);
            this.keyDisplay.Controls.Add(this.greenKeyDisplay);
            this.keyDisplay.Location = new System.Drawing.Point(223, 210);
            this.keyDisplay.Margin = new System.Windows.Forms.Padding(0);
            this.keyDisplay.Name = "keyDisplay";
            this.keyDisplay.Size = new System.Drawing.Size(60, 20);
            this.keyDisplay.TabIndex = 1;
            // 
            // yellowKeyDisplay
            // 
            this.yellowKeyDisplay.Location = new System.Drawing.Point(40, 0);
            this.yellowKeyDisplay.Margin = new System.Windows.Forms.Padding(0);
            this.yellowKeyDisplay.Name = "yellowKeyDisplay";
            this.yellowKeyDisplay.Size = new System.Drawing.Size(20, 20);
            this.yellowKeyDisplay.TabIndex = 2;
            this.yellowKeyDisplay.TabStop = false;
            // 
            // blueKeyDisplay
            // 
            this.blueKeyDisplay.Location = new System.Drawing.Point(20, 0);
            this.blueKeyDisplay.Margin = new System.Windows.Forms.Padding(0);
            this.blueKeyDisplay.Name = "blueKeyDisplay";
            this.blueKeyDisplay.Size = new System.Drawing.Size(20, 20);
            this.blueKeyDisplay.TabIndex = 1;
            this.blueKeyDisplay.TabStop = false;
            // 
            // greenKeyDisplay
            // 
            this.greenKeyDisplay.Location = new System.Drawing.Point(0, 0);
            this.greenKeyDisplay.Margin = new System.Windows.Forms.Padding(0);
            this.greenKeyDisplay.Name = "greenKeyDisplay";
            this.greenKeyDisplay.Size = new System.Drawing.Size(20, 20);
            this.greenKeyDisplay.TabIndex = 0;
            this.greenKeyDisplay.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::giwf2024.Properties.Resources.bg4;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.keyDisplay);
            this.Controls.Add(this.statusLabel);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tricky Forms";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.keyDisplay.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.yellowKeyDisplay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blueKeyDisplay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.greenKeyDisplay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList textures;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Panel keyDisplay;
        private System.Windows.Forms.PictureBox yellowKeyDisplay;
        private System.Windows.Forms.PictureBox blueKeyDisplay;
        private System.Windows.Forms.PictureBox greenKeyDisplay;
    }
}

