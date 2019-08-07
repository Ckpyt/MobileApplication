namespace MobileApplication
{
    partial class ObjectsPage
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
            this.phoneNameBox = new System.Windows.Forms.TextBox();
            this.parentPhoneBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.addButton = new System.Windows.Forms.Button();
            this.operationPriceBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.operationNameBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // phoneNameBox
            // 
            this.phoneNameBox.Location = new System.Drawing.Point(143, 33);
            this.phoneNameBox.Name = "phoneNameBox";
            this.phoneNameBox.Size = new System.Drawing.Size(100, 20);
            this.phoneNameBox.TabIndex = 2;
            // 
            // parentPhoneBox
            // 
            this.parentPhoneBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.parentPhoneBox.FormattingEnabled = true;
            this.parentPhoneBox.Location = new System.Drawing.Point(16, 33);
            this.parentPhoneBox.Name = "parentPhoneBox";
            this.parentPhoneBox.Size = new System.Drawing.Size(121, 21);
            this.parentPhoneBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Parent";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(143, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Phone model name";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(435, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Operation name";
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(586, 59);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 5;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // operationPriceBox
            // 
            this.operationPriceBox.Location = new System.Drawing.Point(561, 33);
            this.operationPriceBox.Name = "operationPriceBox";
            this.operationPriceBox.Size = new System.Drawing.Size(100, 20);
            this.operationPriceBox.TabIndex = 4;
            this.operationPriceBox.TextChanged += new System.EventHandler(this.OperationPriceBox_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(561, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Operation cost";
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.treeView1.FullRowSelect = true;
            this.treeView1.HideSelection = false;
            this.treeView1.Location = new System.Drawing.Point(0, 101);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(800, 349);
            this.treeView1.TabIndex = 6;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView1_AfterSelect);
            // 
            // operationNameBox
            // 
            this.operationNameBox.FormattingEnabled = true;
            this.operationNameBox.Location = new System.Drawing.Point(434, 33);
            this.operationNameBox.Name = "operationNameBox";
            this.operationNameBox.Size = new System.Drawing.Size(121, 21);
            this.operationNameBox.TabIndex = 3;
            this.operationNameBox.SelectedIndexChanged += new System.EventHandler(this.OperationNameBox_SelectedIndexChanged);
            // 
            // ObjectsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.operationNameBox);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.operationPriceBox);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.parentPhoneBox);
            this.Controls.Add(this.phoneNameBox);
            this.Name = "ObjectsPage";
            this.Text = "ObjectsPage";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox phoneNameBox;
        private System.Windows.Forms.ComboBox parentPhoneBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.TextBox operationPriceBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ComboBox operationNameBox;
    }
}