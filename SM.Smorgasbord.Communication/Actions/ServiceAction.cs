using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using SM.Smorgasbord.Communication.Common;
using SM.Smorgasbord.Communication.SoapEntities;
using SM.Smorgasbord.Communication.SoapEntities.Response;

namespace SM.Smorgasbord.Communication.Actions
{
    public class ServiceAction : ActionBase
    {
        private string commandLine;

        public string CommandLine
        {
            get
            {
                return commandLine;
            }
            set
            {
                commandLine = value;
            }
        }

        public ServiceAction(DataBus bus)
            : base("service", bus)
        {
            IsGzip = true;
            //IsFastinfoset = true;
        }

        public override Dictionary<string, string> BuildHeaders()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();

            headers["Connection"] = "keep-alive";
            headers["Content-Type"] = "text/xml";
            headers["Accept-Encoding"] = "gzip";
            headers["Content-Encoding"] = "gzip";

            headers["Accept"] = "application/fastinfoset, text/xml, text/html, image/gif, image/jpeg, *; q=.2, */*; q=.2";

            return headers;
        }

        public override byte[] BuildMessage()
        {
            string xmlData = "<SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\">" +
                                @"<SOAP-ENV:Header/>
	                                <SOAP-ENV:Body>
		                                <service>
			                                <name>commandLine</name>
			                                <command>" + commandLine + @"</command>
		                                </service>
	                                </SOAP-ENV:Body>
                                </SOAP-ENV:Envelope>";
            XmlDocument document = new XmlDocument();
            document.LoadXml(xmlData);

            return Encoding.UTF8.GetBytes(document.InnerXml);
        }

        public override SoapBase ResponseData
        {
            get
            {
                return SoapDecoder.Deserialize<Service>(ResponseMessage.GetContent());
            }
        }
    }
}
