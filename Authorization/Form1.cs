using System;
using System.Collections.Generic;
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
    }
}













