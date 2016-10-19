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
using System.IO;
using System.Collections.ObjectModel;

namespace SM.Smorgasbord.SearchString
{
    public partial class SearchForm : Form
    {

        private delegate void UpdateUI();

        private ConnectionInfo currentConnectionInfo = null;
        private string textToSearch = "";

        public SearchForm()
        {
            InitializeComponent();
            BindServers();
            InitCheckListBox();
        }

        private void SearchForm_Load(object sender, EventArgs e)
        {

        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            currentConnectionInfo = comboBoxServer.SelectedItem as ConnectionInfo;
            textToSearch = SerializeCode(textBoxString.Text.Trim());
            //DisableControls();
            Thread thread = new Thread(new ThreadStart(SearchString));
            thread.Start();
        }

        private void BindServers()
        {
            ServerConnections connectionsInfo = ServerConnections.Load();

            foreach (ConnectionInfo connection in connectionsInfo.Connections)
            {
                comboBoxServer.Items.Add(connection);
            }
        }

        private void SearchString()
        {
            SessionCache sessionCache = new SessionCache();
            ConnectionSession connectionSession = sessionCache[currentConnectionInfo];
            DataBus dataBus = connectionSession.GetDataBus();

            JSCodeRunner codeRunner = new JSCodeRunner();
            codeRunner.Include("JSCode\\JsonEncode.js");
            codeRunner.Include("JSCode\\SearchString.js");
            string codeToRun =ConstructJSForTables() + " return GetAllString(\"" + textToSearch + "\");";
            Collection<SearchResult> results = codeRunner.Run<Collection<SearchResult>>(dataBus, codeToRun);

            Invoke(new UpdateUI(delegate()
            {
                ShowData(results);
            }));
            //return rawData;
        }

        private string SerializeCode(string rawJsCode)
        {
            StringBuilder newCode = new StringBuilder();
            string[] splitedCodes = rawJsCode.Split('\r', '\n');

            foreach (string codeItem in splitedCodes)
            {
                string currentCode = codeItem.Trim();
                //Ignore the comments
                if (!currentCode.StartsWith("//"))
                {
                    newCode.Append(currentCode.Replace("\\", "\\\\").Replace("\"", "\\\"")).Append(" ");
                }
            }
            return newCode.ToString();
        }

        private string ConstructJSForTables()
        {
            const string PushFormat = "tableNames.push(\"{0}\");";
            StringBuilder str = new StringBuilder();


            foreach (object tableName in checkedListBoxTables.CheckedItems)
            {
                str.AppendFormat(PushFormat, tableName);
            }

            return str.ToString();
        }

        private void InitCheckListBox()
        {
            Collection<string> tableNames = GetAllTables();
            foreach (string tableName in tableNames)
            {
                if (tableName != "format")
                {
                    checkedListBoxTables.Items.Add(tableName, true);
                }
                else
                {
                    checkedListBoxTables.Items.Add(tableName, false);
                }
            }
        }

        private Collection<string> GetAllTables()
        {
            Collection<string> allTables = new Collection<string>();
            FileStream fileStream = new FileStream("TableToSearch.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(fileStream);
            while (!reader.EndOfStream)
            {
                string currentTableName = reader.ReadLine().Trim();
                if (!string.IsNullOrEmpty(currentTableName))
                {
                    allTables.Add(currentTableName);
                }
            }
            reader.Close();
            fileStream.Close();
            return allTables;
        }

        private void checkBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxTables.Items.Count; i++)
            {
                checkedListBoxTables.SetItemChecked(i, checkBoxAll.Checked);
            }
        }

        private void ShowData(Collection<SearchResult> results)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Table");
            dataTable.Columns.Add("Key");
            dataTable.Columns.Add("Field");

            foreach (SearchResult result in results)
            {
                DataRow row = dataTable.NewRow();
                row["Table"] = result.TableName;
                row["Key"] = result.TableKeyValue;
                row["Field"] = result.Field;
                dataTable.Rows.Add(row);
            }

            dataGridViewResult.DataSource = dataTable;
        }
    }
}
