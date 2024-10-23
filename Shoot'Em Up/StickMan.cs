using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoot_Em_Up
{
    internal class StickMan : Asset
    {
        public StickMan(Point center)
        {
            Center = center;
        }

        public override void Draw(PaintEventArgs e)
        {
            Pen pen = new Pen(Color.DarkGray, 2);
            Graphics g = e.Graphics;

            // Draw head
            g.DrawEllipse(pen, Center.X - 20, Center.Y - 60, 40, 40);

            // Draw body
            g.DrawLine(pen, Center.X, Center.Y - 20, Center.X, Center.Y + 60);

            // Draw arms
            g.DrawLine(pen, Center.X - 40, Center.Y, Center.X + 40, Center.Y);

            // Draw legs
            g.DrawLine(pen, Center.X, Center.Y + 60, Center.X - 30, Center.Y + 120);
            g.DrawLine(pen, Center.X, Center.Y + 60, Center.X + 30, Center.Y + 120);
        }
    }
}
