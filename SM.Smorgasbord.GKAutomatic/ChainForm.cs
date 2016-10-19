using System;
using System. Collections. Generic;
using System. ComponentModel;
using System. Data;
using System. Drawing;
using System. Linq;
using System. Text;
using System. Windows. Forms;
using System. IO;
using System. Xml. Serialization;
using SM. Smorgasbord. Communication. Sessions;
using System. Collections. ObjectModel;
using SM. Smorgasbord. Communication. Common;
using SM. Smorgasbord. GKAutomatic. Connections;
using SM. Smorgasbord. GKAutomatic. Utils;

namespace SM. Smorgasbord. GKAutomatic
{
    public partial class ChainForm : Form
    {
        private PackageChains chains;

        public ChainForm()
        {
            InitializeComponent();
            SetListData();
            LoadChains();
            InitTree();
        }

        #region  Private Methods

        private void LoadChains()
        {
            chains = PackageUtil. LoadChains();
        }

        private void SaveChains()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(PackageChains));
            StreamWriter connectionWriter = new StreamWriter(@"XmlData\PackageChains.xml");
            xmlSerializer. Serialize(connectionWriter, chains);
            connectionWriter. Close();
        }

        private void InitTree()
        {
            treeViewChains. Nodes. Clear();
            TreeNode rootNode = new TreeNode("Chains");
            treeViewChains. Nodes. Add(rootNode);

            foreach (ChainInfo chain in chains. Chains)
            {
                TreeNode newNode = new TreeNode();
                newNode. Text = chain. Name;
                newNode. Tag = chain;
                rootNode. Nodes. Add(newNode);
            }
            treeViewChains. ExpandAll();
        }

        private void ShowForm()
        {
            TreeNode selectedNode = treeViewChains. SelectedNode;

            if (selectedNode !=null)
            {
                ChainInfo currentChain = selectedNode. Tag as ChainInfo;
                if (currentChain != null)
                {
                    //Set the name of the chain
                    textBoxName. Text = currentChain. Name;
                    
                    //Set source server
                    if (currentChain. Connections. Count > 0)
                    {
                        comboBoxSourceServer. Text = currentChain. Connections[0]. Name;
                    }

                    //Set target servers
                    DataTable dataTable = new DataTable();
                    dataTable. Columns. Add("Connections");

                    for (int i = 1; i < currentChain. Connections. Count; i++)
                    {
                        ConnectionInfo currentConnection = currentChain. Connections[i];
                        DataRow dataRow = dataTable. NewRow();
                        dataRow[0] = currentConnection. Name;
                        dataTable. Rows. Add(dataRow);
                    }
                    dataGridViewConnections. DataSource = dataTable;
                }
            }
        }

        private void SetListData()
        {
            ServerConnections connectionsInfo = ServerConnections. Load();

            Collection<string> connectionStrings = new Collection<string>();

            foreach (ConnectionInfo currentConnection in connectionsInfo. Connections)
            {
                connectionStrings. Add(currentConnection. Name);
            }

            (dataGridViewConnections. Columns[0] as DataGridViewComboBoxColumn). DataSource = connectionStrings;

            comboBoxSourceServer. DataSource = connectionStrings;
        }

        private bool ValidateForm()
        {

            return true;
        }

        #endregion

        private void ButtonAddChainClick(object sender, EventArgs e)
        {
            ChainInfo newChain = new ChainInfo();
            newChain. Name = "New Chain";
            chains. Chains. Add(newChain);
            
            TreeNode newNode = new TreeNode();
            newNode. Text = newChain. Name;
            newNode. Tag = newChain;
            treeViewChains. Nodes[0]. Nodes. Add(newNode);
            treeViewChains. SelectedNode = newNode;
        }

        private void DoButtonSaveClick(object sender, EventArgs e)
        {
            if (!ValidateForm())
            {
                return;
            }
            ServerConnections connectionsInfo = ServerConnections.Load();

            TreeNode selectedNode = treeViewChains. SelectedNode;
            if (selectedNode != null)
            {
                ChainInfo currentChain = selectedNode. Tag as ChainInfo;
                if (currentChain != null)
                {
                    currentChain. Name = textBoxName. Text;
                    //Remove all connections.
                    currentChain. Connections. Clear();

                    //Add source server
                    string sourceConnectionName = comboBoxSourceServer.Text.Trim();
                    ConnectionInfo sourceConnection = connectionsInfo[sourceConnectionName];
                    if (sourceConnection != null)
                    {
                        currentChain. Connections. Add(sourceConnection);
                    }
                    else
                    {
                        MessageBox. Show("Please select proper source server");
                        return;
                    }

                    foreach (DataGridViewRow gridRow in dataGridViewConnections. Rows)
                    {
                        DataGridViewCell firstCell = gridRow. Cells[0];
                        if (firstCell. Value != null)
                        {
                            string connectionName = firstCell. Value. ToString();
                            currentChain. Connections. Add(connectionsInfo[connectionName]);
                        }
                    }
                }
            }

            SaveChains();
        }

        private void TreeViewChainsAfterSelect(object sender, TreeViewEventArgs e)
        {
            ShowForm();
        }

        private void DoButtonCloseClick(object sender, EventArgs e)
        {
            this. Close();
        }

        private void DoButtonRemoveClick(object sender, EventArgs e)
        {
            //chains.Chains.Remove( 
        }
    }
}
