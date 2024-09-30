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

        public TestLevel()
        {
            InitializeComponent();
            this.KeyDown += TestLevel_KeyDown;
            pictureBox_Player1.Image = Properties.Resources.playerTransparentR;

            bulletTimer.Interval = 10;
            bulletTimer.Tick += BulletTimer_Tick;
            bulletTimer.Start();
            //soundPlayer = new SoundPlayer(Properties.Resources.gunshot);
        }

        private void TestLevel_Load(object sender, EventArgs e)
        {
        }

        private void TestLevel_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                int moveDistance = (int)(this.Width * 0.05);
                pictureBox_Player1.Left += moveDistance;
                pictureBox_Player1.Image = Properties.Resources.playerTransparentR;
                isFacingRight = true; // Update direction
            }

            if (e.KeyCode == Keys.Left)
            {
                int moveDistance = (int)(this.Width * 0.05);
                pictureBox_Player1.Left -= moveDistance;
                pictureBox_Player1.Image = Properties.Resources.playerTransparentL;
                isFacingRight = false; // Update direction
            }

            if (e.KeyCode == Keys.Up)
            {
                int moveDistance = (int)(this.Height * 0.05);
                pictureBox_Player1.Top -= moveDistance;
            }

            if (e.KeyCode == Keys.Down)
            {
                int moveDistance = (int)(this.Height * 0.05);
                pictureBox_Player1.Top += moveDistance;
            }

            if (e.KeyCode == Keys.Space)
            {
                ShootBullet(isFacingRight); // Pass the direction when shooting
            }
        }

        private void ShootBullet(bool facingRight)
        {
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
            if (facingRight)
            {
                bullet.Tag = 10; // Move to the right
            }
            else
            {
                bullet.Tag = -10  ; // Move to the left
            }
        }

        private void BulletTimer_Tick(object? sender, EventArgs e)
        {
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                PictureBox bullet = bullets[i];
                bullet.Left += (int)bullet.Tag; // Move bullet based on its Tag

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
