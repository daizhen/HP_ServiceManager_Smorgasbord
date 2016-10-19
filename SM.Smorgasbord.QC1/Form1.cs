using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OTAClientLib;

namespace SM.Smorgasbord.QC
{
    public partial class Form1 : Form
    {
        private const string qcServer = "http://qc1d.atlanta.hp.com/qcbin";
        public Form1()
        {
            InitializeComponent();

            TDConnectionClass connection = new TDConnectionClass();
            connection.InitConnectionEx(qcServer);
        }
    }
}
