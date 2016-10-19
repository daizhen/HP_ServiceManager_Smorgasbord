namespace SM. Smorgasbord. GKAutomatic
{
    partial class PDInfoControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System. ComponentModel. IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components. Dispose();
            }
            base. Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label4 = new System.Windows.Forms.Label();
            this.webBrowserInfo = new System.Windows.Forms.WebBrowser();
            this.textBoxOwner = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxStatus = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Comments:";
            // 
            // webBrowserInfo
            // 
            this.webBrowserInfo.Location = new System.Drawing.Point(8, 96);
            this.webBrowserInfo.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserInfo.Name = "webBrowserInfo";
            this.webBrowserInfo.Size = new System.Drawing.Size(529, 106);
            this.webBrowserInfo.TabIndex = 14;
            // 
            // textBoxOwner
            // 
            this.textBoxOwner.Location = new System.Drawing.Point(86, 58);
            this.textBoxOwner.Name = "textBoxOwner";
            this.textBoxOwner.Size = new System.Drawing.Size(220, 20);
            this.textBoxOwner.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Owner:";
            // 
            // textBoxStatus
            // 
            this.textBoxStatus.Location = new System.Drawing.Point(86, 21);
            this.textBoxStatus.Name = "textBoxStatus";
            this.textBoxStatus.Size = new System.Drawing.Size(220, 20);
            this.textBoxStatus.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "PD status:";
            // 
            // PDInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.webBrowserInfo);
            this.Controls.Add(this.textBoxOwner);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxStatus);
            this.Controls.Add(this.label1);
            this.Name = "PDInfoControl";
            this.Size = new System.Drawing.Size(552, 233);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System. Windows. Forms. Label label4;
        private System. Windows. Forms. WebBrowser webBrowserInfo;
        private System. Windows. Forms. TextBox textBoxOwner;
        private System. Windows. Forms. Label label3;
        private System. Windows. Forms. TextBox textBoxStatus;
        private System. Windows. Forms. Label label1;
    }
}
