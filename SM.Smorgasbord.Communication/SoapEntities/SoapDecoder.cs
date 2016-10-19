using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using SM.Smorgasbord.Communication.SoapEntities.Response;

namespace SM.Smorgasbord.Communication.SoapEntities
{
    public class SoapDecoder
    {
        public static T Deserialize<T>(string xmlData) where T:SoapBase, new()
        {
            XmlDocument xmlDoc = new XmlDocument();
            T soapEntity = null;
            try
            {
                string endingString = ":Envelope>";
                xmlDoc.LoadXml(xmlData.Substring(0,xmlData.IndexOf(endingString) + endingString.Length));
            }
            catch (Exception ex)
            {
                throw new Exception(xmlData.Trim());
            }

            //Root Element
            XmlElement rootElement = xmlDoc.DocumentElement;

            //Header
            XmlElement header = null;
            XmlNodeList headerList = rootElement.GetElementsByTagName("SOAP-ENV:Header");
            if (headerList.Count > 0)
            {
                header = headerList[0] as XmlElement;
            }

            //Body
            XmlElement body = null;
            XmlNodeList bodyList = rootElement.GetElementsByTagName("SOAP-ENV:Body");
            if (bodyList.Count > 0)
            {
                body = bodyList[0] as XmlElement;
                XmlElement bodyTopNode = body.FirstChild as XmlElement;
                if (bodyTopNode.Attributes["xmlns"] != null)
                {
                    bodyTopNode.Attributes["xmlns"].Value = "http://servicecenter.peregrine.com/PWS";
                }

                //Check the fault info.
                XmlNodeList faultList = body.GetElementsByTagName("SOAP-ENV:Fault");
                if (faultList.Count > 0)
                {
                    XmlElement faultElement = faultList[0] as XmlElement;
                    soapEntity = new T();
                    soapEntity.FaultInfo = new Fault();
                    XmlElement faultCodeElement = faultElement.GetElementsByTagName("faultcode")[0] as XmlElement;
                    XmlElement faultStringElement = faultElement.GetElementsByTagName("faultstring")[0] as XmlElement;
                    XmlElement faultActorElement = faultElement.GetElementsByTagName("faultactor")[0] as XmlElement;

                    soapEntity.FaultInfo.FaultActor = faultActorElement.Value;
                    soapEntity.FaultInfo.FaultCode = faultCodeElement.Value;
                    soapEntity.FaultInfo.FaultString = faultStringElement.Value;

                    return soapEntity;
                }
            }
            MemoryStream temStream = new MemoryStream();
            XmlDocument newDoc = new XmlDocument();
            newDoc.LoadXml(body.InnerXml);
  
            byte[] data = Encoding.UTF8.GetBytes(newDoc.OuterXml);
            temStream.Write(data, 0, data.Length);
            temStream.Position = 0;
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                soapEntity = (T)xmlSerializer.Deserialize(temStream);

                soapEntity.OriginalData = xmlData;
            }
            catch(Exception ex)
            {

            }
            finally
            {
                temStream.Close();
            }

            return soapEntity;
        }
    }
}
