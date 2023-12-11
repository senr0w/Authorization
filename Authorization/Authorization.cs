using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Authorization
{
    public partial class Authorization : Form
    {
        private List<User> users = new List<User>();
        private string xmlFilePath = "users.xml";
        public Authorization()
        {
            InitializeComponent();
            LoadUsers();
            //При загрузке приложения автоматически спрятаны кнопка регистрации,надпись "Уже есть аккаунт?" и скрыт пароль в поле ввода
            RegBtn.Hide();
            label3.Hide();
            ShowPass.Hide();
        }
        public class User
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        private void EnterBtn_Click(object sender, EventArgs e)
        {
            string enteredUsername = textLogin.Text;
            string enteredPassword = textPass.Text;

            // Проверка введенного логина и пароля
            User user = users.Find( u => u.Username == enteredUsername && u.Password == enteredPassword);

            if (user != null)
            {
                //Открывается окно "Login" с заставкой 
                Login newForm = new Login();
                newForm.Show();
                //Автроизированный пользователь в окне Login
                newForm.ChangeLabel(this.textLogin.Text);
                //После входа поля очищаются
                textLogin.Text = "";
                textPass.Text = "";         

           

                this.Hide();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль", "Ошибка");
            }
        }
 

        private void RegBtn_Click(object sender, EventArgs e)
        {
            string newUsername = textLogin.Text;
            string newPassword = textPass.Text;


            if (users.Any(u => u.Username == newUsername))
            {
                MessageBox.Show("Пользователь с таким логином уже существует", "Ошибка");
            }
            //Проверка на пустое поле,чтобы не создать "пустого пользователя"
            else if (newUsername == "")
            {
                MessageBox.Show("Поля должны быть заполнены", "Ошибка");
            }
            else
            {
                // Регистрация нового пользователя
                User newUser = new User { Username = newUsername, Password = newPassword };
                users.Add(newUser);
                SaveUsers();
                //После регистрации также очищаются поля
                textLogin.Text = "";
                textPass.Text = "";
                MessageBox.Show("Вы успешно зарегистрировались", "Успех");
            }
            EnterBtn.Show();
            RegBtn.Hide();
        }
        private void LoadUsers()
        {
            if (File.Exists(xmlFilePath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
                using (FileStream fs = new FileStream(xmlFilePath, FileMode.Open))
                {
                    users = (List<User>)serializer.Deserialize(fs);
                }
            }
        }

        private void SaveUsers()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
            using (FileStream fs = new FileStream(xmlFilePath, FileMode.Create))
            {
                serializer.Serialize(fs, users);
            }
        }

        private void Authorization_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        //При нажатии на "Создать новый аккаунт?" текущий текст меняется на другой и появляется кнопка регистрации
        private void label2_Click(object sender, EventArgs e)
        {
            EnterBtn.Hide();
            RegBtn.Show();
            label2.Hide();
            label3.Show();
        }

        //При нажатии на "Уже есть аккаунт?" текущий текст меняется на другой и появляется кнопка войти
        private void label3_Click(object sender, EventArgs e)
        {
            EnterBtn.Show();
            RegBtn.Hide();
            label2.Show();
            label3.Hide();
        }

        //При наведении на label появляется черта под текстом
        private void OnMouseEnter(object sender, EventArgs e)
        {
            label2.Font = new Font(label2.Font, FontStyle.Underline);
            label3.Font = new Font(label3.Font, FontStyle.Underline);
        }

        //И наоборот исчезает
        private void OnMouseLeave(object sender, EventArgs e)
        {
            label2.Font = new Font(label2.Font,  FontStyle.Regular);
            label3.Font = new Font(label3.Font, FontStyle.Regular);
        }

   
        //Глазок показывает пароль в textbox Password
        private void HideEye_Click(object sender, EventArgs e)
        {
            textPass.PasswordChar = (char)0;

            //Смена глаз
            ShowPass.Show();
            HideEye.Hide();
        }

        //Глазок скрывает пароль в textbox Password
        private void ShowPass_Click(object sender, EventArgs e)
        {
            //Скрывает пароль, заменяя все символы на точки
           if (textPass.PasswordChar == (char)0)
            {
                textPass.PasswordChar = '●';
            }

           //Смена глаз
            ShowPass.Hide();
            HideEye.Show();
        }

        private void Authorization_Load(object sender, EventArgs e)
        {

        }
    }
}













