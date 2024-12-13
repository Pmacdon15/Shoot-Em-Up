namespace Shoot_Em_Up
{
    partial class HomeScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomeScreen));
            button_start_game = new Button();
            textBox_PlayerName = new TextBox();
            label_PlayerName = new Label();
            button_Top_Scores = new Button();
            SuspendLayout();
            // 
            // button_start_game
            // 
            button_start_game.FlatStyle = FlatStyle.Popup;
            button_start_game.Location = new Point(860, 474);
            button_start_game.Name = "button_start_game";
            button_start_game.Size = new Size(202, 40);
            button_start_game.TabIndex = 0;
            button_start_game.Text = "Start Game";
            button_start_game.UseVisualStyleBackColor = true;
            button_start_game.Click += button_start_game_Click;
            // 
            // textBox_PlayerName
            // 
            textBox_PlayerName.Location = new Point(394, 474);
            textBox_PlayerName.Margin = new Padding(4, 4, 4, 4);
            textBox_PlayerName.Name = "textBox_PlayerName";
            textBox_PlayerName.Size = new Size(456, 35);
            textBox_PlayerName.TabIndex = 1;
            textBox_PlayerName.Text = "Player1";
            // 
            // label_PlayerName
            // 
            label_PlayerName.BackColor = Color.Black;
            label_PlayerName.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_PlayerName.ForeColor = SystemColors.ButtonHighlight;
            label_PlayerName.Location = new Point(394, 429);
            label_PlayerName.Margin = new Padding(4, 0, 4, 0);
            label_PlayerName.Name = "label_PlayerName";
            label_PlayerName.Size = new Size(222, 40);
            label_PlayerName.TabIndex = 2;
            label_PlayerName.Text = "Player Name";
            // 
            // button_Top_Scores
            // 
            button_Top_Scores.FlatStyle = FlatStyle.Popup;
            button_Top_Scores.Location = new Point(1194, 52);
            button_Top_Scores.Name = "button_Top_Scores";
            button_Top_Scores.Size = new Size(206, 58);
            button_Top_Scores.TabIndex = 3;
            button_Top_Scores.Text = "Top Scores";
            button_Top_Scores.UseVisualStyleBackColor = true;
            button_Top_Scores.Click += button_Top_Scores_Click;
            // 
            // HomeScreen
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.background_home;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1416, 765);
            Controls.Add(button_Top_Scores);
            Controls.Add(label_PlayerName);
            Controls.Add(textBox_PlayerName);
            Controls.Add(button_start_game);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "HomeScreen";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Shoot'Em Up";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button_start_game;
        private TextBox textBox_PlayerName;
        private Label label_PlayerName;
        private Button button_Top_Scores;
    }
}
