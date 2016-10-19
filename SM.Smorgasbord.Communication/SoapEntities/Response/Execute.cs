using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace SM.Smorgasbord.Communication.SoapEntities.Response
{
    [Serializable]
    [XmlRoot("executeResponse", Namespace = "http://servicecenter.peregrine.com/PWS")]
    public class Execute:SoapBase
    {
        private bool attachmentCompressed;

        private ClientRequest clientRequest;

        private Collection<ThreadItem> threads = new Collection<ThreadItem>();

        [XmlAttribute("attachmentCompressed")]
        public bool AttachmentCompressed
        {
            get
            {
                return attachmentCompressed;
            }
            set
            {
                attachmentCompressed = value;
            }
        }

        [XmlArray("threads")]
        [XmlArrayItem("thread")]
        public Collection<ThreadItem> Threads
        {
            get
            {
                return threads;
            }
        }

        [XmlElement("clientRequest")]
        public ClientRequest ClientRequestEntity
        {
            get
            {
                return clientRequest;
            }
            set
            {
                clientRequest = value;
            }
        }
    }
}
