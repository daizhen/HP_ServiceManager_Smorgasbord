using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using SM.Smorgasbord.RADParser;

namespace SM.Smorgasbord.Debuger
{
    [DataContract]
    public class RefRecord
    {
        [DataMember(Name = "name")]
        public string FileName
        { get; set; }

        [DataMember(Name = "value")]
        public FileColumnInfo RootColumn
        {
            get;
            set;
        }
    }
}
