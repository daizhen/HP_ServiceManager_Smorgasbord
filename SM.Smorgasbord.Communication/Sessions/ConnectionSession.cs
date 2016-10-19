using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SM.Smorgasbord.Communication.Common;
using SM.Smorgasbord.Communication.Actions;
using SM.Smorgasbord.Communication.Common.Http;
using SM.Smorgasbord.Communication.Utils;
using SM.Smorgasbord.Communication.SoapEntities.Response;

namespace SM.Smorgasbord.Communication.Sessions
{
    public class ConnectionSession
    {
        private DateTime sessionStart;
        private ConnectionInfo connectionInfo;
        private DataBus dataBus;
        public ConnectionInfo ConnectionInfo
        {
            get
            {
                return connectionInfo;
            }
            set
            {
                connectionInfo = value;
            }
        }

        public DateTime SessionStart
        {
            get
            {
                return sessionStart;
            }
            set
            {
                sessionStart = value;
            }
        }

        public bool IsTimeOut
        {
            get
            {
                if (dataBus != null)
                {
                    try
                    {
                        dataBus.Refresh();
                        GetMessageAction getMessageAction = new GetMessageAction(dataBus);
                        HttpResponseMessage responseMessage = getMessageAction.DoAction();
                        return false;
                    }
                    catch(Exception e)
                    {
                        return true;
                    }

                }
                return true;
            }
        }

        public DataBus GetDataBus()
        {
            bool isTimeOut = IsTimeOut;
            if (isTimeOut)
            {
                if (dataBus != null)
                {
                    try
                    {
                        //Send stop mession first
                        StopAction stopAction = new StopAction(dataBus);
                        HttpResponseMessage response = stopAction.DoAction();
                        string str = response.GetContent();
                    }
                    catch
                    {
                        //Nothing to do.
                    }
                }
            }
            if (dataBus == null || isTimeOut)
            {
                sessionStart = System. DateTime. Now;

                dataBus = LoadBalanceUtil. GetDataBus(connectionInfo);

                //Start action.
                StartAction startAction = new StartAction(dataBus);
                HttpResponseMessage response = startAction. DoAction();

                string str = response. GetContent();

                try
                {
                    Start startEntity = startAction.ResponseData as Start;
                    dataBus.ThreadId = startEntity.Threads[0].ThreadId;
                }
                catch (Exception ex)
                {
                    throw new Exception("Start:"+str);
                }


                ActionUtil. GetFormAndData(dataBus, string. Empty);

                //Login
                LoginAction loginAction = new LoginAction(dataBus);
                loginAction. UserName = dataBus. UserName;
                loginAction. Password = dataBus. EncryptedPassword;
                response = loginAction. DoAction();
                string loginMessage = response. GetContent();

                GetMessageAction fetchMessageAction = new GetMessageAction(dataBus);

                fetchMessageAction. DoAction();
                Execute execute = fetchMessageAction. ResponseData as Execute;

                while (execute!=null && execute. ClientRequestEntity != null)
                {
                    ActionUtil. StoreMessages(execute. ClientRequestEntity);
                    fetchMessageAction = new GetMessageAction(dataBus);
                    fetchMessageAction. DoAction();
                    execute = fetchMessageAction. ResponseData as Execute;
                }

                //fetchMessageAction = new GetMessageAction(dataBus);
                //fetchMessageAction. DoAction();

                ActionUtil. GetFormAndData(dataBus, string. Empty);

                SetPreferencesAction setPreferencesAction = new SetPreferencesAction(dataBus);

                response = setPreferencesAction. DoAction();
                string setPreferencesMessage = response. GetContent();
                SetPreferences setPreferences = setPreferencesAction. ResponseData as SetPreferences;
            }
            else
            {
                dataBus.Refresh();
                dataBus. ThreadId++;
            }

            return dataBus;
        }

        public DataBus GetDataBus(string cookieId, string authorizationString)
        {
            DataBus dataBus = new DataBus();
            dataBus.HostAddress = connectionInfo.Domain;
            dataBus.Port = connectionInfo.Port;
            dataBus.UserName = connectionInfo.Name;
            dataBus.Password = connectionInfo.Password;
            dataBus.CookieId = cookieId;
            dataBus.AuthorizationString = authorizationString;
            return dataBus;
        }
    }

}
