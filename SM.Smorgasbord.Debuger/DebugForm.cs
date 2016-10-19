using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Runtime.InteropServices;
using System.Collections.ObjectModel;
using System.Threading;
using SM.Smorgasbord.Communication.Common.Http;
using System.IO;
using SM.Smorgasbord.Communication.SoapEntities.Response;
using SM.Smorgasbord.Communication.SoapEntities;
using System.Diagnostics;
using SM.Smorgasbord.Communication.Common;
using SM.Smorgasbord.Communication.Sessions;
using SM.Smorgasbord.Communication.Lib;
using System.Reflection;
using SM.Smorgasbord.Communication.Actions;
using SM.Smorgasbord.Communication.Utils;

namespace SM.Smorgasbord.Debuger
{
    public partial class DebugForm : Form
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

        private Semaphore semaphoreFull = null;
        private Semaphore semaphoreEmpty = null;
        private Thread dataSyncThread = null;
        private Thread autoStepThread = null;

        //Indicate if client ever held.
        private bool clientEverHeld = false;

        //Indicate if sm client need be hold, if hold the sm client then debugger will comminicate with sm directly.
        //This will happen when start to debug.
        private bool needHoldSMClient = false;

        private bool debuggerEnable = false;

        public DebugForm()
        {
            InitializeComponent();

            DebugEntity entity = new DebugEntity();

            entity.OnTraceChanged += new TraceChangedHandler(DoTraceChanged);
            //entity.TestBatch(@"C:\Users\daizhen\VS Projects\2008\SM.Smorgasbord\SM.Smorgasbord.Debuger\TestFiles\sample_2.txt");
        }

        #region Private methods

        /// <summary>
        /// Init file mappings, so that can interacte with SM debugger core.
        /// </summary>
        private void InitFileMapping()
        {
            fileMapping = OpenFileMapping(0x000f001f, false, "DataSync");
            mapFile = MapViewOfFile(fileMapping, 0x000f001f, 0, 0, 1024 * 1024 * 4);

            fileMappingBack = OpenFileMapping(0x000f001f, false, "DataSyncBack");
            mapFileBack = MapViewOfFile(fileMappingBack, 0x000f001f, 0, 0, 1024 * 1024 * 4);
        }

        private void InitSemaphore()
        {
            semaphoreFull = Semaphore.OpenExisting("DebuggerFull");
            semaphoreEmpty = Semaphore.OpenExisting("DebuggerEmpty");
        }

        private DataSyncHeader GetHeader()
        {
            DataSyncHeader syncHeader = new DataSyncHeader();
            byte[] ipAddressByteArray = new byte[8];
            //IntPtr fileMapping = OpenFileMapping(0x000f001f, false, "DataSync");
            //IntPtr mapFile = MapViewOfFile(fileMapping, 0x000f001f, 0, 0, 0);
            Marshal.PtrToStructure(mapFile, syncHeader);
            return syncHeader;
        }

        private void WriteDebugHeaderToSharedMemory(DataSyncHeader header)
        {
            IntPtr fileMapping = OpenFileMapping(0x000f001f, false, "DataSync");
            IntPtr mapFile = MapViewOfFile(fileMapping, 0x000f001f, 0, 0, 0);
            Marshal.StructureToPtr(header, mapFile, true);
        }

        private IPAddress GetIpAddress()
        {
            DataSyncHeader syncHeader = GetHeader();
            //byte[] ipAddressByteArray = new byte[8];
            //IntPtr fileMapping = OpenFileMapping(0x000f001f, false, "DataSync");
            //IntPtr mapFile = MapViewOfFile(fileMapping, 0x000f001f, 0, 0, 0);
            //Marshal.Copy(mapFile, ipAddressByteArray, 0, 8);
            //long ipAddressLong = BitConverter.ToInt64(ipAddressByteArray, 0) >> 16;
            long ipAddressLong = syncHeader.IPAddress >> 16;
            IPAddress ipAddress = new IPAddress(ipAddressLong);
            return ipAddress;
        }
        #region Comments the code
        //private int GetCookieLength()
        //{
        //    byte[] rawData = new byte[1024 * 1024 * 4];
        //    Marshal.Copy(mapFile, rawData, 0, 1024 * 1024 * 4);
        //    int cookieLength = BitConverter.ToInt32(rawData, 12);
        //    return cookieLength;
        //}

        //private int GetPort()
        //{
        //    byte[] rawData = new byte[1024 * 1024 * 4];
        //    //IntPtr fileMapping = OpenFileMapping(0x000f001f, false, "DataSync");
        //    //IntPtr mapFile = MapViewOfFile(fileMapping, 0x000f001f, 0, 0, 1024 * 1024 * 4);
        //    Marshal.Copy(mapFile, rawData, 0, 1024 * 1024 * 4);
        //    int cookieLength = BitConverter.ToInt32(rawData, 8);
        //    return cookieLength;
        //}

        //private int GetAuthorizationLength()
        //{
        //    byte[] rawData = new byte[1024 * 1024 * 4];
        //    //IntPtr fileMapping = OpenFileMapping(0x000f001f, false, "DataSync");
        //    //IntPtr mapFile = MapViewOfFile(fileMapping, 0x000f001f, 0, 0, 1024 * 1024 * 4);
        //    Marshal.Copy(mapFile, rawData, 0, 1024 * 1024 * 4);
        //    return BitConverter.ToInt32(rawData, 216);
        //}


        //private string GetCookie()
        //{
        //    byte[] rawData = new byte[1024 * 1024 * 4];
        //    //IntPtr fileMapping = OpenFileMapping(0x000f001f, false, "DataSync");
        //    //IntPtr mapFile = MapViewOfFile(fileMapping, 0x000f001f, 0, 0, 1024 * 1024 * 4);
        //    Marshal.Copy(mapFile, rawData, 0, 1024 * 1024 * 4);

        //    int cookieLength = GetCookieLength();

        //    return Encoding.ASCII.GetString(rawData, 16, cookieLength);
        //}

        //private string GetAuthrizationString()
        //{
        //    byte[] rawData = new byte[1024 * 1024 * 4];
        //    Marshal.Copy(mapFile, rawData, 0, 1024 * 1024 * 4);

        //    int authorizationLength = GetAuthorizationLength();
        //    return Encoding.ASCII.GetString(rawData, 220, authorizationLength);
        //}
        #endregion

        private int GetDataLength()
        {
            byte[] rawData = new byte[1024 * 1024 * 4];
            //Get the offset of the Length data.
            //Plus 4 mean add the 4 bytes of FLAG field.
            int offSet = Marshal.SizeOf(typeof(DataSyncHeader)) + 4;
            Marshal.Copy(mapFile, rawData, 0, 1024 * 1024 * 4);
            return BitConverter.ToInt32(rawData, offSet);
        }

        /// <summary>
        /// Sync data to SM client. 
        /// 
        /// </summary>
        private void SyncNetData()
        {
            int count = 0;
            while (true)
            {
                //In the beginning, not hold the sm client.
                //needHoldSMClient = false;
                try
                {
                    //Wait until semaphorefull is signaled.
                    //Means new data is avaible in shared memory.
                    semaphoreFull.WaitOne();

                    //Build the http message.
                    HttpResponseMessage response = new HttpResponseMessage(GetNetWorkStream());
                    //Get the raw content of http message.
                    string rawData = response.GetContent();

                    //Just for testing. show the data to UI.
                    Invoke(new UpdateUI(delegate()
                    {
                        //richTextBox1.Text = richTextBox1.Text + "\r\n" + rawData;
                    }));
                    SetFlag(false);

                    try
                    {
                        //Deserialize the http message to Execute object.
                        Execute excuteResponse = SoapDecoder.Deserialize<Execute>(rawData);
                        if (excuteResponse.ClientRequestEntity != null &&
                            excuteResponse.ClientRequestEntity.Name == "debug")
                        {
                            if (!clientEverHeld)
                            {
                                foreach (MessageItem item in excuteResponse.ClientRequestEntity.Messages.Messages)
                                {
                                    if (item.Text.StartsWith("Breakpoint reached for"))
                                    {
                                        //When encounter the debug start message then hold sm client.
                                        needHoldSMClient = true;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                needHoldSMClient = true;
                            }

                            //Add trace response to debug entity.
                            if (debugEntity != null)
                            {
                                Invoke(new UpdateUI(delegate()
                                {
                                    debugEntity.AddTrace(excuteResponse);
                                }));
                                autoStepThread = new Thread(new ThreadStart(AutoGotoNextStep));
                                autoStepThread.Start();
                            }
                        }
                        else
                        {
                            needHoldSMClient = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        needHoldSMClient = false;
                        //Nothing to do.
                    }
                    clientEverHeld = (clientEverHeld || needHoldSMClient);

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
                        //Release semaphoreempty, so that debugger core can put data to shared memory.
                        semaphoreEmpty.Release();
                    }
                    catch
                    {
                        //nothing to do
                    }
                }
            }
        }

        /// <summary>
        /// Get the network data from shared memory.
        /// The data is catched by SM debugger core and put it into shared memory.
        /// </summary>
        /// <returns></returns>
        private Stream GetNetWorkStream()
        {
            DataSyncBody syncBody = new DataSyncBody();;
            byte[] rawData = new byte[1024 * 1024 * 4];
            //string xx = Encoding.ASCII.GetString(rawData);

            Marshal.Copy(mapFile, rawData, 0, 1024 * 1024 * 4);
            int netDataLength = GetDataLength();

            //Gets the offset of real network data.
            //Plus 8 means add FLAG and LENGTH fields.
            int offSet = Marshal.SizeOf(typeof(DataSyncHeader)) + 8;

            MemoryStream memoryStream = new MemoryStream(rawData, offSet, netDataLength);
            rawData = null;
            GC.Collect();
            return memoryStream;
        }

        /// <summary>
        /// If true then SM client need append the additional data, else clean up the existing data and add new data to shared folder.
        /// </summary>
        /// <param name="flag"></param>
        private void SetFlag(bool flag)
        {
            int flagValue = 0;
            if (flag)
            {
                flagValue = 1;
            }
            //Offset of FLAG.
            int offSet = Marshal.SizeOf(typeof(DataSyncHeader));
            Marshal.WriteInt32(mapFile, offSet, flagValue);
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

        /// <summary>
        /// Get RAD panel from SM server.
        /// </summary>
        /// <param name="radName"></param>
        /// <param name="panel"></param>
        /// <returns></returns>
        private RADPanel GetRADPanel(string radName, string panel)
        {
            using (DataBus dataBus = GetDataBus())
            {
                JSCodeRunner codeRunner = new JSCodeRunner();
                codeRunner.Include("JSCode\\JsonEncode.js");
                codeRunner.Include("JSCode\\GetRADInfo.js");
                string codeToRun = " return GetRADInfo(\"" + radName + "\",\"" + panel + "\");";
                RADPanel radPanel = codeRunner.Run<RADPanel>(dataBus, codeToRun);
                return radPanel;
            }
        }

        private string ReadLine(Stream stream)
        {
            Collection<byte> data = new Collection<byte>();
            int streamReturnValue = stream.ReadByte();
            byte currentByte = (byte)streamReturnValue;

            while (streamReturnValue != -1 && currentByte != '\n')
            {
                if (currentByte != '\r')
                {
                    data.Add(currentByte);
                }
                streamReturnValue = stream.ReadByte();
                currentByte = (byte)streamReturnValue;
            }
            return Encoding.ASCII.GetString(data.ToArray());
        }

        private void SyncBackChunckedData(MemoryStream stream, Collection<int> chunckStartIndexes)
        {

            if (chunckStartIndexes[1] - chunckStartIndexes[0] == 15)
            {
                SyncBackData(stream, chunckStartIndexes[1]);
            }
            else
            {
                stream.Position = chunckStartIndexes[0];
                string hexValueString = ReadLine(stream);
                int chunkSize = Convert.ToInt32(hexValueString, 16);
                int syncBackChunkLength = chunkSize - 10;
                string hexValueStringBack = Convert.ToString(syncBackChunkLength, 16);

                //Skip 10 bytes.
                stream.Position += 10;
                int streamLength = (int)(stream.Length - stream.Position) + hexValueStringBack.Length + 2;

                byte[] newData = new byte[streamLength + 8];

                //Total length sync to SM client
                BitConverter.GetBytes(streamLength).CopyTo(newData, 0);
                //First chunk Size
                Encoding.ASCII.GetBytes(hexValueStringBack).CopyTo(newData, 8);
                //Break chars
                //\r
                newData[8 + hexValueStringBack.Length] = 0x0D;
                //\n
                newData[8 + hexValueStringBack.Length + 1] = 0x0A;

                stream.Read(newData, 8 + hexValueStringBack.Length + 2, streamLength - hexValueStringBack.Length - 2);
                Marshal.Copy(newData, 0, mapFileBack, streamLength + 8);
            }
        }

        /// <summary>
        /// Sync data back to SM client.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="startIndex"></param>
        private void SyncBackData(MemoryStream stream, int startIndex)
        {
            int streamLength = (int)stream.Length - startIndex;
            stream.Position = startIndex;

            byte[] newData = new byte[streamLength + 8];
            BitConverter.GetBytes(streamLength).CopyTo(newData, 0);

            stream.Read(newData, 8, streamLength);

            Marshal.Copy(newData, 0, mapFileBack, streamLength + 8);

        }


        private void SendDebugCommandASync(string debugCommand)
        {
            Thread thread = new Thread(new ParameterizedThreadStart(SendDebugCommand));
            thread.Start(debugCommand);
        }

        private void SendDebugCommand(object debugCommandObj)
        {
            string debugCommand = debugCommandObj.ToString();
            using (DataBus dataBus = GetDataBus())
            {
                DebugAction debugAction = new DebugAction(dataBus);
                debugAction.CommandLine = debugCommand;
                HttpResponseMessage response = debugAction.DoAction();

                Execute excuteObject = debugAction.ResponseData as Execute;
                ActionUtil.StoreMessages(excuteObject.ClientRequestEntity);

                if (excuteObject.ClientRequestEntity == null)
                {
                    try
                    {
                        needHoldSMClient = false;

                        //SyncBackData(response.CopiedStream, response.ChunkIndexes[1]);
                        SyncBackChunckedData(response.CopiedStream, response.ChunkIndexes);
                        if (dataSyncThread.ThreadState == System.Threading.ThreadState.Suspended)
                        {
                            dataSyncThread.Resume();
                        }
                    }
                    catch
                    {
                        //Nothing to do
                    }
                    return;
                }
                else if (!excuteObject.ClientRequestEntity.Messages.Rearm)
                {
                    debugEntity.AddTrace(excuteObject);
                    return;
                }
                else if (excuteObject.ClientRequestEntity.Name != "message")
                {

                    while (true)
                    {
                        DebugResponseAction debugResponse = new DebugResponseAction(dataBus);
                        response = debugResponse.DoAction();

                        excuteObject = debugResponse.ResponseData as Execute;
                        ActionUtil.StoreMessages(excuteObject.ClientRequestEntity);

                        if (excuteObject.ClientRequestEntity == null)
                        {
                            needHoldSMClient = false;
                            //SyncBackData(response.CopiedStream, response.ChunkIndexes[1]);
                            SyncBackChunckedData(response.CopiedStream, response.ChunkIndexes);
                            //semaphoreEmpty.Release();
                            if (dataSyncThread.ThreadState == System.Threading.ThreadState.Suspended)
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
                        debugEntity.AddTrace(excuteObject);
                    }
                }
                else
                {
                    //Nothing to do.
                }
            }
        }

        private void RemoveAllBreakPoints()
        {
            SendDebugCommand("rb");
        }

        private void GotoNextPanel()
        {
            SendDebugCommand("s");
        }

        private void ContinueDebug()
        {
            SendDebugCommand("c");
        }

        private void EnableTrace()
        {
            SendDebugCommand("t on");
        }

        private void DisableTrace()
        {
            SendDebugCommand("t off");
        }

        private void SetBreakPointAtBeginning()
        {
            SendDebugCommand("b co");
        }

        private void StepOverRAD()
        {
            RemoveAllBreakPoints();

            if (currentRADNode.Parent != null)
            {
                SendDebugCommand("ba " + currentRADNode.Parent.Text);
            }
        }

        /// <summary>
        /// Get Data bus by session id and authorization string.
        /// </summary>
        /// <returns></returns>
        private DataBus GetDataBus()
        {
            //Reset the data from shared memory firstly
            ResetConnectionInfo();
            string sessionId = textBoxCookieId.Text.Trim();
            string authorizationString = textBoxAuthorization.Text.Trim();

            ConnectionInfo currentConnectionInfo = new ConnectionInfo();
            currentConnectionInfo.Domain = textBoxIP.Text.Trim();
            currentConnectionInfo.Port = Convert.ToInt32(textBoxPort.Text.Trim());
            currentConnectionInfo.Name = Guid.NewGuid().ToString();
            SessionCache sessionCache = new SessionCache();
            ConnectionSession connectionSession = sessionCache[currentConnectionInfo];
            DataBus dataBus = connectionSession.GetDataBus(sessionId, authorizationString);
            return dataBus;
        }

        private Format GetFormatInfo(string formatName, string language)
        {
            using (DataBus dataBus = GetDataBus())
            {
                JSCodeRunner codeRunner = new JSCodeRunner();
                codeRunner.Include("JSCode\\JsonEncode.js");
                codeRunner.Include("JSCode\\GetFormatInfo.js");
                string codeToRun = " return GetFormatInfo(\"" + formatName + "\",\"" + language + "\");";
                Format format = codeRunner.Run<Format>(dataBus, codeToRun);
                return format;
            }
        }

        private RTContext GetContext(string radName, string panelName)
        {
            using (DataBus dataBus = GetDataBus())
            {
                JSCodeRunner codeRunner = new JSCodeRunner();
                codeRunner.Include("JSCode\\JsonEncode.js");
                codeRunner.Include("JSCode\\GetRTContext.js");
                string codeToRun = " return GetContext(\"" + radName + "\",\"" + panelName + "\");";
                RTContext context = codeRunner.Run<RTContext>(dataBus, codeToRun);
                return context;
            }
            //return null;
        }

        private void PopulateFormData(RADPanel panelData, FormatControl formatControl)
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
                    object propertyValue = targetProperty.GetValue(panelData, null);
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
                        else if (childControl is ScintillaNET.Scintilla)
                        {
                            if (propertyValue.GetType() == typeof(int) || propertyValue.GetType() == typeof(string))
                            {
                                (childControl as ScintillaNET.Scintilla).Text = propertyValue.ToString();
                            }
                            else //Collection<string>
                            {
                                ScintillaNET.Scintilla richTextBox = childControl as ScintillaNET.Scintilla;
                                Collection<string> values = propertyValue as Collection<string>;

                                foreach (string itemValue in values)
                                {

                                    if (!string.IsNullOrEmpty(itemValue) && !string.IsNullOrEmpty(itemValue.Trim()))
                                    {
                                        richTextBox.Text = richTextBox.Text + itemValue + ";\r\n";
                                    }
                                    else
                                    {
                                        richTextBox.Text = richTextBox.Text + itemValue + "\r\n";
                                    }
                                }
                            }
                        }
                        else if (childControl is ListBox)
                        {
                            if (propertyValue.GetType() == typeof(int) || propertyValue.GetType() == typeof(string))
                            {
                                (childControl as ListBox).Items.Add(propertyValue.ToString());
                            }
                            else //Collection<string>
                            {
                                ListBox listBox = childControl as ListBox;
                                Collection<string> values = propertyValue as Collection<string>;

                                foreach (string itemValue in values)
                                {
                                    listBox.Items.Add(itemValue);
                                }
                            }
                        }
                        else
                        {

                        }
                    }
                }
            }
            foreach (Control subControl in childControl.Controls)
            {
                PopulateChildData(panelData, subControl, fieldMappings);
            }
        }

        private void ShowContext(DebugTracePanel tracePanel)
        {
            listViewContext.Items.Clear();
            foreach (KeyValuePair<string, string> keyValuePair in tracePanel.PreContext.NamedVarValues)
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.Text = keyValuePair.Key;
                listViewItem.SubItems.Add(keyValuePair.Value);
                if (tracePanel.PostContext != null && tracePanel.PostContext.NamedVarValues.Keys.Contains(keyValuePair.Key))
                {
                    listViewItem.SubItems.Add(tracePanel.PostContext.NamedVarValues[keyValuePair.Key]);
                }
                listViewContext.Items.Add(listViewItem);
            }
        }

        private void EnableDebug()
        {
            //To Disable it.
            if (debuggerEnable)
            {
                DataSyncHeader header = GetHeader();
                //Disable debugger.
                header.Enable = 0;
                WriteDebugHeaderToSharedMemory(header);

                //Client is in hold.
                if (needHoldSMClient)
                {
                    RemoveAllBreakPoints();
                    DisableTrace();
                    ContinueDebug();
                    needHoldSMClient = false;
                    if (dataSyncThread.ThreadState == System.Threading.ThreadState.Suspended)
                    {
                        dataSyncThread.Resume();
                    }
                }
            }
            else //enable it.
            {
                DataSyncHeader header = GetHeader();
                //Disable debugger.
                header.Enable = 1;
                WriteDebugHeaderToSharedMemory(header);
            }

            debuggerEnable = (!debuggerEnable);
        }


        private void StopDebugger()
        {
            DataSyncHeader header = GetHeader();
            //Disable debugger.
            header.Enable = 0;
            WriteDebugHeaderToSharedMemory(header);

            //Client is in hold.
            if (needHoldSMClient)
            {
                try
                {
                    semaphoreEmpty.Release();
                }
                catch (Exception ex)
                {

                }
            }
            if (dataSyncThread != null)
            {
                //Abort the thread.
                dataSyncThread.Abort();
            }
        }

        private void ResetConnectionInfo()
        {
            byte[] ipAddressByteArray = new byte[8];
            IPAddress ipAddress = GetIpAddress();
            DataSyncHeader syncHeader = GetHeader();
            string cookie = syncHeader.Cookie;
            string authrizationStr = syncHeader.Authorization;
            int port = syncHeader.Port;

            Invoke(new UpdateUI(delegate()
            {
                textBoxIP.Text = ipAddress.ToString();
                textBoxPort.Text = port.ToString();

                textBoxAuthorization.Text = authrizationStr.Trim();
                textBoxCookieId.Text = cookie.Trim();
            }));
        }

        private void AutoGotoNextStep()
        {
            if (checkboxAutoStep.Checked && needHoldSMClient)
            {
                GotoNextPanel();
            }
        }

        private bool IsSMClientStarted()
        {
            foreach (Process thisproc in System.Diagnostics.Process.GetProcesses())
            {
                if (thisproc.ProcessName.StartsWith("ServiceManager"))
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region P/Invoke

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr OpenFileMapping(int dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, string lpName);

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr MapViewOfFile(IntPtr hFileMapping, uint dwDesiredAccess, uint dwFileOffsetHigh, uint dwFileOffsetLow, uint dwNumberOfBytesToMap);

        #endregion

        #region Event Handler

        private void buttonStartTrace_Click(object sender, EventArgs e)
        {
            using (DataBus dataBus = GetDataBus())
            {
                debugEntity = new DebugEntity();
                debugEntity.OnTraceChanged += new TraceChangedHandler(DoTraceChanged);
                debugEntity.InitStack(dataBus);

                treeViewTraces.Nodes.Clear();
                treeViewTraces.Nodes.Add(new TreeNode());
                ShowTrace(debugEntity.TopRAD, treeViewTraces.Nodes[0]);
                currentRADNode = treeViewTraces.Nodes[0];

                //Set the current RAD node to the leaf node.
                while (currentRADNode.Nodes.Count > 0)
                {
                    currentRADNode = currentRADNode.Nodes[0];
                }
                treeViewTraces.SelectedNode = currentRADNode;
                clientEverHeld = false;
                needHoldSMClient = false;
                if (dataSyncThread.ThreadState == System.Threading.ThreadState.Suspended)
                {
                    dataSyncThread.Resume();
                }
            }
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
            DataSyncHeader syncHeader = GetHeader();
            string cookie = syncHeader.Cookie;
            string authrizationStr = syncHeader.Authorization;
            int port = syncHeader.Port;

            textBoxIP.Text = ipAddress.ToString();
            textBoxPort.Text = port.ToString();

            textBoxAuthorization.Text = authrizationStr.Trim();
            textBoxCookieId.Text = cookie.Trim();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (!IsSMClientStarted())
            {
                ProcessStartInfo processStartInfoSMClient = new ProcessStartInfo();
                processStartInfoSMClient.FileName = @"C:\Program Files (x86)\HP\Service Manager 9.41\Client\ServiceManager.cmd";
                Process.Start(processStartInfoSMClient);
                Thread.Sleep(2000);
            }
            ProcessStartInfo processStartInfoDebuggerCore = new ProcessStartInfo();
            processStartInfoDebuggerCore.FileName = @"C:\Users\daizhen\VS Projects\2008\SMDebugger.Msg\Debug\SMDebugger.Msg.exe";
            Process.Start(processStartInfoDebuggerCore);
            Thread.Sleep(1000);

            InitFileMapping();
            InitSemaphore();

            dataSyncThread = new Thread(SyncNetData);
            dataSyncThread.Start();
        }

        private void DoTraceChanged(Object sender, TraceChangedInfo args)
        {
            if (args.Type == TraceChangedType.NewPanel)
            {
                TreeNode newPanelNode = new TreeNode();
                newPanelNode.Text = args.NewPanel.PanelInfo.PanelName;
                args.NewPanel.PreContext = GetContext(args.NewPanel.RADName, args.NewPanel.PanelInfo.PanelName);
                if (args.OldPanel != null)
                {
                    args.OldPanel.PostContext = GetContext(args.OldPanel.RADName, args.OldPanel.PanelInfo.PanelName);
                }
                Invoke(new UpdateUI(delegate()
                {
                    newPanelNode.Tag = args.NewPanel;
                    currentRADNode.Nodes.Add(newPanelNode);
                    currentPanelNode = newPanelNode;
                    treeViewTraces.SelectedNode = newPanelNode;
                }));
            }
            else if (args.Type == TraceChangedType.NewRADCall)
            {
                //To Avoid the first RAD call.
                if (args.NewRad.RADName != currentRADNode.Text)
                {
                    Invoke(new UpdateUI(delegate()
                    {
                        TreeNode newRADNode = new TreeNode();
                        newRADNode.Text = args.NewRad.RADName;

                        newRADNode.Tag = args.NewRad;
                        currentRADNode.Nodes.Add(newRADNode);
                        currentRADNode = newRADNode;
                        treeViewTraces.SelectedNode = newRADNode;
                    }));
                }
            }
            else if (args.Type == TraceChangedType.RADCallReturn)
            {
                //if (args.OldPanel != null)
                //{
                //    args.OldPanel.PostContext = GetContext(args.OldPanel.RADName, args.OldPanel.PanelInfo.PanelName);
                //}
                currentRADNode = currentRADNode.Parent;
            }
            else
            {
                //Nothing to do.
            }
            //if (needHoldSMClient && checkboxAutoStep.Checked)
            //{
            //    GotoNextPanel();
            //}
        }

        private void treeViewTraces_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            object tagObject = e.Node.Tag;
            if (tagObject is DebugTracePanel)
            {
                DebugTracePanel tracePanel = tagObject as DebugTracePanel;
                string radName = tracePanel.RADName;
                string panelName = tracePanel.PanelInfo.PanelName;
                RADPanel radPanel = GetRADPanel(radName, panelName);
                if (radPanel != null)
                {
                    tracePanel.PanelInfo = radPanel;
                }
                string formatName = tracePanel.PanelInfo.Format;

                Format format = GetFormatInfo(formatName, "en");
                FormatControl formatControl = new FormatControl(format);
                formatControl.DebugTrace = tracePanel;
                panelRADDetail.Controls.Clear();
                panelRADDetail.Controls.Add(formatControl);
                PopulateFormData(tracePanel.PanelInfo, formatControl);

                ShowContext(tracePanel);
            }
        }

        private void buttonSingleStep_Click(object sender, EventArgs e)
        {
            using (DataBus dataBus = GetDataBus())
            {
                debugEntity = new DebugEntity();
                debugEntity.OnTraceChanged += new TraceChangedHandler(DoTraceChanged);
                debugEntity.InitStack(dataBus);

                treeViewTraces.Nodes.Clear();
                treeViewTraces.Nodes.Add(new TreeNode());
                ShowTrace(debugEntity.TopRAD, treeViewTraces.Nodes[0]);
                currentRADNode = treeViewTraces.Nodes[0];

                //Set the current RAD node to the leaf node.
                while (currentRADNode.Nodes.Count > 0)
                {
                    currentRADNode = currentRADNode.Nodes[0];
                }
                treeViewTraces.SelectedNode = currentRADNode;

                EnableTrace();
                RemoveAllBreakPoints();
                SetBreakPointAtBeginning();

                clientEverHeld = false;
                needHoldSMClient = false;
                if (dataSyncThread.ThreadState == System.Threading.ThreadState.Suspended)
                {
                    dataSyncThread.Resume();
                }
            }
        }

        private void buttonNextPanel_Click(object sender, EventArgs e)
        {
            if (needHoldSMClient)
            {
                GotoNextPanel();
            }
            else
            {
                MessageBox.Show("Out of scope");
            }
        }

        private void buttonStepOver_Click(object sender, EventArgs e)
        {
            StepOverRAD();
        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {
            ContinueDebug();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            EnableDebug();

            if (debuggerEnable)
            {
                buttonStop.Text = "Disable";
            }
            else
            {
                buttonStop.Text = "Enable";
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            StopDebugger();
        }

        #endregion
    }
}
