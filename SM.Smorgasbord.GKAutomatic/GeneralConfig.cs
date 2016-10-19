using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SM.Smorgasbord.GKAutomatic
{
    [Serializable]
    public class GeneralConfig
    {
        private string rootPath;
        private string qcUserName;
        private string qcPassword;
        private string qcPrintName;
        private string pdCoordinator;
        private string testingGroup;

        private string exchangeServerUri;
        private string userDomain;

        public string RootPath
        {
            get
            {
                return rootPath;
            }
            set
            {
                rootPath = value;
            }
        }

        public string QCUserName
        {
            get
            {
                return qcUserName;
            }
            set
            {
                qcUserName = value;
            }
        }

        public string QCPassword
        {
            get
            {
                return qcPassword;
            }
            set
            {
                qcPassword = value;
            }
        }

        public string QCPrintName
        {
            get
            {
                return qcPrintName;
            }
            set
            {
                qcPrintName = value;
            }
        }

        /// <summary>
        /// PD coordinator
        /// </summary>
        public string PDCoordinator
        {
            get
            {
                return pdCoordinator;
            }
            set
            {
                pdCoordinator = value;
            }
        }

        /// <summary>
        /// Testing group.
        /// </summary>
        public string TestingGroup
        {
            get
            {
                return testingGroup;
            }
            set
            {
                testingGroup = value;
            }
        }

        public string ExchangeServerUri
        {
            get
            {
                return exchangeServerUri;
            }
            set
            {
                exchangeServerUri = value;
            }
        }

        public string UserDomain
        {
            get
            {
                return userDomain;
            }
            set
            {
                userDomain = value;
            }
        }

    }
}
