namespace SM. Smorgasbord. PackageGenerator
{
    partial class BugInfoControl
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
            this. textBoxSubState = new System. Windows. Forms. TextBox();
            this. label2 = new System. Windows. Forms. Label();
            this. textBoxSubmitter = new System. Windows. Forms. TextBox();
            this. label3 = new System. Windows. Forms. Label();
            this. textBoxOwner = new System. Windows. Forms. TextBox();
            this. webBrowser1 = new System. Windows. Forms. WebBrowser();
            this. label4 = new System. Windows. Forms. Label();
            this. SuspendLayout();
            // 
            // label1
            // 
            this. label1. AutoSize = true;
            this. label1. Location = new System. Drawing. Point(12, 24);
            this. label1. Name = "label1";
            this. label1. Size = new System. Drawing. Size(54, 13);
            this. label1. TabIndex = 0;
            this. label1. Text = "Sub State";
            // 
            // textBoxSubState
            // 
            this. textBoxSubState. Location = new System. Drawing. Point(93, 21);
            this. textBoxSubState. Name = "textBoxSubState";
            this. textBoxSubState. Size = new System. Drawing. Size(220, 20);
            this. textBoxSubState. TabIndex = 1;
            // 
            // label2
            // 
            this. label2. AutoSize = true;
            this. label2. Location = new System. Drawing. Point(12, 58);
            this. label2. Name = "label2";
            this. label2. Size = new System. Drawing. Size(54, 13);
            this. label2. TabIndex = 2;
            this. label2. Text = "Submitter:";
            // 
            // textBoxSubmitter
            // 
            this. textBoxSubmitter. Location = new System. Drawing. Point(93, 55);
            this. textBoxSubmitter. Name = "textBoxSubmitter";
            this. textBoxSubmitter. Size = new System. Drawing. Size(220, 20);
            this. textBoxSubmitter. TabIndex = 3;
            // 
            // label3
            // 
            this. label3. AutoSize = true;
            this. label3. Location = new System. Drawing. Point(12, 89);
            this. label3. Name = "label3";
            this. label3. Size = new System. Drawing. Size(41, 13);
            this. label3. TabIndex = 4;
            this. label3. Text = "Owner:";
            // 
            // textBoxOwner
            // 
            this. textBoxOwner. Location = new System. Drawing. Point(93, 89);
            this. textBoxOwner. Name = "textBoxOwner";
            this. textBoxOwner. Size = new System. Drawing. Size(220, 20);
            this. textBoxOwner. TabIndex = 5;
            // 
            // webBrowser1
            // 
            this. webBrowser1. Location = new System. Drawing. Point(15, 170);
            this. webBrowser1. MinimumSize = new System. Drawing. Size(20, 20);
            this. webBrowser1. Name = "webBrowser1";
            this. webBrowser1. Size = new System. Drawing. Size(397, 106);
            this. webBrowser1. TabIndex = 6;
            // 
            // label4
            // 
            this. label4. AutoSize = true;
            this. label4. Location = new System. Drawing. Point(12, 135);
            this. label4. Name = "label4";
            this. label4. Size = new System. Drawing. Size(59, 13);
            this. label4. TabIndex = 7;
            this. label4. Text = "Comments:";
            // 
            // BugInfoControl
            // 
            this. AutoScaleDimensions = new System. Drawing. SizeF(6F, 13F);
            this. AutoScaleMode = System. Windows. Forms. AutoScaleMode. Font;
            this. Controls. Add(this. label4);
            this. Controls. Add(this. webBrowser1);
            this. Controls. Add(this. textBoxOwner);
            this. Controls. Add(this. label3);
            this. Controls. Add(this. textBoxSubmitter);
            this. Controls. Add(this. label2);
            this. Controls. Add(this. textBoxSubState);
            this. Controls. Add(this. label1);
            this. Name = "BugInfoControl";
            this. Size = new System. Drawing. Size(456, 291);
            this. ResumeLayout(false);
            this. PerformLayout();

        }

        #endregion

        private System. Windows. Forms. Label label1;
        private System. Windows. Forms. TextBox textBoxSubState;
        private System. Windows. Forms. Label label2;
        private System. Windows. Forms. TextBox textBoxSubmitter;
        private System. Windows. Forms. Label label3;
        private System. Windows. Forms. TextBox textBoxOwner;
        private System. Windows. Forms. WebBrowser webBrowser1;
        private System. Windows. Forms. Label label4;
    }
}
