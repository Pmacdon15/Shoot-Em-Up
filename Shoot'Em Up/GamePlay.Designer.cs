namespace Shoot_Em_Up
{
    partial class GamePlay
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
            components = new System.ComponentModel.Container();
            GameLoop = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // GameLoop
            // 
            GameLoop.Tick += GameLoop_Tick;
            // 
            // GamePlay
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Name = "GamePlay";
            Text = "GamePlay";
            WindowState = FormWindowState.Maximized;
            Load += GamePlay_Load;
            KeyUp += GamePlay_KeyUp;
            PreviewKeyDown += GamePlay_PreviewKeyDown;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer GameLoop;
    }
}