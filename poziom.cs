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

        /// <summary>
        /// Strzalka wyjscia w grze
        /// </summary>
        public PictureBox Strzalka;

        /// <summary>
        /// Przeszkoda uzywana w grze
        /// </summary>
        public PictureBox Obiekt;

        /// <summary>
        /// Uzywany wagonik na ostatnim poziomie
        /// </summary>
        private PictureBox wagonik;
        private int speedstrzalki = 2;
        private int pozycjastrzalki;
        /// <summary>
        /// Uzywane do animacji gora dol strzalki
        /// </summary>
        private bool gora = true;
        /// <summary>
        /// Max dystans przebycia strzalki gora dol
        /// </summary>
        static private int maxdystanstrzalki = 20;

        private main Main;
        /// <summary>
        /// Ustawia przeszkode i strzalke wyjscia
        /// </summary>
        /// <param name="Main"></param>
        /// <param name="wysokoscziemii"></param>
        public poziom(main Main, int wysokoscziemii)
        {
            this.Main = Main;

            Strzalka = new PictureBox();
            Strzalka.Image = strzalka;
            Strzalka.Size = new Size(strzalka.Width/2, strzalka.Height/2);
            Strzalka.Location = new Point(Main.Width - Strzalka.Width - 20, Main.Height - wysokoscziemii - Strzalka.Height - 20);
            pozycjastrzalki = Strzalka.Top;

            Obiekt = new PictureBox();
            ustawprzeszkode(1);
            Obiekt.Location = new Point(Main.Width/2, Main.Height - wysokoscziemii - Obiekt.Height+20);

            wagonik = new PictureBox();
            wagonik.Image = wagon;
            wagonik.Visible = false;

            Main.Paint += new PaintEventHandler(paint);
        }

        /// <summary>
        /// Uzywana do DrawImage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Ustawia pozycje przeszkoddy
        /// </summary>
        /// <param name="przeszkoda"></param>
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

        /// <summary>
        /// Funkcja pozwala na poruszanie sie strzalki gora dol
        /// </summary>
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
