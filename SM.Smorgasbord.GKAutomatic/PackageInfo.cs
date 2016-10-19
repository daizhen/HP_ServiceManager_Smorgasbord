using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using SM.Smorgasbord.GKAutomatic.Connections;
using SM.Smorgasbord.GKAutomatic.Utils;

namespace SM.Smorgasbord.GKAutomatic
{
    [Serializable]
    public class PackageInfo
    {
        #region Fields

        private string name;
        private string chainName;
        private bool isEntity = false;
        private string unloadScriptFileLocation;
        private Collection<BackupFileInfo> backupFileLocations = new Collection<BackupFileInfo>();
        private Collection<PackageInfo> children = new Collection<PackageInfo>();
        private string rootDir;
        private PackageStatus status = PackageStatus. Init;

        private string packageNumber;
        private PackageType type = PackageType.CR;


        //Email info

        private string msgFileName;
        private string msgTitle;

        #endregion 
        //private Package parent;

        /// <summary>
        /// The name of the package, keep the same as unload script name
        /// </summary>
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

        /// <summary>
        /// To Check if it is a real package
        /// true: real package.
        /// false: dumb package, only a folder
        /// </summary>
        public bool IsEntity
        {
            get
            {
                return isEntity;
            }
            set
            {
                isEntity = value;
            }
        }

        public PackageStatus Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }

        public Collection<PackageInfo> Children
        {
            get
            {
                return children;
            }
        }

        public string ChainName
        {
            get
            {
                return chainName;
            }
            set
            {
                chainName = value;
            }
        }

        public ChainInfo Chain
        {
            get
            {
                PackageChains packageChains = PackageUtil.LoadChains();
                return packageChains[chainName];
            }
        }

        public string UnloadScriptFileLocation
        {
            get
            {
                return unloadScriptFileLocation;
            }
            set
            {
                unloadScriptFileLocation = value;
            }
        }

        public Collection<BackupFileInfo> BackupFileLocations
        {
            get
            {
                return backupFileLocations;
            }
        }

        public string RootDir
        {
            get
            {
                return rootDir;
            }
            set
            {
                rootDir = value;
            }
        }

        public string PackageNumber
        {
            get
            {
                return packageNumber;
            }
            set
            {
                packageNumber = value;
            }
        }

        public PackageType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        public string MsgFileName
        {
            get
            {
                return msgFileName;
            }
            set
            {
                msgFileName = value;
            }

        }

        public string MsgTitle
        {
            get
            {
                return msgTitle;
            }
            set
            {
                msgTitle = value;
            }
        }

    }
    public enum PackageStatus
    {
        Init = 1,
        Moved = 2,
        Rollbacked = 4,
        Processing = 8
    }

    public enum PackageType
    {
        QCR = 1,
        PD = 2,
        CR = 4
    }
}
