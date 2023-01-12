using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fizyczny_Mag
{
    public class gracz
    {
        static private Image model = Image.FromFile("../../../FizycznyMag_assets/PlayerModelFinished.png");
        public PictureBox Gracz;
        private main Main;
        /// <summary>
        /// Czy gracz idzie w prawo
        /// </summary>
        private bool right = true;
        /// <summary>
        /// Ile pomniejszony ma byc model gracza
        /// </summary>
        private int zoom = 3;
        /// <summary>
        /// Szybkosc poruszania sie gracza
        /// </summary>
        private int speed = 10;
        public gracz(main Main, int wysokoscziemii)
        {
            this.Main = Main;
            Gracz = new PictureBox();
            Gracz.Image = model;
            Gracz.Size = new Size(model.Width/zoom, model.Height/zoom);
            Gracz.Location = new Point(20, Main.Height - wysokoscziemii - Gracz.Height + 20);
            
            Main.Paint += new PaintEventHandler(paint);
        }
        /// <summary>
        /// Uzywane do DrawImage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void paint(object sender, PaintEventArgs e)
        {
            if (Gracz.Visible == true)
            {
                e.Graphics.DrawImage(Gracz.Image, Gracz.Left, Gracz.Top, Gracz.Width, Gracz.Height);
            }
        }
        /// <summary>
        /// Funkcja pozwalajaca na poruszanie sie graczem
        /// Obraca zdjecie symetrycznie po osi X
        /// Zmienia pozycje X gracza
        /// </summary>
        /// <param name="kierunek">W ktora strone</param>
        public void ruch(int kierunek)
        {

            if (kierunek == 1)
            {

                if (right == true)
                {
                    model.RotateFlip(RotateFlipType.RotateNoneFlipX);
                }

                if (Gracz.Left > 0)
                {
                    Gracz.Left -= speed;
                }

                right = false;
            }
            else if (kierunek == 2)
            {
                if (right == false)
                {
                    model.RotateFlip(RotateFlipType.RotateNoneFlipX);
                }

                if (Gracz.Left < Main.Width - Gracz.Width)
                {
                    Gracz.Left += speed;
                }

                right = true;
            }
        }

    }
}
