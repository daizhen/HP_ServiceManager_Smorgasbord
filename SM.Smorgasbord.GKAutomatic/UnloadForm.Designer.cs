namespace SM.Smorgasbord.GKAutomatic
{
    partial class UnloadForm
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
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxUnload = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxType = new System.Windows.Forms.GroupBox();
            this.radioButtonPD = new System.Windows.Forms.RadioButton();
            this.radioButtonCR = new System.Windows.Forms.RadioButton();
            this.radioButtonQCR = new System.Windows.Forms.RadioButton();
            this.textBoxNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listViewMail = new System.Windows.Forms.ListView();
            this.groupBoxType.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(460, 234);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(83, 31);
            this.buttonClose.TabIndex = 7;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.DoButtonCloseClick);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(356, 234);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(83, 31);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.DoButtonSaveClick);
            // 
            // textBoxUnload
            // 
            this.textBoxUnload.Location = new System.Drawing.Point(81, 8);
            this.textBoxUnload.Name = "textBoxUnload";
            this.textBoxUnload.Size = new System.Drawing.Size(413, 20);
            this.textBoxUnload.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Unload Name:";
            // 
            // groupBoxType
            // 
            this.groupBoxType.Controls.Add(this.radioButtonPD);
            this.groupBoxType.Controls.Add(this.radioButtonCR);
            this.groupBoxType.Controls.Add(this.radioButtonQCR);
            this.groupBoxType.Location = new System.Drawing.Point(7, 34);
            this.groupBoxType.Name = "groupBoxType";
            this.groupBoxType.Size = new System.Drawing.Size(173, 45);
            this.groupBoxType.TabIndex = 8;
            this.groupBoxType.TabStop = false;
            this.groupBoxType.Text = "Type";
            // 
            // radioButtonPD
            // 
            this.radioButtonPD.AutoSize = true;
            this.radioButtonPD.Location = new System.Drawing.Point(120, 19);
            this.radioButtonPD.Name = "radioButtonPD";
            this.radioButtonPD.Size = new System.Drawing.Size(40, 17);
            this.radioButtonPD.TabIndex = 2;
            this.radioButtonPD.TabStop = true;
            this.radioButtonPD.Text = "PD";
            this.radioButtonPD.UseVisualStyleBackColor = true;
            // 
            // radioButtonCR
            // 
            this.radioButtonCR.AutoSize = true;
            this.radioButtonCR.Location = new System.Drawing.Point(74, 19);
            this.radioButtonCR.Name = "radioButtonCR";
            this.radioButtonCR.Size = new System.Drawing.Size(40, 17);
            this.radioButtonCR.TabIndex = 1;
            this.radioButtonCR.Text = "CR";
            this.radioButtonCR.UseVisualStyleBackColor = true;
            // 
            // radioButtonQCR
            // 
            this.radioButtonQCR.AutoSize = true;
            this.radioButtonQCR.Location = new System.Drawing.Point(17, 19);
            this.radioButtonQCR.Name = "radioButtonQCR";
            this.radioButtonQCR.Size = new System.Drawing.Size(48, 17);
            this.radioButtonQCR.TabIndex = 0;
            this.radioButtonQCR.TabStop = true;
            this.radioButtonQCR.Text = "QCR";
            this.radioButtonQCR.UseVisualStyleBackColor = true;
            // 
            // textBoxNumber
            // 
            this.textBoxNumber.Location = new System.Drawing.Point(276, 52);
            this.textBoxNumber.Name = "textBoxNumber";
            this.textBoxNumber.Size = new System.Drawing.Size(131, 20);
            this.textBoxNumber.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(200, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Number:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listViewMail);
            this.groupBox1.Location = new System.Drawing.Point(12, 85);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(483, 100);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Drag mail from outlook";
            // 
            // listViewMail
            // 
            this.listViewMail.Location = new System.Drawing.Point(6, 19);
            this.listViewMail.Name = "listViewMail";
            this.listViewMail.Size = new System.Drawing.Size(471, 75);
            this.listViewMail.TabIndex = 0;
            this.listViewMail.UseCompatibleStateImageBehavior = false;
            this.listViewMail.View = System.Windows.Forms.View.Details;
            // 
            // UnloadForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 277);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxNumber);
            this.Controls.Add(this.groupBoxType);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxUnload);
            this.Controls.Add(this.label1);
            this.Name = "UnloadForm";
            this.Text = "UnloadForm";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.UnloadForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.UnloadForm_DragEnter);
            this.groupBoxType.ResumeLayout(false);
            this.groupBoxType.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxUnload;
        private System.Windows.Forms.Label label1;
        private System. Windows. Forms. GroupBox groupBoxType;
        private System. Windows. Forms. RadioButton radioButtonPD;
        private System. Windows. Forms. RadioButton radioButtonCR;
        private System. Windows. Forms. RadioButton radioButtonQCR;
        private System. Windows. Forms. TextBox textBoxNumber;
        private System. Windows. Forms. Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView listViewMail;
    }
}