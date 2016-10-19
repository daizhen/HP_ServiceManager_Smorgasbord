using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SM.Smorgasbord.GKAutomatic
{
    [Serializable]
    public class BackupFileInfo
    {
        private string serverName;
        private string unloadFileLocation;

        public string ServerName
        {
            get
            {
                return serverName;
            }
            set
            {
                serverName = value;
            }
        }

        public string UnloadFileLocation
        {
            get
            {
                return unloadFileLocation;
            }
            set
            {
                unloadFileLocation = value;
            }
        }
    }
}
