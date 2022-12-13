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
        static public Image obraztla = Image.FromFile("../../../FizycznyMag_assets/bg.png");
        static public Image ziemia = Image.FromFile("../../../FizycznyMag_assets/ziemia.png");
        static private Image strzalka = Image.FromFile("../../../FizycznyMag_assets/strzalka.png");

        private PictureBox[] Ziemia;
        private PictureBox Strzalka;
        static private int IloscZiemii = 2;
        private int speed = 5;

        private int speedstrzalki = 1;
        private int pozycjastrzalki;
        private bool gora = true;
        static private int maxdystanstrzalki = 20;

        private main Main;
        public poziom(main Main)
        {
            this.Main = Main;
            Ziemia = new PictureBox[IloscZiemii];
            for (int i = 0; i < IloscZiemii; i++)
            {
                Ziemia[i] = new PictureBox();
                Ziemia[i].Image = ziemia;
                Ziemia[i].Size = new Size(ziemia.Width, ziemia.Height);
                Ziemia[i].Location = new Point(0+Ziemia[i].Width*i, Main.Height - Ziemia[i].Height);
            }

            Strzalka = new PictureBox();
            Strzalka.Image = strzalka;
            Strzalka.Size = new Size(strzalka.Width/2, strzalka.Height/2);
            Strzalka.Location = new Point(Main.Width - Strzalka.Width - 20, Main.Height - ziemia.Height - Strzalka.Height - 20);
            pozycjastrzalki = Strzalka.Top;

            Main.Paint += new PaintEventHandler(paint);
        }

        private void paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < IloscZiemii; i++)
            {
                if (Ziemia[i].Visible == true)
                {
                    e.Graphics.DrawImage(Ziemia[i].Image, Ziemia[i].Left, Ziemia[i].Top, Ziemia[i].Width, Ziemia[i].Height);
                }

                if (Strzalka.Visible == true)
                {
                    e.Graphics.DrawImage(Strzalka.Image, Strzalka.Left, Strzalka.Top, Strzalka.Width, Strzalka.Height);
                }

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
