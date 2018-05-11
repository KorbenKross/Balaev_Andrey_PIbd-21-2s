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
                var responseC = APIClient.GetRequest("api/Element/GetList");
                if (responseC.Result.IsSuccessStatusCode)
                {
                    List<ElementUserViewModel> list = APIClient.GetElement<List<ElementUserViewModel>>(responseC);
                    if (list != null)
                    {
                        comboBoxComponent.DisplayMember = "ElementName";
                        comboBoxComponent.ValueMember = "Id";
                        comboBoxComponent.DataSource = list;
                        comboBoxComponent.SelectedItem = null;
                    }
                }
                else
                {
                    throw new Exception(APIClient.GetError(responseC));
                }
                var responseS = APIClient.GetRequest("api/ProductStorage/GetList");
                if (responseS.Result.IsSuccessStatusCode)
                {
                    List<ProductStorageUserViewModel> list = APIClient.GetElement<List<ProductStorageUserViewModel>>(responseS);
                    if (list != null)
                    {
                        comboBoxStock.DisplayMember = "ProductStorageName";
                        comboBoxStock.ValueMember = "Id";
                        comboBoxStock.DataSource = list;
                        comboBoxStock.SelectedItem = null;
                    }
                }
                else
                {
                    throw new Exception(APIClient.GetError(responseC));
                }
            }
            catch (Exception ex)
            {
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
                var response = APIClient.PostRequest("api/General/PutComponentOnStock", new ProductStorageElementConnectingModel
                {
                    ElementId = Convert.ToInt32(comboBoxComponent.SelectedValue),
                    ProductStorageId = Convert.ToInt32(comboBoxStock.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text)
                });
                if (response.Result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    throw new Exception(APIClient.GetError(response));
                }
            }
            catch (Exception ex)
            {
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
