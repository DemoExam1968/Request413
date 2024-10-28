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
using ОООТехносервис.Model;

namespace ОООТехносервис.View
{
    public partial class ListRequests : Form
    {
        List<Model.Request> requests = new List<Model.Request>();   

        public ListRequests()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Возврат в авторизацию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowRequests()
        {
            requests = Helper.DB.Request.ToList();  //Все заявки
            //Поиск по номеру заявки
            if(textBoxSearch.Text.Length > 0 )
            {
                //точное совпадение
                //requests = requests.Where(r => r.RequestId.ToString() == textBoxSearch.Text).ToList();
                //чатичное совпадение
                requests = requests.Where(r => r.RequestId.ToString().Contains(textBoxSearch.Text)).ToList();
            }

            //Фильтрация по статусу
            if ((int)comboBoxFilter.SelectedIndex != 0)
            {
                requests = requests.Where(r => r.RequestStatusId == (int)comboBoxFilter.SelectedValue).ToList();
                //requests = requests.Where(r => r.RequestStatusId == comboBoxFilter.SelectedIndex).ToList();
            }

            //Фильтрация по роли
            if (Helper.user.UserRoleId == 1)
            {
                requests = requests.Where(r => r.RequestUserId == Helper.user.UserId).ToList();
            }
            if (Helper.user.UserRoleId == 2)
            {
                requests = requests.Where(r => r.RequestMasterId == Helper.user.UserId).ToList();
            }

            // dataGridViewRequests.DataSource = requests;
            //DGV настроим на отображение
            dataGridViewRequests.Rows.Clear();
            int i = 0;
            foreach (Model.Request request in requests)
            {
                dataGridViewRequests.Rows.Add();
                dataGridViewRequests.Rows[i].Cells[0].Value = request.RequestId.ToString();
                dataGridViewRequests.Rows[i].Cells[1].Value = request.RequestDate.ToShortDateString();
                dataGridViewRequests.Rows[i].Cells[2].Value = request.Equipment.EquipmentName.ToString();
                dataGridViewRequests.Rows[i].Cells[3].Value = request.User1.UserFullName.ToString();
                dataGridViewRequests.Rows[i].Cells[4].Value = request.Status.StatusName.ToString();
                dataGridViewRequests.Rows[i].Cells[5].Value = request.User.UserFullName.ToString();
                dataGridViewRequests.Rows[i].Cells[6].Value = request.Stage.StageName.ToString();
                i++;
            }
        }

        private void ListRequests_Shown(object sender, EventArgs e)
        {
            buttonAdd.Visible=buttonEdit.Visible=buttonReport.Visible=false;
            //Активность кнопок от роли
            switch(Helper.user.UserRoleId)
            {
                case 2:             //Мастер
                    buttonEdit.Visible = true;
                    break;
                case 3:             //Оператор
                    buttonAdd.Visible = buttonEdit.Visible = true;
                    break;
                case 4:             //Менеджер
                    buttonReport.Visible = true;
                    break;
            }
            //Настроить список статусов
            List<Model.Status> statuses = Helper.DB.Status.ToList();
            Model.Status status = new Status();
            status.StatusId = 0;
            status.StatusName = "Все статусы";
            statuses.Insert(0, status);
            comboBoxFilter.DataSource = statuses;
            comboBoxFilter.DisplayMember = "StatusName";
            comboBoxFilter.ValueMember = "StatusId";
            ShowRequests();
        }

        /// <summary>
        /// Ввод номеро для поиска
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            ShowRequests();
        }

        /// <summary>
        /// Получение статуса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowRequests();
        }

        /// <summary>
        /// Новая заявка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            View.WorkRequest workRequest = new View.WorkRequest(0);
            this.Hide();
            workRequest.ShowDialog();
            this.Show();
        }
        /// <summary>
        /// Редактирование заявки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            int selectNumber = Convert.ToInt32(dataGridViewRequests.CurrentRow.Cells[0].Value);
            View.WorkRequest workRequest = new View.WorkRequest(selectNumber);
            this.Hide();
            workRequest.ShowDialog();
            this.Show();
            //MessageBox.Show("Номер выбранной заявки "+ selectNumber);
        }
    }
}
