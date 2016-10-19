using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SM.Smorgasbord.Communication.SoapEntities.Response
{
    [Serializable]
    [XmlRoot("thread")]
    public class ThreadItem
    {
          //<thread form="login.prompt.g" formid="login.prompt.g210" type="detail">0</thread> 
        private string form;
        private string formId;
        private string threadType;
        private int threadId;

        private int record;
        private int refresh;

        private int died = -1;

        [XmlAttribute("form")]
        public string Form
        {
            get
            {
                return form;
            }
            set
            {
                form = value;
            }
        }
        [XmlAttribute("formid")]
        public string FormId
        {
            get
            {
                return formId;
            }
            set
            {
                formId = value;
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


        [XmlAttribute("record")]
        public int Record
        {
            get
            {
                return record;
            }
            set
            {
                record = value;
            }
        }


        [XmlAttribute("refresh")]
        public int Refresh
        {
            get
            {
                return refresh;
            }
            set
            {
                refresh = value;
            }
        }

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
    }
}
