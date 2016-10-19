using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SM.Smorgasbord.Debuger
{
    public class Startup
    {
        public Form Start(Form parentForm)
        {
            Form form = new DebugForm();
            return form;
        }
    }
}
