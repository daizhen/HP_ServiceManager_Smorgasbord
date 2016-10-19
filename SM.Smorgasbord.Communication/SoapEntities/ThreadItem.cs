using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SM.Smorgasbord.Communication.SoapEntities
{
    [Serializable]
    [XmlRoot("thread")]
    public class ThreadItem
    {
        private int threadId;

        private string threadType;

        [XmlText]
        public int ThreadId
        {
            get
            {
                return threadId;
            }
            set
            {
                threadId = value;
            }
        }
        [XmlAttribute("type")]
        public string ThreadType
        {
            get
            {
                return threadType;
            }
            set
            {
                threadType = value;
            }
        }
    }
}
