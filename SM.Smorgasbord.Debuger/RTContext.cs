using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace SM.Smorgasbord.Debuger
{
    [DataContract]
    public class RTContext
    {
        private Dictionary<string, string> namedVarValues  = null;

        public Dictionary<string, string> NamedVarValues
        {
            get
            {
                if (namedVarValues == null)
                {
                    if (VarNames != null)
                    {
                        namedVarValues = new Dictionary<string, string>();
                        for (int i = 0; i < VarNames.Count; i++)
                        {
                            namedVarValues.Add(VarNames[i], VarValues[i]);
                        }
                    }
                }
                return namedVarValues;
            }
        }
        [DataMember(Name = "names")]
        public Collection<string> VarNames
        {
            get;
            set;
        }
        [DataMember(Name = "values")]
        public Collection<string> VarValues
        {
            get;
            set;
        }
        [DataMember(Name = "refs")]
        public Collection<RefRecord> Refs
        {
            get;
            set;
        }
    }
}
