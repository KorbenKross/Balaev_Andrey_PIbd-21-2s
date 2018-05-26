namespace VirtualStoreView
{
    partial class VirtualStoreForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.справочникToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.покупателиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.продукцияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.продукцияToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.продуктовыйСкладToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.персоналToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.пополнитьСкладToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.справочникToolStripMenuItem,
            this.пополнитьСкладToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(561, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // справочникToolStripMenuItem
            // 
            this.справочникToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.покупателиToolStripMenuItem,
            this.продукцияToolStripMenuItem,
            this.продукцияToolStripMenuItem1,
            this.продуктовыйСкладToolStripMenuItem,
            this.персоналToolStripMenuItem});
            this.справочникToolStripMenuItem.Name = "справочникToolStripMenuItem";
            this.справочникToolStripMenuItem.Size = new System.Drawing.Size(106, 24);
            this.справочникToolStripMenuItem.Text = "Справочник";
            // 
            // покупателиToolStripMenuItem
            // 
            this.покупателиToolStripMenuItem.Name = "покупателиToolStripMenuItem";
            this.покупателиToolStripMenuItem.Size = new System.Drawing.Size(220, 26);
            this.покупателиToolStripMenuItem.Text = "Покупатели";
            // 
            // продукцияToolStripMenuItem
            // 
            this.продукцияToolStripMenuItem.Name = "продукцияToolStripMenuItem";
            this.продукцияToolStripMenuItem.Size = new System.Drawing.Size(220, 26);
            this.продукцияToolStripMenuItem.Text = "Элементы";
            this.продукцияToolStripMenuItem.Click += new System.EventHandler(this.продукцияToolStripMenuItem_Click);
            // 
            // продукцияToolStripMenuItem1
            // 
            this.продукцияToolStripMenuItem1.Name = "продукцияToolStripMenuItem1";
            this.продукцияToolStripMenuItem1.Size = new System.Drawing.Size(220, 26);
            this.продукцияToolStripMenuItem1.Text = "Продукция";
            // 
            // продуктовыйСкладToolStripMenuItem
            // 
            this.продуктовыйСкладToolStripMenuItem.Name = "продуктовыйСкладToolStripMenuItem";
            this.продуктовыйСкладToolStripMenuItem.Size = new System.Drawing.Size(220, 26);
            this.продуктовыйСкладToolStripMenuItem.Text = "Продуктовый склад";
            // 
            // персоналToolStripMenuItem
            // 
            this.персоналToolStripMenuItem.Name = "персоналToolStripMenuItem";
            this.персоналToolStripMenuItem.Size = new System.Drawing.Size(220, 26);
            this.персоналToolStripMenuItem.Text = "Персонал";
            // 
            // пополнитьСкладToolStripMenuItem
            // 
            this.пополнитьСкладToolStripMenuItem.Name = "пополнитьСкладToolStripMenuItem";
            this.пополнитьСкладToolStripMenuItem.Size = new System.Drawing.Size(141, 24);
            this.пополнитьСкладToolStripMenuItem.Text = "Пополнить склад";
            // 
            // VirtualStoreForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 402);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "VirtualStoreForm";
            this.Text = "VirtualStoreForm";
            this.Load += new System.EventHandler(this.VirtualStoreForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem справочникToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem покупателиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem продукцияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem продукцияToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem продуктовыйСкладToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem персоналToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem пополнитьСкладToolStripMenuItem;
    }
}