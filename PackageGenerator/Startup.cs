using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Windows. Forms;

namespace SM. Smorgasbord. PackageGenerator
{
    public class Startup
    {
        public Form Start(Form parentForm)
        {
            PackageForm form = new PackageForm();
           // form. MdiParent = parentForm;
            //form. Show();

            return form;
        }
    }
}
