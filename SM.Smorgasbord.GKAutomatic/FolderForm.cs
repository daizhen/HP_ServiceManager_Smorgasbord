using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SM.Smorgasbord.GKAutomatic
{
    public partial class FolderForm : Form
    {
         private TreeNode parentNode;

         public FolderForm(TreeNode node)
        {
            parentNode = node;
            InitializeComponent();
        }

        private void DoButtonSaveClick(object sender, EventArgs e)
        {
            PackageInfo package = parentNode.Tag as PackageInfo;

            if (package != null)
            {
                PackageInfo releaseDumbPackage = new PackageInfo();
                releaseDumbPackage.IsEntity = false;
                releaseDumbPackage.Name = textBoxRelease.Text.Trim();
                releaseDumbPackage.ChainName = package.ChainName;
                //releaseDumbPackage.Parent = package;
                package.Children.Add(releaseDumbPackage);

                MessageBox.Show("Success!");
            }
            else
            {
                MessageBox.Show("Error!");
            }

            this.Close();
        }

        private void DoButtonCloseClick(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
