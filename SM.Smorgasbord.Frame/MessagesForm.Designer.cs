namespace SM.Smorgasbord.Frame
{
    partial class MessagesForm
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
            this.components = new System.ComponentModel.Container();
            this.listViewMessage = new System.Windows.Forms.ListView();
            this.imageListMessages = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // listViewMessage
            // 
            this.listViewMessage.Location = new System.Drawing.Point(-2, 12);
            this.listViewMessage.Name = "listViewMessage";
            this.listViewMessage.Size = new System.Drawing.Size(786, 106);
            this.listViewMessage.TabIndex = 0;
            this.listViewMessage.UseCompatibleStateImageBehavior = false;
            this.listViewMessage.SizeChanged += new System.EventHandler(this.listViewMessage_SizeChanged);
            // 
            // imageListMessages
            // 
            this.imageListMessages.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageListMessages.ImageSize = new System.Drawing.Size(16, 16);
            this.imageListMessages.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // MessagesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1215, 335);
            this.Controls.Add(this.listViewMessage);
            this.Name = "MessagesForm";
            this.Text = "MessagesForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewMessage;
        private System.Windows.Forms.ImageList imageListMessages;

    }
}