namespace MobileApplication
{
    partial class UsersPage
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
            this.Id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CustName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Phone = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Rights = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.NameBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PhoneBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.PassBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.InvoceCheckBox = new System.Windows.Forms.CheckBox();
            this.DeviceCheckBox = new System.Windows.Forms.CheckBox();
            this.PriceCheckBox = new System.Windows.Forms.CheckBox();
            this.CustomersCheckBox = new System.Windows.Forms.CheckBox();
            this.LogsCheckBox = new System.Windows.Forms.CheckBox();
            this.Add = new System.Windows.Forms.Button();
            this.delBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Id,
            this.CustName,
            this.Phone,
            this.Rights});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(800, 270);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.ListView1_SelectedIndexChanged);
            this.listView1.SizeChanged += new System.EventHandler(this.ListView1_SizeChanged);
            // 
            // Id
            // 
            this.Id.Text = "Id";
            this.Id.Width = 46;
            // 
            // CustName
            // 
            this.CustName.Text = "Name";
            this.CustName.Width = 88;
            // 
            // Phone
            // 
            this.Phone.Text = "Phone";
            this.Phone.Width = 118;
            // 
            // Rights
            // 
            this.Rights.Text = "Rights";
            this.Rights.Width = 166;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 285);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name";
            // 
            // NameBox
            // 
            this.NameBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.NameBox.Location = new System.Drawing.Point(53, 281);
            this.NameBox.Name = "NameBox";
            this.NameBox.Size = new System.Drawing.Size(100, 20);
            this.NameBox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(159, 285);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Phone";
            // 
            // PhoneBox
            // 
            this.PhoneBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.PhoneBox.Location = new System.Drawing.Point(202, 281);
            this.PhoneBox.Name = "PhoneBox";
            this.PhoneBox.Size = new System.Drawing.Size(100, 20);
            this.PhoneBox.TabIndex = 4;
            
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(309, 285);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Password";
            // 
            // PassBox
            // 
            this.PassBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.PassBox.Location = new System.Drawing.Point(368, 281);
            this.PassBox.Name = "PassBox";
            this.PassBox.Size = new System.Drawing.Size(100, 20);
            this.PassBox.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 305);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Rights:";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 322);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 13);
            this.label5.TabIndex = 8;
            // 
            // InvoceCheckBox
            // 
            this.InvoceCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.InvoceCheckBox.AutoSize = true;
            this.InvoceCheckBox.Location = new System.Drawing.Point(18, 322);
            this.InvoceCheckBox.Name = "InvoceCheckBox";
            this.InvoceCheckBox.Size = new System.Drawing.Size(135, 17);
            this.InvoceCheckBox.TabIndex = 9;
            this.InvoceCheckBox.Text = "Allow to make &Invoices";
            this.InvoceCheckBox.UseVisualStyleBackColor = true;
            // 
            // DeviceCheckBox
            // 
            this.DeviceCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DeviceCheckBox.AutoSize = true;
            this.DeviceCheckBox.Location = new System.Drawing.Point(18, 347);
            this.DeviceCheckBox.Name = "DeviceCheckBox";
            this.DeviceCheckBox.Size = new System.Drawing.Size(210, 17);
            this.DeviceCheckBox.TabIndex = 10;
            this.DeviceCheckBox.Text = "Allow to add new &Devices/functionality";
            this.DeviceCheckBox.UseVisualStyleBackColor = true;
            // 
            // PriceCheckBox
            // 
            this.PriceCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.PriceCheckBox.AutoSize = true;
            this.PriceCheckBox.Location = new System.Drawing.Point(18, 372);
            this.PriceCheckBox.Name = "PriceCheckBox";
            this.PriceCheckBox.Size = new System.Drawing.Size(147, 17);
            this.PriceCheckBox.TabIndex = 11;
            this.PriceCheckBox.Text = "Allow to change the &Price";
            this.PriceCheckBox.UseVisualStyleBackColor = true;
            // 
            // CustomersCheckBox
            // 
            this.CustomersCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CustomersCheckBox.AutoSize = true;
            this.CustomersCheckBox.Location = new System.Drawing.Point(18, 397);
            this.CustomersCheckBox.Name = "CustomersCheckBox";
            this.CustomersCheckBox.Size = new System.Drawing.Size(179, 17);
            this.CustomersCheckBox.TabIndex = 12;
            this.CustomersCheckBox.Text = "Allow to &Customers page access";
            this.CustomersCheckBox.UseVisualStyleBackColor = true;
            // 
            // LogsCheckBox
            // 
            this.LogsCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LogsCheckBox.AutoSize = true;
            this.LogsCheckBox.Location = new System.Drawing.Point(18, 422);
            this.LogsCheckBox.Name = "LogsCheckBox";
            this.LogsCheckBox.Size = new System.Drawing.Size(112, 17);
            this.LogsCheckBox.TabIndex = 13;
            this.LogsCheckBox.Text = "allow to read &Logs";
            this.LogsCheckBox.UseVisualStyleBackColor = true;
            // 
            // Add
            // 
            this.Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Add.Location = new System.Drawing.Point(202, 422);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(75, 23);
            this.Add.TabIndex = 14;
            this.Add.Text = "Add";
            this.Add.UseVisualStyleBackColor = true;
            this.Add.Click += new System.EventHandler(this.Button1_Click);
            // 
            // delBtn
            // 
            this.delBtn.Location = new System.Drawing.Point(287, 422);
            this.delBtn.Name = "delBtn";
            this.delBtn.Size = new System.Drawing.Size(75, 23);
            this.delBtn.TabIndex = 15;
            this.delBtn.Text = "Delete";
            this.delBtn.UseVisualStyleBackColor = true;
            this.delBtn.Click += new System.EventHandler(this.DelBtn_Click);
            // 
            // UsersPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.delBtn);
            this.Controls.Add(this.Add);
            this.Controls.Add(this.LogsCheckBox);
            this.Controls.Add(this.CustomersCheckBox);
            this.Controls.Add(this.PriceCheckBox);
            this.Controls.Add(this.DeviceCheckBox);
            this.Controls.Add(this.InvoceCheckBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.PassBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.PhoneBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.NameBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listView1);
            this.Name = "UsersPage";
            this.Text = "UsersPage";
            this.SizeChanged += new System.EventHandler(this.ListView1_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox NameBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox PhoneBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox PassBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox InvoceCheckBox;
        private System.Windows.Forms.CheckBox DeviceCheckBox;
        private System.Windows.Forms.CheckBox PriceCheckBox;
        private System.Windows.Forms.CheckBox CustomersCheckBox;
        private System.Windows.Forms.CheckBox LogsCheckBox;
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.ColumnHeader CustName;
        private System.Windows.Forms.ColumnHeader Phone;
        private System.Windows.Forms.ColumnHeader Rights;
        private System.Windows.Forms.ColumnHeader Id;
        private System.Windows.Forms.Button delBtn;
    }
}