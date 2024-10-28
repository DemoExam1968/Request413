using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ОООТехносервис.Classes;

namespace ОООТехносервис.View
{
    public partial class Auth : Form
    {
        public Auth()
        {
            InitializeComponent();
            //Установка связи с БД
            try
            {
                Helper.DB = new Model.DBRequests();
                MessageBox.Show("Связь с БД установлена");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Связь с БД не установлена");
            }
        }
        /// <summary>
        /// Завершение приложения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Вход в систему
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonInput_Click(object sender, EventArgs e)
        {
            //string login = textBoxLogin.Text;
            //string password = textBoxPassword.Text;
            //List<Model.User> users =  Helper.DB.User.ToList();
            //users = Helper.DB.User.Where(u=>u.UserLogin == login && u.UserPassword==password).ToList();
            Helper.user = Helper.DB.User.Where(u => u.UserLogin == textBoxLogin.Text && u.UserPassword == textBoxPassword.Text).FirstOrDefault();
            if (Helper.user != null)
            {
                MessageBox.Show("Вы вошли с ролью "+ Helper.user.Role.RoleName);
                View.ListRequests listRequests = new View.ListRequests();
                this.Hide();
                listRequests.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("NOT");

            }
        }
    }
}
