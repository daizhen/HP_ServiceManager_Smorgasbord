using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using SM.Smorgasbord.Communication.Common;
using SM. Smorgasbord. Communication. SoapEntities;
using SM. Smorgasbord. Communication. SoapEntities. Response;

namespace SM.Smorgasbord.Communication.Actions
{

    public class DebugAction : ActionBase
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

        public DebugAction(DataBus bus)
            : base("execute", bus)
        {
            IsGzip = true;
            //IsFastinfoset = true;
        }
        public override Dictionary<string, string> BuildHeaders()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();

            headers["Connection"] = "keep-alive";
            headers["Content-Type"] = "text/xml";
            //"application/fastinfoset";
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
		                                <execute>
			                                <thread>" + ThreadId + @"</thread>
			                                <debugCommand></debugCommand>
		                                </execute>
	                                </SOAP-ENV:Body>
                                </SOAP-ENV:Envelope>";

            XmlDocument document = new XmlDocument();
            try
            {
                document. LoadXml(xmlData);
                XmlElement rootElement = document. DocumentElement;
                XmlElement bodyElement = rootElement. GetElementsByTagName("SOAP-ENV:Body")[0] as XmlElement;
                XmlElement executeElement = bodyElement. GetElementsByTagName("execute")[0] as XmlElement;
                XmlElement debugCommandElement = executeElement. GetElementsByTagName("debugCommand")[0] as XmlElement;
                debugCommandElement. AppendChild(document. CreateTextNode(commandLine));
                
            }
            catch (Exception ex)
            {
 
            }
            return Encoding.UTF8.GetBytes(document.InnerXml);
        }

        public override SoapBase ResponseData
        {
            get
            {
                return SoapDecoder. Deserialize<Execute>(ResponseMessage. GetContent());
            }
        }
    }
}
