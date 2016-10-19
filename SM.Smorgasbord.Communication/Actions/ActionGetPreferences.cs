using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SM.Smorgasbord.Communication.Common;
using SM.Smorgasbord.Communication.SoapEntities;
using SM.Smorgasbord.Communication.SoapEntities.Response;
using System.Net;

namespace SM.Smorgasbord.Communication.Actions
{
    public class ActionGetPreferences : ActionBase
    {
        public ActionGetPreferences(DataBus bus)
            : base("getPreferences", bus)
        {

        }

        public override Dictionary<string, string> BuildHeaders()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();

            headers["Connection"] = "close";
            headers["Content-Type"] = "text/xml; charset=utf-8";
            headers["Accept"] = "text/xml, text/html, image/gif, image/jpeg, *; q=.2, */*; q=.2";

            return headers;
        }

        public override byte[] BuildMessage()
        {
            string hostName = Dns.GetHostName();
            string xmlData = "<SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\"><SOAP-ENV:Header/><SOAP-ENV:Body><getPreferences><ClientSysInfo><release>7</release><version>11</version><level>333</level><build>333</build><hostname>" + hostName + "</hostname><networkAddress>16.98.77.252</networkAddress><hardware>x86</hardware><OSName>OS DaiZhen</OSName><OSVersion>6.0</OSVersion><type>scguiwswt</type><WebTierUrl>Unknown</WebTierUrl></ClientSysInfo></getPreferences></SOAP-ENV:Body></SOAP-ENV:Envelope>";

            return Encoding.UTF8.GetBytes(xmlData);
        }

        public override SoapBase ResponseData
        {
            get
            {
                return SoapDecoder.Deserialize<GetPreferences>(ResponseMessage.GetContent());
            }
        }
    }
}
