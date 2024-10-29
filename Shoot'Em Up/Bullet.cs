using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoot_Em_Up
{
    internal class Bullet : Asset
    {
        public bool FacingRight;
        public Bullet(Point center, bool facingRight)
        {
            Center = center;
            FacingRight = facingRight;
            Collision = new Rectangle(Center.X, Center.Y - 2, 10, 5);
        }

        public override void Draw(PaintEventArgs e, bool isFacingRight)
        {
            using (Brush brush = new SolidBrush(Color.Gold))
            {
                int width = 10;
                int height = 5;
                int x = isFacingRight ? Center.X : Center.X - width;
                int y = Center.Y - height / 2;

                e.Graphics.FillEllipse(brush, x, y, width, height);

                Collision = new Rectangle(x, y, width, height);
                // Draw collision box
                //using (Pen pen = new Pen(Color.Red, 1))
                //{
                //    e.Graphics.DrawRectangle(pen, x, y, width, height);
                //}
            }
        }

        public bool CheckCollision(Asset other)
        {
            return this.Collision.IntersectsWith(other.Collision);
        }
    }
}
