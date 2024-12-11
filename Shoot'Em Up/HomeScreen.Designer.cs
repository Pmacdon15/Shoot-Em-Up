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
            button1 = new Button();
            SuspendLayout();
            // 
            // button_start_game
            // 
            button_start_game.FlatStyle = FlatStyle.Popup;
            button_start_game.Location = new Point(573, 316);
            button_start_game.Margin = new Padding(2);
            button_start_game.Name = "button_start_game";
            button_start_game.Size = new Size(135, 27);
            button_start_game.TabIndex = 0;
            button_start_game.Text = "Start Game";
            button_start_game.UseVisualStyleBackColor = true;
            button_start_game.Click += button_start_game_Click;
            // 
            // textBox_PlayerName
            // 
            textBox_PlayerName.Location = new Point(263, 316);
            textBox_PlayerName.Name = "textBox_PlayerName";
            textBox_PlayerName.Size = new Size(305, 27);
            textBox_PlayerName.TabIndex = 1;
            textBox_PlayerName.Text = "Player1";
            // 
            // label_PlayerName
            // 
            label_PlayerName.BackColor = Color.Black;
            label_PlayerName.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_PlayerName.ForeColor = SystemColors.ButtonHighlight;
            label_PlayerName.Location = new Point(263, 286);
            label_PlayerName.Name = "label_PlayerName";
            label_PlayerName.Size = new Size(148, 27);
            label_PlayerName.TabIndex = 2;
            label_PlayerName.Text = "Player Name";
            // 
            // button1
            // 
            button1.FlatStyle = FlatStyle.Popup;
            button1.Location = new Point(796, 35);
            button1.Margin = new Padding(2);
            button1.Name = "button1";
            button1.Size = new Size(137, 39);
            button1.TabIndex = 3;
            button1.Text = "Top Scores";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // HomeScreen
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.background_home;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(944, 510);
            Controls.Add(button1);
            Controls.Add(label_PlayerName);
            Controls.Add(textBox_PlayerName);
            Controls.Add(button_start_game);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(2);
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
        private Button button1;
    }
}
