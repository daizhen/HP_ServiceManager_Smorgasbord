using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using SM.Smorgasbord.Communication.Common;
using SM.Smorgasbord.Communication.Lib;
using System.IO;
using SM.Smorgasbord.Communication.Sessions;

namespace SM.Smorgasbord.EventIn
{
    public partial class EventInForm : Form
    {
        public EventInForm()
        {
            InitializeComponent();
            BindServers();
        }
        private Collection<EventMapFieldValue> fieldValues = new Collection<EventMapFieldValue>();

        private void BindServers()
        {
            ServerConnections connectionsInfo = ServerConnections.Load();

            foreach (ConnectionInfo connection in connectionsInfo.Connections)
            {
                comboBoxServer.Items.Add(connection);
            }
        }

        private Collection<EventMapFieldValue> GetEventValues(DataBus dataBus, string fileName,string queryString,string eventMapName)
        {
            JSCodeRunner codeRunner = new JSCodeRunner();
            codeRunner.Include("JSCode\\JsonEncode.js");
            codeRunner.Include("JSCode\\GetEventInFieldValues.js");
            string codeToRun = " return generateEventInFieldValues(\"" + fileName + "\",\"" + queryString + "\",\"" + eventMapName + "\");";
            Collection<EventMapFieldValue> fieldValues = codeRunner.Run<Collection<EventMapFieldValue>>(dataBus, codeToRun);
            return fieldValues;
        }

        private void buttonGetFieldValues_Click(object sender, EventArgs e)
        {
            string fileName = textBoxFileName.Text.Trim();
            string queryString = textBoxQueryString.Text.Trim().Replace("\"","\\\"");
            string eventMapName = textBoxEventMapName.Text.Trim();

            SessionCache sessionCache = new SessionCache();
            ConnectionInfo currentConnectionInfo = comboBoxServer.SelectedItem as ConnectionInfo;
            ConnectionSession connectionSession = sessionCache[currentConnectionInfo];
           
            fieldValues = GetEventValues(connectionSession.GetDataBus(), fileName, queryString, eventMapName);

            BindDataToGridView(fieldValues);
        }

        /// <summary>
        /// Bind Data To GridView
        /// </summary>
        /// <param name="fieldValues"></param>
        private void BindDataToGridView(Collection<EventMapFieldValue> fieldValues)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Key");
            table.Columns.Add("FieldName");
            table.Columns.Add("FieldValue");

            foreach (EventMapFieldValue fieldValue in fieldValues)
            {
                DataRow newRow = table.NewRow();
                newRow["Key"] = fieldValue.Position+":"+fieldValue.Sequence;
                newRow["FieldName"] = fieldValue.FieldName;
                newRow["FieldValue"] = fieldValue.FieldValue;
                table.Rows.Add(newRow);
            }

            dataGridViewDetail.DataSource = table; 
        }

        /// <summary>
        /// Collect Field Value from UI.
        /// </summary>
        /// <param name="fieldValues"></param>
        /// <returns></returns>
        private Dictionary<string, EventMapFieldValue> CollectValueFromUI(Collection<EventMapFieldValue> fieldValues)
        {
            Dictionary<string, EventMapFieldValue> eventMapValuesDictionary = new Dictionary<string, EventMapFieldValue>();
            foreach (EventMapFieldValue currentFieldValuePair in fieldValues)
            {
                eventMapValuesDictionary.Add(currentFieldValuePair.Position + ":"+currentFieldValuePair.Sequence, currentFieldValuePair);
            }
            foreach (DataGridViewRow gridViewRow in dataGridViewDetail.Rows)
            {
                //string currentFieldName = gridViewRow.Cells["FieldName"].Value.ToString();

                string currentFieldValue = string.Empty;
                if (gridViewRow.Cells["FieldValue"].Value != null)
                {
                    currentFieldValue = gridViewRow.Cells["FieldValue"].Value.ToString();
                }
                if (gridViewRow.Cells["Key"].Value != null)
                {
                    string currentKey = gridViewRow.Cells["Key"].Value.ToString();
                    eventMapValuesDictionary[currentKey].FieldValue = currentFieldValue;
                }
            }
            return eventMapValuesDictionary;
        }

        /// <summary>
        /// Generate string to create eventin.
        /// </summary>
        /// <param name="fieldValuesDictionary"></param>
        /// <returns></returns>
        private string GenerateString(Dictionary<string, EventMapFieldValue> fieldValuesDictionary)
        {
            StringBuilder resultString = new StringBuilder();

            Collection<int> sequences = new Collection<int>();

            foreach (KeyValuePair<string, EventMapFieldValue> keyValyePair in fieldValuesDictionary)
            {
                EventMapFieldValue currentFieldValue = keyValyePair.Value;
                if (!sequences.Contains(currentFieldValue.Sequence))
                {
                    sequences.Add(currentFieldValue.Sequence);
                }
            }
            int[] sequenceArray = sequences.ToArray();
            //positionArray[0] = 10;
            //positionArray[0] = 1;
            for (int i = 0; i < sequenceArray.Length; i++)
            {
                int sequence = sequenceArray[i];
                string partString = GenerateStringForSequence(fieldValuesDictionary, sequence);
                if (i == 0)
                {
                    resultString.Append(partString);
                }
                else
                {
                    resultString.Append("^").Append(partString);
                }
            }

            Array.Sort(sequenceArray);
            return resultString.ToString();
        }

        private string GenerateStringForSequence(Dictionary<string, EventMapFieldValue> fieldValuesDictionary, int sequence)
        {
            Collection<string> resultValues = new Collection<string>();
            foreach (KeyValuePair<string, EventMapFieldValue> keyValyePair in fieldValuesDictionary)
            {
                EventMapFieldValue currentFieldValue = keyValyePair.Value;
                if (currentFieldValue.Sequence == sequence)
                {
                    int position = currentFieldValue.Position;
                    if (position == resultValues.Count + 1)
                    {
                        resultValues.Add(currentFieldValue.FieldValue);
                    }
                    else if (position > resultValues.Count + 1)
                    {
                        for (int i = resultValues.Count + 1; i < position; i++)
                        {
                            resultValues.Add("");
                        }
                        resultValues.Add(currentFieldValue.FieldValue);
                    }
                    else
                    {
                        resultValues[sequence - 1] = currentFieldValue.FieldValue;
                    }
                }
            }

            StringBuilder resultString = new StringBuilder();
            if (resultValues.Count > 0)
            {
                resultString.Append(resultValues[0]);
            }

            for (int i = 1; i < resultValues.Count; i++)
            {
                resultString.Append("^").Append(resultValues[i]);
            }

            return resultString.ToString();
        }

        private void buttonGenerateString_Click(object sender, EventArgs e)
        {
            Dictionary<string, EventMapFieldValue> uiValues = CollectValueFromUI(fieldValues);
            string resultString = GenerateString(uiValues);
            textBoxResultString.Text = resultString;
        }

    }
}
