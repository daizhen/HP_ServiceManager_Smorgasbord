using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SM.Smorgasbord.RADParser;
using ScintillaNET;

namespace SM.Smorgasbord.Debuger
{
    public partial class RTViewer : UserControl
    {
        public RTContext Context
        {
            get;
            set;
        }

        public string VarName
        {
            get;
            set;
        }
        public RTViewer()
        {
            InitializeComponent();
        }

        public void Display()
        {
            int index = Context.VarNames.IndexOf(VarName);
            this.Controls.Clear();
            if (index >= 0)
            {
                string varValueStr = Context.VarValues[index];
                Scintilla richTextViewer = new Scintilla();
                //richTextViewer.Width = this.Width - 2;
                //richTextViewer.Height = this.Height - 2;
                richTextViewer.Dock = DockStyle.Fill;
                richTextViewer.Text = varValueStr;
                this.Controls.Add(richTextViewer);
            }
        }


        private void ShowStructure(SMStructure structure, string fileName, TreeView treeView)
        {
            TreeNode root = new TreeNode();
            root.Text = fileName;

            treeView.Nodes.Add(root);
            ShowStructure(structure, root);
        }

        private void ShowStructure(SMStructure structure, TreeNode parentNode)
        {
            TreeNode currentNode = new TreeNode();
            currentNode.Tag = structure;
            currentNode.Text = structure.FieldName + "\t\t\t{Structure}";
            parentNode.Nodes.Add(currentNode);
            foreach (KeyValuePair<string, SMFieldItem> subItem in structure.Items)
            {
                ShowItem(subItem.Value, currentNode, subItem.Key);
            }
        }

        private void ShowArray(SMArray array, TreeNode parentNode)
        {
            TreeNode currentNode = new TreeNode();
            currentNode.Tag = array;
            currentNode.Text = array.FieldName + "\t\t\t{Array} " + array.Items.Count;
            parentNode.Nodes.Add(currentNode);

            foreach (SMFieldItem subitem in array.Items)
            {
                ShowItem(subitem, currentNode, array.FieldName);
            }
        }

        private void ShowItem(SMFieldItem item, TreeNode parentNode, string fieldName)
        {
            if (item is SMArray)
            {
                ShowArray(item as SMArray, parentNode);
            }
            else if (item is SMStructure)
            {
                ShowStructure(item as SMStructure, parentNode);
            }
            else
            {
                TreeNode currentNode = new TreeNode();
                currentNode.Tag = item;
                currentNode.Text = fieldName + "\t\t\t\t\t" + item.TextValue;
                parentNode.Nodes.Add(currentNode);
            }
        }
    }
}
