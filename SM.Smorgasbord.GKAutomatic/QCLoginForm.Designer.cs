namespace SM. Smorgasbord. GKAutomatic
{
    partial class QCLoginForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.maskedTextBoxPwd = new System.Windows.Forms.MaskedTextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxPrintName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxSaveUserName = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Login Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(47, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password:";
            // 
            // maskedTextBoxPwd
            // 
            this.maskedTextBoxPwd.Location = new System.Drawing.Point(109, 77);
            this.maskedTextBoxPwd.Name = "maskedTextBoxPwd";
            this.maskedTextBoxPwd.PasswordChar = '*';
            this.maskedTextBoxPwd.Size = new System.Drawing.Size(202, 20);
            this.maskedTextBoxPwd.TabIndex = 2;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(109, 35);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(202, 20);
            this.textBoxName.TabIndex = 1;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(110, 185);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.DoButtonOKClick);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(236, 185);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.DoButtonCancelClick);
            // 
            // textBoxPrintName
            // 
            this.textBoxPrintName.Location = new System.Drawing.Point(109, 103);
            this.textBoxPrintName.Name = "textBoxPrintName";
            this.textBoxPrintName.Size = new System.Drawing.Size(202, 20);
            this.textBoxPrintName.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Print Name:";
            // 
            // checkBoxSaveUserName
            // 
            this.checkBoxSaveUserName.AutoSize = true;
            this.checkBoxSaveUserName.Checked = true;
            this.checkBoxSaveUserName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSaveUserName.Location = new System.Drawing.Point(50, 134);
            this.checkBoxSaveUserName.Name = "checkBoxSaveUserName";
            this.checkBoxSaveUserName.Size = new System.Drawing.Size(113, 17);
            this.checkBoxSaveUserName.TabIndex = 7;
            this.checkBoxSaveUserName.Text = "Save User Name?";
            this.checkBoxSaveUserName.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(65, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(246, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Login name in format abc@hp.com or abc_hp.com";
            // 
            // QCLoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 246);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkBoxSaveUserName);
            this.Controls.Add(this.textBoxPrintName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.maskedTextBoxPwd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "QCLoginForm";
            this.Text = "HP Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System. Windows. Forms. Label label1;
        private System. Windows. Forms. Label label2;
        private System. Windows. Forms. MaskedTextBox maskedTextBoxPwd;
        private System. Windows. Forms. TextBox textBoxName;
        private System. Windows. Forms. Button buttonOK;
        private System. Windows. Forms. Button buttonCancel;
        private System. Windows. Forms. TextBox textBoxPrintName;
        private System. Windows. Forms. Label label3;
        private System. Windows. Forms. CheckBox checkBoxSaveUserName;
        private System.Windows.Forms.Label label4;
    }
}