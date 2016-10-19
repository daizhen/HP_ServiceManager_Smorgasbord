using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using SM.Smorgasbord.Communication.Common;
using SM.Smorgasbord.Communication.Actions;
using SM.Smorgasbord.Communication.Common.Http;
using SM.Smorgasbord.Communication.SoapEntities.Response;
using System.Text.RegularExpressions;
using System.IO;

namespace SM.Smorgasbord.Debuger
{
    public delegate void TraceChangedHandler(Object sender, TraceChangedInfo args);

    public class DebugEntity
    {
        private bool followingReturn = true;
        private Stack<string> radStack = new Stack<string>();
        private DebugTraceBase currentTrance;
        private DebugTraceRADCall currentRAD;
        private DebugTraceRADCall topRAD;
        private Regex radCallReg = new Regex(@"^\|(\d+)\|\s+RAD:\s+(.+)$", RegexOptions.Singleline);
        private Regex returnReg = new Regex(@"^\|(\d+)\|\s+((<return>)||(<null return>))$");
        private Regex panelReg = new Regex(@"^\|(\d+)\|\s+(.+)$");
        public event TraceChangedHandler OnTraceChanged;

        #region Properties

        public bool IsAutoStepDown
        {
            get;
            set;
        }

        public bool SMClientHeld
        {
            get;
            set;
        }

        public Stack<string> RadStacks
        {
            get
            {
                return radStack;
            }
        }

        public DebugTraceBase CurrentTrance
        {
            get
            {
                return currentTrance;
            }
        }

        public DebugTraceRADCall CurrentRAD
        {
            get
            {
                return currentRAD;
            }
        }
        public DebugTraceRADCall TopRAD
        {
            get
            {
                return topRAD;
            }
        }

        #endregion

        private Collection<DebugTraceBase> traces = new Collection<DebugTraceBase>();

        public Collection<DebugTraceBase> Traces
        {
            get
            {
                return traces;
            }
        }

        public void InitStack(DataBus dataBus)
        {
            DebugAction debugAction = new DebugAction(dataBus);
            debugAction.CommandLine = "stack";
            HttpResponseMessage response = debugAction.DoAction();

            Execute excuteObject = debugAction.ResponseData as Execute;
            //ActionUtil.StoreMessages(excuteObject.ClientRequestEntity);
            ClientRequest clientRequest = excuteObject.ClientRequestEntity;


            if (clientRequest != null)
            {
                DebugTraceRADCall parentRadCall = new DebugTraceRADCall();
                parentRadCall.RADName = clientRequest.Messages.Messages[0].Text;
                topRAD = parentRadCall;

                DebugTraceRADCall childRadCall = parentRadCall;

                for (int i = 1; i < clientRequest.Messages.Messages.Count; i++)
                {
                    childRadCall = new DebugTraceRADCall();
                    childRadCall.RADName = clientRequest.Messages.Messages[i].Text;
                    childRadCall.Parent = parentRadCall;
                    parentRadCall.Traces.Add(childRadCall);
                    parentRadCall = childRadCall;
                }
                currentRAD = childRadCall;
                foreach (MessageItem messageItem in clientRequest.Messages.Messages)
                {
                    radStack.Push(messageItem.Text);
                }
            }
        }

        public void AddTrace(Execute excuteObject)
        {
            ClientRequest clientRequest = excuteObject.ClientRequestEntity;

            if (clientRequest != null)
            {
                for (int i = clientRequest.Messages.Messages.Count-1; i >=0 ; i--)
                //for (int i = 0; i < clientRequest.Messages.Messages.Count ; i++)
                {
                    MessageItem messageItem = clientRequest.Messages.Messages[i];
                    string debugMessage = messageItem.Text.Trim();
                    
                    //rad call.
                    if (radCallReg.IsMatch(debugMessage))
                    {
                        TraceChangedInfo changedInfo = new TraceChangedInfo();
                        changedInfo.OldRad = currentRAD;
                        changedInfo.OldPanel = currentTrance as DebugTracePanel;

                        Match radMatch = radCallReg.Match(debugMessage);
                        GroupCollection groups = radMatch.Groups;
                        string threadId = groups[1].Value;
                        string currentRADName = groups[2].Value;
                        if (!followingReturn)
                        {
                            DebugTraceRADCall newRadCall = new DebugTraceRADCall();
                            newRadCall.Parent = currentRAD;
                            newRadCall.RADName = currentRADName;

                            currentRAD.Traces.Add(newRadCall);
                            currentRAD = newRadCall;
                            radStack.Push(currentRADName);

                            currentTrance = null;
                            changedInfo.NewRad = currentRAD;
                            changedInfo.NewPanel = null;
                            changedInfo.Type = TraceChangedType.NewRADCall;
                            if (OnTraceChanged != null)
                            {
                                OnTraceChanged(this, changedInfo);
                            }
                        }
                        else
                        {
                            //Check if current value is the same as the parent rad
                            if (currentRAD.RADName != currentRADName)
                            {
                                do
                                {
                                    radStack.Pop();
                                    currentRAD = currentRAD.Parent as DebugTraceRADCall;
                                }
                                while (currentRADName != currentRAD.RADName);
                                //throw new Exception("RAD stack mismatched...");
                            }
                        }


                        followingReturn = false;
                        //nothing to do
                    }
                    else if (returnReg.IsMatch(debugMessage))
                    {
                        followingReturn = true;
                        TraceChangedInfo changedInfo = new TraceChangedInfo();
                        changedInfo.OldRad = currentRAD;
                        changedInfo.OldPanel = currentTrance as DebugTracePanel;

                        radStack.Pop();
                        currentRAD = currentRAD.Parent as DebugTraceRADCall;
                        
                        currentTrance = null;
                        changedInfo.NewRad = currentRAD;
                        changedInfo.NewPanel = null;
                        changedInfo.Type = TraceChangedType.RADCallReturn;

                        if (OnTraceChanged!=null)
                        {
                            OnTraceChanged(this, changedInfo);
                        }

                    }
                    else if (panelReg.IsMatch(debugMessage))
                    {
                        followingReturn = false;
                        TraceChangedInfo changedInfo = new TraceChangedInfo();
                        changedInfo.OldRad = currentRAD;
                        changedInfo.OldPanel = currentTrance as DebugTracePanel;


                        Match radMatch = panelReg.Match(debugMessage);
                        GroupCollection groups = radMatch.Groups;
                        string threadId = groups[1].Value;
                        string currentPanelName = groups[2].Value;

                        RADPanel panel = new RADPanel();
                        panel.RADName = currentRAD.RADName;
                        panel.PanelName = currentPanelName;

                        DebugTracePanel tracePanel = new DebugTracePanel();
                        tracePanel.PanelInfo = panel;
                        tracePanel.Parent = currentRAD;
                        currentRAD.Traces.Add(tracePanel);
                        currentTrance = tracePanel;

                        changedInfo.NewRad = currentRAD;
                        changedInfo.NewPanel = currentTrance as DebugTracePanel;
                        changedInfo.Type = TraceChangedType.NewPanel;
                        if (OnTraceChanged!=null)
                        {
                            OnTraceChanged(this, changedInfo);
                        }
                    }
                    else
                    {
                        //followingReturn = false;
                    }
                    //radStack.Push(messageItem);
                }
            }
        }

        public void Test()
        {
            string radCallMessage = "|2| <return>";
            if (panelReg.IsMatch(radCallMessage))
            {
                Match radMatch = panelReg.Match(radCallMessage);
                GroupCollection groups = radMatch.Groups;
                string threadId = groups[1].Value;
                string radName = groups[2].Value;
            }
        }

        public void TestBatch(string filePath)
        {
            followingReturn = true;

            currentRAD = new DebugTraceRADCall();
            currentRAD.RADName = "exercise";
            using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                int i = 1;
                StreamReader reader = new StreamReader(stream);
                while (!reader.EndOfStream)
                {
                    string text = reader.ReadLine();
                    Execute sampleExecute = new Execute();
                    sampleExecute.ClientRequestEntity = new ClientRequest();
                    sampleExecute.ClientRequestEntity.Messages = new MessageCollection();
                    sampleExecute.ClientRequestEntity.Messages.Messages.Add(new MessageItem() { Text = text });
                    AddTrace(sampleExecute);
                    Console.WriteLine(i++);
                }

            }
        }
    }
}
