using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using SM. Smorgasbord. Communication. Common;
using System. Collections. ObjectModel;
using System. Xml. Serialization;
using System. IO;

namespace SM. Smorgasbord. Communication. Sessions
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
                    if (connection. Name == name)
                    {
                        return connection;
                    }
                }
                return null;
            }
        }

        public void Remove(ConnectionInfo connectionInfo)
        {
            connections.Remove(connectionInfo);
        }
        public void Remove(string connectionName)
        {
            connections.Remove(this[connectionName]);
        }

        public static ServerConnections Load()
        {
            FileStream connectionStream = new FileStream(@"XmlData\Connections.xml", FileMode. Open);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ServerConnections));
            ServerConnections connectionsInfo = (ServerConnections)xmlSerializer. Deserialize(connectionStream);
            connectionStream. Close();
            return connectionsInfo;
        }

    }
}
