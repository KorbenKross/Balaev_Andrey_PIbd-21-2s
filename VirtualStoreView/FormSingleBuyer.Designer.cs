namespace VirtualStoreView
{
    partial class FormSingleBuyer
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxFIO = new System.Windows.Forms.TextBox();
            this.saveBtn = new System.Windows.Forms.Button();
            this.cancelBrn = new System.Windows.Forms.Button();
            this.textBoxMail = new System.Windows.Forms.TextBox();
            this.labelMail = new System.Windows.Forms.Label();
            this.groupBoxMails = new System.Windows.Forms.GroupBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.groupBoxMails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "ФИО:";
            // 
            // textBoxFIO
            // 
            this.textBoxFIO.Location = new System.Drawing.Point(64, 16);
            this.textBoxFIO.Name = "textBoxFIO";
            this.textBoxFIO.Size = new System.Drawing.Size(250, 22);
            this.textBoxFIO.TabIndex = 1;
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(392, 285);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(100, 33);
            this.saveBtn.TabIndex = 5;
            this.saveBtn.Text = "Сохранить";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // cancelBrn
            // 
            this.cancelBrn.Location = new System.Drawing.Point(473, 285);
            this.cancelBrn.Name = "cancelBrn";
            this.cancelBrn.Size = new System.Drawing.Size(100, 33);
            this.cancelBrn.TabIndex = 6;
            this.cancelBrn.Text = "Отмена";
            this.cancelBrn.UseVisualStyleBackColor = true;
            this.cancelBrn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // textBoxMail
            // 
            this.textBoxMail.Location = new System.Drawing.Point(331, 6);
            this.textBoxMail.Name = "textBoxMail";
            this.textBoxMail.Size = new System.Drawing.Size(217, 22);
            this.textBoxMail.TabIndex = 3;
            // 
            // labelMail
            // 
            this.labelMail.AutoSize = true;
            this.labelMail.Location = new System.Drawing.Point(288, 9);
            this.labelMail.Name = "labelMail";
            this.labelMail.Size = new System.Drawing.Size(53, 17);
            this.labelMail.TabIndex = 2;
            this.labelMail.Text = "Почта:";
            // 
            // groupBoxMails
            // 
            this.groupBoxMails.Controls.Add(this.dataGridView);
            this.groupBoxMails.Location = new System.Drawing.Point(12, 32);
            this.groupBoxMails.Name = "groupBoxMails";
            this.groupBoxMails.Size = new System.Drawing.Size(546, 250);
            this.groupBoxMails.TabIndex = 4;
            this.groupBoxMails.TabStop = false;
            this.groupBoxMails.Text = "Письма";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(3, 18);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.Size = new System.Drawing.Size(540, 229);
            this.dataGridView.TabIndex = 0;
            // 
            // FormSingleBuyer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 337);
            this.Controls.Add(this.groupBoxMails);
            this.Controls.Add(this.textBoxMail);
            this.Controls.Add(this.labelMail);
            this.Controls.Add(this.cancelBrn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.textBoxFIO);
            this.Controls.Add(this.label1);
            this.Name = "FormSingleBuyer";
            this.Text = "Покупатель";
            this.Load += new System.EventHandler(this.FormSingleBuyer_Load);
            this.groupBoxMails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxFIO;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button cancelBrn;
        private System.Windows.Forms.TextBox textBoxMail;
        private System.Windows.Forms.Label labelMail;
        private System.Windows.Forms.GroupBox groupBoxMails;
        private System.Windows.Forms.DataGridView dataGridView;
    }
}