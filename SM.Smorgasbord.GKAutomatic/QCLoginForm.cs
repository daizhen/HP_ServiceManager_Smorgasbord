using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SM.Smorgasbord.GKAutomatic.Utils;

namespace SM.Smorgasbord.GKAutomatic
{
    public partial class QCLoginForm : Form
    {
        private PackagePanel parent;
        public QCLoginForm(PackagePanel parent)
        {
            InitializeComponent();
            LoadUserInfo();
            this.parent = parent;
        }

        private void LoadUserInfo()
        {
            GeneralConfig config = PackageUtil.LoadConfig();
            textBoxName.Text = config.QCUserName;
            textBoxPrintName.Text = config.QCPrintName;

        }

        private void SaveuserInfo()
        {
            GeneralConfig config = PackageUtil.LoadConfig();
            config.QCUserName = textBoxName.Text.Trim();
            config.QCPrintName = textBoxPrintName.Text.Trim();
            PackageUtil.SaveConfig(config);
        }

        private void DoButtonOKClick(object sender, EventArgs e)
        {
            parent.QCUserName = textBoxName.Text.Trim();
            parent.QCPassword = maskedTextBoxPwd.Text.Trim();
            parent.QCPrintName = textBoxPrintName.Text.Trim();
            parent.QCNameProvided = true;

            if (checkBoxSaveUserName.Checked)
            {
                SaveuserInfo();
            }
            this.Close();
        }

        private void DoButtonCancelClick(object sender, EventArgs e)
        {
            parent.QCNameProvided = false;
            this.Close();
        }
    }
}
