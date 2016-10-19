using System;
using System. Collections. Generic;
using System. ComponentModel;
using System. Drawing;
using System. Data;
using System. Linq;
using System. Text;
using System. Windows. Forms;
using System. Threading;
using SM. Smorgasbord. GKAutomatic. Connections;
using SM. Smorgasbord. GKAutomatic. Utils;
using SM. Smorgasbord. Communication. Sessions;
using SM. Smorgasbord. Communication. Common;
using SM. Smorgasbord. Communication. Lib;
using System. Xml. Serialization;
using System. IO;
using OTAClientLib;
using System.Diagnostics;
using Microsoft.Exchange.WebServices.Data;
using SM.Smorgasbord.MailExchange;

namespace SM. Smorgasbord. GKAutomatic
{
    public partial class PackagePanel : UserControl
    {
        private const string qcServer = "http://qc1d.atlanta.hp.com/qcbin";

        private static object synObj = new object();

        private delegate void UpdateTreeNodes();

        #region Fields

        private TreeNode selectedTreeNode;

        private PackageInfo topPackage;

        private TreeView treeViewPackages;

        private FolderPanel folderPanel;

        private BugInfoControl bugInfoControl;
        private PDInfoControl pdInfoControl;

        private string qcUserName = "zhend_hp.com";
        private string qcPassword = "xxxxxx";
        private string qcPrintName = "";
        private bool qcNameProvided = false;

        #endregion

        public TreeNode SelectedTreeNode
        {
            get
            {
                return selectedTreeNode;
            }
            set
            {
                selectedTreeNode = value;
            }
        }

        public PackageInfo Package
        {
            get
            {
                return selectedTreeNode. Tag as PackageInfo;
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

        public TreeView TreeViewPackages
        {
            get
            {
                return treeViewPackages;
            }
            set
            {
                treeViewPackages = value;
            }
        }

        public string QCUserName
        {
            get
            {
                return qcUserName.Replace("@","_");
            }
            set
            {
                qcUserName = value;
            }
        }

        public string QCPassword
        {
            get
            {
                return qcPassword;
            }
            set
            {
                qcPassword = value;
            }
        }

        public string QCPrintName
        {
            get
            {
                return qcPrintName;
            }
            set
            {
                qcPrintName = value;
            }
        }

        public bool QCNameProvided
        {
            get
            {
                return qcNameProvided;
            }
            set
            {
                qcNameProvided = value;
            }
        }

        public PackagePanel()
        {
            InitializeComponent();
            folderPanel = new FolderPanel();
            groupBoxChainInfo. Controls. Add(folderPanel);

            bugInfoControl = new BugInfoControl();
            bugInfoControl. Location = new Point(20, 20);

            tabControlDetail. TabPages[1]. Controls. Add(bugInfoControl);

            pdInfoControl = new PDInfoControl();
            pdInfoControl. Location = new Point(20, 20);
            tabControlDetail. TabPages[1]. Controls. Add(pdInfoControl);
            InitListViewMail();
        }

        private void DoButtonRollBackClick(object sender, EventArgs e)
        {
            if (Package != null)
            {
                if (Package. Status == PackageStatus. Processing)
                {
                    MessageBox. Show("The package is in processing.");
                    return;
                }
                Thread thread = new Thread(new ParameterizedThreadStart(RollBackPackage));
                Package. RootDir = GetRelatedDir(selectedTreeNode);
                string originalText = selectedTreeNode. Text;
                selectedTreeNode. Text = selectedTreeNode. Text + "  Roll back...";
                thread. Start(Package);
            }
            else
            {
                MessageBox. Show("It's not a package!");
            }
        }

        private void DoButtonMoveClick(object sender, EventArgs e)
        {
            if (Package != null)
            {
                //Set to processing
                Package. Status = PackageStatus. Processing;
                Thread thread = new Thread(new ParameterizedThreadStart(MovePackage));
                Package. RootDir = GetRelatedDir(selectedTreeNode);
                string originalText = selectedTreeNode. Text;
                selectedTreeNode. Text = selectedTreeNode. Text + "  Moving...";
                thread. Start(Package);
            }
            else
            {
                MessageBox. Show("It's not a package!");
            }
        }

        public void BindDetailData()
        {
            if (Package. IsEntity)
            {
                labelName. Text = Package. Name;
                labelStatus. Text = Package. Status. ToString();
                labelUnloadScript. Text = Package. UnloadScriptFileLocation;

                labelType. Text = Package. Type. ToString();
                labelNumber. Text = Package. PackageNumber;
                DataTable dataTable = new DataTable();
                dataTable. Columns. Add(new DataColumn("ServerName"));
                dataTable. Columns. Add(new DataColumn("BackupFile"));

                foreach (BackupFileInfo backupFile in Package. BackupFileLocations)
                {
                    DataRow dataRow = dataTable. NewRow();
                    dataRow["ServerName"] = backupFile. ServerName;
                    dataRow["BackupFile"] = backupFile. UnloadFileLocation;
                    dataTable. Rows. Add(dataRow);
                }

                dataGridViewBackup. DataSource = dataTable;
            }

            //Bind the servers
            folderPanel. TopPackage = topPackage;
            folderPanel. FolderPackage = Package;
            folderPanel. BindData();
            ShowMail(Package);
            SetQCInfoControls();
        }

        #region Private Methods

        private void RollBackPackage(Object packageObject)
        {
            lock (synObj)
            {
                PackageInfo package = packageObject as PackageInfo;
                PackageChains packageChains = PackageUtil. LoadChains();
                ServerConnections connectionsInfo = ServerConnections.Load();

                if (package. Status == PackageStatus. Moved || package. Status == PackageStatus. Rollbacked)
                {
                    //Set to processing
                    package. Status = PackageStatus. Processing;

                    string currentChainName = package. ChainName;
                    ChainInfo currentChain = packageChains[currentChainName];
                    if (currentChain != null)
                    {
                        //Get the session of first server
                        SessionCache sessionCache = new SessionCache();
                        string packageRoot = GetRootDir();
                        foreach (BackupFileInfo backupFile in package. BackupFileLocations)
                        {
                            string serverName = backupFile. ServerName;
                            string backupLocation = backupFile. UnloadFileLocation;
                            ConnectionInfo connectionInfo = connectionsInfo[serverName];

                            ConnectionSession targetConnectionSession = sessionCache[connectionInfo];

                            DataBus dataBus = targetConnectionSession. GetDataBus();

                            //Import the backup file to roll back the code.
                            ImportFile rollBackFile = new ImportFile();
                            rollBackFile. UnloadLocation = packageRoot + "\\" + backupLocation;
                            rollBackFile. Import(dataBus);
                        }
                        package. Status = PackageStatus. Rollbacked;
                        SavePackages();
                        ResetTreeNode(package);
                    }
                    else
                    {
                        ShowMessage(currentChainName + " not exist");
                    }
                }
                else
                {
                    ShowMessage("Can not roll back");
                    ResetTreeNode(package);
                }
            }
        }

        private void MovePackage(Object packageObject)
        {
            lock (synObj)
            {
                PackageInfo package = packageObject as PackageInfo;
                PackageChains packageChains = PackageUtil. LoadChains();
                ServerConnections connectionsInfo = ServerConnections.Load();

                if (package != null)
                {
                    try
                    {
                        //Get current pachage server chain
                        ChainInfo currentChain = packageChains[package. ChainName];

                        //Get the first server
                        ConnectionInfo firstServer = connectionsInfo[currentChain. Connections[0].Name];

                        //Get the session of first server
                        SessionCache sessionCache = new SessionCache();
                        ConnectionSession firstConnectionSession = sessionCache[firstServer];

                        //Generate unload script file
                        UnloadScriptFile unloadScript = new UnloadScriptFile();

                        unloadScript. UnloadScriptName = package. Name;

                        string unloadScriptRelatedLocation = package.RootDir + EscapeFileName(package.Name) + ".Unload.Script.unl"; ;
                        string unloadScriptLocation = GetRootDir() + "\\" + unloadScriptRelatedLocation;
                        unloadScript. FileLocation = unloadScriptLocation;

                        //Save the unload script file path to package
                        package. UnloadScriptFileLocation = unloadScriptRelatedLocation;

                        // Generate unload script file
                        unloadScript. GenerateUnloadScript(firstConnectionSession. GetDataBus());

                        //Generate unload data file.
                        UnloadFile unloadFile = new UnloadFile();
                        string unloadFileRelatedLocation = package.RootDir + EscapeFileName(package.Name) + "." + firstServer.Name + ".unl";
                        string unloadFileLocation = GetRootDir() + "\\" + unloadFileRelatedLocation;
                        unloadFile. UnloadLocation = unloadFileLocation;
                        unloadFile. UnloadScriptName = package. Name;
                        unloadFile. GenerateUnload(firstConnectionSession. GetDataBus());
                        // package.
                        for (int i = 1; i < currentChain. Connections. Count; i++)
                        {
                            ConnectionInfo currentServerInfo = connectionsInfo[currentChain.Connections[i].Name];
                            ConnectionSession targetConnectionSession = sessionCache[currentServerInfo];

                            //Upload unload script file
                            ImportFile uploadScriptFile = new ImportFile();
                            uploadScriptFile. UnloadLocation = unloadScriptLocation;
                            uploadScriptFile. Import(targetConnectionSession. GetDataBus());

                            //Backup 
                            UnloadFile targetUnloadFile = new UnloadFile();

                            //Related location
                            string backupRelatedLocation = package.RootDir + EscapeFileName(package.Name) + "." + currentServerInfo.Name + ".unl";
                            //Absolute location.
                            string backupFileLocation = GetRootDir() + "\\" + backupRelatedLocation;
                            targetUnloadFile. UnloadLocation = backupFileLocation;
                            targetUnloadFile. UnloadScriptName = package. Name;
                            targetUnloadFile. GenerateUnload(targetConnectionSession. GetDataBus());

                            BackupFileInfo backupFile = new BackupFileInfo();
                            backupFile. ServerName = currentServerInfo. Name;
                            backupFile. UnloadFileLocation = backupRelatedLocation;
                            package. BackupFileLocations. Add(backupFile);

                            //Import data from source server

                            ImportFile uploadFile = new ImportFile();
                            uploadFile. UnloadLocation = unloadFileLocation;
                            uploadFile. Import(targetConnectionSession. GetDataBus());
                        }
                        package. Status = PackageStatus. Moved;
                        SavePackages();
                        UpdateQC(package);
                        if (checkBoxNeedReply.Checked)
                        {
                            ReplyMail(package);
                        }
                        Invoke(new UpdateTreeNodes(delegate()
                        {
                            TreeNode selectedNode = GetNodeByObject(packageObject, treeViewPackages. Nodes[0]);
                            selectedNode. Text = package. Name;

                            MessageBox. Show("Package moved");
                        }));
                    }
                    catch (Exception ex)
                    {
                        Invoke(new UpdateTreeNodes(delegate()
                           {
                               //Reset the package info.
                               TreeNode selectedNode = GetNodeByObject(packageObject, treeViewPackages.Nodes[0]);
                               selectedNode.Text = package.Name;
                               MessageBox.Show("Error when moving package " + package.Name + " please retry" + "\r\n" + ex.StackTrace + "\r\n" + ex.Message);
                               // throw ex;
                           }));
                    }
                }
                else
                {
                    MessageBox. Show("No Data");
                }
            }
        }

        /// <summary>
        /// Get the root dir
        /// </summary>
        /// <returns></returns>
        private string GetRootDir()
        {
            //Load config
            GeneralConfig config = PackageUtil. LoadConfig();
            string packageRootDir = config. RootPath;
            return packageRootDir;
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

        private void ShowMessage(string message)
        {
            Invoke(new UpdateTreeNodes(delegate()
            {
                MessageBox. Show(message);

            }));
        }

        private void ResetTreeNode(PackageInfo package)
        {
            Invoke(new UpdateTreeNodes(delegate()
            {
                TreeNode selectedNode = GetNodeByObject(package, treeViewPackages. Nodes[0]);
                selectedNode. Text = package. Name;

            }));
        }

        /// <summary>
        /// Get the related path.
        /// </summary>
        /// <param name="selectedNode">Current node</param>
        /// <returns></returns>
        private string GetRelatedDir(TreeNode selectedNode)
        {
            StringBuilder fullPath = new StringBuilder();
            TreeNode temNode = selectedNode;
            while (temNode. Parent != null)
            {
                temNode = temNode. Parent;
                fullPath. Insert(0, temNode. Text + "\\");
            }
            return fullPath. ToString();
        }


        private TreeNode GetNodeByObject(Object tagObj, TreeNode currentNode)
        {
            if (currentNode. Tag == tagObj)
            {
                return currentNode;
            }
            else
            {
                foreach (TreeNode childNode in currentNode. Nodes)
                {
                    TreeNode tem = GetNodeByObject(tagObj, childNode);
                    if (tem != null)
                    {
                        return tem;
                    }
                }
            }
            return null;
        }


        private void UpdateQC(PackageInfo package)
        {
            if (!qcNameProvided)
            {
                QCLoginForm loginForm = new QCLoginForm(this);
                loginForm. ShowDialog();
            }

            if (!qcNameProvided)
            {
                return;
            }
            TDConnectionClass connection = new TDConnectionClass();
            connection. InitConnectionEx(qcServer);

            try
            {
                connection. Login(QCUserName, qcPassword);
                connection. Connect("HPS_OS", "ITO_ESM_Integration");

                //Construct the comments string.
                string newComments = ConstructCommentsString();

                if (package. Type == PackageType. CR)
                {
                    BugFactory bugFactory = connection. BugFactory as BugFactory;
                    ITDFilter2 bugFilter = bugFactory. Filter as ITDFilter2;

                    bugFilter["BG_BUG_ID"] = package. PackageNumber;
                    List bugList = bugFilter. NewList();
                    Bug bug = bugList[1] as Bug;
                    UpdateCR(bug, newComments);
                }
                else if (package. Type == PackageType. PD)
                {
                    ReqFactory reqFactory = connection. ReqFactory as ReqFactory;
                    ITDFilter2 reqFilter = reqFactory. Filter as ITDFilter2;
                    reqFilter["RQ_REQ_ID"] = package. PackageNumber;
                    List reqList = reqFilter. NewList();
                    Req req = reqList[1] as Req;
                    UpdatePD(req, newComments);
                }
                else
                {
                    //Nothing to do.
                }
            }
            catch (Exception ex)
            {
                qcNameProvided = false;

                //Nothing to do.
                Invoke(new UpdateTreeNodes(delegate()
                {
                    MessageBox. Show("Update QC failed!\n-------\n"+ex.Message, "Error", MessageBoxButtons.OK);
                }));
            }
            finally
            {
                connection. Logout();
            }
        }

        private bool UpdateCR(Bug bug,string newComments)
        {
            try
            {
                if (bug != null)
                {
                    //Sub State
                    bug["BG_USER_09"] = "Retesting";
                    //Set the Ower to the submitter
                    bug["BG_RESPONSIBLE"] = bug["BG_DETECTED_BY"];

                    //Update the comments;
                    string rawComments = string. Empty;
                    if (bug["BG_DEV_COMMENTS"] != null)
                    {
                        rawComments = bug["BG_DEV_COMMENTS"]. ToString();
                    }
                    bug["BG_DEV_COMMENTS"] = AddComments(QCUserName, rawComments, newComments);
                    bug. Post();
                    return true;
                }
            }
            finally
            {
                bug. UnLockObject();
            }
            return false;
        }

        private bool UpdatePD(Req request, string newComments)
        {
            try
            {
                if (request != null)
                {
                    GeneralConfig config = PackageUtil.LoadConfig();
                    //Set PD status
                    request["RQ_USER_95"] = "3.4 Testing";
                    //Set PD ower
                    string newOwner = config.PDCoordinator.Replace("@","_");
                    request["RQ_USER_04"] = newOwner;
                    //Set Comments
                    string rawComments = string. Empty;
                    if (request["RQ_DEV_COMMENTS"] != null)
                    {
                        rawComments = request["RQ_DEV_COMMENTS"]. ToString();
                    }
                    request["RQ_DEV_COMMENTS"] = AddComments(QCUserName, rawComments, newComments);
                    request. Post();
                    return true;
                }
                return false;
            }
            finally
            {
                request. UnLockObject();
            }
        }

        private string AddComments(string userName, string rawComments, string commentsToAdd)
        {
            //0: userName
            //1: email
            //2: Date
            //3: detail string
            const string CommentsTemplate = "<br><font color='#000080'><b>________________________________</b></font><font color='#000080'><br/><b>{0} &lt;{1}&gt;, {2}: </b></font> {3}";

            StringBuilder resultString = new StringBuilder();

            if (string.IsNullOrEmpty(rawComments))
            {
                resultString.Append("<html><body>");
                resultString.AppendFormat(CommentsTemplate, qcPrintName, userName, System.DateTime.Now.ToString("yyyy/MM/dd"), commentsToAdd);
                resultString.Append("</body></html>");
            }
            else
            {
                resultString.Append(rawComments);

                if (rawComments.EndsWith("</body>\r\n</html>"))
                {
                    resultString.Insert(resultString.Length - 16, "<br>");
                    resultString.Insert(resultString.Length - 16, string.Format(CommentsTemplate, qcPrintName, userName, System.DateTime.Now.ToString("yyyy/MM/dd"), commentsToAdd));
                }
                else
                {
                    resultString.Insert(resultString.Length - 14, "<br>");
                    resultString.Insert(resultString.Length - 14, string.Format(CommentsTemplate, qcPrintName, userName, System.DateTime.Now.ToString("yyyy/MM/dd"), commentsToAdd));
                }
            }
            return resultString. ToString();
        }

        private void ShowQCInfo(PackageInfo package)
        {
            if (!qcNameProvided)
            {
                QCLoginForm loginForm = new QCLoginForm(this);
                loginForm. ShowDialog();
            }

            if (!qcNameProvided)
            {
                return;
            }
            TDConnectionClass connection = new TDConnectionClass();
            connection. InitConnectionEx(qcServer);

            try
            {
                connection. Login(QCUserName, qcPassword);
                connection. Connect("HPS_OS", "ITO_ESM_Integration");

                if (package. Type == PackageType. CR)
                {
                    BugFactory bugFactory = connection. BugFactory as BugFactory;
                    ITDFilter2 bugFilter = bugFactory. Filter as ITDFilter2;

                    bugFilter["BG_BUG_ID"] = package. PackageNumber;
                    List bugList = bugFilter. NewList();
                    Bug bug = bugList[1] as Bug;
                    bugInfoControl. CRInfo = bug;
                    bugInfoControl. Visible = true;
                    bugInfoControl. BindData();
                    pdInfoControl. Visible = false;
                }
                else if (package. Type == PackageType. PD)
                {
                    ReqFactory reqFactory = connection. ReqFactory as ReqFactory;
                    ITDFilter2 reqFilter = reqFactory. Filter as ITDFilter2;
                    reqFilter["RQ_REQ_ID"] = package. PackageNumber;
                    List reqList = reqFilter. NewList();
                    Req req = reqList[1] as Req;

                    pdInfoControl. PDInfo = req;
                    pdInfoControl. Visible = true;
                    pdInfoControl. BindData();
                    bugInfoControl. Visible = false;
                }
                else
                {
                    //Nothing to do.
                }
            }
            catch (Exception ex)
            {
                qcNameProvided = false;
                //Nothing to do.
                Invoke(new UpdateTreeNodes(delegate()
                {
                    MessageBox. Show("Get QC Info error", "Error", MessageBoxButtons. OKCancel);
                }));
            }
            finally
            {
                connection. Logout();
            }

        }

        private void SetQCInfoControls()
        {
            if (Package. Type == PackageType. CR)
            {
                bugInfoControl. Visible = true;
                pdInfoControl. Visible = false;
            }
            else if (Package. Type == PackageType. PD)
            {
                pdInfoControl. Visible = true;
                bugInfoControl. Visible = false;
            }
            else
            {
                //Nothing to do.
            }
        }

        private void InitListViewMail()
        {
            //Init List view
            ImageList imageList = new ImageList();
            imageList.Images.Add("Mail", Image.FromFile("Images\\SingleMessage.ico"));

            listViewMail.SmallImageList = imageList;

            listViewMail.Dock = DockStyle.Fill;
            listViewMail.View = View.Details;

            listViewMail.Columns.Add("Mail", listViewMail.Width - 10);
            listViewMail.HeaderStyle = ColumnHeaderStyle.None;
        }

        private void AddMailItem(PackageInfo package)
        {
            ListViewItem mailItem = new ListViewItem();
            mailItem.Text = package.MsgTitle;
            mailItem.ImageKey = "Mail";
            mailItem.Tag = GetRootDir() + "\\Packages\\Mails\\" + package.MsgFileName;
            listViewMail.Items.Add(mailItem);
        }

        private void ShowMail(PackageInfo package)
        {
            listViewMail.Items.Clear();
            if (!string.IsNullOrEmpty(package.MsgTitle) && !string.IsNullOrEmpty(package.MsgFileName))
            {
                AddMailItem(package);
            }
        }

        #endregion

        private void DoButtonUpdateQCClick(object sender, EventArgs e)
        {
            UpdateQC(Package);
        }

        private void DoButtonGetQCInfoClick(object sender, EventArgs e)
        {
            ShowQCInfo(Package);
        }

        private void listViewMail_DoubleClick(object sender, EventArgs e)
        {
            if (listViewMail.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listViewMail.SelectedItems[0];
                Process.Start(selectedItem.Tag.ToString());
            }
        }

        private string EscapeFileName(string rawName)
        {
            return rawName.Replace('\\', '1').Replace('/', '2').Replace(':', '3').Replace('*', '4')
                .Replace('?', '5').Replace('"', '6').Replace('<', '7').Replace('>', '8').Replace('|', '9');
        }

        private void ReplyMail(PackageInfo package)
        {
            if (string.IsNullOrEmpty(package.MsgFileName) ||
                string.IsNullOrEmpty(package.MsgTitle))
            {
                //Nothing to do.
            }
            if (!qcNameProvided)
            {
                QCLoginForm loginForm = new QCLoginForm(this);
                loginForm.ShowDialog();
            }

            if (!qcNameProvided)
            {
                return;
            }
            GeneralConfig generalConfig = PackageUtil.LoadConfig();
            MsgExtractor extractor = ExtractMail();
            
            extractor.Domain = generalConfig.UserDomain;
            extractor.UserName = qcUserName.Replace("_","@");
            extractor.UriString = generalConfig.ExchangeServerUri;
            extractor.Password = qcPassword;
            extractor.Login();
            extractor.CreateReply();
            extractor.Message.ToRecipients.Add(extractor.Message.Sender);

            if (package.Type == PackageType.PD)
            {
                extractor.Message.ToRecipients.Add(generalConfig.PDCoordinator);
            }
            extractor.Message.ToRecipients.Add(generalConfig.TestingGroup);
            extractor.Message.Body.Text = "Hi, All<br/><br/>" + ConstructCommentsString() + "<br/>Please retest it.<br/><br/>Thanks<br/><br/>" + extractor.Message.Body.Text;
            extractor.Message.SendAndSaveCopy();
        }

        /// <summary>
        /// Extract mail object from msg file.
        /// After execute this method, the Message field is filled.
        /// </summary>
        /// <returns></returns>
        private MsgExtractor ExtractMail()
        {
            MsgExtractor extractor = new MsgExtractor();
            string msgLocation = GetRootDir() + "\\Packages\\Mails\\" + Package.MsgFileName;
            FileStream fileStream = new FileStream(msgLocation,FileMode.Open,FileAccess.Read);
            extractor.BuildFromMSGFile(fileStream);
            fileStream.Close();
            return extractor;
        }

        /// <summary>
        /// Construct comments string, which will be added to QC.
        /// Also can be used as mail content.
        /// </summary>
        /// <returns> "Package have been moved from source to target1 and target2...</returns>
        private string ConstructCommentsString()
        {
            //Construct the comments string.
            StringBuilder newComments = new StringBuilder("Package have been moved from ");
            newComments.Append(Package.Chain.SourceServer.Name);
            if (Package.Chain.Connections.Count > 1)
            {
                newComments.Append(" to ");
                newComments.Append(Package.Chain.Connections[1].Name);
                for (int i = 2; i < Package.Chain.Connections.Count; i++)
                {
                    newComments.Append(" and ");
                    newComments.Append(Package.Chain.Connections[i].Name);
                }
            }
            return newComments.ToString();
        }
    }
}
