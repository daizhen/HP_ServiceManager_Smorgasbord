using System;
using System. Collections. Generic;
using System. ComponentModel;
using System. Drawing;
using System. Data;
using System. Linq;
using System. Text;
using System. Windows. Forms;
using SM.Smorgasbord.GKAutomatic.Utils;
using SM.Smorgasbord.GKAutomatic.Connections;
using SM. Smorgasbord. Communication. Common;
using System. Xml. Serialization;
using System. IO;

namespace SM. Smorgasbord. GKAutomatic
{
    public partial class FolderPanel : UserControl
    {
        private PackageInfo topPackage;
        private PackageInfo folderPackage;

        public PackageInfo FolderPackage
        {
            get
            {
                return folderPackage;
            }
            set
            {
                folderPackage = value;
            }
        }

        public PackageInfo TopPackage
        {
            get
            {
                return topPackage;
            }
            set
            {
                topPackage = value;
            }
        }
        public FolderPanel()
        {
            InitializeComponent();
        }

        public void BindData()
        {
            BindChains();
            BindServers();
        }

        private void BindChains()
        {
            comboBoxChain. Items. Clear();
            PackageChains chains = PackageUtil. LoadChains();
            foreach (ChainInfo chainInfo in chains. Chains)
            {
                comboBoxChain. Items. Add(chainInfo. Name);

                if (chainInfo. Name == folderPackage. ChainName)
                {
                    comboBoxChain. SelectedIndex = comboBoxChain. Items. Count - 1;
                }
            }
        }

        private void BindServers()
        {
            PackageChains chains = PackageUtil. LoadChains();
            ChainInfo currentChain = chains[folderPackage. ChainName];
            if (currentChain != null)
            {
                if (currentChain. SourceServer != null)
                {
                    textBoxSourceServer. Text = currentChain. SourceServer. Name;
                }

                listBoxTargetServers. Items. Clear();
                for (int i = 1; i < currentChain. Connections. Count; i++)
                {
                    ConnectionInfo connectionInfo = currentChain. Connections[i];
                    listBoxTargetServers. Items. Add(connectionInfo);
                }

            }
        }

        private void SavePackages()
        {
            if (topPackage != null)
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(PackageInfo));
                StreamWriter connectionWriter = new StreamWriter(@"XmlData\PackageInfo.xml");
                xmlSerializer. Serialize(connectionWriter, topPackage);
                connectionWriter. Close();
            }
        }
        private void DoButtonRefresh(object sender, EventArgs e)
        {
            BindChains();
        }

        private void DoComboBoxChainSelectedIndexChanged(object sender, EventArgs e)
        {
            folderPackage. ChainName = comboBoxChain. SelectedItem. ToString();
            BindServers();
        }

        private void DoButtonSaveClick(object sender, EventArgs e)
        {
            SavePackages();
        }
    }
}
