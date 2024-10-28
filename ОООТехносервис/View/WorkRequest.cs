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
    public partial class WorkRequest : Form
    {
        //Переданная заявка
        Model.Request request=null;
        
        public WorkRequest()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Конструктор с параметром
        /// </summary>
        /// <param name="number"> 0 - новая, номер выбранной заявки</param>
        public WorkRequest(int number)
        {
            InitializeComponent();
            if (number!=0)
            {
                request = Helper.DB.Request.Where(r=>r.RequestId==number).FirstOrDefault();
                MessageBox.Show("ФИО клиента " + request.User1.UserFullName);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
