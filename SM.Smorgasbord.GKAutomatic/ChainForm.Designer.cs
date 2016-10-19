namespace SM. Smorgasbord. GKAutomatic
{
    partial class ChainForm
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
            this. label5 = new System. Windows. Forms. Label();
            this. textBoxName = new System. Windows. Forms. TextBox();
            this. button1 = new System. Windows. Forms. Button();
            this. buttonAddChain = new System. Windows. Forms. Button();
            this. dataGridViewConnections = new System. Windows. Forms. DataGridView();
            this. Connections = new System. Windows. Forms. DataGridViewComboBoxColumn();
            this. buttonCancel = new System. Windows. Forms. Button();
            this. buttonSave = new System. Windows. Forms. Button();
            this. treeViewChains = new System. Windows. Forms. TreeView();
            this. label1 = new System. Windows. Forms. Label();
            this. comboBoxSourceServer = new System. Windows. Forms. ComboBox();
            this. groupBoxTargets = new System. Windows. Forms. GroupBox();
            ((System. ComponentModel. ISupportInitialize)(this. dataGridViewConnections)). BeginInit();
            this. groupBoxTargets. SuspendLayout();
            this. SuspendLayout();
            // 
            // label5
            // 
            this. label5. AutoSize = true;
            this. label5. Location = new System. Drawing. Point(218, 19);
            this. label5. Name = "label5";
            this. label5. Size = new System. Drawing. Size(38, 13);
            this. label5. TabIndex = 34;
            this. label5. Text = "Name:";
            // 
            // textBoxName
            // 
            this. textBoxName. Location = new System. Drawing. Point(270, 12);
            this. textBoxName. Name = "textBoxName";
            this. textBoxName. Size = new System. Drawing. Size(145, 20);
            this. textBoxName. TabIndex = 33;
            // 
            // button1
            // 
            this. button1. Location = new System. Drawing. Point(73, 341);
            this. button1. Name = "button1";
            this. button1. Size = new System. Drawing. Size(76, 24);
            this. button1. TabIndex = 32;
            this. button1. Text = "Remove";
            this. button1. UseVisualStyleBackColor = true;
            this. button1. Click += new System. EventHandler(this. DoButtonRemoveClick);
            // 
            // buttonAddChain
            // 
            this. buttonAddChain. Location = new System. Drawing. Point(3, 341);
            this. buttonAddChain. Name = "buttonAddChain";
            this. buttonAddChain. Size = new System. Drawing. Size(64, 24);
            this. buttonAddChain. TabIndex = 31;
            this. buttonAddChain. Text = "Add";
            this. buttonAddChain. UseVisualStyleBackColor = true;
            this. buttonAddChain. Click += new System. EventHandler(this. ButtonAddChainClick);
            // 
            // dataGridViewConnections
            // 
            this. dataGridViewConnections. ColumnHeadersHeightSizeMode = System. Windows. Forms. DataGridViewColumnHeadersHeightSizeMode. AutoSize;
            this. dataGridViewConnections. Columns. AddRange(new System. Windows. Forms. DataGridViewColumn[] {
            this.Connections});
            this. dataGridViewConnections. Location = new System. Drawing. Point(12, 19);
            this. dataGridViewConnections. Name = "dataGridViewConnections";
            this. dataGridViewConnections. Size = new System. Drawing. Size(208, 239);
            this. dataGridViewConnections. TabIndex = 30;
            // 
            // Connections
            // 
            this. Connections. DataPropertyName = "Connections";
            this. Connections. HeaderText = "Connections";
            this. Connections. Items. AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this. Connections. Name = "Connections";
            // 
            // buttonCancel
            // 
            this. buttonCancel. Location = new System. Drawing. Point(363, 361);
            this. buttonCancel. Name = "buttonCancel";
            this. buttonCancel. Size = new System. Drawing. Size(73, 36);
            this. buttonCancel. TabIndex = 29;
            this. buttonCancel. Text = "Cclose";
            this. buttonCancel. UseVisualStyleBackColor = true;
            this. buttonCancel. Click += new System. EventHandler(this. DoButtonCloseClick);
            // 
            // buttonSave
            // 
            this. buttonSave. Location = new System. Drawing. Point(221, 361);
            this. buttonSave. Name = "buttonSave";
            this. buttonSave. Size = new System. Drawing. Size(73, 36);
            this. buttonSave. TabIndex = 28;
            this. buttonSave. Text = "Save";
            this. buttonSave. UseVisualStyleBackColor = true;
            this. buttonSave. Click += new System. EventHandler(this. DoButtonSaveClick);
            // 
            // treeViewChains
            // 
            this. treeViewChains. Location = new System. Drawing. Point(3, 12);
            this. treeViewChains. Name = "treeViewChains";
            this. treeViewChains. Size = new System. Drawing. Size(194, 323);
            this. treeViewChains. TabIndex = 27;
            this. treeViewChains. AfterSelect += new System. Windows. Forms. TreeViewEventHandler(this. TreeViewChainsAfterSelect);
            // 
            // label1
            // 
            this. label1. AutoSize = true;
            this. label1. Location = new System. Drawing. Point(218, 50);
            this. label1. Name = "label1";
            this. label1. Size = new System. Drawing. Size(44, 13);
            this. label1. TabIndex = 35;
            this. label1. Text = "Source:";
            // 
            // comboBoxSourceServer
            // 
            this. comboBoxSourceServer. FormattingEnabled = true;
            this. comboBoxSourceServer. Location = new System. Drawing. Point(270, 50);
            this. comboBoxSourceServer. Name = "comboBoxSourceServer";
            this. comboBoxSourceServer. Size = new System. Drawing. Size(145, 21);
            this. comboBoxSourceServer. TabIndex = 36;
            // 
            // groupBoxTargets
            // 
            this. groupBoxTargets. Controls. Add(this. dataGridViewConnections);
            this. groupBoxTargets. Location = new System. Drawing. Point(218, 77);
            this. groupBoxTargets. Name = "groupBoxTargets";
            this. groupBoxTargets. Size = new System. Drawing. Size(226, 278);
            this. groupBoxTargets. TabIndex = 37;
            this. groupBoxTargets. TabStop = false;
            this. groupBoxTargets. Text = "Targets";
            // 
            // ChainForm
            // 
            this. AutoScaleDimensions = new System. Drawing. SizeF(6F, 13F);
            this. AutoScaleMode = System. Windows. Forms. AutoScaleMode. Font;
            this. ClientSize = new System. Drawing. Size(448, 421);
            this. Controls. Add(this. groupBoxTargets);
            this. Controls. Add(this. comboBoxSourceServer);
            this. Controls. Add(this. label1);
            this. Controls. Add(this. label5);
            this. Controls. Add(this. textBoxName);
            this. Controls. Add(this. button1);
            this. Controls. Add(this. buttonAddChain);
            this. Controls. Add(this. buttonCancel);
            this. Controls. Add(this. buttonSave);
            this. Controls. Add(this. treeViewChains);
            this. Name = "ChainForm";
            this. Text = "Chain Config";
            ((System. ComponentModel. ISupportInitialize)(this. dataGridViewConnections)). EndInit();
            this. groupBoxTargets. ResumeLayout(false);
            this. ResumeLayout(false);
            this. PerformLayout();

        }

        #endregion

        private System. Windows. Forms. Label label5;
        private System. Windows. Forms. TextBox textBoxName;
        private System. Windows. Forms. Button button1;
        private System. Windows. Forms. Button buttonAddChain;
        private System. Windows. Forms. DataGridView dataGridViewConnections;
        private System. Windows. Forms. DataGridViewComboBoxColumn Connections;
        private System. Windows. Forms. Button buttonCancel;
        private System. Windows. Forms. Button buttonSave;
        private System. Windows. Forms. TreeView treeViewChains;
        private System. Windows. Forms. Label label1;
        private System. Windows. Forms. ComboBox comboBoxSourceServer;
        private System. Windows. Forms. GroupBox groupBoxTargets;

    }
}