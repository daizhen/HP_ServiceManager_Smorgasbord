using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SM.Smorgasbord.Debuger
{
    public partial class VariableDisplayForm : Form
    {
        public string VarName
        {
            get;
            set;
        }

        public RTContext PreContext
        {
            get;
            set;
        }
        public RTContext PostContext
        {
            get;
            set;
        }
        public VariableDisplayForm()
        {
            InitializeComponent();
        }
    }
}
