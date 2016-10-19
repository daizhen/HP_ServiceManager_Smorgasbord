using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace SM.Smorgasbord.Communication.SoapEntities
{
    [Serializable]
    [XmlRoot("clientRequest")]
    public class ClientRequest
    {
        private Collection<MessageItem> messages = new Collection<MessageItem>();
       // private MessageItem[] message;
        private string name;

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


        //[XmlArray]
        //[XmlElement("messages")]
        //[XmlIgnore]
        //public MessageItem[] Messages
        //{
        //    get
        //    {
        //        //MessageItem[] temMessages = new MessageItem[messages.Count];
        //        //for (int i = 0; i < temMessages.Length; i++)
        //        //{
        //        //    temMessages[i] = messages[i];
        //        //}
        //        //return temMessages;
        //        return message;
        //    }
        //    set
        //    {
        //        //messages.Clear();

        //        //for (int i = 0; i < value.Length; i++)
        //        //{
        //        //    messages.Add(value[i]);
        //        //}
        //        message = value;
        //    }
        //}
        [XmlElement("messages")]
        public Collection<MessageItem> MessageItems
        {
            get
            {
                return messages;
            }
        }
    }
}
