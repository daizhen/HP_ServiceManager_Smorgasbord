using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SM.Smorgasbord.Communication.Common;

namespace SM.Smorgasbord.Communication.Sessions
{
    public class SessionCache
    {
        private static Dictionary<string, ConnectionSession> connectionSessions = new Dictionary<string, ConnectionSession>();

        public Dictionary<string, ConnectionSession> ConnectionSessions
        {
            get
            {
                return connectionSessions;
            }
        }

        public ConnectionSession this[ConnectionInfo serverInfo]
        {
            get
            {
                if (!connectionSessions.Keys.Contains(serverInfo.Name))
                {
                    ConnectionSession connectionSession = new ConnectionSession();
                    connectionSession.ConnectionInfo = serverInfo;
                    connectionSessions.Add(serverInfo.Name, connectionSession);
                }
                return connectionSessions[serverInfo.Name];
            }
        }
    }
}
