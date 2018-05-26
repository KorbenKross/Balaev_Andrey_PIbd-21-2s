using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualStorePlace.UserViewModel;

namespace VirtualStoreView
{
    public partial class FormMails : Form
    {
        public FormMails()
        {
            InitializeComponent();
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                
        }

        private void dataGridView_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                List<MessageInfoViewModel> list = Task.Run(() =>
                APIClient.GetRequestData<List<MessageInfoViewModel>>("api/MessageInfo/GetList")).Result;
                if (list != null)
                {
                    dataGridView.DataSource = list;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
