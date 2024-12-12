//using Shoot_Em_Up;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Shoot_Em_Up
{
    public partial class GamePlay : Form
    {

        int Score;
        int Lives;
        bool isGameOver;
        string PlayerName;
        string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;        

        StickMan Player = new StickMan(new Point(550, 350));
        //List<StickMan> Enemies = new List<StickMan>();
        List<SuicideBomber> Enemies = new List<SuicideBomber>();
        List<Explosion> Explosions = new List<Explosion>();
        List<Bullet> Bullets = new List<Bullet>();
        List<Obstacle> Obstacles = new List<Obstacle>(); 
        public GamePlay(string playerName)
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            Score = 0;
            Lives = 5;
            PlayerName = playerName;
            isGameOver = false;

        }

        private void GamePlay_Load(object sender, EventArgs e)
        {
            int countBadGuys = 0;
            int maxBadGuys = 5;

            Random random = new Random();

            while (countBadGuys < maxBadGuys)
            {
                addNewEnemy();
                countBadGuys++;
            }

            // Randomly initializes 3-5 obstacles 
            int numberOfObstacles = random.Next(3, 6); // Generates 3, 4, or 5 obstacles

            // Adjusting for the white border
            int playAreaWidth = (int)(this.Width * 0.95);
            int playAreaHeight = (int)(this.Height * 0.80);
            int playAreaX = (this.Width - playAreaWidth) / 2;
            int playAreaY = (this.Height - playAreaHeight) / 2;

            for (int i = 0; i < numberOfObstacles; i++)
            {
                // Ensure obstacle fits within the white border, accounting for its size
                int x = random.Next(playAreaX + Obstacle.Width / 2, playAreaX + playAreaWidth - Obstacle.Width / 2);
                int y = random.Next(playAreaY + Obstacle.Height / 2, playAreaY + playAreaHeight - Obstacle.Height / 2);
                Obstacles.Add(new Obstacle(new Point(x, y)));
            }

            GameLoop.Interval = 24;
            GameLoop.Start();

            this.Size = new Size(this.Width, this.Height);
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.Black;
            this.Paint += new PaintEventHandler(PaintObjects);
        }

        private void addNewEnemy()
        {
            int[] movement = { -7, -5, -3, 0, 5, 7 };
            int[] movement2 = { -7, -5, -3, 3, 5, 7 };
            Random random = new Random();
            int x = random.Next(0, (int)(this.Width - (this.Width * 0.2)));
            int y = random.Next(0, (int)(this.Height - (this.Height * 0.2)));
            SuicideBomber enemy = new SuicideBomber(new Point(x, y));
            //StickMan enemy = new StickMan(new Point(x, y));

            //// Set initial movement based on position
            enemy.MoveX = (x > 550) ? movement[random.Next(movement.Length)]: movement2[random.Next(movement2.Length)];
            enemy.MoveY = movement[random.Next(movement.Length)];

            Enemies.Add(enemy);
        }

        // Updated PaintObjects method
        protected void PaintObjects(object? sender, PaintEventArgs e)
        {
            if (isGameOver)
            {
            return;
            }

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

            // Draw obstacles
            foreach (Obstacle obstacle in Obstacles)
            {
                obstacle.Draw(e, false);
                //using (Pen redPen = new Pen(Color.Red, 2))
                //{
                  //e.Graphics.DrawRectangle(redPen, obstacle.Collision);
                //}
            }

            for (int i = Enemies.Count - 1; i >= 0; i--)
            {
                SuicideBomber enemy = Enemies[i];
                enemy.Draw(e, enemy.FacingRight);

                // Check for player collision with enemy
                if (Player.CollisionCheck(enemy))
                {
                    //Score--;
                    Lives--;
                    Explosion explosion = new Explosion { Center = enemy.Center };
                    Explosions.Add(explosion); // Add explosion to the list
                    enemiesToRemove.Add(enemy);
                    continue;
                }

                //Check if enemy is in explosion radius
                foreach (var explosion in Explosions)
                {
                    if (explosion.CheckCollision(enemy))
                    {
                        //Score++;
                        Explosions.Add(explosion); // Add explosion to the list
                        enemiesToRemove.Add(enemy);

                        addNewEnemy();
                        addNewEnemy();
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

                        addNewEnemy();
                        addNewEnemy();
                        break;
                    }
                }
            }

            // Remove collided enemies and bullets after the loop
            foreach (var enemy in enemiesToRemove) Enemies.Remove(enemy);
            foreach (var bullet in bulletsToRemove) Bullets.Remove(bullet);

            e.Graphics.ResetClip();

            // Display Score and Lifes
            e.Graphics.DrawString("Score: " + Score.ToString(), new Font("Arial", 12, FontStyle.Regular), Brushes.Red, new PointF((float)(this.Width * 0.90), (float)(this.Height * 0.05)));
            e.Graphics.DrawString("Lives: " + Lives.ToString(), new Font("Arial", 12, FontStyle.Regular), Brushes.Red, new PointF((float)(this.Width * 0.45), (float)(this.Height * 0.05)));

            //Display PlayerName
            e.Graphics.DrawString(PlayerName, new Font("Arial", 12, FontStyle.Regular), Brushes.Red, new PointF((float)(this.Width * 0.1), (float)(this.Height * 0.05)));

            if (Lives <= 0)
            {
                isGameOver = true;
                SaveScore(PlayerName, Score);
                MessageBox.Show("GAME OVER\r\nYour Score: " + Score, "GAME OVER" , MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                
            }

        }


        private void GameLoop_Tick(object sender, EventArgs e)
        {
            if (isGameOver)
            {
                return;
            }

            int rectWidth = (int)(this.Width * 0.91);
            int rectHeight = (int)(this.Height * 0.73);
            Rectangle rectangle = new Rectangle((this.Width - rectWidth) / 2, (this.Height - rectHeight) / 2, rectWidth, rectHeight);

            // Movement restriction for player and enemies by obstacles
            List<Asset> characters = new List<Asset> { Player };
            characters.AddRange(Enemies);

            foreach (var character in characters)
            {
                Point previousPosition = character.Center; // Store current position

                // Movement in each direction separately
                int newX = character.Center.X;
                int newY = character.Center.Y;

                if (character is StickMan stickMan)
                {
                    // Check if moving X would cause collision
                    Rectangle newCollisionX = new Rectangle(stickMan.Center.X + stickMan.MoveX - 24, stickMan.Center.Y - 36, 48, 110);
                    bool canMoveX = true;
                    foreach (Obstacle obstacle in Obstacles)
                    {
                        if (obstacle.CheckCollision(newCollisionX))
                        {
                            canMoveX = false;
                            break;
                        }
                    }

                    // Check if moving Y would cause collision
                    Rectangle newCollisionY = new Rectangle(stickMan.Center.X - 24, stickMan.Center.Y + stickMan.MoveY - 36, 48, 110);
                    bool canMoveY = true;
                    foreach (Obstacle obstacle in Obstacles)
                    {
                        if (obstacle.CheckCollision(newCollisionY))
                        {
                            canMoveY = false;
                            break;
                        }
                    }

                    // Update position only for movements that don't collide
                    newX = canMoveX ? stickMan.Center.X + stickMan.MoveX : stickMan.Center.X;
                    newY = canMoveY ? stickMan.Center.Y + stickMan.MoveY : stickMan.Center.Y;
                    stickMan.Center = new Point(newX, newY);
                }
                else if (character is SuicideBomber bomber)
                {
                    // Calculate the direction towards the player
                    int deltaX = Player.Center.X - bomber.Center.X;
                    int deltaY = Player.Center.Y - bomber.Center.Y;

                    // Normalize the direction and apply a speed factor (e.g., 3)
                    double distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
                    double speed = 3; // Adjust speed as needed

                    if (distance > 0) // Avoid division by zero
                    {
                        int moveX = (int)(speed * (deltaX / distance));
                        int moveY = (int)(speed * (deltaY / distance));

                        // Check if moving X would cause collision
                        Rectangle newCollisionX = new Rectangle(bomber.Center.X + moveX - 24, bomber.Center.Y - 36, 48, 110);
                        bool canMoveX = true;
                        foreach (Obstacle obstacle in Obstacles)
                        {
                            if (obstacle.CheckCollision(newCollisionX))
                            {
                                canMoveX = false;
                                break;
                            }
                        }

                        // Check if moving Y would cause collision
                        Rectangle newCollisionY = new Rectangle(bomber.Center.X - 24, bomber.Center.Y + moveY - 36, 48, 110);
                        bool canMoveY = true;
                        foreach (Obstacle obstacle in Obstacles)
                        {
                            if (obstacle.CheckCollision(newCollisionY))
                            {
                                canMoveY = false;
                                break;
                            }
                        }

                        // Update position only for movements that don't collide
                        newX = canMoveX ? bomber.Center.X + moveX : bomber.Center.X;
                        newY = canMoveY ? bomber.Center.Y + moveY : bomber.Center.Y;
                        bomber.Center = new Point(newX, newY);
                        bomber.FacingRight = moveX >= 0;
                    }
                }

                // Additional boundary check after moving
                character.Center = new Point(
                    Math.Max(rectangle.X, Math.Min(character.Center.X, rectangle.X + rectangle.Width)),
                    Math.Max(rectangle.Y, Math.Min(character.Center.Y, rectangle.Y + rectangle.Height))
                );
            }

            // Bullet-Obstacle collision
            for (int i = Bullets.Count - 1; i >= 0; i--)
            {
                Rectangle bulletCollision = new Rectangle(
                    Bullets[i].Center.X - 5, 
                    Bullets[i].Center.Y - 2, 
                    10, 5
                );
                foreach (Obstacle obstacle in Obstacles)
                {
                    if (obstacle.CheckCollision(bulletCollision))
                    {
                        Bullets.RemoveAt(i);
                        break;
                    }
                }
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
            string connectionString = ConfigurationManager.ConnectionStrings["Shoot_Em_Up.Properties.Settings.connectionString"].ConnectionString;
            try
            {

                SqlConnection con = new SqlConnection(connectionString);
                con.Open();

                string Query = "INSERT INTO TopScores (PlayerName, Score) " +
                    "VALUES ('" + playerName + "', " + score + ");";

                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {
            SaveScore(PlayerName, Score);
            this.Close();
        }
    }
}