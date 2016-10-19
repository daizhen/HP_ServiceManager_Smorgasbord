using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace SM.Smorgasbord.Communication.SoapEntities.Response
{
    [Serializable]
    [XmlRoot("clientRequest")]
    public class ClientRequest
    {
        private string name;
        private FileInfo file;
        //private Collection<MessageItem> messages = new Collection<MessageItem>();

        private MessageCollection messages = new MessageCollection();
        [XmlElement("file")]
        public FileInfo File
        {
            get
            {
                return file;
            }
            set
            {
                file = value;
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
       
        //[XmlArray("messages")]
        //[XmlArrayItem("message")]
        [XmlElement("messages")]
        public MessageCollection Messages
        {
            get
            {
                return messages;
            }
            set
            {
                messages = value;
            }
        }
    }
}
