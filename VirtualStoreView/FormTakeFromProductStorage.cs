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
    public partial class FormTakeFromProductStorage : Form
    {

        public int Id { set { id = value; } }

        private int? id;


        public FormTakeFromProductStorage()
        {
            InitializeComponent();
        }

        private void FormTakeFromProductStorage_Load(object sender, EventArgs e)
        {
            try
            {
                if (!id.HasValue)
                {
                    MessageBox.Show("Не указан заказ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
                List<KitchenerUserViewModel> list = Task.Run(() => APIClient.GetRequestData<List<KitchenerUserViewModel>>("api/Kitchener/GetList")).Result;
                if (list != null)
                {
                    comboBoxImplementer.DisplayMember = "KitchenerFIO";
                    comboBoxImplementer.ValueMember = "Id";
                    comboBoxImplementer.DataSource = list;
                    comboBoxImplementer.SelectedItem = null;
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
            if (comboBoxImplementer.SelectedValue == null)
            {
                MessageBox.Show("Выберите исполнителя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                int implementerId = Convert.ToInt32(comboBoxImplementer.SelectedValue);
                Task task = Task.Run(() => APIClient.PostRequestData("api/General/TakeOrderInWork", new CustomerSelectionModel
                {
                    Id = id.Value,
                    KitchenerId = implementerId
                }));

                task.ContinueWith((prevTask) => MessageBox.Show("Заказ передан в работу. Обновите список", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information),
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
