using System;
using System. Collections. Generic;
using System. ComponentModel;
using System. Drawing;
using System. Data;
using System. Linq;
using System. Text;
using System. Windows. Forms;
using OTAClientLib;

namespace SM. Smorgasbord. GKAutomatic
{
    public partial class BugInfoControl : UserControl
    {
        private Bug bug;

        public BugInfoControl()
        {
            InitializeComponent();
            webBrowserComments. Navigate("about:blank");
        }

        public Bug CRInfo
        {
            get
            {
                return bug;
            }
            set
            {
                bug = value;
            }
        }

        public void BindData()
        {
            //Sub State
            if (bug["BG_USER_09"] != null)
            {
                textBoxSubState. Text = bug["BG_USER_09"]. ToString();
            }

            //Set the Ower to the submitter
            if (bug["BG_RESPONSIBLE"] != null)
            {
                textBoxOwner. Text = bug["BG_RESPONSIBLE"]. ToString();
            }
            if (bug["BG_DETECTED_BY"] != null)
            {
                textBoxSubmitter. Text = bug["BG_DETECTED_BY"]. ToString();
            }
            //Update the comments;
            //string newComments = "Package has been moved";
            string rawComments = string. Empty;
            if (bug["BG_DEV_COMMENTS"] != null)
            {
                rawComments = bug["BG_DEV_COMMENTS"]. ToString();
            }
            webBrowserComments. Document. OpenNew(true);
            webBrowserComments. Document. Write(rawComments);
        }
    }
}
