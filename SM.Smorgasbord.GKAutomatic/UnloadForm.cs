using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using iwantedue.Windows.Forms;
using System.IO;
using SM.Smorgasbord.MailExchange;
using SM.Smorgasbord.GKAutomatic.Utils;
using System.Text.RegularExpressions;

namespace SM.Smorgasbord.GKAutomatic
{
    public partial class UnloadForm : Form
    {
        private TreeNode parentNode;
        private MemoryStream messageStream = null;

        private string mailSubject;

        public UnloadForm(TreeNode node)
        {
            parentNode = node;
            InitializeComponent();

            //Init List view
            ImageList imageList = new ImageList();
            imageList.Images.Add("Mail", Image.FromFile("Images\\SingleMessage.ico"));

            listViewMail.SmallImageList = imageList;

            listViewMail.Dock = DockStyle.Fill;
            listViewMail.View = View.Details;

            listViewMail.Columns.Add("Mail", listViewMail.Width - 10);
            listViewMail.HeaderStyle = ColumnHeaderStyle.None;
        }

        private void DoButtonSaveClick(object sender, EventArgs e)
        {
            PackageInfo package = parentNode.Tag as PackageInfo;

            if (package != null)
            {
                PackageInfo unloadPackage = new PackageInfo();
                unloadPackage.IsEntity = true;
                unloadPackage.ChainName = package.ChainName;
                unloadPackage.Name = textBoxUnload.Text.Trim();
                if (messageStream != null)
                {
                    unloadPackage.MsgFileName = Guid.NewGuid().ToString() + ".msg";
                    unloadPackage.MsgTitle = mailSubject + ".msg";
                }
                unloadPackage. PackageNumber = textBoxNumber. Text. Trim();

                if (radioButtonCR. Checked)
                {
                    unloadPackage. Type = PackageType. CR;
                }
                else if (radioButtonQCR. Checked)
                {
                    unloadPackage. Type = PackageType. QCR;
                }
                else if (radioButtonPD. Checked)
                {
                    unloadPackage. Type = PackageType. PD;
                }
                else
                {
                    MessageBox. Show("Please select package type!");
                    return;
                }
                //unloadPackage.Parent = package;
                SaveMail(unloadPackage.MsgFileName);
                package.Children.Add(unloadPackage);

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

        private void UnloadForm_DragDrop(object sender, DragEventArgs e)
        {
            OutlookDataObject dataObject = new OutlookDataObject(e.Data);

            //get the names and data streams of the files dropped
            string[] filenames = (string[])dataObject.GetData("FileGroupDescriptorW");
            MemoryStream[] filestreams = (MemoryStream[])dataObject.GetData("FileContents");
            if (messageStream != null)
            {
                messageStream.Close();
            }
            messageStream = filestreams[0];
            MsgExtractor mailExtractor = new MsgExtractor();
            mailExtractor.BuildFromMSGFile(messageStream);

            //Close other stream if exists.
            for (int i = 1; i < filestreams.Length; i++)
            {
                filestreams[i].Close();
            }
            mailSubject = mailExtractor.Message.Subject;
            ShowMail();
            ExtractPackageInfoFromMailSubject(mailSubject);
        }

        private void UnloadForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else if (e.Data.GetDataPresent("FileGroupDescriptor"))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void ShowMail()
        {
            listViewMail.Items.Clear();
            ListViewItem item= new ListViewItem();
            item.ImageKey = "Mail";
            item.Text = mailSubject;
            listViewMail.Items.Add(item);
        }

        /// <summary>
        /// Get the root dir
        /// </summary>
        /// <returns></returns>
        private string GetRootDir()
        {
            //Load config
            GeneralConfig config = PackageUtil.LoadConfig();
            string packageRootDir = config.RootPath;
            return packageRootDir;
        }

        /// <summary>
        /// Save mail to local folder.
        /// </summary>
        /// <param name="fileName"></param>
        private void SaveMail(string fileName)
        {
            if (messageStream != null)
            {
                string dir = GetRootDir() + "\\Packages\\Mails\\" ;
                
                if(!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                FileStream fileStream = new FileStream(dir+ fileName, FileMode.Create, FileAccess.Write);
                messageStream.Position = 0;
                int messageByte = messageStream.ReadByte();
                while(messageByte>=0)
                {
                    fileStream.WriteByte((byte)messageByte); 
                    messageByte = messageStream.ReadByte();  
                }
                fileStream.Flush();
                fileStream.Close();
                messageStream.Close();
            }
 
        }


        private void ExtractPackageInfoFromMailSubject(string subject)
        {
            string unloadScriptName = string.Empty;
            PackageType packageType = PackageType.CR;
            string packageNumber = string.Empty;
            string gkPrefix = "Gatekeeper Approval Request for";
            int gkPrefixIndex = subject.IndexOf(gkPrefix);
            if (gkPrefixIndex >= 0)
            {
                unloadScriptName = subject.Substring(gkPrefixIndex + gkPrefix.Length).Trim();

            }
            Regex crRegex = new Regex("CR\\s*\\d+", RegexOptions.IgnoreCase);

            Match crMatch = crRegex.Match(subject);
            if (crMatch.Success)
            {
                packageType = PackageType.CR;

                radioButtonCR.Select();
                packageNumber = crMatch.Value.Substring(2);
            }
            else
            {
                Regex pdRegex = new Regex("PD\\s*\\d+", RegexOptions.IgnoreCase);
                Match pdMatch = pdRegex.Match(subject);
                if (pdMatch.Success)
                {
                    packageType = PackageType.PD;

                    radioButtonPD.Select();
                    packageNumber = pdMatch.Value.Substring(2);
                }
                else
                {
                    Regex qcrRegex = new Regex("QCR\\s*\\d+", RegexOptions.IgnoreCase);
                    Match qcrMatch = qcrRegex.Match(subject);
                    if (qcrMatch.Success)
                    {
                        packageType = PackageType.QCR;
                        radioButtonQCR.Select();
                        packageNumber = qcrMatch.Value.Substring(3).Trim();
                    }
                }
            }
            textBoxUnload.Text = unloadScriptName;
            textBoxNumber.Text = packageNumber;
        }
    }
}
