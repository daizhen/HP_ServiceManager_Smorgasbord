using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SM.Smorgasbord.GKAutomatic.Connections
{
    [Serializable]
    public class ConnectionInfo
    {
        private string name;
        private string domain;
        private int port;
        private string userName;
        private string password;

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

        public string Domain
        {
            get
            {
                return domain;
            }
            set
            {
                domain = value;
            }
        }

        public int Port
        {
            get
            {
                return port;
            }
            set
            {
                port = value;
            }
        }

        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        public override string ToString()
        {
            return name;
        }
    }
}
