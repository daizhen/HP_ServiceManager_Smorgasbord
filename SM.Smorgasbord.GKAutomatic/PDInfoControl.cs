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
    public partial class PDInfoControl : UserControl
    {
        private Req request;
        public PDInfoControl()
        {
            InitializeComponent();

            webBrowserInfo. Navigate("about:blank");
        }

        public Req PDInfo
        {
            get
            {
                return request;
            }
            set
            {
                request = value;
            }
        }

        public void BindData()
        {
            //Set PD status
            if (request["RQ_USER_95"] != null)
            {
                textBoxStatus. Text = request["RQ_USER_95"]. ToString();
            }
            //Set PD ower
            if (request["RQ_USER_04"] != null)
            {
                textBoxOwner. Text = request["RQ_USER_04"]. ToString();
            }
            string rawComments = string. Empty;
            if (request["RQ_DEV_COMMENTS"] != null)
            {
                rawComments = request["RQ_DEV_COMMENTS"]. ToString();
            }
            webBrowserInfo. Document. OpenNew(true);
            webBrowserInfo. Document. Write(rawComments);
        }
    }
}
