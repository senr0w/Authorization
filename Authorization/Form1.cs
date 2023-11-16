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
            RegBtn.Hide();
            label3.Hide();
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
            User user = users.Find(u => u.Username == enteredUsername && u.Password == enteredPassword);

            if (user != null)
            {
                Login newForm = new Login();
                newForm.Show();
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

        private void Authorization_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            EnterBtn.Hide();
            RegBtn.Show();
            label2.Hide();
            label3.Show();
        }
        private void OnMouseEnter(object sender, EventArgs e)
        {
            label2.Font = new Font(label2.Font,  FontStyle.Underline);
            label3.Font = new Font(label3.Font, FontStyle.Underline);
        }
        private void OnMouseLeave(object sender, EventArgs e)
        {
            label2.Font = new Font(label2.Font,  FontStyle.Regular);
            label3.Font = new Font(label3.Font, FontStyle.Regular);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            EnterBtn.Show();
            RegBtn.Hide();
            label2.Show();
            label3.Hide();
        }
    }
}













