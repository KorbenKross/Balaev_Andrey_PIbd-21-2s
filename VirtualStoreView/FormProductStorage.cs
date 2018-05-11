using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualStorePlace.ConnectingModel;
using VirtualStorePlace.LogicInterface;
using System.Net.Http;
using VirtualStorePlace.RealiseInterface;
using VirtualStorePlace.UserViewModel;

namespace VirtualStoreView
{

    public partial class FormProductStorage : Form
    {
       
        public int Id { set { id = value; } }

        private int? id;

        public FormProductStorage()
        {
            InitializeComponent();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string name = textBoxName.Text;
            Task task;
            if (id.HasValue)
            {
                task = Task.Run(() => APIClient.PostRequestData("api/ProductStorage/UpdElement", new ProductStorageConnectingModel
                {
                    Id = id.Value,
                    StockName = name
                }));
            }
            else
            {
                task = Task.Run(() => APIClient.PostRequestData("api/ProductStorage/AddElement", new ProductStorageConnectingModel
                {
                    StockName = name
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

        private void FormProductStorage_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var stock = Task.Run(() => APIClient.GetRequestData<ProductStorageUserViewModel>("api/ProductStorage/Get/" + id.Value)).Result;
                    textBoxName.Text = stock.ProductStorageName;
                    dataGridView.DataSource = stock.ProductStorageElements;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[2].Visible = false;
                    dataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
