using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace SM.Smorgasbord.Communication.SoapEntities.Response
{
    [Serializable]
    [XmlRoot("serviceResponse", Namespace = "http://servicecenter.peregrine.com/PWS")]
    public class Service:SoapBase
    {
        private bool attachmentCompressed;

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
    }
}
