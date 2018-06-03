using System;
using System.Drawing;
using System.Windows.Forms;

namespace mono_test
{
    public partial class monotest : Form
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            monotest app = new monotest();
            
            Application.Run(app);
        }
        
        public monotest()
        {
            InitializeComponent();
        }
        
        private void onForm_Load(object sender, EventArgs e)
        {
            Console.WriteLine("onForm Load");
        }

        private void onExit_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Exit Click");
        }
    }
}