using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SM.Smorgasbord.GKAutomatic.Connections;
using System.Xml.Serialization;
using System.IO;
using SM.Smorgasbord.Communication.Common;
using SM. Smorgasbord. Communication. Sessions;

namespace SM. Smorgasbord. Frame
{
    public partial class ConnectionForm : Form
    {
        private ServerConnections connectionsInfo;
        private TreeNode currentNode = null;

        public ConnectionForm()
        {
            InitializeComponent();

            LoadConnections();
            InitTree();
        }

        #region Events

        private void ButtonAddClick(object sender, EventArgs e)
        {
            ConnectionInfo newConnection = new ConnectionInfo();
            newConnection.Name = "New Connection";
            connectionsInfo.Connections.Add(newConnection);
            InitTree();

            treeViewConnections.SelectedNode = treeViewConnections.Nodes[0].LastNode;
        }

        private void ButtonSaveClick(object sender, EventArgs e)
        {
            SaveConnections();
        }

        private void TreeViewConnectionsSelectChanged(object sender, EventArgs e)
        {
            //ShowForm();
        }

        private void TreeViewConnectionsAfterSelect(object sender, TreeViewEventArgs e)
        {
            if (currentNode != e.Node)
            {
                if (IsConnectionChanged())
                {
                    //Collect Data
                    CollectDataFromUI();
                }
                if (e.Node != treeViewConnections.Nodes[0])
                {
                    currentNode = e.Node;
                }
                else
                {
                    currentNode = null;
                }
                ShowForm();
            }
        }

        //private void ButtonApplyClick(object sender, EventArgs e)
        //{
        //    int selectedIndex = GetCurrentSelectedIndex();

        //    if (selectedIndex >= 0)
        //    {
        //        ConnectionInfo currentConnection = connectionsInfo.Connections[selectedIndex];
        //        currentConnection.Name = textBoxName.Text;
        //        currentConnection.Domain = textBoxDomain.Text;
        //        currentConnection.Port = Convert.ToInt32(textBoxPort.Text);
        //        currentConnection.UserName = textBoxUserName.Text;
        //        currentConnection.Password = textBoxPassword.Text;
        //        treeViewConnections.Nodes[0].Nodes[selectedIndex].Text = currentConnection.Name;
        //    }
        //}

        private void ButtonCancelClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TreeViewConnectionsBeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (currentNode != e.Node)
            {
               // e.Cancel = true;
            }
        }

        #endregion

        #region Private Methods

        private void LoadConnections()
        {
            FileStream connectionStream = new FileStream(@"XmlData\Connections.xml", FileMode.Open);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ServerConnections));
            connectionsInfo = (ServerConnections)xmlSerializer.Deserialize(connectionStream);
            connectionStream.Close();
        }

        private void SaveConnections()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ServerConnections));
            StreamWriter connectionWriter = new StreamWriter(@"XmlData\Connections.xml");
            xmlSerializer.Serialize(connectionWriter, connectionsInfo);
            connectionWriter.Close();
        }

        private void InitTree()
        {
            treeViewConnections.Nodes.Clear();

            TreeNode rootNode = new TreeNode("Connections");
            treeViewConnections.Nodes.Add(rootNode);

            foreach (ConnectionInfo connection in connectionsInfo.Connections)
            {
                rootNode.Nodes.Add(connection.Name);
            }
            treeViewConnections.ExpandAll();
            treeViewConnections.SelectedNode = treeViewConnections.Nodes[0].FirstNode;
        }

        private void ShowForm()
        {

            if (currentNode != null)
            {
                ConnectionInfo currentConnection = connectionsInfo[currentNode.Text];
                textBoxName.Text = currentConnection.Name;
                textBoxDomain.Text = currentConnection.Domain;
                textBoxPort.Text = currentConnection.Port.ToString();
                textBoxUserName.Text = currentConnection.UserName;
                textBoxPassword.Text = currentConnection.Password;
            }
        }

        private int GetCurrentSelectedIndex()
        {
            TreeNode currentSelectedNode = treeViewConnections.SelectedNode;
            TreeNode rootNode = treeViewConnections.Nodes[0];
            int selectedIndex = -1;
            foreach (TreeNode childNode in rootNode.Nodes)
            {
                selectedIndex++;
                if (childNode == currentSelectedNode)
                {
                    break;
                }
            }

            return selectedIndex;
        }

        private bool IsConnectionChanged()
        {
            if (currentNode == null)
            {
                return false;
            }
            bool isChanged = false;
            ConnectionInfo currentConnectionInfo = connectionsInfo[currentNode.Text];
            if (currentConnectionInfo == null)
            {
                return false;
            }
            isChanged = isChanged || (currentConnectionInfo.Domain != textBoxDomain.Text.Trim());
            isChanged = isChanged || (currentConnectionInfo.Name != textBoxName.Text.Trim());
            isChanged = isChanged || (currentConnectionInfo.Password != textBoxPassword.Text.Trim());
            isChanged = isChanged || (currentConnectionInfo.Port.ToString() != textBoxPort.Text.Trim());
            isChanged = isChanged || (currentConnectionInfo.UserName != textBoxUserName.Text.Trim());
            return isChanged;
        }

        private void CollectDataFromUI()
        {
            if (currentNode != null)
            {
                ConnectionInfo currentConnection = connectionsInfo[currentNode.Text];
                currentConnection.Name = textBoxName.Text;
                currentConnection.Domain = textBoxDomain.Text;
                currentConnection.Port = Convert.ToInt32(textBoxPort.Text);
                currentConnection.UserName = textBoxUserName.Text;
                currentConnection.Password = textBoxPassword.Text;
                currentNode.Text = currentConnection.Name;
            }
        }
        #endregion

        private void DoButtonRemoveClick(object sender, EventArgs e)
        {
            TreeNode temNextNode = null;
            if (currentNode != null)
            {
                connectionsInfo.Remove(currentNode.Text);
                currentNode.Remove();
               // currentNode = temNextNode;
               // treeViewConnections.SelectedNode = currentNode;
                ShowForm();
            }
        }
    }
}
