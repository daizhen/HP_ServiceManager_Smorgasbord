using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SM.Smorgasbord.Communication.Common;
using System.Xml;

namespace SM.Smorgasbord.Communication.Actions
{
    public class GetDataAction : ActionBase
    {
        private string formName;

        public string FormName
        {
            get
            {
                return formName;
            }
            set
            {
                formName = value;
            }
        }

        public GetDataAction(DataBus bus)
            : base("getData", bus)
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
		                            <getDocument>
			                            <thread>"+ThreadId+"</thread>";
            if(!string.IsNullOrEmpty(formName))
            {
                xmlData = xmlData +  "<formname>" + formName + @"</formname>";
            }
                            xmlData+=@"<type>detail</type>
		                            </getDocument>
	                            </SOAP-ENV:Body>
                            </SOAP-ENV:Envelope>";
            XmlDocument document = new XmlDocument();
            document.LoadXml(xmlData);

            return Encoding.UTF8.GetBytes(document.InnerXml);
        }
    }
}
