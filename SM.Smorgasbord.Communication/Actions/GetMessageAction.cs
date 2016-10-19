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
    public class GetMessageAction : ActionBase
    {
        public GetMessageAction(DataBus bus)
            : base("getMessages", bus)
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
            string xmlData = "<SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\"><SOAP-ENV:Header></SOAP-ENV:Header><SOAP-ENV:Body><getMessages activity=\"1\"></getMessages></SOAP-ENV:Body></SOAP-ENV:Envelope>";

            XmlDocument document = new XmlDocument();
            document.LoadXml(xmlData);
            return Encoding.UTF8.GetBytes(xmlData);

            //return new byte[] { 0X1F, 0X8B, 0X08, 0X00, 0X00, 0X00, 0X00, 0X00, 0X00, 0X00, 0X7B, 0XC0, 0XC0, 0XC0, 0XC8, 0X60, 0X71, 0X9E, 0X3D, 0XD8, 0XDF, 0X31, 0X40, 0XD7, 0XD5, 0X2F, 0X4C, 0X23, 0XA3, 0XA4, 0XA4, 0XC0, 0X4A, 0X5F, 0XBF, 0X38, 0X39, 0X23, 0X35, 0X37, 0XB1, 0X58, 0XAF, 0X22, 0X37, 0XA7, 0X38, 0X3F, 0XB1, 0X40, 0X2F, 0XBF, 0X28, 0X5D, 0X1F, 0XC4, 0XD0, 0X4F, 0XCD, 0X2B, 0X4B, 0XCD, 0XC9, 0X2F, 0X48, 0XD5, 0XFF, 0X60, 0XDF, 0XD8, 0XC8, 0XEE, 0X0A, 0XE5, 0X01, 0XD9, 0XAC, 0X1E, 0XA9, 0X89, 0X29, 0XA9, 0X45, 0X20, 0X61, 0X66, 0XA7, 0XFC, 0X94, 0X4A, 0X1B, 0X96, 0XE2, 0X92, 0XC4, 0XA2, 0X92, 0XFF, 0XFF, 0X1F, 0X80, 0XAC, 0XB0, 0XE1, 0X50, 0X4E, 0XC9, 0X4F, 0X2E, 0XCD, 0X4D, 0XCD, 0X2B, 0XA1, 0XAD, 0X6D, 0X1F, 0X00, 0X16, 0X17, 0X0F, 0X4B, 0XD2, 0X00, 0X00, 0X00 };
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
