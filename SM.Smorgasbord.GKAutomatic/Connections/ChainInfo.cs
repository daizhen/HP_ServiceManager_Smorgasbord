using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using SM.Smorgasbord.Communication.Common;

namespace SM.Smorgasbord.GKAutomatic.Connections
{
    [Serializable]
    public class ChainInfo
    {
        private string name;
        private Collection<ConnectionInfo> connections = new Collection<ConnectionInfo>();

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public Collection<ConnectionInfo> Connections
        {
            get
            {
                return connections;
            }
        }

        public ConnectionInfo this[string serverName]
        {
            get
            {
                foreach (ConnectionInfo connectionInfo in connections)
                {
                    if (connectionInfo. Name. Equals(serverName))
                    {
                        return connectionInfo;
                    }
                }
                return null;
            }
        }

        public ConnectionInfo SourceServer
        {
            get
            {
                if (Connections. Count > 0)
                {
                    return Connections[0];
                }
                else
                {
                    return null;
                }
            }
        }

        public override string ToString()
        {
            return name;
        }
    }
}
