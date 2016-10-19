using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using SM.Smorgasbord.Communication.Common;

namespace SM.Smorgasbord.GKAutomatic.Connections
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ServerConnections
    {
        private Collection<ConnectionInfo> connections = new Collection<ConnectionInfo>();

        public Collection<ConnectionInfo> Connections
        {
            get
            {
                return connections;
            }
        }

        public ConnectionInfo this[string name]
        {
            get
            {
                foreach (ConnectionInfo connection in connections)
                {
                    if (connection.Name == name)
                    {
                        return connection;
                    }
                }
                return null;
            }
        }
    }
}
