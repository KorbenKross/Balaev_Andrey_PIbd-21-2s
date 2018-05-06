using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

using VirtualStorePlace.ConnectingModel;
using VirtualStorePlace.LogicInterface;
using VirtualStorePlace.RealiseInterface;
using VirtualStorePlace.UserViewModel;

namespace VirtualStoreView
{
    public partial class FormSingleBuyer : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly IBuyerCustomer service;

        private int? id;

        public FormSingleBuyer(IBuyerCustomer service)
        {
            InitializeComponent();
            this.service = service;     
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFIO.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    service.UpdElement(new BuyerConnectingModel
                    {
                        Id = id.Value,
                        BuyerFIO = textBoxFIO.Text
                    });
                }
                else
                {
                    service.AddElement(new BuyerConnectingModel
                    {
                        BuyerFIO = textBoxFIO.Text
                    });
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
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
                    BuyerUserViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxFIO.Text = view.BuyerFIO;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
