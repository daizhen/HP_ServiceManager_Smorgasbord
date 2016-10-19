using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SM.Smorgasbord.Communication.SoapEntities.Response
{
    [Serializable]
    [XmlRoot("message")]
    public class MessageItem
    {
        private int id;
        private string module;
        private int severity;
        private string text;

        [XmlAttribute("id")]
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        [XmlAttribute("module")]
        public string Module
        {
            get
            {
                return module;
            }
            set
            {
                module = value;
            }
        }

        [XmlAttribute("severity")]
        public int Severity
        {
            get
            {
                return severity;
            }
            set
            {
                severity = value;
            }
        }
        
        [XmlText]
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
            }
        }

    }
}
