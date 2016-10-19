using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace SM.Smorgasbord.Debuger
{
    [DataContract]
    public class FormatField
    {
        [DataMember(Name = "flags")]
        public int Flags
        {
            get;
            set;
        }
        [DataMember(Name = "line")]
        public int Line
        {
            get;
            set;
        }
        [DataMember(Name = "column")]
        public int Column
        {
            get;
            set;
        }
        [DataMember(Name = "length")]
        public int Length
        {
            get;
            set;
        }
        [DataMember(Name = "window")]
        public int Window
        {
            get;
            set;
        }
        [DataMember(Name = "attribute")]
        public int Attribute
        {
            get;
            set;
        }
        [DataMember(Name = "graph")]
        public Collection<string> Graph
        {
            get;
            set;
        }
        [DataMember(Name = "input")]
        public string Input
        {
            get;
            set;
        }
        [DataMember(Name = "output")]
        public string Output
        {
            get;
            set;
        }
        [DataMember(Name = "format")]
        public string Format
        {
            get;
            set;
        }
        [DataMember(Name = "property")]
        public string Property
        {
            get;
            set;
        }

        public FieldProperty PropertyObject
        {
            get
            {
                return new FieldProperty(Property);
            }
        }
    }
}
