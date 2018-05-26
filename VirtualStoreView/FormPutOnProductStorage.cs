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
using VirtualStorePlace.RealiseInterface;
using VirtualStorePlace.UserViewModel;

namespace VirtualStoreView
{
    public partial class FormPutOnProductStorage : Form
    {

        public FormPutOnProductStorage()
        {
            InitializeComponent();
        }

        private void FormPutOnProductStorage_Load(object sender, EventArgs e)
        {
            try
            {
                List<ElementUserViewModel> listC = Task.Run(() => APIClient.GetRequestData<List<ElementUserViewModel>>("api/Element/GetList")).Result;
                if (listC != null)
                {
                    comboBoxComponent.DisplayMember = "ElementName";
                    comboBoxComponent.ValueMember = "Id";
                    comboBoxComponent.DataSource = listC;
                    comboBoxComponent.SelectedItem = null;
                }

                List<ProductStorageUserViewModel> listS = Task.Run(() => APIClient.GetRequestData<List<ProductStorageUserViewModel>>("api/ProductStorage/GetList")).Result;
                if (listS != null)
                {
                    comboBoxStock.DisplayMember = "ProductStorageName";
                    comboBoxStock.ValueMember = "Id";
                    comboBoxStock.DataSource = listS;
                    comboBoxStock.SelectedItem = null;
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

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxComponent.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxStock.SelectedValue == null)
            {
                MessageBox.Show("Выберите склад", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                int componentId = Convert.ToInt32(comboBoxComponent.SelectedValue);
                int stockId = Convert.ToInt32(comboBoxStock.SelectedValue);
                int count = Convert.ToInt32(textBoxCount.Text);
                Task task = Task.Run(() => APIClient.PostRequestData("api/General/PutComponentOnStock", new ProductStorageElementConnectingModel
                {
                    ElementId = componentId,
                    ProductStorageId = stockId,
                    Count = count
                }));

                task.ContinueWith((prevTask) => MessageBox.Show("Склад пополнен", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information),
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
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
