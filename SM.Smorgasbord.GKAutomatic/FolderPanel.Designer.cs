namespace SM. Smorgasbord. GKAutomatic
{
    partial class FolderPanel
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
            this. label1 = new System. Windows. Forms. Label();
            this. comboBoxChain = new System. Windows. Forms. ComboBox();
            this. buttonRefresh = new System. Windows. Forms. Button();
            this. buttonSave = new System. Windows. Forms. Button();
            this. label2 = new System. Windows. Forms. Label();
            this. textBoxSourceServer = new System. Windows. Forms. TextBox();
            this. label3 = new System. Windows. Forms. Label();
            this. listBoxTargetServers = new System. Windows. Forms. ListBox();
            this. SuspendLayout();
            // 
            // label1
            // 
            this. label1. AutoSize = true;
            this. label1. Location = new System. Drawing. Point(50, 46);
            this. label1. Name = "label1";
            this. label1. Size = new System. Drawing. Size(104, 13);
            this. label1. TabIndex = 0;
            this. label1. Text = "Default Move Chain:";
            // 
            // comboBoxChain
            // 
            this. comboBoxChain. FormattingEnabled = true;
            this. comboBoxChain. Location = new System. Drawing. Point(160, 42);
            this. comboBoxChain. Name = "comboBoxChain";
            this. comboBoxChain. Size = new System. Drawing. Size(250, 21);
            this. comboBoxChain. TabIndex = 1;
            this. comboBoxChain. SelectedIndexChanged += new System. EventHandler(this. DoComboBoxChainSelectedIndexChanged);
            // 
            // buttonRefresh
            // 
            this. buttonRefresh. Location = new System. Drawing. Point(416, 38);
            this. buttonRefresh. Name = "buttonRefresh";
            this. buttonRefresh. Size = new System. Drawing. Size(52, 23);
            this. buttonRefresh. TabIndex = 2;
            this. buttonRefresh. Text = "Refresh";
            this. buttonRefresh. UseVisualStyleBackColor = true;
            this. buttonRefresh. Click += new System. EventHandler(this. DoButtonRefresh);
            // 
            // buttonSave
            // 
            this. buttonSave. Location = new System. Drawing. Point(490, 38);
            this. buttonSave. Name = "buttonSave";
            this. buttonSave. Size = new System. Drawing. Size(75, 23);
            this. buttonSave. TabIndex = 3;
            this. buttonSave. Text = "Save";
            this. buttonSave. UseVisualStyleBackColor = true;
            this. buttonSave. Click += new System. EventHandler(this. DoButtonSaveClick);
            // 
            // label2
            // 
            this. label2. AutoSize = true;
            this. label2. Location = new System. Drawing. Point(69, 100);
            this. label2. Name = "label2";
            this. label2. Size = new System. Drawing. Size(78, 13);
            this. label2. TabIndex = 4;
            this. label2. Text = "Source Server:";
            // 
            // textBoxSourceServer
            // 
            this. textBoxSourceServer. Location = new System. Drawing. Point(160, 97);
            this. textBoxSourceServer. Name = "textBoxSourceServer";
            this. textBoxSourceServer. Size = new System. Drawing. Size(250, 20);
            this. textBoxSourceServer. TabIndex = 5;
            // 
            // label3
            // 
            this. label3. AutoSize = true;
            this. label3. Location = new System. Drawing. Point(69, 156);
            this. label3. Name = "label3";
            this. label3. Size = new System. Drawing. Size(80, 13);
            this. label3. TabIndex = 6;
            this. label3. Text = "Target Servers:";
            // 
            // listBoxTargetServers
            // 
            this. listBoxTargetServers. FormattingEnabled = true;
            this. listBoxTargetServers. Location = new System. Drawing. Point(160, 156);
            this. listBoxTargetServers. Name = "listBoxTargetServers";
            this. listBoxTargetServers. Size = new System. Drawing. Size(250, 121);
            this. listBoxTargetServers. TabIndex = 7;
            // 
            // FolderPanel
            // 
            this. AutoScaleDimensions = new System. Drawing. SizeF(6F, 13F);
            this. AutoScaleMode = System. Windows. Forms. AutoScaleMode. Font;
            this. Controls. Add(this. listBoxTargetServers);
            this. Controls. Add(this. label3);
            this. Controls. Add(this. textBoxSourceServer);
            this. Controls. Add(this. label2);
            this. Controls. Add(this. buttonSave);
            this. Controls. Add(this. buttonRefresh);
            this. Controls. Add(this. comboBoxChain);
            this. Controls. Add(this. label1);
            this. Name = "FolderPanel";
            this. Size = new System. Drawing. Size(590, 424);
            this. ResumeLayout(false);
            this. PerformLayout();

        }

        #endregion

        private System. Windows. Forms. Label label1;
        private System. Windows. Forms. ComboBox comboBoxChain;
        private System. Windows. Forms. Button buttonRefresh;
        private System. Windows. Forms. Button buttonSave;
        private System. Windows. Forms. Label label2;
        private System. Windows. Forms. TextBox textBoxSourceServer;
        private System. Windows. Forms. Label label3;
        private System. Windows. Forms. ListBox listBoxTargetServers;
    }
}
