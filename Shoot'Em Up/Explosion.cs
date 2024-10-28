using System;
using System.Drawing;
using System.Windows.Forms;

namespace Shoot_Em_Up
{
    internal class Explosion : Asset
    {
        private int explosionRadius = 100; // Increased radius for a larger explosion

        public override void Draw(PaintEventArgs e, bool isFacingRight)
        {
            Graphics g = e.Graphics;

            // Define explosion colors and larger sizes
            Color[] explosionColors = { Color.Red, Color.Orange, Color.Yellow, Color.White };
            int[] explosionSizes = { 100, 70, 40, 20 };

            // Adjust starting point based on facing direction
            int offsetX = isFacingRight ? 20 : -20;

            // Draw larger concentric circles for explosion effect
            for (int i = 0; i < explosionColors.Length; i++)
            {
                Brush brush = new SolidBrush(explosionColors[i]);
                int size = explosionSizes[i];

                // Center the explosion, with offset based on facing direction
                int centerX = Center.X + offsetX - (size / 2);
                int centerY = Center.Y - (size / 2);

                // Draw filled circle for explosion layer
                g.FillEllipse(brush, centerX, centerY, size, size);
            }

            // Add longer random lines to enhance explosion effect
            Pen explosionPen = new Pen(Color.White, 3);
            Random random = new Random();
            for (int i = 0; i < 12; i++) // Increased number of explosion lines
            {
                // Randomized explosion lines radiating from center
                int startX = Center.X + offsetX;
                int startY = Center.Y;
                int endX = startX + random.Next(-40, 40); // Increased line length for larger effect
                int endY = startY + random.Next(-40, 40);

                g.DrawLine(explosionPen, startX, startY, endX, endY);
            }

            // Draw collision boundary (for debugging purposes)
            Pen collisionPen = new Pen(Color.Red, 2);
            Collision = new Rectangle(Center.X - explosionRadius, Center.Y - explosionRadius, explosionRadius * 2, explosionRadius * 2);
            // g.DrawEllipse(collisionPen, Collision); // Uncomment to view collision boundary
        }

        public bool CollisionCheck(Rectangle target)
        {
            return Collision.IntersectsWith(target);
        }
    }
}
