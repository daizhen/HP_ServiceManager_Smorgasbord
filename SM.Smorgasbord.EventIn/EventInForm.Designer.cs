namespace SM.Smorgasbord.EventIn
{
    partial class EventInForm
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
            this.textBoxFileName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxQueryString = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxEventMapName = new System.Windows.Forms.TextBox();
            this.dataGridViewDetail = new System.Windows.Forms.DataGridView();
            this.FieldName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FieldValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Key = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comboBoxServer = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonGetFieldValues = new System.Windows.Forms.Button();
            this.textBoxResultString = new System.Windows.Forms.TextBox();
            this.buttonGenerateString = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxFileName
            // 
            this.textBoxFileName.Location = new System.Drawing.Point(103, 47);
            this.textBoxFileName.Name = "textBoxFileName";
            this.textBoxFileName.Size = new System.Drawing.Size(280, 20);
            this.textBoxFileName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "File Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "QueryString";
            // 
            // textBoxQueryString
            // 
            this.textBoxQueryString.Location = new System.Drawing.Point(103, 73);
            this.textBoxQueryString.Name = "textBoxQueryString";
            this.textBoxQueryString.Size = new System.Drawing.Size(280, 20);
            this.textBoxQueryString.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "EventMap Name";
            // 
            // textBoxEventMapName
            // 
            this.textBoxEventMapName.Location = new System.Drawing.Point(103, 99);
            this.textBoxEventMapName.Name = "textBoxEventMapName";
            this.textBoxEventMapName.Size = new System.Drawing.Size(280, 20);
            this.textBoxEventMapName.TabIndex = 4;
            // 
            // dataGridViewDetail
            // 
            this.dataGridViewDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FieldName,
            this.FieldValue,
            this.Key});
            this.dataGridViewDetail.Location = new System.Drawing.Point(14, 135);
            this.dataGridViewDetail.Name = "dataGridViewDetail";
            this.dataGridViewDetail.Size = new System.Drawing.Size(443, 440);
            this.dataGridViewDetail.TabIndex = 6;
            // 
            // FieldName
            // 
            this.FieldName.DataPropertyName = "FieldName";
            this.FieldName.HeaderText = "Field Name";
            this.FieldName.Name = "FieldName";
            this.FieldName.Width = 200;
            // 
            // FieldValue
            // 
            this.FieldValue.DataPropertyName = "FieldValue";
            this.FieldValue.HeaderText = "FieldValue";
            this.FieldValue.Name = "FieldValue";
            this.FieldValue.Width = 200;
            // 
            // Key
            // 
            this.Key.DataPropertyName = "Key";
            this.Key.HeaderText = "Key";
            this.Key.Name = "Key";
            this.Key.Visible = false;
            // 
            // comboBoxServer
            // 
            this.comboBoxServer.FormattingEnabled = true;
            this.comboBoxServer.Location = new System.Drawing.Point(103, 11);
            this.comboBoxServer.Name = "comboBoxServer";
            this.comboBoxServer.Size = new System.Drawing.Size(280, 21);
            this.comboBoxServer.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Server:";
            // 
            // buttonGetFieldValues
            // 
            this.buttonGetFieldValues.Location = new System.Drawing.Point(417, 9);
            this.buttonGetFieldValues.Name = "buttonGetFieldValues";
            this.buttonGetFieldValues.Size = new System.Drawing.Size(112, 23);
            this.buttonGetFieldValues.TabIndex = 9;
            this.buttonGetFieldValues.Text = "GetFieldValues";
            this.buttonGetFieldValues.UseVisualStyleBackColor = true;
            this.buttonGetFieldValues.Click += new System.EventHandler(this.buttonGetFieldValues_Click);
            // 
            // textBoxResultString
            // 
            this.textBoxResultString.Location = new System.Drawing.Point(487, 135);
            this.textBoxResultString.Multiline = true;
            this.textBoxResultString.Name = "textBoxResultString";
            this.textBoxResultString.Size = new System.Drawing.Size(226, 440);
            this.textBoxResultString.TabIndex = 10;
            // 
            // buttonGenerateString
            // 
            this.buttonGenerateString.Location = new System.Drawing.Point(417, 50);
            this.buttonGenerateString.Name = "buttonGenerateString";
            this.buttonGenerateString.Size = new System.Drawing.Size(112, 23);
            this.buttonGenerateString.TabIndex = 11;
            this.buttonGenerateString.Text = "Generate string";
            this.buttonGenerateString.UseVisualStyleBackColor = true;
            this.buttonGenerateString.Click += new System.EventHandler(this.buttonGenerateString_Click);
            // 
            // EventInForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 605);
            this.Controls.Add(this.buttonGenerateString);
            this.Controls.Add(this.textBoxResultString);
            this.Controls.Add(this.buttonGetFieldValues);
            this.Controls.Add(this.comboBoxServer);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dataGridViewDetail);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxEventMapName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxQueryString);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxFileName);
            this.Name = "EventInForm";
            this.Text = "EventInForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDetail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxFileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxQueryString;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxEventMapName;
        private System.Windows.Forms.DataGridView dataGridViewDetail;
        private System.Windows.Forms.ComboBox comboBoxServer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonGetFieldValues;
        private System.Windows.Forms.TextBox textBoxResultString;
        private System.Windows.Forms.Button buttonGenerateString;
        private System.Windows.Forms.DataGridViewTextBoxColumn FieldName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FieldValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn Key;
    }
}