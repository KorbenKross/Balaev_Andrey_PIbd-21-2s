namespace VirtualStoreView
{
    partial class FormTakeFromProductStorage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBoxImplementer = new System.Windows.Forms.ComboBox();
            this.labelImplementer = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBoxImplementer
            // 
            this.comboBoxImplementer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxImplementer.FormattingEnabled = true;
            this.comboBoxImplementer.Location = new System.Drawing.Point(127, 7);
            this.comboBoxImplementer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxImplementer.Name = "comboBoxImplementer";
            this.comboBoxImplementer.Size = new System.Drawing.Size(288, 24);
            this.comboBoxImplementer.TabIndex = 1;
            // 
            // labelImplementer
            // 
            this.labelImplementer.AutoSize = true;
            this.labelImplementer.Location = new System.Drawing.Point(16, 11);
            this.labelImplementer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelImplementer.Name = "labelImplementer";
            this.labelImplementer.Size = new System.Drawing.Size(99, 17);
            this.labelImplementer.TabIndex = 0;
            this.labelImplementer.Text = "Исполнитель:";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(271, 52);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 28);
            this.buttonCancel.TabIndex = 9;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(163, 52);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(100, 28);
            this.buttonSave.TabIndex = 8;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // FormTakeFromProductStorage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 92);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.comboBoxImplementer);
            this.Controls.Add(this.labelImplementer);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormTakeFromProductStorage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Отдать заказ в работу";
            this.Load += new System.EventHandler(this.FormTakeFromProductStorage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBoxImplementer;
        private System.Windows.Forms.Label labelImplementer;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
    }
}