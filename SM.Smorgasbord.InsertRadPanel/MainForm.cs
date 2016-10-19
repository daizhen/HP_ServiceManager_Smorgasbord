using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SM.Smorgasbord.Communication.Sessions;
using SM.Smorgasbord.Communication.Common;
using System.Threading;
using SM.Smorgasbord.Communication.Lib;

namespace SM.Smorgasbord.InsertRadPanel
{
    public partial class MainForm : Form
    {
        #region Private fields

        private ConnectionInfo currentConnectionInfo = null;
        private string processName = "";
        private int indexToInsert = 0;
        private int countToInsert = 0;

        #endregion

        public MainForm()
        {
            InitializeComponent();
            BindServers();
        }

        private void BindServers()
        {
            ServerConnections connectionsInfo = ServerConnections.Load();

            foreach (ConnectionInfo connection in connectionsInfo.Connections)
            {
                comboBoxServer.Items.Add(connection);
            }
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            currentConnectionInfo = comboBoxServer.SelectedItem as ConnectionInfo;
           
            //Collect value from UI.
            processName = textBoxProcess.Text.Trim();
            indexToInsert = Convert.ToInt32(numericUpDownIndex.Value);
            countToInsert = Convert.ToInt32(numericUpDownCount.Value);
            
            //DisableControls();
            Thread thread = new Thread(new ThreadStart(InsertRad));
            thread.Start();
        }

        private void InsertRad()
        {
            SessionCache sessionCache = new SessionCache();
            ConnectionSession connectionSession = sessionCache[currentConnectionInfo];
            DataBus dataBus = connectionSession.GetDataBus();

            JSCodeRunner codeRunner = new JSCodeRunner();
            codeRunner.Include("JSCode\\JsonEncode.js");
            codeRunner.Include("JSCode\\InsertRad.js");
            string codeToRun = " return Insert(\"" + processName + "\"," + indexToInsert + "," + countToInsert + ");";
            JsonRaw rawData = codeRunner.Run<JsonRaw>(dataBus, codeToRun);
            //return rawData;
        }
    }
}
