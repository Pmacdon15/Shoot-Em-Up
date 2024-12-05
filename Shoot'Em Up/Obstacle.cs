using System;
using System.Drawing;
using System.Windows.Forms;

namespace Shoot_Em_Up
{
    internal class Obstacle : Asset
    {
        // Static dimensions for the obstacle's rectangle
        public static int Width = 80; 
        public static int Height = 80;
        private int _radius = 40; 

        public Obstacle(Point center)
        {
            Center = center;
            // Define collision as a rectangle
            Collision = new Rectangle(Center.X - Width / 2, Center.Y - Height / 2, Width, Height);
        }

        public override void Draw(PaintEventArgs e, bool isFacingRight)
        {
            using (Brush brush = new SolidBrush(Color.White))
            {
                
                e.Graphics.FillEllipse(brush, Center.X - _radius, Center.Y - _radius, _radius * 2, _radius * 2);
            }
            //Collision is rectangle but we draw a circle
        }

        public bool CheckCollision(Rectangle other)
        {
            return Collision.IntersectsWith(other);
        }
    }
}