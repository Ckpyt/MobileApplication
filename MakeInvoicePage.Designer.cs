namespace MobileApplication
{
    partial class MakeInvoicePage
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.Device = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Desription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Count = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.UnitPrice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Total = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelDevice = new System.Windows.Forms.Label();
            this.deviceBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.descriptionBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.qtyBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.priceBox = new System.Windows.Forms.TextBox();
            this.addButton = new System.Windows.Forms.Button();
            this.saveInvoiceButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Device,
            this.Desription,
            this.Count,
            this.UnitPrice,
            this.Total});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.Location = new System.Drawing.Point(0, 101);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(800, 350);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.ListView1_SelectedIndexChanged);
            // 
            // Device
            // 
            this.Device.Text = "Device";
            this.Device.Width = 112;
            // 
            // Desription
            // 
            this.Desription.Text = "Description";
            this.Desription.Width = 333;
            // 
            // Count
            // 
            this.Count.Text = "Count";
            this.Count.Width = 53;
            // 
            // UnitPrice
            // 
            this.UnitPrice.Text = "Unit Price";
            // 
            // Total
            // 
            this.Total.Text = "Total";
            this.Total.Width = 175;
            // 
            // labelDevice
            // 
            this.labelDevice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDevice.AutoSize = true;
            this.labelDevice.Location = new System.Drawing.Point(53, 16);
            this.labelDevice.Name = "labelDevice";
            this.labelDevice.Size = new System.Drawing.Size(41, 13);
            this.labelDevice.TabIndex = 1;
            this.labelDevice.Text = "Device";
            // 
            // deviceBox
            // 
            this.deviceBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deviceBox.FormattingEnabled = true;
            this.deviceBox.Location = new System.Drawing.Point(100, 12);
            this.deviceBox.Name = "deviceBox";
            this.deviceBox.Size = new System.Drawing.Size(121, 21);
            this.deviceBox.TabIndex = 2;
            this.deviceBox.SelectedIndexChanged += new System.EventHandler(this.DeviceBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(227, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Description";
            // 
            // descriptionBox
            // 
            this.descriptionBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionBox.FormattingEnabled = true;
            this.descriptionBox.Location = new System.Drawing.Point(294, 12);
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.Size = new System.Drawing.Size(121, 21);
            this.descriptionBox.TabIndex = 4;
            this.descriptionBox.SelectedIndexChanged += new System.EventHandler(this.DeviceBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(422, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Count";
            // 
            // qtyBox
            // 
            this.qtyBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.qtyBox.Location = new System.Drawing.Point(457, 12);
            this.qtyBox.MaxLength = 10;
            this.qtyBox.Name = "qtyBox";
            this.qtyBox.Size = new System.Drawing.Size(43, 20);
            this.qtyBox.TabIndex = 6;
            this.qtyBox.TextChanged += new System.EventHandler(this.OnlyDecimalChecker);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(507, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Unit Price";
            // 
            // priceBox
            // 
            this.priceBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.priceBox.Location = new System.Drawing.Point(567, 12);
            this.priceBox.Name = "priceBox";
            this.priceBox.Size = new System.Drawing.Size(100, 20);
            this.priceBox.TabIndex = 8;
            this.priceBox.TextChanged += new System.EventHandler(this.OnlyFloatChecker);
            // 
            // addButton
            // 
            this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addButton.Location = new System.Drawing.Point(708, 12);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(80, 23);
            this.addButton.TabIndex = 9;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.Add_Click);
            // 
            // saveInvoiceButton
            // 
            this.saveInvoiceButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.saveInvoiceButton.Location = new System.Drawing.Point(708, 42);
            this.saveInvoiceButton.Name = "saveInvoiceButton";
            this.saveInvoiceButton.Size = new System.Drawing.Size(80, 23);
            this.saveInvoiceButton.TabIndex = 10;
            this.saveInvoiceButton.Text = "Save invoice";
            this.saveInvoiceButton.UseVisualStyleBackColor = true;
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteButton.Location = new System.Drawing.Point(592, 42);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 11;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // MakeInvoicePage
            // 
#if DEBUG
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
#endif
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(800, 451);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.saveInvoiceButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.priceBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.qtyBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.descriptionBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.deviceBox);
            this.Controls.Add(this.labelDevice);
            this.Controls.Add(this.listView1);
            this.Name = "MakeInvoicePage";
            this.Text = "Invoices";
            this.Resize += new System.EventHandler(this.MakeInvoicePage_ResizeEnd);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

#endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader Device;
        private System.Windows.Forms.ColumnHeader Desription;
        private System.Windows.Forms.ColumnHeader Count;
        private System.Windows.Forms.ColumnHeader UnitPrice;
        private System.Windows.Forms.ColumnHeader Total;
        private System.Windows.Forms.Label labelDevice;
        private System.Windows.Forms.ComboBox deviceBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox descriptionBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox qtyBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox priceBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button saveInvoiceButton;
        private System.Windows.Forms.Button deleteButton;
    }
}