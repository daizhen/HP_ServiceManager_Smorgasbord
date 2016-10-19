using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SM.Smorgasbord.Communication.SoapEntities.Response
{
    [Serializable]
    [XmlRoot("preference")]
    public class Preference
    {
        private string name;
        private string _value;

        [XmlAttribute("name")]
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

        [XmlAttribute("value")]
        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }
    }
}
