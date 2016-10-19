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

    public class MessageResponseAction : ActionBase
    {
        public MessageResponseAction(DataBus bus)
            : base("execute", bus)
        {
            IsGzip = true;
            ThreadId = bus.ThreadId;
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

        public virtual string BuildXmlCommandString()
        {
            string xmlData = "<SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\"><SOAP-ENV:Header></SOAP-ENV:Header><SOAP-ENV:Body><execute><thread>"+ThreadId+"</thread><messageResponse></messageResponse></execute></SOAP-ENV:Body></SOAP-ENV:Envelope>";
            XmlDocument document = new XmlDocument();
            document.LoadXml(xmlData);
            return document.InnerXml;
        }

        public override byte[] BuildMessage()
        {
            return Encoding.UTF8.GetBytes(BuildXmlCommandString());
        }


        public override SoapBase ResponseData
        {
            get
            {
                return SoapDecoder.Deserialize<Execute>(ResponseMessage.GetContent());
            }
        }
    }
}
