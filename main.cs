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
                kpoziom.animacjastrza³ki();
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
                    knaukowiec.dialog.Text = "II zasada dynamiki -  Je¿eli na cia³o dzia³a sta³a niezrównowa¿ona si³a, lub kilka si³ których wypadkowa jest sta³a, to cia³o porusza siê z przyspieszeniem wprost proporcjonalnym do si³y wypadkowej, a odwrotnie proporcjonalnym do masy cia³a. Oj, upad³a mi notatka..";
                }
                else if (kgracz.Gracz.Location.X > 300)
                {
                    knaukowiec.dialog.Text = "I zasada dynamiki - Je¿eli na cia³o nie dzia³a ¿adna si³a lub wszystkie dzia³aj¹ce si³y siê równowa¿¹ to cia³o porusza siê ruchem jednostajnym prostoliniowym, w przypadku gdy prêdkoœæ ta jest równa 0 cia³o jest w spoczynku";
                }
                else if (kgracz.Gracz.Location.X > 0)
                {
                    knaukowiec.dialog.Text = "Co tu robi ten kamieñ? Musia³y siê tu stoczyæ z pobliskich gór.. Je¿eli na swojej drodze spotkasz podobn¹ przeszkodê mo¿esz j¹ przesun¹æ korzystaj¹c z mojej pierwszej zasady.";
                }
            }
            else if (level == 2)
            {
                wzor = "F = f * m * g";
                zadanietekst.Visible = true;
                zadanietekst.Text = "Na drodze napotkano kamieñ o masie m = 5kg, jak¹ si³¹ nale¿y na niego zadzia³aæ aby usun¹æ przeszkodê. Wspó³czynnik tarcia mi = 0,5;   Przyœpieszenie ziemskie g ~= 10N/Kg ";
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
                zadanietekst.Text = "Krokodyl znajduje siê w odleg³oœci 200m i p³ynie z prêdkoœci¹ 8m/s. Ile czasu ma cz³owiek, ¿eby przep³yn¹æ na drug¹ stronê rzeki zanim przyp³ynie krokodyl?";
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
                zadanietekst.Text = "Znalaz³eœ studniê w której jest woda, aby j¹ nabraæ musisz spuœciæ w niej wiadro na sznurze, Einstein aby nie przenosiæ zbyt d³ugiego, a co za tym idzie - ciê¿kiego kawa³ka sznura musi oceniæ g³êbokoœæ studni. Wrzuci³eœ do niej kamieñ, a po t = 365/68s us³ysza³eœ 'plum'. Jak¹ g³êbokoœæ ma studnia?";

                knaukowiec.dialog.Text = "Witaj poszukiwaczu! Widzisz, potrzebuje nabraæ wody ze studni, ale chce wiedziec ile dok³adnie sznura u¿yæ ¿eby siê nie nadŸwigaæ z domu. Myœlê ¿e naj³atwiej by³oby to zrobiæ wrzucaj¹c kamieñ do studni...";
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
                    knaukowiec.dialog.Text = "Wyliczy³em tak¹ sam¹ d³ugoœæ liny jak ty! Myœlê ¿e to mo¿e siê udaæ, dziêkuje bardzo za pomoc i powodzenia w dalszych przygodach!";
                }

                rezystoryimage.Visible = false;
                this.BackgroundImage = poziom.bg3;
            }
            else if (level == 5)
            {
                wzor = "Wzór podany w dialogu!";
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
                    knaukowiec.dialog.Text = "Mog³eœ ju¿ na swojej drodze spotkaæ Izaaka Newtona, ale pewnie nie powiedzia³ Ci jak wyliczaæ si³ê odœrodkow¹, dlatego Ci pomogê. Widzisz, ¿eby nie oderwaæ siê od toru na pêtli wystarczy abyœ równowa¿y³ si³ê, która œci¹ga Ciê w dó³. Do tego mo¿esz u¿yæ wzoru F=mv^2/r, przyrównaj go do si³y przyspieszenia grawitacyjnego i bez problemu pokonasz pêtlê!";
                }
                else if (kgracz.Gracz.Location.X > 50)
                {
                    knaukowiec.dialog.Text = "Najpierw jednak trzeba Ciê nauczyæ, czym s¹ rezystory i jak je ³¹czyæ. W prostych s³owach rezystory ograniczaj¹ pr¹d p³yn¹cy w obwodzie, mo¿emy je po³¹czyæ na dwa sposoby, równolegle i szeregowo, równoleg³e po³¹czenie to takie, gdy rezystory s¹ jeden nad drugim, a szeregowe, gdy obok siebie, a ich opór podany w Ohmach liczy siê tak: Szeregowo: R = R1 + R2      Równolegle: 1/R = 1/R1 + 1/R2";
                }
                else if (kgracz.Gracz.Location.X > 0) 
                {
                    knaukowiec.dialog.Text = "Witaj nazywam siê Nikola Tesla! Odkry³em wiele rzeczy zwi¹zanych z pr¹dem przemiennym, ale jako ¿e dopiero zaczynasz swoj¹ przygodê z fizyk¹ to nie jesteœ jeszcze gotowy na moje odkryci¹. Nauczê ciê natomiast podstawowego prawa zwi¹zanego z elektronik¹, czyli prawa Ohma!";
                }                

                if (progress == 3)
                {
                    zadanietekst.Text = "Wyznacz pr¹d, który musi wygenerowaæ bateria aby napiêcie na akumulatorze osi¹gnê³o 12V; R = 3 ohm";
                }

                if (progress == 4)
                {
                    zadanietekst.Text = "Mamy kolejkê gorsk¹ z pêtelk¹ o promieniu R = 10m, z jak¹ prêdkoœci¹ musimy jechaæ aby nie spaœæ jad¹c do góry nogami?";
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
                DialogResult wyjscie = MessageBox.Show("Czy na pewno chcesz wyjœæ z gry?", "FIZYCZNY MAG", MessageBoxButtons.YesNo);
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
                    MessageBox.Show("Uff, w koñcu uda³o siê przesun¹æ ten kamieñ!");
                }
                else
                    MessageBox.Show("Chyba u¿y³em z³ej iloœci si³y aby przesun¹æ kamieñ... Spróbuje u¿yæ innej...");

            }
            else if (progress == 1 && level == 3)
            {
                if (textBox1.Text == "25")
                {
                    progress++;
                    MessageBox.Show("Hmm, myœlê ¿e to jest poprawna prêdkoœæ z jak¹ powinienem przep³yn¹æ!");
                }
                else
                    MessageBox.Show("Krokodyl prawie mnie zjad³, ale uda³o mi siê dop³yn¹æ spowrotem do brzegu!");
            }
            else if (progress == 2 && level == 4)
            {
                if (textBox1.Text == "125")
                {
                    progress++;
                    MessageBox.Show("Hmm, myœlê ¿e to jest poprawna d³ugoœæ liny!");
                }
                else
                    MessageBox.Show("Einstein powiedzia³ ¿e raczej ta d³ugoœæ liny jest niepoprawna... spróbujê jeszcze raz!");
            }
            else if (progress == 3 && level == 5)
            {
                if (textBox1.Text == "2,7")
                {
                    progress++;
                    MessageBox.Show("Tesla mówi ¿e to poprawny wynik!");
                }
                else
                    MessageBox.Show("Niestety wózek nie rusza...");
            }
            else if (progress == 4 && level == 5)
            {
                if (textBox1.Text == "7,07")
                {
                    level++;
                    progress++;
                    MessageBox.Show("Tesla mówi ¿e to poprawny wynik i mogê ruszaæ dalej!");
                }
                else
                    MessageBox.Show("Tesla mówi ¿e Ÿle to obliczy³em, dobrze ¿e mnie pilnuje i nic mi siê nie sta³o...");
            }

            gamedesign();
        }

        private void ziemia_Click(object sender, EventArgs e)
        {

        }
    }
}