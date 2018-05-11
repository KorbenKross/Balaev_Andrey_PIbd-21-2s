﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using VirtualStorePlace.ConnectingModel;
using VirtualStorePlace.LogicInterface;
using VirtualStorePlace.RealiseInterface;
using VirtualStorePlace.UserViewModel;

namespace VirtualStoreView
{
    public partial class FormIngredient : Form
    {

        public int Id { set { id = value; } }

        private int? id;

        private List<IngredientElementUserViewModel> ingredientElements;


        public FormIngredient()
        {
            InitializeComponent();
        }

        private void FormIngredient_Load_1(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var response = APIClient.GetRequest("api/Ingredient/Get/" + id.Value);
                    if (response.Result.IsSuccessStatusCode)
                    {
                        var product = APIClient.GetElement<IngredientUserViewModel>(response);
                        textBoxName.Text = product.IngredientName;
                        textBoxPrice.Text = product.Price.ToString();
                        ingredientElements = product.IngredientElement;
                        LoadData();
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
            var form = new FormIngredientElement();
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
                var form = new FormIngredientElement();
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
                List<IngredientElementConnectingModel> productComponentBM = new List<IngredientElementConnectingModel>();
                for (int i = 0; i < ingredientElements.Count; ++i)
                {
                    productComponentBM.Add(new IngredientElementConnectingModel
                    {
                        Id = ingredientElements[i].Id,
                        IngredientId = ingredientElements[i].IngredientId,
                        ElementId = ingredientElements[i].ElementId,
                        Count = ingredientElements[i].Count
                    });
                }
                Task<HttpResponseMessage> response;
                if (id.HasValue)
                {
                    response = APIClient.PostRequest("api/Ingredient/UpdElement", new IngredientConnectingModel
                    {
                        Id = id.Value,
                        IngredientName = textBoxName.Text,
                        Value = Convert.ToInt32(textBoxPrice.Text),
                        IngredientElement = productComponentBM
                    });
                }
                else
                {
                    response = APIClient.PostRequest("api/Ingredient/AddElement", new IngredientConnectingModel
                    {
                        IngredientName = textBoxName.Text,
                        Value = Convert.ToInt32(textBoxPrice.Text),
                        IngredientElement = productComponentBM
                    });
                }
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
