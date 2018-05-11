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
using VirtualStorePlace.UserViewModel;



namespace VirtualStoreView
{
    public partial class FormProductStorageLoad : Form
    {

        public FormProductStorageLoad()
        {
            InitializeComponent();
        }

        private void FormProductStorageLoad_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView.Rows.Clear();
                foreach (var elem in Task.Run(() => APIClient.GetRequestData<List<ProductStorageLoadUserViewModel>>("api/Report/GetStocksLoad")).Result)
                {
                    dataGridView.Rows.Add(new object[] { elem.ProductStorageName, "", "" });
                    foreach (var listElem in elem.Elements)
                    {
                        dataGridView.Rows.Add(new object[] { "", listElem.Item1, listElem.Item2 });
                    }
                    dataGridView.Rows.Add(new object[] { "Итого", "", elem.TotalCount });
                    dataGridView.Rows.Add(new object[] { });
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

        private void buttonSaveToExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "xls|*.xls|xlsx|*.xlsx"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string fileName = sfd.FileName;
                Task task = Task.Run(() => APIClient.PostRequestData("api/Report/SaveStocksLoad", new ReportConnectingModel
                {
                    FileName = fileName
                }));

                task.ContinueWith((prevTask) => MessageBox.Show("Выполнено", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information),
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
            }
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
