using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;

using SM.Smorgasbord.Communication.Sessions;
using SM.Smorgasbord.Communication.Common;
using SM.Smorgasbord.Communication.Lib;
using SM.Smorgasbord.Communication.Actions;
using SM.Smorgasbord.Communication.Common.Http;
using SM.Smorgasbord.Communication.SoapEntities.Response;
using SM.Smorgasbord.Communication.Utils;
using System.Net;
using System.IO;
using System.Threading;
using SM.Smorgasbord.Communication.SoapEntities;
using System.Collections.ObjectModel;
using System.Reflection;

namespace SM.Smorgasbord.Debuger
{
    public partial class Form1 : Form
    {
        private TreeNode currentRADNode = null;
        private TreeNode currentPanelNode = null;

        private DebugEntity debugEntity;
        private IntPtr fileMapping;
        private IntPtr mapFile;
        private IntPtr fileMappingBack;
        private IntPtr mapFileBack;
        private delegate void UpdateUI();
        private Collection<RADPanel> radPanelList = new Collection<RADPanel>();


        private Semaphore semaphoreFull = Semaphore.OpenExisting("DebuggerFull");
        private Semaphore semaphoreEmpty = Semaphore.OpenExisting("DebuggerEmpty");
        private Thread dataSyncThread = null;

        private void InitFileMapping()
        {
            fileMapping = OpenFileMapping(0x000f001f, false, "DataSync");
            mapFile = MapViewOfFile(fileMapping, 0x000f001f, 0, 0, 1024 * 1024 * 4);

            fileMappingBack = OpenFileMapping(0x000f001f, false, "DataSyncBack");
            mapFileBack = MapViewOfFile(fileMappingBack, 0x000f001f, 0, 0, 1024 * 1024 * 4);
        }

        private void SyncNetData()
        {
            bool needHoldSMClient = false;
            int count = 0;

            while (true)
            {
                needHoldSMClient = false;
                try
                {
                    semaphoreFull.WaitOne();
                    HttpResponseMessage response = new HttpResponseMessage(GetNetWorkStream());
                    string rawData = response.GetContent();

                    Invoke(new UpdateUI(delegate()
                        {
                            richTextBox1.Text = richTextBox1.Text + "\r\n" + rawData;
                        }));
                    SetFlag(false);

                    try
                    {
                        Execute excuteResponse = SoapDecoder.Deserialize<Execute>(rawData);
                        if (excuteResponse.ClientRequestEntity != null &&
                            excuteResponse.ClientRequestEntity.Name == "debug")
                        {
                            foreach (MessageItem item in excuteResponse.ClientRequestEntity.Messages.Messages)
                            {
                                if (item.Text.StartsWith("Breakpoint reached for"))
                                {
                                    needHoldSMClient = true;
                                    break;
                                }
                            }
                            if (debugEntity != null)
                            {
                                Invoke(new UpdateUI(delegate()
                                {
                                    debugEntity.AddTrace(excuteResponse);
                                }));
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        //Nothing to do.
                    }
                    if (needHoldSMClient)
                    {
                        //break;
                        Thread.CurrentThread.Suspend();
                    }
                    count = 0;

                }
                catch (Exception ex)
                {
                    SetFlag(true);
                    count++;
                    if (count >= 2)
                    {
                        //
                    }
                }
                finally
                {
                    try
                    {
                        semaphoreEmpty.Release();
                    }
                    catch
                    {
                        //nothing to do
                    }
                }
            }
        }

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr OpenFileMapping(int dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, string lpName);

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr MapViewOfFile(IntPtr hFileMapping, uint dwDesiredAccess, uint dwFileOffsetHigh, uint dwFileOffsetLow, uint dwNumberOfBytesToMap);


        public Form1()
        {
            InitializeComponent();
            BindServers();
            dataSyncThread = new Thread(SyncNetData);
        }

        private void BindServers()
        {
            ServerConnections connectionsInfo = ServerConnections.Load();

            foreach (ConnectionInfo connection in connectionsInfo.Connections)
            {
                comboBoxServer.Items.Add(connection);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sessionId = textBoxCookieId.Text.Trim();
            string authorizationString = textBoxAuthorization.Text.Trim();

            ConnectionInfo currentConnectionInfo = comboBoxServer.SelectedItem as ConnectionInfo;
            SessionCache sessionCache = new SessionCache();
            ConnectionSession connectionSession = sessionCache[currentConnectionInfo];
            DataBus dataBus = connectionSession.GetDataBus(sessionId, authorizationString);

            DebugAction debugAction = new DebugAction(dataBus);
            debugAction.CommandLine = textBoxDebug.Text.Trim();
            HttpResponseMessage response = debugAction.DoAction();

            Execute excuteObject = debugAction.ResponseData as Execute;
            ActionUtil.StoreMessages(excuteObject.ClientRequestEntity);

            debugEntity.AddTrace(excuteObject);
            if (excuteObject.ClientRequestEntity == null)
            {
                try
                {
                    SyncBackData(response.CopiedStream, response.ChunkIndexes[1]);
                    //semaphoreEmpty.Release();
                    if (dataSyncThread.ThreadState == ThreadState.Suspended)
                    {
                        dataSyncThread.Resume();
                    }
                }
                catch
                {

                }
                return;
            }
            else if (!excuteObject.ClientRequestEntity.Messages.Rearm)
            {
                return;
            }
            else
            {
                //return;
            }

            while (true)
            {
                DebugResponseAction debugResponse = new DebugResponseAction(dataBus);
                response = debugResponse.DoAction();

                excuteObject = debugResponse.ResponseData as Execute;
                ActionUtil.StoreMessages(excuteObject.ClientRequestEntity);

                debugEntity.AddTrace(excuteObject);
                if (excuteObject.ClientRequestEntity == null)
                {
                    SyncBackData(response.CopiedStream, response.ChunkIndexes[1]);
                    //semaphoreEmpty.Release();
                    if (dataSyncThread.ThreadState == ThreadState.Suspended)
                    {
                        dataSyncThread.Resume();
                    }
                    break;
                }
                else if (excuteObject.ClientRequestEntity.Messages.Rearm == false)
                {
                    //semaphoreEmpty.Release();
                    //if (dataSyncThread.ThreadState == ThreadState.Suspended)
                    //{
                    //    dataSyncThread.Resume();
                    //}
                    //break;
                }
                else
                {
                    //Nothing to do.
                }
            }
        }

        private void richTextBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this.richTextBox1.SelectionStart.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] ipAddressByteArray = new byte[8];

            // IntPtr fileMapping = OpenFileMapping(0x000f001f, false, "DataSync");

            //IntPtr mapFile = MapViewOfFile(fileMapping, 0x000f001f, 0, 0, 0);
            // Marshal.Copy(mapFile, ipAddressByteArray, 0, 8);


            // IPAddress ipaddress2 = IPAddress.Parse("16.173.242.115");
            //// long xx = ipaddress2.Address;
            // long ipAddressLong = BitConverter.ToInt64(ipAddressByteArray, 0)>>16;
            // IPAddress ipAddress = new IPAddress(ipAddressLong);
            // string ip = ipAddress.ToString();

            // byte[] cookieArray = new byte[8];
            //Marshal.Copy(mapFile, ipAddressByteArray, 0, 8);


            //int cookieLength = GetCookieLength();
            IPAddress ipAddress = GetIpAddress();
            string cookie = GetCookie();
            string authrizationStr = GetAuthrizationString();
            int port = GetPort();

            textBoxAuthorization.Text = authrizationStr.Trim();
            textBoxCookieId.Text = cookie.Trim();
        }

        private IPAddress GetIpAddress()
        {
            byte[] ipAddressByteArray = new byte[8];
            IntPtr fileMapping = OpenFileMapping(0x000f001f, false, "DataSync");
            IntPtr mapFile = MapViewOfFile(fileMapping, 0x000f001f, 0, 0, 0);
            Marshal.Copy(mapFile, ipAddressByteArray, 0, 8);
            long ipAddressLong = BitConverter.ToInt64(ipAddressByteArray, 0) >> 16;
            IPAddress ipAddress = new IPAddress(ipAddressLong);
            return ipAddress;
        }

        private int GetCookieLength()
        {
            byte[] rawData = new byte[1024 * 1024 * 4];
            Marshal.Copy(mapFile, rawData, 0, 1024 * 1024 * 4);
            int cookieLength = BitConverter.ToInt32(rawData, 12);
            return cookieLength;
        }

        private int GetPort()
        {
            byte[] rawData = new byte[1024 * 1024 * 4];
            //IntPtr fileMapping = OpenFileMapping(0x000f001f, false, "DataSync");
            //IntPtr mapFile = MapViewOfFile(fileMapping, 0x000f001f, 0, 0, 1024 * 1024 * 4);
            Marshal.Copy(mapFile, rawData, 0, 1024 * 1024 * 4);
            int cookieLength = BitConverter.ToInt32(rawData, 8);
            return cookieLength;
        }

        private int GetAuthorizationLength()
        {
            byte[] rawData = new byte[1024 * 1024 * 4];
            //IntPtr fileMapping = OpenFileMapping(0x000f001f, false, "DataSync");
            //IntPtr mapFile = MapViewOfFile(fileMapping, 0x000f001f, 0, 0, 1024 * 1024 * 4);
            Marshal.Copy(mapFile, rawData, 0, 1024 * 1024 * 4);
            return BitConverter.ToInt32(rawData, 216);
        }

        private int GetDataLength()
        {
            byte[] rawData = new byte[1024 * 1024 * 4];
            // IntPtr fileMapping = OpenFileMapping(0x000f001f, false, "DataSync");
            //IntPtr mapFile = MapViewOfFile(fileMapping, 0x000f001f, 0, 0, 1024 * 1024 * 4);
            Marshal.Copy(mapFile, rawData, 0, 1024 * 1024 * 4);
            return BitConverter.ToInt32(rawData, 424);
        }

        private string GetCookie()
        {
            byte[] rawData = new byte[1024 * 1024 * 4];
            //IntPtr fileMapping = OpenFileMapping(0x000f001f, false, "DataSync");
            //IntPtr mapFile = MapViewOfFile(fileMapping, 0x000f001f, 0, 0, 1024 * 1024 * 4);
            Marshal.Copy(mapFile, rawData, 0, 1024 * 1024 * 4);

            int cookieLength = GetCookieLength();

            return Encoding.ASCII.GetString(rawData, 16, cookieLength);
        }
        private string GetAuthrizationString()
        {
            byte[] rawData = new byte[1024 * 1024 * 4];
            //IntPtr fileMapping = OpenFileMapping(0x000f001f, false, "DataSync");
            //IntPtr mapFile = MapViewOfFile(fileMapping, 0x000f001f, 0, 0, 1024 * 1024 * 4);
            Marshal.Copy(mapFile, rawData, 0, 1024 * 1024 * 4);

            int authorizationLength = GetAuthorizationLength();
            return Encoding.ASCII.GetString(rawData, 220, authorizationLength);
        }

        private Stream GetNetWorkStream()
        {
            byte[] rawData = new byte[1024 * 1024 * 4];
            //IntPtr fileMapping = OpenFileMapping(0x000f001f, false, "DataSync");
            //IntPtr mapFile = MapViewOfFile(fileMapping, 0x000f001f, 0, 0, 1024 * 1024 * 4);
            Marshal.Copy(mapFile, rawData, 0, 1024 * 1024 * 4);
            int netData = GetDataLength();
            MemoryStream memoryStream = new MemoryStream(rawData, 428, netData);
            return memoryStream;
        }

        private void SyncBackData(MemoryStream stream, int startIndex)
        {
            int streamLength = (int)stream.Length - startIndex;
            stream.Position = startIndex;
            //byte[] cotent = new byte[streamLength];

            byte[] newData = new byte[streamLength + 4];
            BitConverter.GetBytes(streamLength).CopyTo(newData, 0);

            stream.Read(newData, 4, streamLength);

            Marshal.Copy(newData, 0, mapFileBack, streamLength + 4);
        }

        private void SetFlag(bool flag)
        {
            byte[] rawData = new byte[1024 * 1024 * 4];
            //IntPtr fileMapping = OpenFileMapping(0x000f001f, false, "DataSync");
            //IntPtr mapFile = MapViewOfFile(fileMapping, 0x000f001f, 0, 0, 1024 * 1024 * 4);
            int flagValue = 0;
            if (flag)
            {
                flagValue = 1;
            }
            Marshal.WriteInt32(mapFile, 420, flagValue);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InitFileMapping();
            dataSyncThread.Start();
        }

        private RADPanel GetRADPanel(string radName, string panel)
        {
            string sessionId = textBoxCookieId.Text.Trim();
            string authorizationString = textBoxAuthorization.Text.Trim();

            ConnectionInfo currentConnectionInfo = comboBoxServer.SelectedItem as ConnectionInfo;
            SessionCache sessionCache = new SessionCache();
            ConnectionSession connectionSession = sessionCache[currentConnectionInfo];
            DataBus dataBus = connectionSession.GetDataBus(sessionId, authorizationString);

            JSCodeRunner codeRunner = new JSCodeRunner();
            codeRunner.Include("JSCode\\JsonEncode.js");
            codeRunner.Include("JSCode\\GetRADInfo.js");
            string codeToRun = " return GetRADInfo(\"" + radName + "\",\"" + panel+ "\");";
            RADPanel radPanel = codeRunner.Run<RADPanel>(dataBus, codeToRun);
            return radPanel;
        }

        private Format GetFormatInfo(string formatName, string language)
        {
            string sessionId = textBoxCookieId.Text.Trim();
            string authorizationString = textBoxAuthorization.Text.Trim();

            ConnectionInfo currentConnectionInfo = comboBoxServer.SelectedItem as ConnectionInfo;
            SessionCache sessionCache = new SessionCache();
            ConnectionSession connectionSession = sessionCache[currentConnectionInfo];
            DataBus dataBus = connectionSession.GetDataBus(sessionId, authorizationString);

            JSCodeRunner codeRunner = new JSCodeRunner();
            codeRunner.Include("JSCode\\JsonEncode.js");
            codeRunner.Include("JSCode\\GetFormatInfo.js");
            string codeToRun = " return GetFormatInfo(\"" + formatName + "\",\"" + language + "\");";
            Format format = codeRunner.Run<Format>(dataBus, codeToRun);
            return format;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //GetRADPanel("cc.router", "cleanup");
            //DebugEntity entity = new DebugEntity();
            //entity.Test();
           Format format = GetFormatInfo("Test", "en");
           FormatControl formatControl = new FormatControl(format);
           panel1.Controls.Add(formatControl);
        }

        private void buttonStartTrace_Click(object sender, EventArgs e)
        {
            string sessionId = textBoxCookieId.Text.Trim();
            string authorizationString = textBoxAuthorization.Text.Trim();

            ConnectionInfo currentConnectionInfo = comboBoxServer.SelectedItem as ConnectionInfo;
            SessionCache sessionCache = new SessionCache();
            ConnectionSession connectionSession = sessionCache[currentConnectionInfo];
            DataBus dataBus = connectionSession.GetDataBus(sessionId, authorizationString);

            debugEntity = new DebugEntity();
            debugEntity.OnTraceChanged += new TraceChangedHandler(DoTraceChanged);
            debugEntity.InitStack(dataBus);

            treeViewTraces.Nodes.Clear();
            treeViewTraces.Nodes.Add(new TreeNode());
            ShowTrace(debugEntity.TopRAD, treeViewTraces.Nodes[0]);
            currentRADNode = treeViewTraces.Nodes[0];
            while (currentRADNode.Nodes.Count > 0)
            {
                currentRADNode = currentRADNode.Nodes[0];
            }
            treeViewTraces.SelectedNode = currentRADNode;
        }

        private void DoTraceChanged(Object sender, TraceChangedInfo args)
        {
            //treeViewTraces.Nodes.Clear();
            //treeViewTraces.Nodes.Add(new TreeNode());
            //ShowTrace(debugEntity.TopRAD, treeViewTraces.Nodes[0]);
            if (args.Type == TraceChangedType.NewPanel)
            {
                TreeNode newPanelNode = new TreeNode();
                newPanelNode.Text = args.NewPanel.PanelInfo.PanelName;
                newPanelNode.Tag = args.NewPanel;
                currentRADNode.Nodes.Add(newPanelNode);
                currentPanelNode = newPanelNode;
                treeViewTraces.SelectedNode = newPanelNode;
            }
            else if (args.Type == TraceChangedType.NewRADCall)
            {
                TreeNode newRADNode = new TreeNode();
                newRADNode.Text = args.NewRad.RADName;
                newRADNode.Tag = args.NewRad;
                currentRADNode.Nodes.Add(newRADNode);
                currentRADNode = newRADNode;
                treeViewTraces.SelectedNode = newRADNode;
            }
            else
            {
                currentRADNode = currentRADNode.Parent;
            }
        }
        private void ShowTrace(DebugTraceRADCall rad, TreeNode treeNode)
        {
            treeNode.Text = rad.RADName;
            foreach (DebugTraceBase subTrace in rad.Traces)
            {
                TreeNode childNode = new TreeNode();
                if (subTrace is DebugTracePanel)
                {
                    childNode.Text = (subTrace as DebugTracePanel).PanelInfo.PanelName;
                   
                }
                else if (subTrace is DebugTraceRADCall)
                {
                    ShowTrace(subTrace as DebugTraceRADCall, childNode);
                }
                else
                {
                    //nothing to do
                }
                childNode.Tag = subTrace;
                treeNode.Nodes.Add(childNode);
            }
        }

        private void treeViewTraces_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            object tagObject = e.Node.Tag;
            if (tagObject is DebugTracePanel)
            {
                DebugTracePanel tracePanel = tagObject as DebugTracePanel;
                string radName = tracePanel.RADName;
                string panelName = tracePanel.PanelInfo.PanelName;
                tracePanel.PanelInfo = GetRADPanel(radName, panelName);

                string formatName = tracePanel.PanelInfo.Format;

                Format format = GetFormatInfo(formatName, "en");
                FormatControl formatControl = new FormatControl(format);
                panel1.Controls.Clear();
                panel1.Controls.Add(formatControl);

                PopulateFormData(tracePanel.PanelInfo, formatControl);

            }
        }

        private void PopulateFormData(RADPanel panelData,FormatControl formatControl)
        {
            Dictionary<string, string> fieldMappings = new Dictionary<string, string>();
            fieldMappings.Add("application", "RADName");
            fieldMappings.Add("label", "PanelName");
            fieldMappings.Add("comments", "Comments");
            fieldMappings.Add("normal", "Normal");
            fieldMappings.Add("error", "Error");
            fieldMappings.Add("format", "Format");
            fieldMappings.Add("file", "File");
            fieldMappings.Add("all_null", "AllNull");
            fieldMappings.Add("key_null", "KeyNull");
            fieldMappings.Add("key_dupl", "KeyDupl");
            fieldMappings.Add("second_file", "SecondFile");
            fieldMappings.Add("target_file", "TargetFile");
            fieldMappings.Add("record", "Record");
            fieldMappings.Add("query", "Query");
            fieldMappings.Add("name", "Name");
            fieldMappings.Add("names", "Names");
            fieldMappings.Add("values", "Values");
            fieldMappings.Add("prompt", "Prompt");
            fieldMappings.Add("condition", "Conditions");
            fieldMappings.Add("option", "Option");
            fieldMappings.Add("description", "Description");
            fieldMappings.Add("exit", "Exit");
            fieldMappings.Add("empty", "Empty");
            fieldMappings.Add("one_rec", "OneRec");
            fieldMappings.Add("text", "Text");
            fieldMappings.Add("statements", "Statements");
            fieldMappings.Add("cond_input", "CondInput");
            fieldMappings.Add("sort", "Sort");
            fieldMappings.Add("types", "Types");
            fieldMappings.Add("levels", "Levels");
            fieldMappings.Add("numbers", "Numbers");
            fieldMappings.Add("number1", "Number1");
            fieldMappings.Add("string1", "String1");
            fieldMappings.Add("time1", "Time1");
            fieldMappings.Add("boolean1", "Boolean1");
            fieldMappings.Add("times", "Times");
            fieldMappings.Add("expressions", "Expressions");
            fieldMappings.Add("comments_more", "CommentsMore");
            fieldMappings.Add("file_variables", "FileVariables");
            fieldMappings.Add("second_record", "SecondRecord");
            fieldMappings.Add("booleans", "Booleans");
            fieldMappings.Add("table", "Table");
            fieldMappings.Add("tables", "Tables");

            foreach (Control subControl in formatControl.Controls)
            {
                PopulateChildData(panelData, subControl, fieldMappings);
            }
        }

        private void PopulateChildData(RADPanel panelData, Control childControl, Dictionary<string, string> fieldMappings)
        {
            string controlName = childControl.Name;
            if (fieldMappings.ContainsKey(controlName))
            {
                string targetPropertyName = fieldMappings[controlName];
                PropertyInfo[] properties = typeof(RADPanel).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                PropertyInfo targetProperty = null;
                foreach (PropertyInfo temProperty in properties)
                {
                    if (temProperty.Name == targetPropertyName)
                    {
                        targetProperty = temProperty;
                        break;
                    }
                }
                if (targetProperty != null)
                {
                    object propertyValue = targetProperty.GetValue(panelData,null);
                    if (propertyValue != null)
                    {
                        if (childControl is TextBox)
                        {
                            if (propertyValue.GetType() == typeof(int) || propertyValue.GetType() == typeof(string))
                            {
                                (childControl as TextBox).Text = propertyValue.ToString();
                            }
                            else //Collection<string>
                            {
                                (childControl as TextBox).Text = (propertyValue as Collection<string>)[0];
                            }
                        }
                        else if (childControl is RichTextBox)
                        {
                            if (propertyValue.GetType() == typeof(int) || propertyValue.GetType() == typeof(string))
                            {
                                (childControl as RichTextBox).Text = propertyValue.ToString();
                            }
                            else //Collection<string>
                            {
                                RichTextBox richTextBox = childControl as RichTextBox;
                                Collection<string> values  = propertyValue as Collection<string>;

                                foreach (string itemValue in values)
                                {
                                    richTextBox.Text = richTextBox.Text + itemValue + "\r\n";
                                }
                            }
                        }
                    }
                }
            }
            foreach (Control subControl in childControl.Controls)
            {
                PopulateChildData(panelData, subControl, fieldMappings);
            }
        }
    }
}
