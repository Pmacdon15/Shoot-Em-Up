using System;
using System.Drawing;
using System.Windows.Forms;
// Chat gpt was used here to help with the darwing of the man and vest
namespace Shoot_Em_Up
{
    internal class SuicideBomber : Asset
    {
        public bool FacingRight;

        public SuicideBomber(Point center)
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

            // Draw bomb vest around body
            Brush vestBrush = new SolidBrush(Color.Brown);
            g.FillRectangle(vestBrush, Center.X - 15, Center.Y - 12, 30, 50); // Vest outline
            g.DrawRectangle(Pens.Black, Center.X - 15, Center.Y - 12, 30, 50);

            // Draw small explosive packs within vest
            Brush packBrush = new SolidBrush(Color.Red);
            int packWidth = 8, packHeight = 12;
            g.FillRectangle(packBrush, Center.X - 12, Center.Y - 8, packWidth, packHeight);
            g.FillRectangle(packBrush, Center.X + 4, Center.Y - 8, packWidth, packHeight);
            g.FillRectangle(packBrush, Center.X - 12, Center.Y + 10, packWidth, packHeight);
            g.FillRectangle(packBrush, Center.X + 4, Center.Y + 10, packWidth, packHeight);

            // Draw fuse on vest
            Pen fusePen = new Pen(Color.Red, 2);
            g.DrawLine(fusePen, Center.X, Center.Y + 10, Center.X, Center.Y - 15); // Fuse

            // Draw weapon or detonator in hand
            if (isFacingRight)
            {
                g.DrawLine(pen, Center.X + 24, Center.Y, Center.X + 34, Center.Y - 5);
                g.DrawRectangle(pen, Center.X + 34, Center.Y - 8, 10, 6);
                g.DrawLine(pen, Center.X + 34, Center.Y - 5, Center.X + 34, Center.Y + 5);
            }
            else
            {
                g.DrawLine(pen, Center.X - 24, Center.Y, Center.X - 34, Center.Y - 5);
                g.DrawRectangle(pen, Center.X - 44, Center.Y - 8, 10, 6);
                g.DrawLine(pen, Center.X - 34, Center.Y - 5, Center.X - 34, Center.Y + 5);
            }

            // Adjusted collision rectangle to be lower
            Collision = new Rectangle(Center.X - 24, Center.Y - 36, 48, 110);
            Pen redPen = new Pen(Color.Red, 2); // Collision boundary
            //e.Graphics.DrawRectangle(redPen, Collision);
        }

        public bool CollisionCheck(StickMan player)
        {
            return Collision.IntersectsWith(player.Collision);
        }
    }
}
