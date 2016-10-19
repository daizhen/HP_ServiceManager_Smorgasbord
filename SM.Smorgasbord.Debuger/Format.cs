using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;

namespace SM.Smorgasbord.Debuger
{
    [DataContract]
    public class Format
    {
        [DataMember(Name = "name")]
        public string Name
        {
            get;
            set;
        }

        [DataMember(Name = "field")]
        public Collection<FormatField> Fields
        {
            get;
            set;
        }
        [DataMember(Name = "file_name")]
        public string FileName
        {
            get;
            set;
        }
        [DataMember(Name = "editor")]
        public string Editor
        {
            get;
            set;
        }
        [DataMember(Name = "last_update")]
        public string LastUpdate
        {
            get;
            set;
        }
        [DataMember(Name = "help_topic")]
        public string HelpTopic
        {
            get;
            set;
        }
        [DataMember(Name = "syslanguage")]
        public string Syslanguage
        {
            get;
            set;
        }
        [DataMember(Name = "description")]
        public string Description
        {
            get;
            set;
        }
    }
}
