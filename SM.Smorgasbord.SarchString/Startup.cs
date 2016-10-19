using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SM.Smorgasbord.SearchString
{
   public class Startup
    {
        public Form Start(Form parentForm)
        {
            SearchForm form = new SearchForm();
            // form. MdiParent = parentForm;
            //form. Show();

            return form;
        }
    }
}
