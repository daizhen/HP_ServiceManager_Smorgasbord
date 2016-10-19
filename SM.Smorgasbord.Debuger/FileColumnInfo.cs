using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace SM.Smorgasbord.Debuger
{
    [DataContract]
    public class FileColumnInfo
    {
        [DataMember(Name = "name")]
        public string Name
        {
            get;
            set;
        }

        [DataMember(Name = "type")]
        public int Type
        {
            get;
            set;
        }
        [DataMember(Name = "level")]
        public int Level
        {
            get;
            set;
        }
        [DataMember(Name = "children")]
        public Collection<FileColumnInfo> Children
        {
            get;
            set;
        }
    }
}
