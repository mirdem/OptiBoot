
using System.Runtime.InteropServices;

namespace OptiBoot
{
    public partial class FormWelcomeScreen : Form
    {

        public FormWelcomeScreen()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.Black;  // Saydam olacak renk
            this.TransparencyKey = Color.Black;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            FormSelectScreen formSelectScreen = new FormSelectScreen();
            formSelectScreen.Show();
            this.Hide();
            timer1.Enabled = false;
        }

        private void FormWelcomeScreen_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.AlwaysTop == true)
            {
                this.TopMost = true;
            }
        }
    }
}
