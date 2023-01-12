using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fizyczny_Mag
{

    public class poziom
    {
        static public Image bg1 = Image.FromFile("../../../FizycznyMag_assets/bg1.png");
        static public Image bg2 = Image.FromFile("../../../FizycznyMag_assets/bg2.png");
        static public Image bg3 = Image.FromFile("../../../FizycznyMag_assets/bg3.png");

        static private Image kamien = Image.FromFile("../../../FizycznyMag_assets/kamien.png");
        static private Image krokodyl = Image.FromFile("../../../FizycznyMag_assets/krokodyl.png");
        static private Image studnia = Image.FromFile("../../../FizycznyMag_assets/well.png");
        static private Image wagon = Image.FromFile("../../../FizycznyMag_assets/minecart.png");
        static private Image loop = Image.FromFile("../../../FizycznyMag_assets/looptydiscoop.png");
        static private Image strzalka = Image.FromFile("../../../FizycznyMag_assets/strzalka.png");

        public PictureBox Strzalka;
        public PictureBox Obiekt;
        private PictureBox wagonik;
        private int speed = 5;

        private int speedstrzalki = 2;
        private int pozycjastrzalki;
        private bool gora = true;
        static private int maxdystanstrzalki = 20;

        private main Main;
        public poziom(main Main, int ziemia)
        {
            this.Main = Main;

            Strzalka = new PictureBox();
            Strzalka.Image = strzalka;
            Strzalka.Size = new Size(strzalka.Width/2, strzalka.Height/2);
            Strzalka.Location = new Point(Main.Width - Strzalka.Width - 20, Main.Height - ziemia - Strzalka.Height - 20);
            pozycjastrzalki = Strzalka.Top;

            Obiekt = new PictureBox();
            ustawprzeszkode(1);
            Obiekt.Location = new Point(Main.Width/2, Main.Height - ziemia - Obiekt.Height+20);

            wagonik = new PictureBox();
            wagonik.Image = wagon;

            Main.Paint += new PaintEventHandler(paint);
        }

        private void paint(object sender, PaintEventArgs e)
        {
            if (Obiekt.Visible == true)
            {
                e.Graphics.DrawImage(Obiekt.Image, Obiekt.Left, Obiekt.Top, Obiekt.Width, Obiekt.Height);
            }
            if (wagonik.Visible == true && Obiekt.Visible == true)
            {
                e.Graphics.DrawImage(wagonik.Image, wagonik.Left, wagonik.Top, wagonik.Width, wagonik.Height);
            }
            if (Strzalka.Visible == true)
            {

                e.Graphics.DrawImage(Strzalka.Image, Strzalka.Left, Strzalka.Top, Strzalka.Width, Strzalka.Height);
            }
        }

        public void ustawprzeszkode(int przeszkoda)
        {
            if (przeszkoda == 1)
            {
                Obiekt.Image = kamien;
                Obiekt.Size = new Size(kamien.Width / 10, kamien.Height / 10);
                Obiekt.Location = new Point(500, 407);
            }
            else if (przeszkoda == 2)
            {
                Obiekt.Image = krokodyl;
                Obiekt.Size = new Size(krokodyl.Width / 5, krokodyl.Height / 5);
                Obiekt.Location = new Point(335, 647);
            }
            else if (przeszkoda == 3)
            {
                Obiekt.Image = studnia;
                Obiekt.Size = new Size(studnia.Width / 3, studnia.Height / 3);
                Obiekt.Location = new Point(500, 380);
                wagonik.Visible = false;
            }
            else if (przeszkoda == 4)
            {
                Obiekt.Image = loop;
                Obiekt.Size = new Size(loop.Width, loop.Height);
                Obiekt.Location = new Point(300, 125);

                wagonik.Size = new Size(wagon.Width/4, wagon.Height/4);
                wagonik.Location = new Point(300, 485);
                wagonik.Visible = true;
            }
        }

        public void animacjastrzałki()
        {
            if (Strzalka.Visible == true)
            {
                if (gora == true)
                {
                    if (Strzalka.Top > pozycjastrzalki - maxdystanstrzalki)
                    {
                        Strzalka.Top -= speedstrzalki;
                    }
                    else
                    {
                        gora = false;
                    }
                }
                else
                {
                    if (Strzalka.Top < pozycjastrzalki)
                    {
                        Strzalka.Top += speedstrzalki;
                    }
                    else
                    {
                        gora = true;
                    }
                }
            }
        }
        
    }
}
