using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace SM.Smorgasbord.Communication.SoapEntities.Response
{
    [XmlRoot("file")]
    public class FileInfo
    {
        private string mode;
        private string name;
        private Collection<Attachment> attachments = new Collection<Attachment>();

        [XmlAttribute("mode")]
        public string Mode
        {
            get
            {
                return mode;
            }
            set
            {
                mode = value;
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

        [XmlArray("attachments")]
        [XmlArrayItem("attachment")]
        public Collection<Attachment> Attachments
        {
            get
            {
                return attachments;
            }
        }
    }
}
