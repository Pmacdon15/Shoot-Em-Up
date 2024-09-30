namespace Shoot_Em_Up
{
    partial class TestLevel
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
            pictureBox_Player1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox_Player1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox_Player1
            // 
            pictureBox_Player1.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox_Player1.ErrorImage = Properties.Resources.playerTransparentR;
            pictureBox_Player1.Image = Properties.Resources.playerTransparentL;
            pictureBox_Player1.InitialImage = Properties.Resources.player;
            pictureBox_Player1.Location = new Point(2585, 916);
            pictureBox_Player1.Name = "pictureBox_Player1";
            pictureBox_Player1.Size = new Size(117, 183);
            pictureBox_Player1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox_Player1.TabIndex = 0;
            pictureBox_Player1.TabStop = false;
            // 
            // TestLevel
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Center;
            ClientSize = new Size(2828, 1152);
            Controls.Add(pictureBox_Player1);
            Name = "TestLevel";
            Text = "TexstLevel";
            ((System.ComponentModel.ISupportInitialize)pictureBox_Player1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox_Player1;
    }
}