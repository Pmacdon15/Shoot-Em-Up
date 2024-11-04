using System.Drawing;
using System.Windows.Forms;

namespace Shoot_Em_Up
{
    internal class Obstacle : Asset
    {
        public Obstacle(Point center)
        {
            Center = center;
        }

        public override void Draw(PaintEventArgs e, bool isFacingRight)
        {
            // Draw the obstacle as a rectangle
            e.Graphics.FillRectangle(Brushes.Green, Center.X - 20, Center.Y - 20, 40, 40); // Adjust size as needed
            Collision = new Rectangle(Center.X - 20, Center.Y - 20, 40, 40); // Define the collision area
        }
    }
}