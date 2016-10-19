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
    public class LoginAction : ActionBase
    {
        private string userName;
        private string password;
        private string type;
        private int eventId;

        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        public int EventId
        {
            get
            {
                return eventId;
            }
            set
            {
                eventId = value;
            }

        }

        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        public LoginAction(DataBus bus)
            : base("execute", bus)
        {
            IsGzip = true;
            eventId = 0;
            type = "detail";
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
            string xmlData = "<SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\">" +
                                    @"<SOAP-ENV:Header/>
                                    <SOAP-ENV:Body>
                                        <execute>
                                            <thread>" + ThreadId + @"</thread>
                                            <type>" + type + @"</type>
                                            <event>" + eventId + @"</event>
                                            <authModel>
                                                <var>" +
                                                    @"<user.id>" + userName + "</user.id>"
                                                    + "<old.password Password=\"1\">" + password + @"</old.password>
                                                    <L.language>en</L.language>
                                                </var>
                                            </authModel>
                                        </execute>
                                    </SOAP-ENV:Body>
                                </SOAP-ENV:Envelope>";
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
