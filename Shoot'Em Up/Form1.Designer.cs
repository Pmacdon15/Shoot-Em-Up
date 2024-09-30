namespace Shoot_Em_Up
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button_start_game = new Button();
            SuspendLayout();
            // 
            // button_start_game
            // 
            button_start_game.Location = new Point(399, 58);
            button_start_game.Name = "button_start_game";
            button_start_game.Size = new Size(131, 40);
            button_start_game.TabIndex = 0;
            button_start_game.Text = "Start Game";
            button_start_game.UseVisualStyleBackColor = true;
            button_start_game.Click += button_start_game_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(577, 257);
            Controls.Add(button_start_game);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button button_start_game;
    }
}
