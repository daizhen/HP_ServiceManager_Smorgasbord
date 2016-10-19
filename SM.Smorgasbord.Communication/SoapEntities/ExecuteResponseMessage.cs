using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace SM.Smorgasbord.Communication.SoapEntities
{
    [Serializable]
    [XmlRoot("executeResponse")]
    public class ExecuteResponseMessage
    {
       // [XmlIgnore]
        //private Collection<ThreadItem> threads = new Collection<ThreadItem>();
        private ThreadItem[] threads;
        private ClientRequest requestMessage;

        [XmlArray]
        [XmlElement("threads")]
        public ThreadItem[] Threads
        {
            get
            {
                //if (threads.Count == 0)
                //{
                //    return null;
                //}

                //ThreadItem[] temThreads = new ThreadItem[threads.Count];

                //for (int i = 0; i < temThreads.Length; i++)
                //{
                //    temThreads[i] = threads[i];
                //}
                //return temThreads;
                return threads;
            }
            set
            {
                //for (int i = 0; i < value.Length; i++)
                //{
                //    threads.Add(value[i]);
                //}
                threads = value;
            }
        }

        //[XmlIgnore]
        //public Collection<ThreadItem> ThreadCollection
        //{
        //    get
        //    {
        //        return threads;
        //    }
        //}

        [XmlElement("clientRequest")]
        public ClientRequest RequestMessage
        {
            get

            {
                return requestMessage;
            }
            set
            {
                requestMessage = value;
            }
        }
    }
}
