namespace SM.Smorgasbord.GKAutomatic
{
    partial class MainForm
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
            this. components = new System. ComponentModel. Container();
            this. treeViewPackages = new System. Windows. Forms. TreeView();
            this. contextMenuStripTreeView = new System. Windows. Forms. ContextMenuStrip(this. components);
            this. addPackageToolStripMenuItem = new System. Windows. Forms. ToolStripMenuItem();
            this. addFolderToolStripMenuItem = new System. Windows. Forms. ToolStripMenuItem();
            this. removeToolStripMenuItem = new System. Windows. Forms. ToolStripMenuItem();
            this. splitContainer1 = new System. Windows. Forms. SplitContainer();
            this. contextMenuStripTreeView. SuspendLayout();
            this. splitContainer1. Panel1. SuspendLayout();
            this. splitContainer1. SuspendLayout();
            this. SuspendLayout();
            // 
            // treeViewPackages
            // 
            this. treeViewPackages. ContextMenuStrip = this. contextMenuStripTreeView;
            this. treeViewPackages. Dock = System. Windows. Forms. DockStyle. Fill;
            this. treeViewPackages. Font = new System. Drawing. Font("Microsoft Sans Serif", 12F, System. Drawing. FontStyle. Regular, System. Drawing. GraphicsUnit. Point, ((byte)(0)));
            this. treeViewPackages. Location = new System. Drawing. Point(0, 0);
            this. treeViewPackages. Name = "treeViewPackages";
            this. treeViewPackages. Size = new System. Drawing. Size(207, 521);
            this. treeViewPackages. TabIndex = 1;
            this. treeViewPackages. NodeMouseClick += new System. Windows. Forms. TreeNodeMouseClickEventHandler(this. treeViewPackages_NodeMouseClick);
            // 
            // contextMenuStripTreeView
            // 
            this. contextMenuStripTreeView. Items. AddRange(new System. Windows. Forms. ToolStripItem[] {
            this.addPackageToolStripMenuItem,
            this.addFolderToolStripMenuItem,
            this.removeToolStripMenuItem});
            this. contextMenuStripTreeView. Name = "contextMenuStripTreeView";
            this. contextMenuStripTreeView. Size = new System. Drawing. Size(137, 70);
            // 
            // addPackageToolStripMenuItem
            // 
            this. addPackageToolStripMenuItem. Name = "addPackageToolStripMenuItem";
            this. addPackageToolStripMenuItem. Size = new System. Drawing. Size(136, 22);
            this. addPackageToolStripMenuItem. Text = "Add Package";
            this. addPackageToolStripMenuItem. Click += new System. EventHandler(this. addPackageToolStripMenuItem_Click);
            // 
            // addFolderToolStripMenuItem
            // 
            this. addFolderToolStripMenuItem. Name = "addFolderToolStripMenuItem";
            this. addFolderToolStripMenuItem. Size = new System. Drawing. Size(136, 22);
            this. addFolderToolStripMenuItem. Text = "Add Folder";
            this. addFolderToolStripMenuItem. Click += new System. EventHandler(this. addFolderToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this. removeToolStripMenuItem. Name = "removeToolStripMenuItem";
            this. removeToolStripMenuItem. Size = new System. Drawing. Size(136, 22);
            this. removeToolStripMenuItem. Text = "Remove";
            // 
            // splitContainer1
            // 
            this. splitContainer1. Dock = System. Windows. Forms. DockStyle. Fill;
            this. splitContainer1. Location = new System. Drawing. Point(0, 0);
            this. splitContainer1. Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this. splitContainer1. Panel1. Controls. Add(this. treeViewPackages);
            this. splitContainer1. Size = new System. Drawing. Size(882, 521);
            this. splitContainer1. SplitterDistance = 207;
            this. splitContainer1. TabIndex = 6;
            // 
            // MainForm
            // 
            this. AutoScaleDimensions = new System. Drawing. SizeF(6F, 13F);
            this. AutoScaleMode = System. Windows. Forms. AutoScaleMode. Font;
            this. ClientSize = new System. Drawing. Size(882, 521);
            this. Controls. Add(this. splitContainer1);
            this. Name = "MainForm";
            this. Text = "MainForm";
            this. contextMenuStripTreeView. ResumeLayout(false);
            this. splitContainer1. Panel1. ResumeLayout(false);
            this. splitContainer1. ResumeLayout(false);
            this. ResumeLayout(false);

        }

        #endregion

        private System. Windows. Forms. TreeView treeViewPackages;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTreeView;
        private System.Windows.Forms.ToolStripMenuItem addPackageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System. Windows. Forms. SplitContainer splitContainer1;
    }
}