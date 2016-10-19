namespace SM. Smorgasbord. PackageGenerator
{
    partial class PackageForm
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
            this. label1 = new System. Windows. Forms. Label();
            this. comboBoxServer = new System. Windows. Forms. ComboBox();
            this. buttonGenerate = new System. Windows. Forms. Button();
            this. label2 = new System. Windows. Forms. Label();
            this. textBoxFolder = new System. Windows. Forms. TextBox();
            this. label3 = new System. Windows. Forms. Label();
            this. textBoxReleaseManager = new System. Windows. Forms. TextBox();
            this. label4 = new System. Windows. Forms. Label();
            this. textBoxRelease = new System. Windows. Forms. TextBox();
            this. label5 = new System. Windows. Forms. Label();
            this. textBoxQCR = new System. Windows. Forms. TextBox();
            this. label6 = new System. Windows. Forms. Label();
            this. textBoxCR = new System. Windows. Forms. TextBox();
            this. label7 = new System. Windows. Forms. Label();
            this. textBoxAuditName = new System. Windows. Forms. TextBox();
            this. textBoxDescription = new System. Windows. Forms. TextBox();
            this. label8 = new System. Windows. Forms. Label();
            this. buttonBrowse = new System. Windows. Forms. Button();
            this. textBoxMoveInstructionLocation = new System. Windows. Forms. TextBox();
            this. label9 = new System. Windows. Forms. Label();
            this. textBoxUnloadFile = new System. Windows. Forms. TextBox();
            this. label10 = new System. Windows. Forms. Label();
            this. groupBox1 = new System. Windows. Forms. GroupBox();
            this. checkBoxMoveInstruction = new System. Windows. Forms. CheckBox();
            this. checkBoxAuditlog = new System. Windows. Forms. CheckBox();
            this. checkBoxCreateNewUnload = new System. Windows. Forms. CheckBox();
            this. buttonGetPackageInfo = new System. Windows. Forms. Button();
            this. buttonGetDocInfo = new System. Windows. Forms. Button();
            this. textBoxUnloadName = new System. Windows. Forms. TextBox();
            this. label11 = new System. Windows. Forms. Label();
            this. groupBox2 = new System. Windows. Forms. GroupBox();
            this. buttonOpen = new System. Windows. Forms. Button();
            this. groupBox1. SuspendLayout();
            this. groupBox2. SuspendLayout();
            this. SuspendLayout();
            // 
            // label1
            // 
            this. label1. AutoSize = true;
            this. label1. Location = new System. Drawing. Point(22, 14);
            this. label1. Name = "label1";
            this. label1. Size = new System. Drawing. Size(41, 13);
            this. label1. TabIndex = 0;
            this. label1. Text = "Server:";
            // 
            // comboBoxServer
            // 
            this. comboBoxServer. FormattingEnabled = true;
            this. comboBoxServer. Location = new System. Drawing. Point(80, 12);
            this. comboBoxServer. Name = "comboBoxServer";
            this. comboBoxServer. Size = new System. Drawing. Size(303, 21);
            this. comboBoxServer. TabIndex = 1;
            // 
            // buttonGenerate
            // 
            this. buttonGenerate. Location = new System. Drawing. Point(498, 524);
            this. buttonGenerate. Name = "buttonGenerate";
            this. buttonGenerate. Size = new System. Drawing. Size(75, 34);
            this. buttonGenerate. TabIndex = 3;
            this. buttonGenerate. Text = "Create";
            this. buttonGenerate. UseVisualStyleBackColor = true;
            this. buttonGenerate. Click += new System. EventHandler(this. DoButtonGenerateClick);
            // 
            // label2
            // 
            this. label2. AutoSize = true;
            this. label2. Location = new System. Drawing. Point(77, 64);
            this. label2. Name = "label2";
            this. label2. Size = new System. Drawing. Size(39, 13);
            this. label2. TabIndex = 4;
            this. label2. Text = "Folder:";
            // 
            // textBoxFolder
            // 
            this. textBoxFolder. Location = new System. Drawing. Point(128, 56);
            this. textBoxFolder. Name = "textBoxFolder";
            this. textBoxFolder. Size = new System. Drawing. Size(331, 20);
            this. textBoxFolder. TabIndex = 5;
            // 
            // label3
            // 
            this. label3. AutoSize = true;
            this. label3. Location = new System. Drawing. Point(2, 59);
            this. label3. Name = "label3";
            this. label3. Size = new System. Drawing. Size(94, 13);
            this. label3. TabIndex = 6;
            this. label3. Text = "Release Manager:";
            // 
            // textBoxReleaseManager
            // 
            this. textBoxReleaseManager. Location = new System. Drawing. Point(103, 56);
            this. textBoxReleaseManager. Name = "textBoxReleaseManager";
            this. textBoxReleaseManager. Size = new System. Drawing. Size(469, 20);
            this. textBoxReleaseManager. TabIndex = 7;
            this. textBoxReleaseManager. MouseLeave += new System. EventHandler(this. DoTextBoxReleasemanagerMouseLeave);
            this. textBoxReleaseManager. Leave += new System. EventHandler(this. DoTextBoxReleasemanagerFocusLeave);
            // 
            // label4
            // 
            this. label4. AutoSize = true;
            this. label4. Location = new System. Drawing. Point(60, 37);
            this. label4. Name = "label4";
            this. label4. Size = new System. Drawing. Size(18, 13);
            this. label4. TabIndex = 8;
            this. label4. Text = "R:";
            // 
            // textBoxRelease
            // 
            this. textBoxRelease. Location = new System. Drawing. Point(81, 33);
            this. textBoxRelease. Name = "textBoxRelease";
            this. textBoxRelease. Size = new System. Drawing. Size(72, 20);
            this. textBoxRelease. TabIndex = 9;
            // 
            // label5
            // 
            this. label5. AutoSize = true;
            this. label5. Location = new System. Drawing. Point(22, 63);
            this. label5. Name = "label5";
            this. label5. Size = new System. Drawing. Size(53, 13);
            this. label5. TabIndex = 10;
            this. label5. Text = "QCR/PD:";
            // 
            // textBoxQCR
            // 
            this. textBoxQCR. Location = new System. Drawing. Point(81, 59);
            this. textBoxQCR. Name = "textBoxQCR";
            this. textBoxQCR. Size = new System. Drawing. Size(72, 20);
            this. textBoxQCR. TabIndex = 11;
            // 
            // label6
            // 
            this. label6. AutoSize = true;
            this. label6. Location = new System. Drawing. Point(50, 85);
            this. label6. Name = "label6";
            this. label6. Size = new System. Drawing. Size(25, 13);
            this. label6. TabIndex = 12;
            this. label6. Text = "CR:";
            // 
            // textBoxCR
            // 
            this. textBoxCR. Location = new System. Drawing. Point(81, 82);
            this. textBoxCR. Name = "textBoxCR";
            this. textBoxCR. Size = new System. Drawing. Size(72, 20);
            this. textBoxCR. TabIndex = 13;
            // 
            // label7
            // 
            this. label7. AutoSize = true;
            this. label7. Location = new System. Drawing. Point(46, 100);
            this. label7. Name = "label7";
            this. label7. Size = new System. Drawing. Size(70, 13);
            this. label7. TabIndex = 14;
            this. label7. Text = "Audit log File:";
            // 
            // textBoxAuditName
            // 
            this. textBoxAuditName. Location = new System. Drawing. Point(128, 97);
            this. textBoxAuditName. Name = "textBoxAuditName";
            this. textBoxAuditName. Size = new System. Drawing. Size(384, 20);
            this. textBoxAuditName. TabIndex = 15;
            // 
            // textBoxDescription
            // 
            this. textBoxDescription. Location = new System. Drawing. Point(103, 82);
            this. textBoxDescription. Name = "textBoxDescription";
            this. textBoxDescription. Size = new System. Drawing. Size(470, 20);
            this. textBoxDescription. TabIndex = 17;
            this. textBoxDescription. Leave += new System. EventHandler(this. DoTextBoxDescriptionFocusLeave);
            // 
            // label8
            // 
            this. label8. AutoSize = true;
            this. label8. Location = new System. Drawing. Point(4, 85);
            this. label8. Name = "label8";
            this. label8. Size = new System. Drawing. Size(63, 13);
            this. label8. TabIndex = 16;
            this. label8. Text = "Description:";
            // 
            // buttonBrowse
            // 
            this. buttonBrowse. Location = new System. Drawing. Point(458, 55);
            this. buttonBrowse. Name = "buttonBrowse";
            this. buttonBrowse. Size = new System. Drawing. Size(62, 20);
            this. buttonBrowse. TabIndex = 18;
            this. buttonBrowse. Text = "Browse...";
            this. buttonBrowse. UseVisualStyleBackColor = true;
            this. buttonBrowse. Click += new System. EventHandler(this. OnButtonBrowseClick);
            // 
            // textBoxMoveInstructionLocation
            // 
            this. textBoxMoveInstructionLocation. Location = new System. Drawing. Point(128, 128);
            this. textBoxMoveInstructionLocation. Name = "textBoxMoveInstructionLocation";
            this. textBoxMoveInstructionLocation. Size = new System. Drawing. Size(384, 20);
            this. textBoxMoveInstructionLocation. TabIndex = 20;
            // 
            // label9
            // 
            this. label9. AutoSize = true;
            this. label9. Location = new System. Drawing. Point(8, 128);
            this. label9. Name = "label9";
            this. label9. Size = new System. Drawing. Size(108, 13);
            this. label9. TabIndex = 19;
            this. label9. Text = "Move Instruction File:";
            // 
            // textBoxUnloadFile
            // 
            this. textBoxUnloadFile. Location = new System. Drawing. Point(128, 159);
            this. textBoxUnloadFile. Name = "textBoxUnloadFile";
            this. textBoxUnloadFile. Size = new System. Drawing. Size(384, 20);
            this. textBoxUnloadFile. TabIndex = 22;
            // 
            // label10
            // 
            this. label10. AutoSize = true;
            this. label10. Location = new System. Drawing. Point(53, 162);
            this. label10. Name = "label10";
            this. label10. Size = new System. Drawing. Size(63, 13);
            this. label10. TabIndex = 21;
            this. label10. Text = "Unload File:";
            // 
            // groupBox1
            // 
            this. groupBox1. Controls. Add(this. checkBoxCreateNewUnload);
            this. groupBox1. Controls. Add(this. textBoxUnloadName);
            this. groupBox1. Controls. Add(this. label11);
            this. groupBox1. Controls. Add(this. buttonGetPackageInfo);
            this. groupBox1. Controls. Add(this. textBoxCR);
            this. groupBox1. Controls. Add(this. label4);
            this. groupBox1. Controls. Add(this. textBoxRelease);
            this. groupBox1. Controls. Add(this. label5);
            this. groupBox1. Controls. Add(this. textBoxQCR);
            this. groupBox1. Controls. Add(this. label6);
            this. groupBox1. Location = new System. Drawing. Point(5, 108);
            this. groupBox1. Name = "groupBox1";
            this. groupBox1. Size = new System. Drawing. Size(705, 160);
            this. groupBox1. TabIndex = 23;
            this. groupBox1. TabStop = false;
            this. groupBox1. Text = "Package Info";
            // 
            // checkBoxMoveInstruction
            // 
            this. checkBoxMoveInstruction. AutoSize = true;
            this. checkBoxMoveInstruction. Location = new System. Drawing. Point(244, 185);
            this. checkBoxMoveInstruction. Name = "checkBoxMoveInstruction";
            this. checkBoxMoveInstruction. Size = new System. Drawing. Size(145, 17);
            this. checkBoxMoveInstruction. TabIndex = 19;
            this. checkBoxMoveInstruction. Text = "Create Move Instruction?";
            this. checkBoxMoveInstruction. UseVisualStyleBackColor = true;
            // 
            // checkBoxAuditlog
            // 
            this. checkBoxAuditlog. AutoSize = true;
            this. checkBoxAuditlog. Location = new System. Drawing. Point(61, 185);
            this. checkBoxAuditlog. Name = "checkBoxAuditlog";
            this. checkBoxAuditlog. Size = new System. Drawing. Size(104, 17);
            this. checkBoxAuditlog. TabIndex = 18;
            this. checkBoxAuditlog. Text = "Create Auditlog?";
            this. checkBoxAuditlog. UseVisualStyleBackColor = true;
            // 
            // checkBoxCreateNewUnload
            // 
            this. checkBoxCreateNewUnload. AutoSize = true;
            this. checkBoxCreateNewUnload. Location = new System. Drawing. Point(12, 140);
            this. checkBoxCreateNewUnload. Name = "checkBoxCreateNewUnload";
            this. checkBoxCreateNewUnload. Size = new System. Drawing. Size(251, 17);
            this. checkBoxCreateNewUnload. TabIndex = 20;
            this. checkBoxCreateNewUnload. Text = "Create Unload Record From Reversion Control?";
            this. checkBoxCreateNewUnload. UseVisualStyleBackColor = true;
            // 
            // buttonGetPackageInfo
            // 
            this. buttonGetPackageInfo. Location = new System. Drawing. Point(445, 22);
            this. buttonGetPackageInfo. Name = "buttonGetPackageInfo";
            this. buttonGetPackageInfo. Size = new System. Drawing. Size(116, 23);
            this. buttonGetPackageInfo. TabIndex = 21;
            this. buttonGetPackageInfo. Text = "Get Package Info";
            this. buttonGetPackageInfo. UseVisualStyleBackColor = true;
            this. buttonGetPackageInfo. Click += new System. EventHandler(this. DoButtonGetPackageInfoClick);
            // 
            // buttonGetDocInfo
            // 
            this. buttonGetDocInfo. Location = new System. Drawing. Point(444, 19);
            this. buttonGetDocInfo. Name = "buttonGetDocInfo";
            this. buttonGetDocInfo. Size = new System. Drawing. Size(116, 23);
            this. buttonGetDocInfo. TabIndex = 22;
            this. buttonGetDocInfo. Text = "Get Document Info";
            this. buttonGetDocInfo. UseVisualStyleBackColor = true;
            this. buttonGetDocInfo. Click += new System. EventHandler(this. DoButtonGetDocInfoClick);
            // 
            // textBoxUnloadName
            // 
            this. textBoxUnloadName. Location = new System. Drawing. Point(80, 114);
            this. textBoxUnloadName. Name = "textBoxUnloadName";
            this. textBoxUnloadName. Size = new System. Drawing. Size(384, 20);
            this. textBoxUnloadName. TabIndex = 27;
            // 
            // label11
            // 
            this. label11. AutoSize = true;
            this. label11. Location = new System. Drawing. Point(1, 114);
            this. label11. Name = "label11";
            this. label11. Size = new System. Drawing. Size(75, 13);
            this. label11. TabIndex = 26;
            this. label11. Text = "Unload Name:";
            // 
            // groupBox2
            // 
            this. groupBox2. Controls. Add(this. buttonOpen);
            this. groupBox2. Controls. Add(this. checkBoxMoveInstruction);
            this. groupBox2. Controls. Add(this. label2);
            this. groupBox2. Controls. Add(this. buttonGetDocInfo);
            this. groupBox2. Controls. Add(this. textBoxFolder);
            this. groupBox2. Controls. Add(this. checkBoxAuditlog);
            this. groupBox2. Controls. Add(this. label7);
            this. groupBox2. Controls. Add(this. textBoxAuditName);
            this. groupBox2. Controls. Add(this. textBoxUnloadFile);
            this. groupBox2. Controls. Add(this. buttonBrowse);
            this. groupBox2. Controls. Add(this. label10);
            this. groupBox2. Controls. Add(this. label9);
            this. groupBox2. Controls. Add(this. textBoxMoveInstructionLocation);
            this. groupBox2. Location = new System. Drawing. Point(6, 283);
            this. groupBox2. Name = "groupBox2";
            this. groupBox2. Size = new System. Drawing. Size(704, 214);
            this. groupBox2. TabIndex = 24;
            this. groupBox2. TabStop = false;
            this. groupBox2. Text = "Document Info";
            // 
            // buttonOpen
            // 
            this. buttonOpen. Location = new System. Drawing. Point(526, 56);
            this. buttonOpen. Name = "buttonOpen";
            this. buttonOpen. Size = new System. Drawing. Size(62, 20);
            this. buttonOpen. TabIndex = 23;
            this. buttonOpen. Text = "Open";
            this. buttonOpen. UseVisualStyleBackColor = true;
            this. buttonOpen. Click += new System. EventHandler(this. DoButtonOpenClick);
            // 
            // PackageForm
            // 
            this. AutoScaleDimensions = new System. Drawing. SizeF(6F, 13F);
            this. AutoScaleMode = System. Windows. Forms. AutoScaleMode. Font;
            this. ClientSize = new System. Drawing. Size(809, 580);
            this. Controls. Add(this. groupBox2);
            this. Controls. Add(this. groupBox1);
            this. Controls. Add(this. buttonGenerate);
            this. Controls. Add(this. comboBoxServer);
            this. Controls. Add(this. label1);
            this. Controls. Add(this. label3);
            this. Controls. Add(this. textBoxDescription);
            this. Controls. Add(this. textBoxReleaseManager);
            this. Controls. Add(this. label8);
            this. Name = "PackageForm";
            this. Text = "Form1";
            this. Load += new System. EventHandler(this. PackageForm_Load);
            this. groupBox1. ResumeLayout(false);
            this. groupBox1. PerformLayout();
            this. groupBox2. ResumeLayout(false);
            this. groupBox2. PerformLayout();
            this. ResumeLayout(false);
            this. PerformLayout();

        }

        #endregion

        private System. Windows. Forms. Label label1;
        private System. Windows. Forms. ComboBox comboBoxServer;
        private System. Windows. Forms. Button buttonGenerate;
        private System. Windows. Forms. Label label2;
        private System. Windows. Forms. TextBox textBoxFolder;
        private System. Windows. Forms. Label label3;
        private System. Windows. Forms. TextBox textBoxReleaseManager;
        private System. Windows. Forms. Label label4;
        private System. Windows. Forms. TextBox textBoxRelease;
        private System. Windows. Forms. Label label5;
        private System. Windows. Forms. TextBox textBoxQCR;
        private System. Windows. Forms. Label label6;
        private System. Windows. Forms. TextBox textBoxCR;
        private System. Windows. Forms. Label label7;
        private System. Windows. Forms. TextBox textBoxAuditName;
        private System. Windows. Forms. TextBox textBoxDescription;
        private System. Windows. Forms. Label label8;
        private System. Windows. Forms. Button buttonBrowse;
        private System. Windows. Forms. TextBox textBoxMoveInstructionLocation;
        private System. Windows. Forms. Label label9;
        private System. Windows. Forms. TextBox textBoxUnloadFile;
        private System. Windows. Forms. Label label10;
        private System. Windows. Forms. GroupBox groupBox1;
        private System. Windows. Forms. CheckBox checkBoxMoveInstruction;
        private System. Windows. Forms. CheckBox checkBoxAuditlog;
        private System. Windows. Forms. CheckBox checkBoxCreateNewUnload;
        private System. Windows. Forms. Button buttonGetPackageInfo;
        private System. Windows. Forms. Button buttonGetDocInfo;
        private System. Windows. Forms. TextBox textBoxUnloadName;
        private System. Windows. Forms. Label label11;
        private System. Windows. Forms. GroupBox groupBox2;
        private System. Windows. Forms. Button buttonOpen;
    }
}

