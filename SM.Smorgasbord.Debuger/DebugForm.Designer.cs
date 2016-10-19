namespace SM.Smorgasbord.Debuger
{
    partial class DebugForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeViewTraces = new System.Windows.Forms.TreeView();
            this.tabControlDetail = new System.Windows.Forms.TabControl();
            this.tabPageConnection = new System.Windows.Forms.TabPage();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.checkboxAutoStep = new System.Windows.Forms.CheckBox();
            this.buttonContinue = new System.Windows.Forms.Button();
            this.buttonStepOver = new System.Windows.Forms.Button();
            this.buttonNextPanel = new System.Windows.Forms.Button();
            this.buttonSingleStep = new System.Windows.Forms.Button();
            this.buttonSend = new System.Windows.Forms.Button();
            this.buttonStartTrace = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.textBoxDebug = new System.Windows.Forms.TextBox();
            this.textBoxAuthorization = new System.Windows.Forms.TextBox();
            this.textBoxCookieId = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxServer = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonStop = new System.Windows.Forms.Button();
            this.tabPageDebugDetail = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panelRADDetail = new System.Windows.Forms.Panel();
            this.tabPageContext = new System.Windows.Forms.TabPage();
            this.listViewContext = new System.Windows.Forms.ListView();
            this.columnHeaderVarName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderPreValue = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderPostValue = new System.Windows.Forms.ColumnHeader();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControlDetail.SuspendLayout();
            this.tabPageConnection.SuspendLayout();
            this.tabPageDebugDetail.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabPageContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeViewTraces);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControlDetail);
            this.splitContainer1.Size = new System.Drawing.Size(1266, 561);
            this.splitContainer1.SplitterDistance = 212;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeViewTraces
            // 
            this.treeViewTraces.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewTraces.Location = new System.Drawing.Point(0, 0);
            this.treeViewTraces.Name = "treeViewTraces";
            this.treeViewTraces.Size = new System.Drawing.Size(212, 561);
            this.treeViewTraces.TabIndex = 0;
            this.treeViewTraces.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewTraces_NodeMouseClick);
            // 
            // tabControlDetail
            // 
            this.tabControlDetail.Controls.Add(this.tabPageConnection);
            this.tabControlDetail.Controls.Add(this.tabPageDebugDetail);
            this.tabControlDetail.Controls.Add(this.tabPageContext);
            this.tabControlDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlDetail.Location = new System.Drawing.Point(0, 0);
            this.tabControlDetail.Name = "tabControlDetail";
            this.tabControlDetail.SelectedIndex = 0;
            this.tabControlDetail.Size = new System.Drawing.Size(1050, 561);
            this.tabControlDetail.TabIndex = 1;
            // 
            // tabPageConnection
            // 
            this.tabPageConnection.Controls.Add(this.textBoxIP);
            this.tabPageConnection.Controls.Add(this.checkboxAutoStep);
            this.tabPageConnection.Controls.Add(this.buttonContinue);
            this.tabPageConnection.Controls.Add(this.buttonStepOver);
            this.tabPageConnection.Controls.Add(this.buttonNextPanel);
            this.tabPageConnection.Controls.Add(this.buttonSingleStep);
            this.tabPageConnection.Controls.Add(this.buttonSend);
            this.tabPageConnection.Controls.Add(this.buttonStartTrace);
            this.tabPageConnection.Controls.Add(this.button2);
            this.tabPageConnection.Controls.Add(this.label6);
            this.tabPageConnection.Controls.Add(this.textBoxPort);
            this.tabPageConnection.Controls.Add(this.textBoxDebug);
            this.tabPageConnection.Controls.Add(this.textBoxAuthorization);
            this.tabPageConnection.Controls.Add(this.textBoxCookieId);
            this.tabPageConnection.Controls.Add(this.label5);
            this.tabPageConnection.Controls.Add(this.buttonStart);
            this.tabPageConnection.Controls.Add(this.label4);
            this.tabPageConnection.Controls.Add(this.label3);
            this.tabPageConnection.Controls.Add(this.label2);
            this.tabPageConnection.Controls.Add(this.comboBoxServer);
            this.tabPageConnection.Controls.Add(this.label1);
            this.tabPageConnection.Controls.Add(this.buttonStop);
            this.tabPageConnection.Location = new System.Drawing.Point(4, 22);
            this.tabPageConnection.Name = "tabPageConnection";
            this.tabPageConnection.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConnection.Size = new System.Drawing.Size(1042, 535);
            this.tabPageConnection.TabIndex = 0;
            this.tabPageConnection.Text = "Server Info";
            this.tabPageConnection.UseVisualStyleBackColor = true;
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(106, 51);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(411, 20);
            this.textBoxIP.TabIndex = 26;
            // 
            // checkboxAutoStep
            // 
            this.checkboxAutoStep.AutoSize = true;
            this.checkboxAutoStep.Location = new System.Drawing.Point(121, 234);
            this.checkboxAutoStep.Name = "checkboxAutoStep";
            this.checkboxAutoStep.Size = new System.Drawing.Size(106, 17);
            this.checkboxAutoStep.TabIndex = 38;
            this.checkboxAutoStep.Text = "Auto step down?";
            this.checkboxAutoStep.UseVisualStyleBackColor = true;
            // 
            // buttonContinue
            // 
            this.buttonContinue.Location = new System.Drawing.Point(567, 195);
            this.buttonContinue.Name = "buttonContinue";
            this.buttonContinue.Size = new System.Drawing.Size(100, 23);
            this.buttonContinue.TabIndex = 36;
            this.buttonContinue.Text = "Continue";
            this.buttonContinue.UseVisualStyleBackColor = true;
            this.buttonContinue.Click += new System.EventHandler(this.buttonContinue_Click);
            // 
            // buttonStepOver
            // 
            this.buttonStepOver.Location = new System.Drawing.Point(409, 195);
            this.buttonStepOver.Name = "buttonStepOver";
            this.buttonStepOver.Size = new System.Drawing.Size(141, 23);
            this.buttonStepOver.TabIndex = 35;
            this.buttonStepOver.Text = "Step Over This RAD>>";
            this.buttonStepOver.UseVisualStyleBackColor = true;
            this.buttonStepOver.Click += new System.EventHandler(this.buttonStepOver_Click);
            // 
            // buttonNextPanel
            // 
            this.buttonNextPanel.Location = new System.Drawing.Point(291, 195);
            this.buttonNextPanel.Name = "buttonNextPanel";
            this.buttonNextPanel.Size = new System.Drawing.Size(105, 23);
            this.buttonNextPanel.TabIndex = 34;
            this.buttonNextPanel.Text = "Next Panel>";
            this.buttonNextPanel.UseVisualStyleBackColor = true;
            this.buttonNextPanel.Click += new System.EventHandler(this.buttonNextPanel_Click);
            // 
            // buttonSingleStep
            // 
            this.buttonSingleStep.Location = new System.Drawing.Point(121, 195);
            this.buttonSingleStep.Name = "buttonSingleStep";
            this.buttonSingleStep.Size = new System.Drawing.Size(147, 23);
            this.buttonSingleStep.TabIndex = 33;
            this.buttonSingleStep.Text = "Start Single Step Debug";
            this.buttonSingleStep.UseVisualStyleBackColor = true;
            this.buttonSingleStep.Click += new System.EventHandler(this.buttonSingleStep_Click);
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(524, 295);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(75, 23);
            this.buttonSend.TabIndex = 32;
            this.buttonSend.Text = "Send";
            this.buttonSend.UseVisualStyleBackColor = true;
            // 
            // buttonStartTrace
            // 
            this.buttonStartTrace.Location = new System.Drawing.Point(2, 195);
            this.buttonStartTrace.Name = "buttonStartTrace";
            this.buttonStartTrace.Size = new System.Drawing.Size(96, 23);
            this.buttonStartTrace.TabIndex = 31;
            this.buttonStartTrace.Text = "New Trace";
            this.buttonStartTrace.UseVisualStyleBackColor = true;
            this.buttonStartTrace.Click += new System.EventHandler(this.buttonStartTrace_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(294, 17);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(103, 26);
            this.button2.TabIndex = 40;
            this.button2.Text = "Get Connection";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(56, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 13);
            this.label6.TabIndex = 29;
            this.label6.Text = "Port";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(106, 77);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(411, 20);
            this.textBoxPort.TabIndex = 28;
            // 
            // textBoxDebug
            // 
            this.textBoxDebug.Location = new System.Drawing.Point(107, 297);
            this.textBoxDebug.Name = "textBoxDebug";
            this.textBoxDebug.Size = new System.Drawing.Size(411, 20);
            this.textBoxDebug.TabIndex = 23;
            // 
            // textBoxAuthorization
            // 
            this.textBoxAuthorization.Location = new System.Drawing.Point(106, 170);
            this.textBoxAuthorization.Name = "textBoxAuthorization";
            this.textBoxAuthorization.Size = new System.Drawing.Size(411, 20);
            this.textBoxAuthorization.TabIndex = 20;
            // 
            // textBoxCookieId
            // 
            this.textBoxCookieId.Location = new System.Drawing.Point(106, 135);
            this.textBoxCookieId.Name = "textBoxCookieId";
            this.textBoxCookieId.Size = new System.Drawing.Size(411, 20);
            this.textBoxCookieId.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(54, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "IP";
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(6, 20);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 25;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 297);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Debug Code";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 170);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Authorization Code";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Cookie ID";
            // 
            // comboBoxServer
            // 
            this.comboBoxServer.FormattingEnabled = true;
            this.comboBoxServer.Location = new System.Drawing.Point(106, 101);
            this.comboBoxServer.Name = "comboBoxServer";
            this.comboBoxServer.Size = new System.Drawing.Size(274, 21);
            this.comboBoxServer.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Server:";
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(106, 17);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(78, 23);
            this.buttonStop.TabIndex = 37;
            this.buttonStop.Text = "Enable";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // tabPageDebugDetail
            // 
            this.tabPageDebugDetail.Controls.Add(this.splitContainer2);
            this.tabPageDebugDetail.Location = new System.Drawing.Point(4, 22);
            this.tabPageDebugDetail.Name = "tabPageDebugDetail";
            this.tabPageDebugDetail.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDebugDetail.Size = new System.Drawing.Size(1042, 535);
            this.tabPageDebugDetail.TabIndex = 1;
            this.tabPageDebugDetail.Text = "Details";
            this.tabPageDebugDetail.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.panelRADDetail);
            this.splitContainer2.Size = new System.Drawing.Size(1036, 529);
            this.splitContainer2.SplitterDistance = 416;
            this.splitContainer2.TabIndex = 0;
            // 
            // panelRADDetail
            // 
            this.panelRADDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRADDetail.Location = new System.Drawing.Point(0, 0);
            this.panelRADDetail.Name = "panelRADDetail";
            this.panelRADDetail.Size = new System.Drawing.Size(1036, 416);
            this.panelRADDetail.TabIndex = 0;
            // 
            // tabPageContext
            // 
            this.tabPageContext.Controls.Add(this.listViewContext);
            this.tabPageContext.Location = new System.Drawing.Point(4, 22);
            this.tabPageContext.Name = "tabPageContext";
            this.tabPageContext.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageContext.Size = new System.Drawing.Size(1042, 535);
            this.tabPageContext.TabIndex = 2;
            this.tabPageContext.Text = "Run Time Context";
            this.tabPageContext.UseVisualStyleBackColor = true;
            // 
            // listViewContext
            // 
            this.listViewContext.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderVarName,
            this.columnHeaderPreValue,
            this.columnHeaderPostValue});
            this.listViewContext.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewContext.Location = new System.Drawing.Point(3, 3);
            this.listViewContext.Name = "listViewContext";
            this.listViewContext.Size = new System.Drawing.Size(1036, 529);
            this.listViewContext.TabIndex = 0;
            this.listViewContext.UseCompatibleStateImageBehavior = false;
            this.listViewContext.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderVarName
            // 
            this.columnHeaderVarName.Text = "Var Name";
            this.columnHeaderVarName.Width = 174;
            // 
            // columnHeaderPreValue
            // 
            this.columnHeaderPreValue.Text = "Pre value";
            this.columnHeaderPreValue.Width = 329;
            // 
            // columnHeaderPostValue
            // 
            this.columnHeaderPostValue.Text = "Post Value";
            this.columnHeaderPostValue.Width = 519;
            // 
            // DebugForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1266, 561);
            this.Controls.Add(this.splitContainer1);
            this.Name = "DebugForm";
            this.Text = "DebugForm";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tabControlDetail.ResumeLayout(false);
            this.tabPageConnection.ResumeLayout(false);
            this.tabPageConnection.PerformLayout();
            this.tabPageDebugDetail.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.tabPageContext.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeViewTraces;
        private System.Windows.Forms.TabControl tabControlDetail;
        private System.Windows.Forms.TabPage tabPageConnection;
        private System.Windows.Forms.Button buttonStartTrace;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.TextBox textBoxDebug;
        private System.Windows.Forms.TextBox textBoxAuthorization;
        private System.Windows.Forms.TextBox textBoxCookieId;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPageDebugDetail;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel panelRADDetail;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.Button buttonSingleStep;
        private System.Windows.Forms.Button buttonStepOver;
        private System.Windows.Forms.Button buttonNextPanel;
        private System.Windows.Forms.Button buttonContinue;
        private System.Windows.Forms.TabPage tabPageContext;
        private System.Windows.Forms.ListView listViewContext;
        private System.Windows.Forms.ColumnHeader columnHeaderVarName;
        private System.Windows.Forms.ColumnHeader columnHeaderPreValue;
        private System.Windows.Forms.ColumnHeader columnHeaderPostValue;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.CheckBox checkboxAutoStep;
    }
}