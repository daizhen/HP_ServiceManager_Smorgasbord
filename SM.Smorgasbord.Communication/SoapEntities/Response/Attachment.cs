using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SM.Smorgasbord.Communication.SoapEntities.Response
{
    [Serializable]
    [XmlRoot("attachment")]
    public class Attachment
    {
        private string attachmentType;
        private string href;
        private int length;
        private string name;
        private string type;

        [XmlAttribute("attachmentType")]
        public string AttachmentType
        {
            get
            {
                return attachmentType;
            }
            set
            {
                attachmentType = value;
            }
        }

        [XmlAttribute("href")]
        public string Href
        {
            get
            {
                return href;
            }
            set
            {
                href = value;
            }
        }

        [XmlAttribute("len")]
        public int Length
        {
            get
            {
                return length;
            }
            set
            {
                length = value;
            }
        }

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

        [XmlAttribute("type")]
        public string Type
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
    }
}
