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
    public partial class RootPathForm : Form
    {
        public RootPathForm()
        {
            InitializeComponent();
            
            //load the root string.
            GeneralConfig config = PackageUtil.LoadConfig();
            textBoxRoot.Text = config.RootPath;
        }

        private void DoButtonSaveClick(object sender, EventArgs e)
        {
            GeneralConfig config = PackageUtil.LoadConfig();
            config.RootPath = textBoxRoot.Text.Trim();
            PackageUtil.SaveConfig(config);
            this.Close();
        }

        private void DoButtonCloseClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DoButtonBrowseClick(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.SelectedPath = textBoxRoot.Text;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBoxRoot.Text = dialog.SelectedPath;
            }
        }
    }
}
