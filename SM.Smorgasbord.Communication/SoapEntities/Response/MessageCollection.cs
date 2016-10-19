using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Collections. ObjectModel;
using System. Xml. Serialization;

namespace SM. Smorgasbord. Communication. SoapEntities. Response
{
    [Serializable]
    [XmlRoot("messages")]
    public class MessageCollection
    {
        private Collection<MessageItem> messages = new Collection<MessageItem>();
        private bool rearm = true;

        [XmlElement("message")]
        public Collection<MessageItem> Messages
        {
            get
            {
                return messages;
            }
        }
        [XmlAttribute("rearm")]
        public bool Rearm
        {
            get
            {
                return rearm;
            }
            set
            {
                rearm = value;
            }
        }
        [XmlIgnore]
        public int Count
        {
            get
            {
                return messages. Count;
            }
        }
        [XmlIgnore]
        public MessageItem this[int index]
        {
            get
            {
                return messages[index];
            }
            set
            {
                messages[index] = value;
            }
        }
    }
}
