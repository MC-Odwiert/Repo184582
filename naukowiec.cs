using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fizyczny_Mag
{
    
    internal class naukowiec
    {

        static private Image model = Image.FromFile("../../../FizycznyMag_assets/Newton.png");
        private PictureBox Naukowiec;
        private main Main;
        private int zoom = 5;
        public naukowiec(main Main)
        {
            this.Main = Main;
            Naukowiec = new PictureBox();
            Naukowiec.Image = model;
            Naukowiec.Size = new Size(model.Width / zoom, model.Height / zoom);
            Naukowiec.Location = new Point(700, Main.Height - poziom.ziemia.Height - Naukowiec.Height + 40);

            Main.Paint += new PaintEventHandler(paint);
        }

        private void paint(object sender, PaintEventArgs e)
        {
            if (Naukowiec.Visible == true)
            {
               
                e.Graphics.DrawImage(Naukowiec.Image, Naukowiec.Left, Naukowiec.Top, Naukowiec.Width, Naukowiec.Height);
            }
        }
    }
}
