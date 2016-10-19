using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SM.Smorgasbord.Communication.Common;
using SM.Smorgasbord.Communication.Common.Http;
using System.Xml;
using SM.Smorgasbord.Communication.Actions;
using SM.Smorgasbord.Communication.SoapEntities;
using SM.Smorgasbord.Communication.SoapEntities.Response;

namespace SM.Smorgasbord.Communication.Utils
{
    public class LoadBalanceUtil
    {
        public static DataBus GetDataBus(ConnectionInfo serverInfo)
        {
            DataBus finalBus = new DataBus();
            finalBus.UserName = serverInfo.UserName;
            finalBus.Password = serverInfo.Password;

            DataBus dataBus = new DataBus();
            dataBus.HostAddress = serverInfo.Domain;
            dataBus.Port = serverInfo.Port;
            dataBus.UserName = serverInfo.Name;
            dataBus.Password = serverInfo.Password;

            ActionGetPreferences getPreferences = new ActionGetPreferences(dataBus);

            HttpResponseMessage response = getPreferences.DoAction();

            HttpResponseMessage finalResponse = null;
            ActionGetPreferences finalPreferencesAction = getPreferences;

            if (response.CommonHeaders.Keys.Contains("Location") ||
                response.CommonHeaders.Keys.Contains("location"))
            {
                //Location:  http://gscapps1.gre.hp.com:52502/SM/ui?lbHost=gscapps1&lbPort=13085

                //Load balanced DataBus
                DataBus dataBusLB = new DataBus();
                string location = string.Empty;
                if (response.CommonHeaders.Keys.Contains("Location"))
                {
                    location = response.CommonHeaders["Location"].Trim();
                }
                else
                {
                    location = response.CommonHeaders["location"].Trim();
                }
                string removedHeaderLocation = location.Substring(7);
                int firstDash = removedHeaderLocation.IndexOf('/');

                string hostAndPort = removedHeaderLocation.Substring(0, firstDash);
                string hostAddress = hostAndPort.Split(':')[0];
                string portString = hostAndPort.Split(':')[1];

                string uri = removedHeaderLocation.Substring(firstDash);

                dataBusLB.HttpRequest.URI = uri;
                dataBusLB.HostAddress = hostAddress;
                dataBusLB.Port = Convert.ToInt32(portString);

                ActionGetPreferences getPreferencesLB = new ActionGetPreferences(dataBusLB);
                finalPreferencesAction = getPreferencesLB;
                HttpResponseMessage responseLB = getPreferencesLB.DoAction();

                finalResponse = responseLB;
                Console.WriteLine(responseLB.GetContent());
                //finalBus = dataBusLB;

                finalBus.HttpRequest.URI = dataBusLB.HttpRequest.URI;
                finalBus.HostAddress = dataBusLB.HostAddress;
                finalBus.Port = dataBusLB.Port;
            }
            else
            {
                finalBus.HttpRequest.URI = dataBus.HttpRequest.URI;
                finalBus.HostAddress = dataBus.HostAddress;
                finalBus.Port = dataBus.Port;
                finalResponse = response;
            }

            //Set Timestamp and cookie.
            GetPreferences soapEntity = finalPreferencesAction.ResponseData as GetPreferences;

            if (soapEntity != null)
            {
                Preference preference = soapEntity["SessionStartTimestamp"];
                if (preference != null)
                {
                    finalBus.TimeStamp = preference.Value;
                }
                else
                {
                    throw new Exception("Get preference error: no SessionStartTimestamp");
                }
            }
            else
            {
                throw new Exception("Get preference error");
            }

            #region Commented code

            //XmlDocument document = new XmlDocument();
            //document.LoadXml(finalResponse.GetContent());

            //XmlNode envelopeNode = document.GetElementsByTagName("SOAP-ENV:Envelope")[0];
            //XmlNode bodyNode = envelopeNode.ChildNodes[1];
            //XmlNode preferencesNode = bodyNode.ChildNodes[0].ChildNodes[0];

            //foreach (XmlNode currentNode in preferencesNode.ChildNodes)
            //{
            //    string name = currentNode.Attributes["name"].Value;
            //    string value = currentNode.Attributes["value"].Value;
            //    if (name == "SessionStartTimestamp")
            //    {
            //        finalBus.TimeStamp = value;
            //    }
            //}
            #endregion

            finalBus.CookieId = finalResponse.CommonHeaders["Set-Cookie"];

            return finalBus;
        }
    }
}
