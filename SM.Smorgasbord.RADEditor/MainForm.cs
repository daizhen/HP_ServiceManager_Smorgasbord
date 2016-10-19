using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SM.Smorgasbord.RADParser;
using SM.Smorgasbord.Communication.Lib;
using Aga.Controls.Tree;

namespace SM.Smorgasbord.RADEditor
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Test();
        }

        private void TestTree()
        {
            treeViewAdv1.Model = new TreeModel();
        }

        private void Test()
        {
            string path = @"C:\Users\daizhen\Desktop\Process.txt";
            FileStream stream = new FileStream(path,FileMode.Open);
            StreamReader reader = new StreamReader(stream);
            string rawStr = reader.ReadToEnd();
            stream.Dispose();
            RADLexer lexer = new RADParser.RADLexer(rawStr);
            lexer.Build();
            richTextBox1.Text = lexer.ToString();
        }

        private RadParser GetParser()
        {
            string path = @"C:\Users\daizhen\Desktop\Process.txt";
            FileStream stream = new FileStream(path, FileMode.Open);
            StreamReader reader = new StreamReader(stream);
            string rawStr = reader.ReadToEnd();
            stream.Dispose();
            RADLexer lexer = new RADParser.RADLexer(rawStr);
            lexer.Build();
            RadParser parser = new RadParser(lexer.Tokens);
            return parser;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            RADLexer lexer = new RADParser.RADLexer(richTextBox1.Text);
            lexer.Build();
            RADParser.RadParser parser = new RadParser(lexer.Tokens);
            string formatedStr = parser.ParseStatements().ToString(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string path = @"jsonProcess.txt";
            FileStream stream = new FileStream(path, FileMode.Open);
            StreamReader reader = new StreamReader(stream);
            string rawStr = reader.ReadToEnd();
            stream.Dispose();

            FileColumnInfo columnInfo = RunCode.JsonToObject<FileColumnInfo>(rawStr);

            SMStructure structure = GetParser().ParseSMStructure(columnInfo);

            //richTextBox1.Text = structure.ToString(0);
            ShowStructure(structure, "Process", treeView1);
        }


        private void ShowStructure(SMStructure structure, string fileName, TreeView treeView)
        {
            TreeNode root = new TreeNode();
            root.Text = fileName;

            treeView.Nodes.Add(root);
            ShowStructure(structure, root);
        }

        private void ShowStructure(SMStructure structure,TreeNode parentNode)
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
            currentNode.Text = array.FieldName + "\t\t\t{Array} "+array.Items.Count;
            parentNode.Nodes.Add(currentNode);

            foreach (SMFieldItem subitem in array.Items)
            {
                ShowItem(subitem, currentNode, array.FieldName);
            }
        }

        private void ShowItem(SMFieldItem item, TreeNode parentNode,string fieldName)
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
