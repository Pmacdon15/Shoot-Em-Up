//using Shoot_Em_Up;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shoot_Em_Up
{
    public partial class GamePlay : Form
    {

        int Score;
        int Collision;
        int Counter;
        string PlayerName;
        string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            //"C:\\Users\\wagne\\WFORMS\\Shoot-Em-Up\\Shoot'Em Up";



        StickMan Player = new StickMan(new Point(550, 350));
        //List<StickMan> Enemies = new List<StickMan>();
        List<SuicideBomber> Enemies = new List<SuicideBomber>();
        List<Explosion> Explosions = new List<Explosion>();
        List<Bullet> Bullets = new List<Bullet>();
        public GamePlay(string playerName)
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            Score = 0;
            Collision = 0;
            Counter = 0;
            PlayerName = playerName;

        }

        private void GamePlay_Load(object sender, EventArgs e)
        {
            int countBadGuys = 0;
            int maxBadGuys = 5;

            int[] movement = { -7, -5, -3, 0, 5, 7 };
            int[] movement2 = { -7, -5, -3, 3, 5, 7 };
            Random random = new Random();

            while (countBadGuys < maxBadGuys)
            {
                int x = random.Next(0, (int)(this.Width - (this.Width * 0.2)));
                int y = random.Next(0, (int)(this.Height - (this.Height * 0.2)));
                SuicideBomber enemy = new SuicideBomber(new Point(x, y));
                //StickMan enemy = new StickMan(new Point(x, y));

                //// Set initial movement based on position
                enemy.MoveX = (x > 550) ? movement[random.Next(movement.Length)] : movement2[random.Next(movement2.Length)];
                enemy.MoveY = movement[random.Next(movement.Length)];

                Enemies.Add(enemy);
                countBadGuys++;
            }

            GameLoop.Interval = 24;
            GameLoop.Start();

            this.Size = new Size(this.Width, this.Height);
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.Black;
            this.Paint += new PaintEventHandler(PaintObjects);
        }

        // Updated PaintObjects method
        protected void PaintObjects(object sender, PaintEventArgs e)
        {
            int rectWidth = (int)(this.Width * 0.95);
            int rectHeight = (int)(this.Height * 0.80);
            Rectangle rectangle = new Rectangle((this.Width - rectWidth) / 2, (this.Height - rectHeight) / 2, rectWidth, rectHeight);
            e.Graphics.DrawRectangle(Pens.White, rectangle);
            Region clippingRegion = new Region(rectangle);
            e.Graphics.Clip = clippingRegion;

            // Draw Player
            Player.Draw(e, Player.FacingRight);

            // Track items to remove after the loop
            List<SuicideBomber> enemiesToRemove = new List<SuicideBomber>();
            List<Bullet> bulletsToRemove = new List<Bullet>();


            // Draw explosions
            foreach (var explosion in Explosions)
            {
                explosion.Draw(e, Player.FacingRight);
            }

            // Draw bullets
            foreach (Bullet bullet in Bullets)
            {
                bullet.Draw(e, Player.FacingRight);
            }

            for (int i = Enemies.Count - 1; i >= 0; i--)
            {
                SuicideBomber enemy = Enemies[i];
                enemy.Draw(e, enemy.FacingRight);

                // Check for player collision with enemy
                if (Player.CollisionCheck(enemy))
                {
                    Score--;
                    Explosion explosion = new Explosion { Center = enemy.Center };
                    Explosions.Add(explosion); // Add explosion to the list
                    enemiesToRemove.Add(enemy);
                    continue;
                }

                //Check if enemy is is in explosion radius
                foreach (var explosion in Explosions)
                {
                    if (explosion.CheckCollision(enemy))
                    {
                        Score++;
                        Explosions.Add(explosion); // Add explosion to the list
                        enemiesToRemove.Add(enemy);
                        break;
                    }
                }
                // Check for bullet collision with enemy
                for (int j = Bullets.Count - 1; j >= 0; j--)
                {
                    Bullet bullet = Bullets[j];
                    if (bullet.CheckCollision(enemy))
                    {
                        Score++;
                        Explosion explosion = new Explosion { Center = enemy.Center };
                        Explosions.Add(explosion); // Add explosion to the list
                        enemiesToRemove.Add(enemy);
                        bulletsToRemove.Add(bullet);
                        break;
                    }
                }
            }

            // Remove collided enemies and bullets after the loop
            foreach (var enemy in enemiesToRemove) Enemies.Remove(enemy);
            foreach (var bullet in bulletsToRemove) Bullets.Remove(bullet);

            e.Graphics.ResetClip();

            // Display Score
            e.Graphics.DrawString("Score: " + Score.ToString(), new Font("Arial", 12, FontStyle.Regular), Brushes.Red, new PointF((float)(this.Width * 0.90), (float)(this.Height * 0.05)));

            //Display PlayerName
            e.Graphics.DrawString(PlayerName, new Font("Arial", 12, FontStyle.Regular), Brushes.Red, new PointF((float)(this.Width * 0.1), (float)(this.Height * 0.05)));

        }


        private void GameLoop_Tick(object sender, EventArgs e)
        {
            int rectWidth = (int)(this.Width * 0.91);
            int rectHeight = (int)(this.Height * 0.73);
            Rectangle rectangle = new Rectangle((this.Width - rectWidth) / 2, (this.Height - rectHeight) / 2, rectWidth, rectHeight);

            // Move Player within boundaries
            Player.Move(rectangle.X, rectangle.X + rectangle.Width, rectangle.Y, rectangle.Y + rectangle.Height);

            foreach (SuicideBomber enemy in Enemies)
            {
                // Calculate the direction towards the player
                int deltaX = Player.Center.X - enemy.Center.X;
                int deltaY = Player.Center.Y - enemy.Center.Y;

                // Normalize the direction and apply a speed factor (e.g., 3)
                double distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
                double speed = 3; // Adjust speed as needed

                if (distance > 0) // Avoid division by zero
                {
                    enemy.MoveX = (int)(speed * (deltaX / distance));
                    enemy.MoveY = (int)(speed * (deltaY / distance));
                    enemy.FacingRight = enemy.MoveX >= 0;
                }

                // Move the enemy within boundaries
                enemy.Move(rectangle.X, rectangle.X + rectangle.Width, rectangle.Y, rectangle.Y + rectangle.Height);
            }
            // Remove explosion after a certain duration      
            for (int i = Explosions.Count - 1; i >= 0; i--)
            {
                Explosions[i].Counter += 1;
                if (Explosions[i].Counter >= 30) Explosions.RemoveAt(i);
            }


            // Move bullets and check for boundary conditions
            for (int i = Bullets.Count - 1; i >= 0; i--)
            {
                Bullets[i].Center = new Point(
                    Bullets[i].Center.X + (Bullets[i].FacingRight ? 10 : -10),
                    Bullets[i].Center.Y
                );

                // Remove bullets out of bounds
                if (Bullets[i].Center.X < 0 || Bullets[i].Center.X > this.Width)
                {
                    Bullets.RemoveAt(i);
                }
            }
            this.Refresh();
        }

        private void GamePlay_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                Player.MoveX = 0;
            }
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                Player.MoveY = 0;
            }
        }
        private void GamePlay_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                Player.MoveX = -5;
                Player.FacingRight = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                Player.MoveX = +5;
                Player.FacingRight = true;
            }
            if (e.KeyCode == Keys.Up)
            {
                Player.MoveY = -5;
            }
            if (e.KeyCode == Keys.Down)
            {
                Player.MoveY = +5;
            }
            if (e.KeyCode == Keys.Space)
            {
                Bullet bullet = Player.Shoot();
                Bullets.Add(bullet);
            }
        }
        public void SaveScore(string playerName, int score)
        {
            File.AppendAllTextAsync(Path.Combine(currentDirectory, "topscores.txt"), Environment.NewLine + ($"{playerName},{score}"));
        }

        private void label1_Click(object sender, EventArgs e)
        {
            SaveScore(PlayerName, Score);
            this.Close();
        }
    }
}
  