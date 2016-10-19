using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SM.Smorgasbord.Communication.SoapEntities.Response
{
    public class Fault
    {
        //<faultcode>SOAP-ENV:Server</faultcode>
        //    <faultstring>Max Sessions Exceeded.</faultstring>
        //    <faultactor>Server</faultactor>

        private string faultCode;
        private string faultString;
        private string faultActor;

        public string FaultCode
        {
            get
            {
                return faultCode;
            }
            set
            {
                faultCode = value;
            }
        }

        public string FaultString
        {
            get
            {
                return faultString;
            }
            set
            {
                faultString = value;
            }
        }

        public string FaultActor
        {
            get
            {
                return faultActor;
            }
            set
            {
                faultActor = value;
            }
        }

    }
}
