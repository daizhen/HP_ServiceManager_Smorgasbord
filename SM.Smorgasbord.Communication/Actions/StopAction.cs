using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using SM.Smorgasbord.Communication.Common;

namespace SM.Smorgasbord.Communication.Actions
{
    public class StopAction : ActionBase
    {
        private XmlDocument executeData = new XmlDocument();

        public StopAction(DataBus bus)
            : base("stop", bus)
        {
            IsGzip = true;
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

        public virtual string BuildXmlCommandString()
        {
            string xmlData = "<SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\"><SOAP-ENV:Header></SOAP-ENV:Header><SOAP-ENV:Body><stop></stop></SOAP-ENV:Body></SOAP-ENV:Envelope>";
            XmlDocument document = new XmlDocument();
            document.LoadXml(xmlData);

            return document.InnerXml;
        }

        public override byte[] BuildMessage()
        {
            return Encoding.UTF8.GetBytes(BuildXmlCommandString());
        }
    }
}
