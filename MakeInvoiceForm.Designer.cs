namespace MobileApplication
{
    partial class MakeInvoiceForm
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
            this.AllTabs = new System.Windows.Forms.TabControl();
            this.MakeInvoice = new System.Windows.Forms.TabPage();
            this.KeywordsTab = new System.Windows.Forms.TabPage();
            this.CustomersTab = new System.Windows.Forms.TabPage();
            this.LogsTab = new System.Windows.Forms.TabPage();
            this.AllTabs.SuspendLayout();
            this.SuspendLayout();
            // 
            // AllTabs
            // 
            this.AllTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AllTabs.Controls.Add(this.MakeInvoice);
            this.AllTabs.Controls.Add(this.KeywordsTab);
            this.AllTabs.Controls.Add(this.CustomersTab);
            this.AllTabs.Controls.Add(this.LogsTab);
            this.AllTabs.Location = new System.Drawing.Point(0, 0);
            this.AllTabs.Name = "AllTabs";
            this.AllTabs.Padding = new System.Drawing.Point(0, 0);
            this.AllTabs.SelectedIndex = 0;
            this.AllTabs.Size = new System.Drawing.Size(807, 456);
            this.AllTabs.TabIndex = 0;
            // 
            // MakeInvoice
            // 
            this.MakeInvoice.Location = new System.Drawing.Point(4, 22);
            this.MakeInvoice.Name = "MakeInvoice";
            this.MakeInvoice.Padding = new System.Windows.Forms.Padding(3);
            this.MakeInvoice.Size = new System.Drawing.Size(799, 430);
            this.MakeInvoice.TabIndex = 0;
            this.MakeInvoice.Text = "Make invoice";
            this.MakeInvoice.UseVisualStyleBackColor = true;
            // 
            // KeywordsTab
            // 
            this.KeywordsTab.Location = new System.Drawing.Point(4, 22);
            this.KeywordsTab.Name = "KeywordsTab";
            this.KeywordsTab.Padding = new System.Windows.Forms.Padding(3);
            this.KeywordsTab.Size = new System.Drawing.Size(799, 430);
            this.KeywordsTab.TabIndex = 1;
            this.KeywordsTab.Text = "Keywords";
            this.KeywordsTab.UseVisualStyleBackColor = true;
            // 
            // CustomersTab
            // 
            this.CustomersTab.Location = new System.Drawing.Point(4, 22);
            this.CustomersTab.Name = "CustomersTab";
            this.CustomersTab.Size = new System.Drawing.Size(799, 430);
            this.CustomersTab.TabIndex = 2;
            this.CustomersTab.Text = "Customers.";
            this.CustomersTab.UseVisualStyleBackColor = true;
            // 
            // LogsTab
            // 
            this.LogsTab.Location = new System.Drawing.Point(4, 22);
            this.LogsTab.Name = "LogsTab";
            this.LogsTab.Size = new System.Drawing.Size(799, 430);
            this.LogsTab.TabIndex = 3;
            this.LogsTab.Text = "Logs";
            this.LogsTab.UseVisualStyleBackColor = true;
            // 
            // MakeInvoiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.AllTabs);
            this.Name = "MakeInvoiceForm";
            this.Text = "MakeInvoice";
            this.AllTabs.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl AllTabs;
        private System.Windows.Forms.TabPage MakeInvoice;
        private System.Windows.Forms.TabPage KeywordsTab;
        private System.Windows.Forms.TabPage CustomersTab;
        private System.Windows.Forms.TabPage LogsTab;
    }
}

