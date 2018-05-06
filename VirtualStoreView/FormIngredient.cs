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
    public partial class FormIngredient : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly IIngredientService service;

        private int? id;

        private List<IngredientElementUserViewModel> ingredientElements;


        public FormIngredient(IIngredientService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void FormIngredient_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    IngredientUserViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxName.Text = view.IngredientName;
                        textBoxPrice.Text = view.Price.ToString();
                        ingredientElements = view.IngredientElement;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                ingredientElements = new List<IngredientElementUserViewModel>();
            }
        }

        private void FormIngredient_Load_1(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    IngredientUserViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxName.Text = view.IngredientName;
                        textBoxPrice.Text = view.Price.ToString();
                        ingredientElements = view.IngredientElement;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                ingredientElements = new List<IngredientElementUserViewModel>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (ingredientElements != null)
                {
                    dataGridView.DataSource = null;
                    dataGridView.DataSource = ingredientElements;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[2].Visible = false;
                    dataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormIngredientElement>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.Model != null)
                {
                    if (id.HasValue)
                    {
                        form.Model.IngredientId = id.Value;
                    }
                    ingredientElements.Add(form.Model);
                }
                LoadData();
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormIngredientElement>();
                form.Model = ingredientElements[dataGridView.SelectedRows[0].Cells[0].RowIndex];
                if (form.ShowDialog() == DialogResult.OK)
                {
                    ingredientElements[dataGridView.SelectedRows[0].Cells[0].RowIndex] = form.Model;
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        ingredientElements.RemoveAt(dataGridView.SelectedRows[0].Cells[0].RowIndex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (ingredientElements == null || ingredientElements.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                List<IngredientElementConnectingModel> ingredientElementBM = new List<IngredientElementConnectingModel>();
                for (int i = 0; i < ingredientElements.Count; ++i)
                {
                    ingredientElementBM.Add(new IngredientElementConnectingModel
                    {
                        Id = ingredientElements[i].Id,
                        IngredientId = ingredientElements[i].IngredientId,
                        ElementId = ingredientElements[i].ElementId,
                        Count = ingredientElements[i].Count
                    });
                }
                if (id.HasValue)
                {
                    service.UpdElement(new IngredientConnectingModel
                    {
                        Id = id.Value,
                        IngredientName = textBoxName.Text,
                        Value = Convert.ToInt32(textBoxPrice.Text),
                        IngredientElement = ingredientElementBM
                    });
                }
                else
                {
                    service.AddElement(new IngredientConnectingModel
                    {
                        IngredientName = textBoxName.Text,
                        Value = Convert.ToInt32(textBoxPrice.Text),
                        IngredientElement = ingredientElementBM
                    });
                }
                MessageBox.Show("Сохранение успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
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

        private void groupBoxComponents_Enter(object sender, EventArgs e)
        {

        }
    }
}
