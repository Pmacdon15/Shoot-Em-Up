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
        //Ship player = new Ship(new Point(550, 350));
        StickMan player = new StickMan(new Point(550, 350));        

        public GamePlay()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        private void GamePlay_Load(object sender, EventArgs e)
        {
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

            player.Draw(e, player.FacingRight);

            e.Graphics.ResetClip();

            e.Graphics.DrawString("Score: 0", new Font("Arial", 12, FontStyle.Regular), Brushes.Gold, 10, 20);
        }

        private void GamePlay_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                player.MoveX = -5;
                player.FacingRight = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                player.MoveX = +5;
                player.FacingRight = true;

            }
            if (e.KeyCode == Keys.Up)
            {
                player.MoveY = -5;
            }
            if (e.KeyCode == Keys.Down)
            {
                player.MoveY = +5;
            }
        }

        private void GameLoop_Tick(object sender, EventArgs e)
        {
            int rectWidth = (int)(this.Width * 0.91);
            int rectHeight = (int)(this.Height * 0.73);
            Rectangle rectangle = new Rectangle((this.Width - rectWidth) / 2, (this.Height - rectHeight) / 2, rectWidth, rectHeight);

            player.Move(rectangle.X, rectangle.X + rectangle.Width, rectangle.Y, rectangle.Y + rectangle.Height);
            this.Refresh();
        }

        private void GamePlay_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                player.MoveX = 0;
            }
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                player.MoveY = 0;
            }
        }
    }
}