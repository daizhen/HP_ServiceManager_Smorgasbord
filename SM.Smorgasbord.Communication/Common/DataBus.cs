using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SM.Smorgasbord.Communication.Common.Http;
using System.IO;
using System.Net;
using System.Collections.ObjectModel;
using SM.Smorgasbord.Communication.Utils;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace SM.Smorgasbord.Communication.Common
{
    public class DataBus:IDisposable
    {
        #region Fields

        private HttpRequestMessage httpRequest = new HttpRequestMessage();
        private string hostAddress = "gsmtpah3.chn.hp.com";
        private int port = 14010;
        private string userName = string.Empty;
        private string password;
        private string timeStamp;
        private string cookieId;
        private int requestSeq = 1;

        private int threadId;
        private string formName;
        private string authorizationString = string.Empty;

        #endregion
        public string AuthorizationString
        {
            get
            {
                return authorizationString;
            }
            set
            {
                authorizationString = value;
            }
        }
        public HttpRequestMessage HttpRequest
        {
            get
            {
                return httpRequest;
            }
            set
            {
                httpRequest = value;
            }
        }

        public string HostAddress
        {
            get
            {
                return hostAddress;
            }
            set
            {
                hostAddress = value;
            }
        }

        public int Port
        {
            get
            {
                return port;
            }
            set
            {
                port = value;
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

        public string TimeStamp
        {
            get
            {
                return timeStamp;
            }
            set
            {
                timeStamp = value;
            }
        }

        public string EncryptedPassword
        {
            get
            {
                return EncryptPassword(password);
            }
        }

        public DataBus()
        {
            //Set Default For Test
            userName = "ASIAPACIFIC_DAIZHEN";
            password = "";
        }

        public string CookieId
        {
            get
            {
                return cookieId;
            }
            set
            {
                cookieId = value;
            }
        }

        public int ThreadId
        {
            get
            {
                return threadId;
            }
            set
            {
                threadId = value;
            }
        }

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

        public void Send(byte[] message)
        {
            httpRequest.CommonHeaders["Content-Length"] = message.Length.ToString();
            httpRequest.CommonHeaders["Pragma"] = "requestnum=\"" + requestSeq + "\"";
            requestSeq++;

            byte[] headerData = Encoding.UTF8.GetBytes(httpRequest.GetHeaderString());
            Collection<byte> totalData = new Collection<byte>();
            for (int i = 0; i < headerData.Length; i++)
            {
                totalData.Add(headerData[i]);
            }
            for (int i = 0; i < message.Length; i++)
            {
                totalData.Add(message[i]);
            }

            byte[] totalBytes = totalData.ToArray();

            Stream requestStream = httpRequest.GetRequestStream(new IPEndPoint(GetIPV4Address(hostAddress), port));
            requestStream.Write(totalBytes, 0, totalBytes.Length);
            requestStream.Flush();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Send(string message)
        {
            byte[] rawData = Encoding.UTF8.GetBytes(message);
            byte[] fastinfosetData = null;
            byte[] compressedData = null;

            //Fast Info Set
            if (httpRequest.CommonHeaders.Keys.Contains("Content-Type") &&
               httpRequest.CommonHeaders["Content-Type"].Contains("application/fastinfoset"))
            {
                fastinfosetData = FastinfosetUtil.GenerateFastInfoSet(message);
            }
            else
            {
                fastinfosetData = rawData;
            }

            //Gzip Compress
            if (httpRequest.CommonHeaders.Keys.Contains("Accept-Encoding") &&
               httpRequest.CommonHeaders["Accept-Encoding"].Equals("gzip"))
            {
                compressedData = GzipUtil.Compress(fastinfosetData);
            }
            else
            {
                compressedData = fastinfosetData;
            }
            httpRequest.CommonHeaders["Content-Length"] = compressedData.Length.ToString();
            httpRequest.CommonHeaders["Pragma"] = "requestnum=\"" + requestSeq + "\"";
            requestSeq++;

            //Conbine the Header and message body

            byte[] headerData = Encoding.UTF8.GetBytes(httpRequest.GetHeaderString());
            Collection<byte> totalData = new Collection<byte>();
            for (int i = 0; i < headerData.Length; i++)
            {
                totalData.Add(headerData[i]);
            }
            for (int i = 0; i < compressedData.Length; i++)
            {
                totalData.Add(compressedData[i]);
            }

            byte[] totalBytes = totalData.ToArray();
            Stream requestStream = httpRequest.GetRequestStream(new IPEndPoint(Dns.GetHostEntry(hostAddress).AddressList[0], port));

            requestStream.Write(totalBytes, 0, totalBytes.Length);

        }

        public HttpResponseMessage Receive()
        {
            HttpResponseMessage response = httpRequest. GetResponse();
            return response;
        }

        public void SetCommonHeaders()
        {
            httpRequest.CommonHeaders.Clear();
            if (!string.IsNullOrEmpty(cookieId))
            {
                httpRequest.CommonHeaders.Add("Cookie", cookieId);
            }
            httpRequest.CommonHeaders.Add("User-Agent", "Java/1.6.0_20");
            httpRequest.CommonHeaders.Add("Host", hostAddress + ":" + port);
            httpRequest.CommonHeaders.Add("Cache-Control", "no-cache");
            if (string.IsNullOrEmpty(authorizationString) && !string.IsNullOrEmpty(timeStamp))
            {
                authorizationString = GenerateBasicAuthorization();
            }

            if (!string.IsNullOrEmpty(authorizationString))
            {
                httpRequest.CommonHeaders.Add("Authorization", authorizationString);
            }
        }

        public void Refresh()
        {
            httpRequest = new HttpRequestMessage();
        }

        #region Private methods

        private string GenerateBasicAuthorization()
        {
            const string codeFornat = "Basic {0}";
            string name = userName.Trim();
            string pwd = EncryptPassword(password.Trim());
            string rawString = name + ":" + pwd;

            return string.Format(codeFornat, Convert.ToBase64String(Encoding.ASCII.GetBytes(rawString)));
        }

        private string EncryptPassword(string password)
        {
            PKCSKeyGenerator kp = new PKCSKeyGenerator();
            ICryptoTransform crypt = kp.Generate("ServiceCenter", GetBytes(timeStamp),
                17,             // iterations of MD5 hashing
                  1);  // number of 16-byte segments to create.  // 1 to mimic Java behaviour.
            String command = password;
            byte[] newbytes = crypt.TransformFinalBlock(Encoding.UTF8.GetBytes(command), 0, command.Length);

            string str = BitConverter.ToString(newbytes).Replace("-", "");
            return str;
        }

        private byte[] GetBytes(string str)
        {
            byte[] temBytes = new byte[str.Length / 2];
            for (int i = 0; i < str.Length / 2; i++)
            {
                temBytes[i] = Convert.ToByte(str.Substring(i * 2, 2), 16);
            }
            return temBytes;
        }

        private IPAddress GetIPV4Address(string hostAddress)
        {
            Regex ipAddressReg = new Regex("^\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}$");
            if (ipAddressReg.IsMatch(hostAddress))
            {
                return IPAddress.Parse(hostAddress);
            }

            IPHostEntry hostEntry = Dns.GetHostEntry(hostAddress);
            IPAddress[] addresses = hostEntry.AddressList;
            foreach (IPAddress address in addresses)
            {
                if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return address;
                }
            }
            return null;
        }

        #endregion

        public void Dispose()
        {
            httpRequest.Dispose();
        }
    }
}
