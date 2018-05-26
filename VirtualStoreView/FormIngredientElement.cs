﻿using System;
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
    public partial class FormIngredientElement : Form
    {
        public IngredientElementUserViewModel Model { set { model = value; } get { return model; } }

        private IngredientElementUserViewModel model;

        public FormIngredientElement()
        {
            InitializeComponent();
        }

        private void FormIngredientElement_Load(object sender, EventArgs e)
        {
            
        }

        private void FormIngredientElement_Load_1(object sender, EventArgs e)
        {
            try
            {
                comboBoxComponent.DisplayMember = "IngredientName";
                comboBoxComponent.ValueMember = "Id";
                comboBoxComponent.DataSource = Task.Run(() => APIClient.GetRequestData<List<ElementUserViewModel>>("api/Element/GetList")).Result;
                comboBoxComponent.SelectedItem = null;
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (model != null)
            {
                comboBoxComponent.Enabled = false;
                comboBoxComponent.SelectedValue = model.ElementId;
                textBoxCount.Text = model.Count.ToString();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void comboBoxComponent_SelectedIndexChanged(object sender, EventArgs e)
        {

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
            try
            {
                if (model == null)
                {
                    model = new IngredientElementUserViewModel
                    {
                        ElementId = Convert.ToInt32(comboBoxComponent.SelectedValue),
                        ElementName = comboBoxComponent.Text,
                        Count = Convert.ToInt32(textBoxCount.Text)
                    };
                }
                else
                {
                    model.Count = Convert.ToInt32(textBoxCount.Text);
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
    }
}
