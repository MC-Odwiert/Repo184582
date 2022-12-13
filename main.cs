using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Fizyczny_Mag
{
    public partial class main : Form
    {

        private menu kmenu;
        private poziom kpoziom;
        private gracz kgracz;
        private naukowiec knaukowiec;
        
        public main()
        {
            InitializeComponent();
            this.CenterToScreen();
            this.BackgroundImage = poziom.obraztla;
            
            kpoziom = new poziom(this);
            kgracz = new gracz(this);
            knaukowiec = new naukowiec(this);
            kmenu = new menu(this);
            Application.Idle += gra;

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
            if (e.KeyCode == Keys.A)
            {
                kgracz.ruch(1);
            }
            if (e.KeyCode == Keys.D)
            {
                kgracz.ruch(2);
            }
        }
    }
}