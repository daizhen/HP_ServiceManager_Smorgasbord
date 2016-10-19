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
    public class SetPreferencesAction : ActionBase
    {
        public SetPreferencesAction(DataBus bus)
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
                              @"  <SOAP-ENV:Header/>
	                            <SOAP-ENV:Body>
		                            <setPreferences>
			                            <name>setPreferences</name>
			                            <preferences>"
                                           + "<preference name=\"viewactivenotes\" value=\"false\"/>"
                                           + "<preference name=\"viewpromptforsave\" value=\"true\"/>"
                                           + " <preference name=\"clientprinting\" value=\"true\"/>"
                                           + " <preference name=\"clientsideunload\" value=\"true\"/>"
                                           + " <preference name=\"viewrecordlist\" value=\"true\"/>"
                                           + " <preference name=\"heartbeatinterval\" value=\"15\"/>"
                                           + " <preference name=\"compress_soap\" value=\"\"/>"
                                           + " <preference name=\"ssl\" value=\"\"/>"
                                           + " <preference name=\"viewclassicmenu\" value=\"false\"/>"
                                           + " <preference name=\"useservertabs\" value=\"false\"/>"
                                           + " <preference name=\"recordlistcount\" value=\"32\"/>"
                                           + " <preference name=\"clientformcache\" value=\"true\"/>"
                                           + " <preference name=\"chartrefresh\" value=\"60\"/>"
                                           + " <preference name=\"essuser\" value=\"\"/>"
                                       + @" </preferences>
		                            </setPreferences>
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
                return SoapDecoder.Deserialize<SetPreferences>(ResponseMessage.GetContent());
            }
        }
    }
}
