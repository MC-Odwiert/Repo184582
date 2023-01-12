using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fizyczny_Mag
{
    
    internal class naukowiec
    {

        private Image model;
        private static Image newton = Image.FromFile("../../../FizycznyMag_assets/Newton.png");
        private static Image tesla = Image.FromFile("../../../FizycznyMag_assets/Tesla.png");
        private static Image einstein = Image.FromFile("../../../FizycznyMag_assets/Einstein.png");


        public PictureBox Naukowiec;
        public Label dialog;
        private main Main;
        private int zoom = 4;
        private int wysokoscziemii;
        public naukowiec(main Main, int wysokoscziemii)
        {
            this.Main = Main;
            this.wysokoscziemii = wysokoscziemii;
            model = newton;
            Naukowiec = new PictureBox();
            Naukowiec.Image = model;
            Naukowiec.Size = new Size(model.Width / zoom, model.Height / zoom);

            dialog = new Label();
            dialog.AutoSize = true;
            dialog.MaximumSize = new Size(200, 200);
            ustawnaukowca(1);
            Main.Controls.Add(dialog);
            Main.Paint += new PaintEventHandler(paint);
        }

        public void ustawnaukowca(int naukowiec)
        {
            Naukowiec.Visible = true;
            dialog.Visible = true;
            if (naukowiec == 1)
            {
                model = newton;
                Naukowiec.Location = new Point(700, Main.Height - wysokoscziemii - Naukowiec.Height + 20);
                Naukowiec.Size = new Size(model.Width / zoom, model.Height / zoom);
                dialog.Location = new Point(Naukowiec.Location.X - Naukowiec.Width/2, Naukowiec.Location.Y - 130);
            }
            else if (naukowiec == 2)
            {
                model = tesla;
                Naukowiec.Size = new Size(model.Width / zoom, model.Height / zoom);
                Naukowiec.Location = new Point(200, Main.Height - wysokoscziemii - Naukowiec.Height + 17);
                dialog.Location = new Point(Naukowiec.Location.X - Naukowiec.Width / 2, Naukowiec.Location.Y - 150);
            }
            else if (naukowiec == 3)
            {
                model = einstein;
                Naukowiec.Size = new Size(model.Width / zoom, model.Height / zoom);
                Naukowiec.Location = new Point(700, Main.Height - wysokoscziemii - Naukowiec.Height + 18);
                dialog.Location = new Point(Naukowiec.Location.X - Naukowiec.Width / 2, Naukowiec.Location.Y - 130);
            }
            Naukowiec.Image = model;
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
