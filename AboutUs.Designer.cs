namespace MobileApplication
{
    partial class AboutUs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutUs));
            this.Logo2 = new System.Windows.Forms.PictureBox();
            this.CloseButton = new System.Windows.Forms.Button();
            this.AboutText1 = new System.Windows.Forms.Label();
            this.AboutText2 = new System.Windows.Forms.Label();
            this.AboutText3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Logo2)).BeginInit();
            this.SuspendLayout();
            // 
            // Logo2
            // 
            this.Logo2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Logo2.BackgroundImage = global::MobileApplication.Properties.Resources.Techcare_1;
            this.Logo2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Logo2.Location = new System.Drawing.Point(3, 1);
            this.Logo2.Name = "Logo2";
            this.Logo2.Size = new System.Drawing.Size(222, 79);
            this.Logo2.TabIndex = 0;
            this.Logo2.TabStop = false;
            // 
            // CloseButton
            // 
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.CloseButton.Location = new System.Drawing.Point(621, 405);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(151, 33);
            this.CloseButton.TabIndex = 2;
            this.CloseButton.Text = "EXIT";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // AboutText1
            // 
            this.AboutText1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AboutText1.AutoSize = true;
            this.AboutText1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AboutText1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.AboutText1.Location = new System.Drawing.Point(22, 115);
            this.AboutText1.Name = "AboutText1";
            this.AboutText1.Size = new System.Drawing.Size(490, 55);
            this.AboutText1.TabIndex = 3;
            this.AboutText1.Text = "About the Proponents";
            // 
            // AboutText2
            // 
            this.AboutText2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AboutText2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AboutText2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.AboutText2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.AboutText2.Location = new System.Drawing.Point(26, 176);
            this.AboutText2.Name = "AboutText2";
            this.AboutText2.Size = new System.Drawing.Size(714, 73);
            this.AboutText2.TabIndex = 4;
            this.AboutText2.Text = "The proponents are Dimitrii Shibalin and Angelica Primavera. They are currently s" +
    "tudying in Aspire2 International taking Diploma in Computing and Software Develo" +
    "pment as its strand.\r\n";
            this.AboutText2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // AboutText3
            // 
            this.AboutText3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AboutText3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.AboutText3.Location = new System.Drawing.Point(26, 266);
            this.AboutText3.Name = "AboutText3";
            this.AboutText3.Size = new System.Drawing.Size(729, 82);
            this.AboutText3.TabIndex = 4;
            this.AboutText3.Text = resources.GetString("AboutText3.Text");
            // 
            // AboutUs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.AboutText3);
            this.Controls.Add(this.AboutText2);
            this.Controls.Add(this.AboutText1);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.Logo2);
            this.Name = "AboutUs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AboutUs";
            ((System.ComponentModel.ISupportInitialize)(this.Logo2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Logo2;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Label AboutText1;
        private System.Windows.Forms.Label AboutText2;
        private System.Windows.Forms.Label AboutText3;
    }
}