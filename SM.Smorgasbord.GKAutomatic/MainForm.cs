using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.IO;

using SM.Smorgasbord.Communication.Utils;
using SM.Smorgasbord.GKAutomatic.Utils;
using SM.Smorgasbord.Communication.Lib;
using SM.Smorgasbord.Communication.Sessions;
using SM.Smorgasbord.GKAutomatic.Connections;
using SM.Smorgasbord.Communication.Actions;
using SM.Smorgasbord.Communication.Common;
using SM.Smorgasbord.Communication.Common.Http;

namespace SM.Smorgasbord.GKAutomatic
{
    public partial class MainForm : Form
    {
        private PackageInfo topPackage;

        private PackagePanel packagePanel = null ;
        private FolderPanel folderPanel = null;

        public MainForm()
        {
            InitializeComponent();
            LoadPackages();
            InitTreeView();
            InitCustomControls();
        }

        #region  Private methods

        private void LoadPackages()
        {
            FileStream connectionStream = new FileStream(@"XmlData\PackageInfo.xml", FileMode.Open);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(PackageInfo));

            try
            {
                topPackage = (PackageInfo)xmlSerializer.Deserialize(connectionStream);
            }
            catch
            {

            }
            finally
            {
                connectionStream.Close();
            }
        }

        private void InitTreeView()
        {
            // Init images
            treeViewPackages. ImageList = new ImageList();
            treeViewPackages. ImageList. ColorDepth = ColorDepth. Depth32Bit;
            treeViewPackages. ImageList. ImageSize = new Size(28, 28);
            treeViewPackages. ImageList. Images. Add("FolderOpen", new Icon("Images/36.ico"));
            treeViewPackages. ImageList. Images. Add("FolderClose", new Icon("Images/37.ico"));
            treeViewPackages. ImageList. Images. Add("Unl", new Icon("Images/unl.ico"));

            //Clear all first.
            treeViewPackages.Nodes.Clear();

            PackageChains packageChains = PackageUtil.LoadChains();
            if (topPackage == null)
            {
                topPackage = new PackageInfo();
                topPackage.Name = "Packages";
                topPackage.IsEntity = false;
            }
            //Show the nodes in treeview
            AddNode(null, topPackage);
        }

        private void InitCustomControls()
        {
            packagePanel = new PackagePanel();
            packagePanel. TopPackage = topPackage;
            packagePanel. TreeViewPackages = treeViewPackages;

            folderPanel = new FolderPanel();
            folderPanel. TopPackage = topPackage;
            splitContainer1. Panel2. Controls. Add(packagePanel);
            splitContainer1. Panel2. Controls. Add(folderPanel);

            packagePanel. Visible = false;

        }

        private void AddNode(TreeNode parentNode, PackageInfo package)
        {
            TreeNode newNode = new TreeNode(package.Name);
            if (package. IsEntity)
            {
                newNode. ImageKey = "Unl";
                newNode. SelectedImageKey = "Unl";
            }
            else
            {
                newNode. ImageKey = "FolderOpen";
                newNode. SelectedImageKey = "FolderOpen";
            }
            newNode.Tag = package;
            if (parentNode == null)
            {
                treeViewPackages.Nodes.Add(newNode);
            }
            else
            {
                parentNode.Nodes.Add(newNode);
            }
            AddNode(newNode, package.Children);
        }

        private void AddNode(TreeNode parentNode, Collection<PackageInfo> packages)
        {
            foreach (PackageInfo package in packages)
            {
                TreeNode newNode = new TreeNode(package.Name);
               
                if (package. IsEntity)
                {
                    newNode. ImageKey = "Unl";
                    newNode. SelectedImageKey = "Unl";
                }
                else
                {
                    newNode. ImageKey = "FolderOpen";
                } 
                newNode. Tag = package;
                parentNode.Nodes.Add(newNode);
                AddNode(newNode, package.Children);
            }
        }

        /// <summary>
        /// Check whether the given chain exist in the tree
        /// </summary>
        /// <param name="chainName"></param>
        /// <returns></returns>
        private bool ChainExist(string chainName)
        {
            foreach (PackageInfo childPackage in topPackage. Children)
            {
                if (childPackage. Name == chainName)
                {
                    return true;
                }
            }
            return false;
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

        private void BindDetailData()
        {

            TreeNode selectedNode = treeViewPackages. SelectedNode;
            PackageInfo package = selectedNode. Tag as PackageInfo;
            if (package != null)
            {
                if (package. IsEntity)
                {
                    packagePanel. Visible = true;
                    folderPanel. Visible = false;

                    packagePanel. SelectedTreeNode = selectedNode;
                    packagePanel. BindDetailData();
                }
                else
                {
                    packagePanel. Visible = false;
                    folderPanel. Visible = true;

                    folderPanel. FolderPackage = package;
                    folderPanel. BindData();
                }
            }
        }
        #endregion

        private void configureRootToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Add a package 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addPackageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeViewPackages. SelectedNode;
            int nodeLevel = selectedNode. Level;

            UnloadForm unloadForm = new UnloadForm(selectedNode);
            unloadForm. ShowDialog();
            selectedNode. Nodes. Clear();
            PackageInfo package = selectedNode. Tag as PackageInfo;
            AddNode(selectedNode, package. Children);
            SavePackages();

        }

        /// <summary>
        /// Add a folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeViewPackages.SelectedNode;
            //int nodeLevel = selectedNode.Level;
            FolderForm releaseForm = new FolderForm(selectedNode);
            releaseForm. ShowDialog();
            selectedNode. Nodes. Clear();
            PackageInfo package = selectedNode. Tag as PackageInfo;

            AddNode(selectedNode, package. Children);
            SavePackages();
        }

        /// <summary>
        /// Tree view node click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeViewPackages_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            e. Node. TreeView. SelectedNode = e. Node;
            PackageInfo package = e. Node. Tag as PackageInfo;

            //
            if (package != null)
            {
                BindDetailData();
            }
        }
    }
}
