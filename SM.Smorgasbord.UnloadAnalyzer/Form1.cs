using System;
using System. Collections. Generic;
using System. ComponentModel;
using System. Data;
using System. Drawing;
using System. Linq;
using System. Text;
using System. Windows. Forms;
using System. IO;

namespace SM. Smorgasbord. UnloadAnalyzer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            const string unlFileLocation = @"C:\Project\testFC.unl";

            Analyzer analyzer = new Analyzer();
            TableDef dbdictDef = analyzer. InitDbdictDef();
            FileStream fileStream = new FileStream(unlFileLocation, FileMode. Open, FileAccess. Read);
            
            //Skip 6 bytes.
            fileStream. Position += 6;
            SMTableRecord smLocalizationMap = analyzer. GetTableRecord(fileStream, dbdictDef);

            fileStream. Dispose();
        }
    }
}
