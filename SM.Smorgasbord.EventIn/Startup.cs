using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SM.Smorgasbord.EventIn
{
    public class Startup
    {
        public Form Start(Form parentForm)
        {
            EventInForm form = new EventInForm();
            // form. MdiParent = parentForm;
            //form. Show();

            return form;
        }
    }
}
