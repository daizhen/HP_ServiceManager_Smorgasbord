namespace SM. Smorgasbord. GKAutomatic
{
    partial class PackagePanel
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
            this.dataGridViewBackup = new System.Windows.Forms.DataGridView();
            this.ServerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BackupFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelUnloadScript = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonMove = new System.Windows.Forms.Button();
            this.buttonRollBack = new System.Windows.Forms.Button();
            this.groupBoxChainInfo = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelType = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelNumber = new System.Windows.Forms.Label();
            this.buttonUpdateQC = new System.Windows.Forms.Button();
            this.tabControlDetail = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.buttonGetQCInfo = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.checkBoxNeedReply = new System.Windows.Forms.CheckBox();
            this.listViewMail = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBackup)).BeginInit();
            this.tabControlDetail.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewBackup
            // 
            this.dataGridViewBackup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBackup.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ServerName,
            this.BackupFile});
            this.dataGridViewBackup.Location = new System.Drawing.Point(27, 124);
            this.dataGridViewBackup.Name = "dataGridViewBackup";
            this.dataGridViewBackup.Size = new System.Drawing.Size(663, 93);
            this.dataGridViewBackup.TabIndex = 21;
            // 
            // ServerName
            // 
            this.ServerName.DataPropertyName = "ServerName";
            this.ServerName.HeaderText = "Server";
            this.ServerName.Name = "ServerName";
            // 
            // BackupFile
            // 
            this.BackupFile.DataPropertyName = "BackupFile";
            this.BackupFile.HeaderText = "BackupFile";
            this.BackupFile.Name = "BackupFile";
            this.BackupFile.Width = 300;
            // 
            // labelUnloadScript
            // 
            this.labelUnloadScript.AutoSize = true;
            this.labelUnloadScript.Location = new System.Drawing.Point(117, 98);
            this.labelUnloadScript.Name = "labelUnloadScript";
            this.labelUnloadScript.Size = new System.Drawing.Size(0, 13);
            this.labelUnloadScript.TabIndex = 20;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "UnloadScript:";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(117, 73);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(0, 13);
            this.labelStatus.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Status";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(117, 44);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(0, 13);
            this.labelName.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Name:";
            // 
            // buttonMove
            // 
            this.buttonMove.Location = new System.Drawing.Point(64, 8);
            this.buttonMove.Name = "buttonMove";
            this.buttonMove.Size = new System.Drawing.Size(75, 23);
            this.buttonMove.TabIndex = 13;
            this.buttonMove.Text = "Move";
            this.buttonMove.UseVisualStyleBackColor = true;
            this.buttonMove.Click += new System.EventHandler(this.DoButtonMoveClick);
            // 
            // buttonRollBack
            // 
            this.buttonRollBack.Location = new System.Drawing.Point(237, 8);
            this.buttonRollBack.Name = "buttonRollBack";
            this.buttonRollBack.Size = new System.Drawing.Size(75, 23);
            this.buttonRollBack.TabIndex = 14;
            this.buttonRollBack.Text = "Roll  Back";
            this.buttonRollBack.UseVisualStyleBackColor = true;
            this.buttonRollBack.Click += new System.EventHandler(this.DoButtonRollBackClick);
            // 
            // groupBoxChainInfo
            // 
            this.groupBoxChainInfo.Location = new System.Drawing.Point(4, 14);
            this.groupBoxChainInfo.Name = "groupBoxChainInfo";
            this.groupBoxChainInfo.Size = new System.Drawing.Size(597, 213);
            this.groupBoxChainInfo.TabIndex = 22;
            this.groupBoxChainInfo.TabStop = false;
            this.groupBoxChainInfo.Text = "Source and Targets";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(175, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Package Type:";
            // 
            // labelType
            // 
            this.labelType.AutoSize = true;
            this.labelType.Location = new System.Drawing.Point(258, 73);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(80, 13);
            this.labelType.TabIndex = 24;
            this.labelType.Text = "Package Type:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(393, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Package Number:";
            // 
            // labelNumber
            // 
            this.labelNumber.AutoSize = true;
            this.labelNumber.Location = new System.Drawing.Point(492, 73);
            this.labelNumber.Name = "labelNumber";
            this.labelNumber.Size = new System.Drawing.Size(80, 13);
            this.labelNumber.TabIndex = 26;
            this.labelNumber.Text = "Package Type:";
            // 
            // buttonUpdateQC
            // 
            this.buttonUpdateQC.Location = new System.Drawing.Point(411, 8);
            this.buttonUpdateQC.Name = "buttonUpdateQC";
            this.buttonUpdateQC.Size = new System.Drawing.Size(75, 23);
            this.buttonUpdateQC.TabIndex = 27;
            this.buttonUpdateQC.Text = "Update QC";
            this.buttonUpdateQC.UseVisualStyleBackColor = true;
            this.buttonUpdateQC.Click += new System.EventHandler(this.DoButtonUpdateQCClick);
            // 
            // tabControlDetail
            // 
            this.tabControlDetail.Controls.Add(this.tabPage1);
            this.tabControlDetail.Controls.Add(this.tabPage2);
            this.tabControlDetail.Controls.Add(this.tabPage3);
            this.tabControlDetail.Location = new System.Drawing.Point(26, 230);
            this.tabControlDetail.Name = "tabControlDetail";
            this.tabControlDetail.SelectedIndex = 0;
            this.tabControlDetail.Size = new System.Drawing.Size(664, 272);
            this.tabControlDetail.TabIndex = 28;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBoxChainInfo);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(656, 246);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Source and Targets";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.buttonGetQCInfo);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(656, 246);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "QC info";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // buttonGetQCInfo
            // 
            this.buttonGetQCInfo.Location = new System.Drawing.Point(478, 3);
            this.buttonGetQCInfo.Name = "buttonGetQCInfo";
            this.buttonGetQCInfo.Size = new System.Drawing.Size(75, 23);
            this.buttonGetQCInfo.TabIndex = 0;
            this.buttonGetQCInfo.Text = "Get QC Info";
            this.buttonGetQCInfo.UseVisualStyleBackColor = true;
            this.buttonGetQCInfo.Click += new System.EventHandler(this.DoButtonGetQCInfoClick);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.checkBoxNeedReply);
            this.tabPage3.Controls.Add(this.listViewMail);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(656, 246);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Mail";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // checkBoxNeedReply
            // 
            this.checkBoxNeedReply.AutoSize = true;
            this.checkBoxNeedReply.Location = new System.Drawing.Point(12, 65);
            this.checkBoxNeedReply.Name = "checkBoxNeedReply";
            this.checkBoxNeedReply.Size = new System.Drawing.Size(136, 17);
            this.checkBoxNeedReply.TabIndex = 30;
            this.checkBoxNeedReply.Text = "Reply Mail After Move?";
            this.checkBoxNeedReply.UseVisualStyleBackColor = true;
            // 
            // listViewMail
            // 
            this.listViewMail.Location = new System.Drawing.Point(6, 6);
            this.listViewMail.Name = "listViewMail";
            this.listViewMail.Size = new System.Drawing.Size(622, 58);
            this.listViewMail.TabIndex = 29;
            this.listViewMail.UseCompatibleStateImageBehavior = false;
            this.listViewMail.View = System.Windows.Forms.View.Details;
            this.listViewMail.DoubleClick += new System.EventHandler(this.listViewMail_DoubleClick);
            // 
            // PackagePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControlDetail);
            this.Controls.Add(this.buttonUpdateQC);
            this.Controls.Add(this.labelNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.labelType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridViewBackup);
            this.Controls.Add(this.labelUnloadScript);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonMove);
            this.Controls.Add(this.buttonRollBack);
            this.Name = "PackagePanel";
            this.Size = new System.Drawing.Size(860, 625);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBackup)).EndInit();
            this.tabControlDetail.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System. Windows. Forms. DataGridView dataGridViewBackup;
        private System. Windows. Forms. DataGridViewTextBoxColumn ServerName;
        private System. Windows. Forms. DataGridViewTextBoxColumn BackupFile;
        private System. Windows. Forms. Label labelUnloadScript;
        private System. Windows. Forms. Label label5;
        private System. Windows. Forms. Label labelStatus;
        private System. Windows. Forms. Label label3;
        private System. Windows. Forms. Label labelName;
        private System. Windows. Forms. Label label1;
        private System. Windows. Forms. Button buttonMove;
        private System. Windows. Forms. Button buttonRollBack;
        private System. Windows. Forms. GroupBox groupBoxChainInfo;
        private System. Windows. Forms. Label label2;
        private System. Windows. Forms. Label labelType;
        private System. Windows. Forms. Label label4;
        private System. Windows. Forms. Label labelNumber;
        private System. Windows. Forms. Button buttonUpdateQC;
        private System. Windows. Forms. TabControl tabControlDetail;
        private System. Windows. Forms. TabPage tabPage1;
        private System. Windows. Forms. TabPage tabPage2;
        private System. Windows. Forms. Button buttonGetQCInfo;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListView listViewMail;
        private System.Windows.Forms.CheckBox checkBoxNeedReply;
    }
}
