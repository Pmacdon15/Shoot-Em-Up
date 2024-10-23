using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoot_Em_Up
{
    internal class StickMan : Asset
    {
        public bool FacingRight;

        public StickMan(Point center)
        {
            Center = center;
            FacingRight = true;
        }

        public override void Draw(PaintEventArgs e, bool isFacingRight)
        {
            Pen pen = new Pen(Color.DarkGray, 2);
            Graphics g = e.Graphics;

            // Draw head (20% bigger)
            g.DrawEllipse(pen, Center.X - 12, Center.Y - 36, 24, 24);

            // Draw body (20% longer)
            g.DrawLine(pen, Center.X, Center.Y - 12, Center.X, Center.Y + 36);

            // Draw arms (20% longer)
            g.DrawLine(pen, Center.X - 24, Center.Y, Center.X + 24, Center.Y);

            // Draw legs (20% longer)
            g.DrawLine(pen, Center.X, Center.Y + 36, Center.X - 18, Center.Y + 72);
            g.DrawLine(pen, Center.X, Center.Y + 36, Center.X + 18, Center.Y + 72);

            if (isFacingRight == true)
            {
                // Draw gun in right hand
                g.DrawLine(pen, Center.X + 24, Center.Y, Center.X + 34, Center.Y - 5);
                g.DrawRectangle(pen, Center.X + 34, Center.Y - 8, 10, 6);

                // Draw gun handle
                g.DrawLine(pen, Center.X + 34, Center.Y - 5, Center.X + 34, Center.Y + 5);
            }
            else
            {
                // Draw gun in left hand
                g.DrawLine(pen, Center.X - 24, Center.Y, Center.X - 34, Center.Y - 5);
                g.DrawRectangle(pen, Center.X - 44, Center.Y - 8, 10, 6);

                // Draw gun handle
                g.DrawLine(pen, Center.X - 34, Center.Y - 5, Center.X - 34, Center.Y + 5);
            }
        }
    }
}
