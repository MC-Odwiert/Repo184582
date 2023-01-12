using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Fizyczny_Mag
{
    public partial class main : Form
    {
        private int level = 1;
        private int progress = 0;

        private menu kmenu;
        private poziom kpoziom;
        private gracz kgracz;
        private naukowiec knaukowiec;
        private string wzor;
        public int wysokoscziemii;
        private static Image zadanie2wykonane = Image.FromFile("../../../FizycznyMag_assets/zadanie2wykonane.png");
        private static Image dziuraimage = Image.FromFile("../../../FizycznyMag_assets/groundTile_hole.png");

        public main()
        {
            InitializeComponent();
            this.CenterToScreen();
            this.BackgroundImage = poziom.bg1;
            wysokoscziemii = ziemia.Height+15;
            kpoziom = new poziom(this, wysokoscziemii);
            kgracz = new gracz(this, wysokoscziemii);
            knaukowiec = new naukowiec(this, wysokoscziemii);
            kmenu = new menu(this);
            Application.Idle += gra;
            gamedesign();
        }

        private void gra(object sender, EventArgs e)
        {
            while (IsApplicationIdle())
            {
                kpoziom.animacjastrza�ki();
                Invalidate();
            }

        }





       
        [StructLayout(LayoutKind.Sequential)]
        public struct NativeMessage
        {
            public IntPtr Handle;
            public uint Message;
            public IntPtr WParameter;
            public IntPtr LParameter;
            public uint Time;
            public Point Location;
        }
        [DllImport("user32.dll")]
        public static extern int PeekMessage(out NativeMessage message, IntPtr window, uint filterMin, uint filterMax, uint remove);
        bool IsApplicationIdle()
        {
            NativeMessage result;
            return PeekMessage(out result, IntPtr.Zero, (uint)0, (uint)0, (uint)0) == 0;
        }

        private void main_Load(object sender, EventArgs e)
        {
            
        }

        private void main_KeyDown(object sender, KeyEventArgs e)
        {
            lewoprawopoziom();
            gamedesign();

            if (e.KeyCode == Keys.A)
            {
                kgracz.ruch(1);
            }
            if (e.KeyCode == Keys.D)
            {
                kgracz.ruch(2);
            }
            if (e.KeyCode == Keys.I) {
                button1.Enabled = true;
                textBox1.Enabled = true;
                zatwierdzwynik.Enabled = true;
                button1.Text = "TRYB CHODZENIA [WCISNIJ PRZYCISK MYSZKA]";
            }
        }

        private void lewoprawopoziom()
        {
            if (kgracz.Gracz.Location.X >= kpoziom.Strzalka.Location.X - 30 && kpoziom.Strzalka.Visible)
            {
                if (level < 6)
                {
                    level++;
                    kgracz.Gracz.Location = new Point(20, kgracz.Gracz.Location.Y);
                }
            }
            else if (kgracz.Gracz.Location.X <= 0)
            {
                if (level > 1)
                {
                    level--;
                    kgracz.Gracz.Location = new Point(this.Width - 200, kgracz.Gracz.Location.Y);
                }
            }
        }

        private void gamedesign()
        {
            if (level == 1)
            {
                wzor = "Brak wzoru!";
                zadanietekst.Visible = false;
                dziura.Visible = false;
                kmenu.ustawpoziom(1);
                kpoziom.ustawprzeszkode(1);
                knaukowiec.ustawnaukowca(1);
                kpoziom.Strzalka.Visible = true;
                this.BackgroundImage = poziom.bg1;
                if (kgracz.Gracz.Location.X > 600)
                {
                    knaukowiec.dialog.Text = "II zasada dynamiki -  Je�eli na cia�o dzia�a sta�a niezr�wnowa�ona si�a, lub kilka si� kt�rych wypadkowa jest sta�a, to cia�o porusza si� z przyspieszeniem wprost proporcjonalnym do si�y wypadkowej, a odwrotnie proporcjonalnym do masy cia�a. Oj, upad�a mi notatka..";
                }
                else if (kgracz.Gracz.Location.X > 300)
                {
                    knaukowiec.dialog.Text = "I zasada dynamiki - Je�eli na cia�o nie dzia�a �adna si�a lub wszystkie dzia�aj�ce si�y si� r�wnowa�� to cia�o porusza si� ruchem jednostajnym prostoliniowym, w przypadku gdy pr�dko�� ta jest r�wna 0 cia�o jest w spoczynku";
                }
                else if (kgracz.Gracz.Location.X > 0)
                {
                    knaukowiec.dialog.Text = "Co tu robi ten kamie�? Musia�y si� tu stoczy� z pobliskich g�r.. Je�eli na swojej drodze spotkasz podobn� przeszkod� mo�esz j� przesun�� korzystaj�c z mojej pierwszej zasady.";
                }
            }
            else if (level == 2)
            {
                wzor = "F = f * m * g";
                zadanietekst.Visible = true;
                zadanietekst.Text = "Na drodze napotkano kamie� o masie m = 5kg, jak� si�� nale�y na niego zadzia�a� aby usun�� przeszkod�. Wsp�czynnik tarcia mi = 0,5;   Przy�pieszenie ziemskie g ~= 10N/Kg ";
                dziura.Visible = true;
                kmenu.ustawpoziom(1);
                kpoziom.ustawprzeszkode(1);
                knaukowiec.dialog.Visible = false;
                knaukowiec.Naukowiec.Visible = false;
                if (progress == 0)
                {
                    kpoziom.Obiekt.Location = new Point(300,407);

                    if (kgracz.Gracz.Location.X >= dziura.Location.X - 100)
                    {
                       kgracz.Gracz.Location = new Point(dziura.Location.X - 100, kgracz.Gracz.Location.Y);
                    }
                }
                else
                {
                    dziura.Image = zadanie2wykonane;
                    kpoziom.Obiekt.Visible = false;
                    kpoziom.Strzalka.Visible = true;
                }

                ziemia.Visible = true;
            }
            else if (level == 3)
            {
                wzor = "T = s / v";
                dziura.Visible = false;
                zadanietekst.Text = "Krokodyl znajduje si� w odleg�o�ci 200m i p�ynie z pr�dko�ci� 8m/s. Ile czasu ma cz�owiek, �eby przep�yn�� na drug� stron� rzeki zanim przyp�ynie krokodyl?";
                knaukowiec.Naukowiec.Visible = false;
                knaukowiec.dialog.Visible = false;
                ziemia.Visible = false;
                kpoziom.ustawprzeszkode(2);
                kpoziom.Obiekt.Visible = true;
                kmenu.ustawpoziom(2);
                this.BackgroundImage = poziom.bg2;
                
                if (progress == 1)
                {
                    if (kgracz.Gracz.Location.X > 300 && kgracz.Gracz.Location.X < 640)
                    {
                        kgracz.Gracz.Location = new Point(300, kgracz.Gracz.Location.Y);
                    }
                }
                else
                {
                    kpoziom.Strzalka.Visible = true;
                    if (kgracz.Gracz.Location.X > 300 && kgracz.Gracz.Location.X < 640)
                    {
                        kgracz.Gracz.Location = new Point(680, kgracz.Gracz.Location.Y);
                    }
                    else if (kgracz.Gracz.Location.X > 300 && kgracz.Gracz.Location.X < 660)
                    {
                        kgracz.Gracz.Location = new Point(280, kgracz.Gracz.Location.Y);
                    }
                }

            }
            else if (level == 4)
            {
                wzor = "t1 + t2 = t  =  s/v + sqrt(2*(s/g)";
                ziemia.Visible = true;
                dziura.Visible = false;
                zadanietekst.Text = "Znalaz�e� studni� w kt�rej jest woda, aby j� nabra� musisz spu�ci� w niej wiadro na sznurze, Einstein aby nie przenosi� zbyt d�ugiego, a co za tym idzie - ci�kiego kawa�ka sznura musi oceni� g��boko�� studni. Wrzuci�e� do niej kamie�, a po t = 365/68s us�ysza�e� 'plum'. Jak� g��boko�� ma studnia?";

                knaukowiec.dialog.Text = "Witaj poszukiwaczu! Widzisz, potrzebuje nabra� wody ze studni, ale chce wiedziec ile dok�adnie sznura u�y� �eby si� nie nad�wiga� z domu. My�l� �e naj�atwiej by�oby to zrobi� wrzucaj�c kamie� do studni...";
                kpoziom.Obiekt.Visible = true;
                kpoziom.ustawprzeszkode(3);
                kmenu.ustawpoziom(3);
                knaukowiec.ustawnaukowca(3);
                if (progress == 2)
                {
                    kpoziom.Strzalka.Visible = false;
                }
                else
                {
                    kpoziom.Strzalka.Visible = true;
                    knaukowiec.dialog.Text = "Wyliczy�em tak� sam� d�ugo�� liny jak ty! My�l� �e to mo�e si� uda�, dzi�kuje bardzo za pomoc i powodzenia w dalszych przygodach!";
                }

                rezystoryimage.Visible = false;
                this.BackgroundImage = poziom.bg3;
            }
            else if (level == 5)
            {
                wzor = "Wz�r podany w dialogu!";
                kmenu.ustawpoziom(4);
                kpoziom.ustawprzeszkode(4);
                knaukowiec.ustawnaukowca(2);

                if (dziura.Visible == false)
                {
                    dziura.Visible = true;
                    dziura.Image = dziuraimage;
                }

                if (kgracz.Gracz.Location.X > 150)
                {
                    knaukowiec.dialog.Text = "Mog�e� ju� na swojej drodze spotka� Izaaka Newtona, ale pewnie nie powiedzia� Ci jak wylicza� si�� od�rodkow�, dlatego Ci pomog�. Widzisz, �eby nie oderwa� si� od toru na p�tli wystarczy aby� r�wnowa�y� si��, kt�ra �ci�ga Ci� w d�. Do tego mo�esz u�y� wzoru F=mv^2/r, przyr�wnaj go do si�y przyspieszenia grawitacyjnego i bez problemu pokonasz p�tl�!";
                }
                else if (kgracz.Gracz.Location.X > 50)
                {
                    knaukowiec.dialog.Text = "Najpierw jednak trzeba Ci� nauczy�, czym s� rezystory i jak je ��czy�. W prostych s�owach rezystory ograniczaj� pr�d p�yn�cy w obwodzie, mo�emy je po��czy� na dwa sposoby, r�wnolegle i szeregowo, r�wnoleg�e po��czenie to takie, gdy rezystory s� jeden nad drugim, a szeregowe, gdy obok siebie, a ich op�r podany w Ohmach liczy si� tak: Szeregowo: R = R1 + R2      R�wnolegle: 1/R = 1/R1 + 1/R2";
                }
                else if (kgracz.Gracz.Location.X > 0) 
                {
                    knaukowiec.dialog.Text = "Witaj nazywam si� Nikola Tesla! Odkry�em wiele rzeczy zwi�zanych z pr�dem przemiennym, ale jako �e dopiero zaczynasz swoj� przygod� z fizyk� to nie jeste� jeszcze gotowy na moje odkryci�. Naucz� ci� natomiast podstawowego prawa zwi�zanego z elektronik�, czyli prawa Ohma!";
                }                

                if (progress == 3)
                {
                    zadanietekst.Text = "Wyznacz pr�d, kt�ry musi wygenerowa� bateria aby napi�cie na akumulatorze osi�gn�o 12V; R = 3 ohm";
                }

                if (progress == 4)
                {
                    zadanietekst.Text = "Mamy kolejk� gorsk� z p�telk� o promieniu R = 10m, z jak� pr�dko�ci� musimy jecha� aby nie spa�� jad�c do g�ry nogami?";
                }

                if (progress == 3 || progress == 4)
                {
                    kpoziom.Strzalka.Visible = false;
                    rezystoryimage.Visible = true;
                    if (kgracz.Gracz.Location.X >= dziura.Location.X - 100)
                    {
                        kgracz.Gracz.Location = new Point(dziura.Location.X - 100, kgracz.Gracz.Location.Y);
                    }
                }
                else
                {
                    kgracz.Gracz.Location = new Point(kpoziom.Strzalka.Location.X, kgracz.Gracz.Location.Y);
                    rezystoryimage.Visible = false;
                    kpoziom.Strzalka.Visible = true;
                }

                knaukowiec.Naukowiec.Visible = true;
                this.BackgroundImage = poziom.bg3;
            }
            else if (level == 6)
            {
                if (kgracz.Gracz.Location.X <= 50)
                {
                    kgracz.Gracz.Location = new Point(50, kgracz.Gracz.Location.Y);
                }
                if (kgracz.Gracz.Location.X >= dziura.Location.X - 100)
                {
                    kgracz.Gracz.Location = new Point(dziura.Location.X - 100, kgracz.Gracz.Location.Y);
                }
                logo.Visible = true;
                knaukowiec.dialog.Visible = false;
                kpoziom.Obiekt.Visible = false;
                przepasc.Visible = true;
                zadanietekst.Visible = false;
                rezystoryimage.Visible = false;
                kpoziom.Strzalka.Visible = false;
                knaukowiec.Naukowiec.Visible = false;
            }
        }
        private void main_MouseClick(object sender, MouseEventArgs e)
        {
            if (knaukowiec.Naukowiec.Visible && e.X > knaukowiec.Naukowiec.Location.X && e.X < knaukowiec.Naukowiec.Location.X + knaukowiec.Naukowiec.Width && e.Y > knaukowiec.Naukowiec.Location.Y && e.Y < knaukowiec.Naukowiec.Location.Y + knaukowiec.Naukowiec.Height)
            {
                if (knaukowiec.dialog.Visible == true)
                    knaukowiec.dialog.Visible = false;
                else
                    knaukowiec.dialog.Visible = true;
            }

            if (knaukowiec.dialog.Visible && e.X > knaukowiec.dialog.Location.X && e.X < knaukowiec.dialog.Location.X + knaukowiec.dialog.Width && e.Y > knaukowiec.dialog.Location.Y && e.Y < knaukowiec.dialog.Location.Y + knaukowiec.dialog.Height)
            {
                if (knaukowiec.Naukowiec.Visible == true)
                    knaukowiec.Naukowiec.Visible = false;
                else
                    knaukowiec.Naukowiec.Visible = true;
            }

            if (e.X > kmenu.Scroll[1].Location.X && e.X < kmenu.Scroll[1].Location.X + kmenu.Scroll[1].Width && e.Y > kmenu.Scroll[1].Location.Y && e.Y < kmenu.Scroll[1].Location.Y + kmenu.Scroll[1].Height)
            {
                DialogResult wyjscie = MessageBox.Show("Czy na pewno chcesz wyj�� z gry?", "FIZYCZNY MAG", MessageBoxButtons.YesNo);
                if (wyjscie == DialogResult.Yes)
                {
                    Application.Exit();
                }
                else if (wyjscie == DialogResult.No)
                {
                }
            }

            if (e.X > kmenu.Scroll[0].Location.X && e.X < kmenu.Scroll[0].Location.X + kmenu.Scroll[0].Width && e.Y > kmenu.Scroll[0].Location.Y && e.Y < kmenu.Scroll[0].Location.Y + kmenu.Scroll[0].Height)
            {
                MessageBox.Show(wzor);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            button1.Enabled = false;
            textBox1.Enabled = false;
            zatwierdzwynik.Enabled = false;
            button1.Text = "TRYB WPISYWANIA [WCISNIJ I NA KLAWIATURZE]";
        }

        private void zatwierdzwynik_Click(object sender, EventArgs e)
        {
            if (progress == 0 && level == 2)
            {
                if (textBox1.Text == "25")
                {
                    progress++;
                    MessageBox.Show("Uff, w ko�cu uda�o si� przesun�� ten kamie�!");
                }
                else
                    MessageBox.Show("Chyba u�y�em z�ej ilo�ci si�y aby przesun�� kamie�... Spr�buje u�y� innej...");

            }
            else if (progress == 1 && level == 3)
            {
                if (textBox1.Text == "25")
                {
                    progress++;
                    MessageBox.Show("Hmm, my�l� �e to jest poprawna pr�dko�� z jak� powinienem przep�yn��!");
                }
                else
                    MessageBox.Show("Krokodyl prawie mnie zjad�, ale uda�o mi si� dop�yn�� spowrotem do brzegu!");
            }
            else if (progress == 2 && level == 4)
            {
                if (textBox1.Text == "125")
                {
                    progress++;
                    MessageBox.Show("Hmm, my�l� �e to jest poprawna d�ugo�� liny!");
                }
                else
                    MessageBox.Show("Einstein powiedzia� �e raczej ta d�ugo�� liny jest niepoprawna... spr�buj� jeszcze raz!");
            }
            else if (progress == 3 && level == 5)
            {
                if (textBox1.Text == "2,7")
                {
                    progress++;
                    MessageBox.Show("Tesla m�wi �e to poprawny wynik!");
                }
                else
                    MessageBox.Show("Niestety w�zek nie rusza...");
            }
            else if (progress == 4 && level == 5)
            {
                if (textBox1.Text == "7,07")
                {
                    level++;
                    progress++;
                    MessageBox.Show("Tesla m�wi �e to poprawny wynik i mog� rusza� dalej!");
                }
                else
                    MessageBox.Show("Tesla m�wi �e �le to obliczy�em, dobrze �e mnie pilnuje i nic mi si� nie sta�o...");
            }

            gamedesign();
        }

        private void ziemia_Click(object sender, EventArgs e)
        {

        }
    }
}