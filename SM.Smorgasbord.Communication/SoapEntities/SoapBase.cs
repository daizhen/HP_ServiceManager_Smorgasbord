using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SM.Smorgasbord.Communication.SoapEntities.Response;

namespace SM.Smorgasbord.Communication.SoapEntities
{
    [Serializable]
    public class SoapBase
    {
        private string originalData;
        private Fault faultInfo;

        [XmlIgnore]
        public string OriginalData
        {
            get
            {
                return originalData;
            }
            set
            {
                originalData = value;
            }
        }

        public Fault FaultInfo
        {
            get
            {
                return faultInfo;
            }
            set
            {
                faultInfo = value;
            }
        }

        public SoapBase(string xmlString)
        {
            originalData = xmlString;
        }

        public SoapBase()
        {
        }
    }
}
