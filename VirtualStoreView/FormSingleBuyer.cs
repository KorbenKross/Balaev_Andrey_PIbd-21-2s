using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Windows.Forms;

using VirtualStorePlace.ConnectingModel;
using VirtualStorePlace.LogicInterface;
using VirtualStorePlace.RealiseInterface;
using VirtualStorePlace.UserViewModel;

namespace VirtualStoreView
{
    public partial class FormSingleBuyer : Form
    {

        public int Id { set { id = value; } }

        private int? id;

        public FormSingleBuyer()
        {
            InitializeComponent();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFIO.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string fio = textBoxFIO.Text;
            Task task;
            if (id.HasValue)
            {
                task = Task.Run(() => APIClient.PostRequestData("api/Buyer/UpdElement", new BuyerConnectingModel
                {
                    Id = id.Value,
                    BuyerFIO = fio
                }));
            }
            else
            {
                task = Task.Run(() => APIClient.PostRequestData("api/Buyer/AddElement", new BuyerConnectingModel
                {
                    BuyerFIO = fio
                }));
            }

            task.ContinueWith((prevTask) => MessageBox.Show("Сохранение прошло успешно. Обновите список", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information),
                TaskContinuationOptions.OnlyOnRanToCompletion);
            task.ContinueWith((prevTask) =>
            {
                var ex = (Exception)prevTask.Exception;
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }, TaskContinuationOptions.OnlyOnFaulted);

            Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FormSingleBuyer_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var client = Task.Run(() => APIClient.GetRequestData<BuyerUserViewModel>("api/Buyer/Get/" + id.Value)).Result;
                    textBoxFIO.Text = client.BuyerFIO;
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
}
