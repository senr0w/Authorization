using System;
using System.Reflection.Emit;
using System.Windows.Forms;


namespace Authorization
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //Windows Media Player на форме для проигрования заставки при авторизации
            Player1.URL = "Video.mp4";
            Player1.uiMode = "none";
            Player1.settings.playCount = 1000;
            Player1.settings.autoStart = true;
            Player1.enableContextMenu = false;
            Player1.Enabled = false;
        
        }

        //Выход из данного окна,на окно авторизации/регистрации
        private void ExitBtn_Click(object sender, EventArgs e)
        {   Authorization form = new Authorization();
            form.Show();
            this.Close();
        }

        public void ChangeLabel(string s)
        {
            LbLogin.Text = s;
        }
        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
        }
    }
}
