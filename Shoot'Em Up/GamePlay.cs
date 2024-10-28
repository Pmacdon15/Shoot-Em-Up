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
       
        StickMan Player = new StickMan(new Point(550, 350));
        List<StickMan> Enemies = new List<StickMan>();

        public GamePlay()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            Score = 0;
            Collision = 0;
            Counter = 0;

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

                StickMan enemy = new StickMan(new Point(x, y));

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

        protected void PaintObjects(object sender, PaintEventArgs e)
        {
            int rectWidth = (int)(this.Width * 0.95);
            int rectHeight = (int)(this.Height * 0.80);
            Rectangle rectangle = new Rectangle((this.Width - rectWidth) / 2, (this.Height - rectHeight) / 2, rectWidth, rectHeight);
            e.Graphics.DrawRectangle(Pens.White, rectangle);

            Region clippingRegion = new Region(rectangle);
            e.Graphics.Clip = clippingRegion;

            Player.Draw(e, Player.FacingRight);

            for (int i = Enemies.Count - 1; i >= 0; i--)
            {
                StickMan enemy = Enemies[i];          
                

                enemy.Draw(e, enemy.FacingRight);

                if (Player.CollisionCheck(enemy))
                {
                    Collision--;
                    Enemies.RemoveAt(i);
                }
            }

            e.Graphics.ResetClip();

            e.Graphics.DrawString("Score: " + Collision.ToString(), new Font("Arial", 12, FontStyle.Regular), Brushes.Red, new PointF((float)(this.Width * 0.90), (float)(this.Height * 0.05)));
        }

        private void GameLoop_Tick(object sender, EventArgs e)
        {
            int rectWidth = (int)(this.Width * 0.91);
            int rectHeight = (int)(this.Height * 0.73);
            Rectangle rectangle = new Rectangle((this.Width - rectWidth) / 2, (this.Height - rectHeight) / 2, rectWidth, rectHeight);

            Player.Move(rectangle.X, rectangle.X + rectangle.Width, rectangle.Y, rectangle.Y + rectangle.Height);
            
            foreach (StickMan enemy in Enemies)
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
        }
    }
}