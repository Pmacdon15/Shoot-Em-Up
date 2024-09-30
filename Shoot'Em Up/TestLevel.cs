using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace Shoot_Em_Up
{
    public partial class TestLevel : Form
    {
        private System.Windows.Forms.Timer bulletTimer = new System.Windows.Forms.Timer();
        private List<PictureBox> bullets = new List<PictureBox>();
        private bool isFacingRight = true; // Track the player's facing direction
        private SoundPlayer soundPlayer;

        public TestLevel()
        {
            InitializeComponent();
            this.KeyDown += TestLevel_KeyDown;
            pictureBox_Player1.Image = Properties.Resources.playerTransparentR;

            bulletTimer.Interval = 10;
            bulletTimer.Tick += BulletTimer_Tick;
            bulletTimer.Start();
            soundPlayer = new SoundPlayer(Properties.Resources.gunshot);
        }

        private void TestLevel_Load(object sender, EventArgs e)
        {
        }

        private void TestLevel_KeyDown(object? sender, KeyEventArgs e)
        {
            HandlePlayerMovement(e.KeyCode);
            if (e.KeyCode == Keys.Space)
            {
                ShootBullet(isFacingRight);
            }
        }

        private void HandlePlayerMovement(Keys keyCode)
        {
            int moveDistance = GetMoveDistance(keyCode);
            switch (keyCode)
            {
                case Keys.Right:
                    MovePlayerRight(moveDistance);
                    break;
                case Keys.Left:
                    MovePlayerLeft(moveDistance);
                    break;
                case Keys.Up:
                    MovePlayerUp(moveDistance);
                    break;
                case Keys.Down:
                    MovePlayerDown(moveDistance);
                    break;
            }
        }

        private int GetMoveDistance(Keys keyCode)
        {
            return keyCode == Keys.Right || keyCode == Keys.Left
                ? (int)(this.Width * 0.05)
                : (int)(this.Height * 0.05);
        }

        private void MovePlayerRight(int moveDistance)
        {
            pictureBox_Player1.Left += moveDistance;
            pictureBox_Player1.Image = Properties.Resources.playerTransparentR;
            isFacingRight = true;
        }

        private void MovePlayerLeft(int moveDistance)
        {
            pictureBox_Player1.Left -= moveDistance;
            pictureBox_Player1.Image = Properties.Resources.playerTransparentL;
            isFacingRight = false;
        }

        private void MovePlayerUp(int moveDistance)
        {
            pictureBox_Player1.Top -= moveDistance;
        }

        private void MovePlayerDown(int moveDistance)
        {
            pictureBox_Player1.Top += moveDistance;
        }

        private void ShootBullet(bool facingRight)
        {
            soundPlayer.Play();
            int bulletX = pictureBox_Player1.Left + (pictureBox_Player1.Width / 2) - (Properties.Resources.bullet_transparent.Width / 2);
            int bulletY = pictureBox_Player1.Top + (pictureBox_Player1.Height / 2) - (Properties.Resources.bullet_transparent.Height / 2);

            PictureBox bullet = new PictureBox
            {
                Image = Properties.Resources.bullet_transparent,
                SizeMode = PictureBoxSizeMode.AutoSize,
                Location = new Point(bulletX, bulletY)
            };
            this.Controls.Add(bullet);
            bullet.BringToFront();

            bullets.Add(bullet);

            // Adjust bullet movement direction based on player facing direction
            bullet.Tag = facingRight ? 0.01 : -0.01; // Use percentage for movement
        }

        private void BulletTimer_Tick(object? sender, EventArgs e)
        {
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                PictureBox bullet = bullets[i];
                int moveDistance = (int)(this.Width * (double)bullet.Tag); // Calculate movement distance based on form width
                bullet.Left += moveDistance; // Move bullet based on calculated distance

                if (bullet.Left < 0 || bullet.Left > this.Width) // Check if the bullet is off the screen
                {
                    this.Controls.Remove(bullet);
                    bullet.Dispose();
                    bullets.RemoveAt(i);
                }
            }
        }

    }
}
