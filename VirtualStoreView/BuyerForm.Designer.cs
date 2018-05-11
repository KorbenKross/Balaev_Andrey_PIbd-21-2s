namespace VirtualStoreView
{
    partial class BuyerForm
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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.updateBtn = new System.Windows.Forms.Button();
            this.addBtn = new System.Windows.Forms.Button();
            this.changeBtn = new System.Windows.Forms.Button();
            this.deleteBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(-3, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.Size = new System.Drawing.Size(447, 563);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellContentClick);
            // 
            // updateBtn
            // 
            this.updateBtn.Location = new System.Drawing.Point(483, 394);
            this.updateBtn.Name = "updateBtn";
            this.updateBtn.Size = new System.Drawing.Size(95, 30);
            this.updateBtn.TabIndex = 1;
            this.updateBtn.Text = "Обновить";
            this.updateBtn.UseVisualStyleBackColor = true;
            this.updateBtn.Click += new System.EventHandler(this.updateBtn_Click);
            // 
            // addBtn
            // 
            this.addBtn.Location = new System.Drawing.Point(482, 57);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(96, 31);
            this.addBtn.TabIndex = 2;
            this.addBtn.Text = "Добавить";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // changeBtn
            // 
            this.changeBtn.Location = new System.Drawing.Point(482, 161);
            this.changeBtn.Name = "changeBtn";
            this.changeBtn.Size = new System.Drawing.Size(96, 31);
            this.changeBtn.TabIndex = 3;
            this.changeBtn.Text = "Изменить";
            this.changeBtn.UseVisualStyleBackColor = true;
            this.changeBtn.Click += new System.EventHandler(this.changeBtn_Click);
            // 
            // deleteBtn
            // 
            this.deleteBtn.Location = new System.Drawing.Point(482, 277);
            this.deleteBtn.Name = "deleteBtn";
            this.deleteBtn.Size = new System.Drawing.Size(96, 31);
            this.deleteBtn.TabIndex = 4;
            this.deleteBtn.Text = "Удалить";
            this.deleteBtn.UseVisualStyleBackColor = true;
            this.deleteBtn.Click += new System.EventHandler(this.deleteBtn_Click);
            // 
            // BuyerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 564);
            this.Controls.Add(this.deleteBtn);
            this.Controls.Add(this.changeBtn);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.updateBtn);
            this.Controls.Add(this.dataGridView);
            this.Name = "BuyerForm";
            this.Text = "Покупатели";
            this.Load += new System.EventHandler(this.BuyerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button updateBtn;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.Button changeBtn;
        private System.Windows.Forms.Button deleteBtn;
    }
}