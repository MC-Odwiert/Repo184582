using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fizyczny_Mag
{
    internal class menu
    {
        static private Image scroll = Image.FromFile("../../../FizycznyMag_assets/scrollOpen.png");
        static private Font font = new Font("Arial", 15);
        private PictureBox[] Scroll;
        private Label[] podpis;
        private int IloscScrolli = 3;
        private int padding = 20;
        private int zoom = 2;
        private main Main;
        public menu(main Main)
        {
            this.Main = Main;
            Scroll = new PictureBox[IloscScrolli];
            podpis = new Label[IloscScrolli];
            for (int i = 0; i < IloscScrolli; i++)
            {
                podpis[i] = new Label();
                podpis[i].Font = font;
                podpis[i].ForeColor = Color.Black;


                Scroll[i] = new PictureBox();
                Scroll[i].Image = scroll;
                Scroll[i].Size = new Size(scroll.Width, scroll.Height/zoom);
                Scroll[i].Location = new Point(Main.Width - Scroll[i].Width - padding - (Scroll[i].Width+padding)*i, padding);
            }
            podpis[0].Text = "WIEDZA";
            podpis[1].Text = "MENU";
            podpis[2].Text = "POZIOM:\n       1";

            Scroll[2].Location = new Point(padding, padding);
            for (int i = 0; i < IloscScrolli; i++)
            {
                int X = Scroll[i].Location.X + (Scroll[i].Width / 2) - TextRenderer.MeasureText(podpis[i].Text, podpis[i].Font).Width / 2;
                int Y = Scroll[i].Location.Y + (Scroll[i].Height/2) - TextRenderer.MeasureText(podpis[i].Text, podpis[i].Font).Height / 2;
                podpis[i].Location = new Point(X, Y);
            }

          
            Main.Paint += new PaintEventHandler(paint);
        }

        private void paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < IloscScrolli; i++)
            {
                SolidBrush brush = new SolidBrush(podpis[i].ForeColor);
                if (Scroll[i].Visible == true)
                {
                    e.Graphics.DrawImage(Scroll[i].Image, Scroll[i].Left, Scroll[i].Top, Scroll[i].Width, Scroll[i].Height);
                    e.Graphics.DrawString(podpis[i].Text, podpis[i].Font, brush, podpis[i].Location);
                }

            }
        }

    }
}
