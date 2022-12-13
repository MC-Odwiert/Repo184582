using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fizyczny_Mag
{
    internal class gracz
    {
        static private Image model = Image.FromFile("../../../FizycznyMag_assets/playerModel.png");
        private PictureBox Gracz;
        private main Main;
        private int zoom = 5;
        private int speed = 10;
        public gracz(main Main)
        {
            this.Main = Main;
            Gracz = new PictureBox();
            Gracz.Image = model;
            Gracz.Size = new Size(model.Width/zoom, model.Height/zoom);
            Gracz.Location = new Point(20, Main.Height - poziom.ziemia.Height - Gracz.Height + 5);

            Main.Paint += new PaintEventHandler(paint);
        }

        private void paint(object sender, PaintEventArgs e)
        {

            if (Gracz.Visible == true)
            {
                e.Graphics.DrawImage(Gracz.Image, Gracz.Left, Gracz.Top, Gracz.Width, Gracz.Height);
            }
        }

        public void ruch(int kierunek)
        {
            if (kierunek == 1)
            {
                if (Gracz.Left > 0)
                {
                    Gracz.Left -= speed;
                }

            }
            else if (kierunek == 2)
            {
                if (Gracz.Left < Main.Width - Gracz.Width)
                {
                    Gracz.Left += speed;
                }
            }
        }

    }
}
