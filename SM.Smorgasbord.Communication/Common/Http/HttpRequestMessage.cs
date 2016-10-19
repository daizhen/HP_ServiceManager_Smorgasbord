using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Net;

namespace SM.Smorgasbord.Communication.Common.Http
{
    public class HttpRequestMessage:IDisposable
    {
        private Encoding messageEncoding;
        private TcpClient client;
        private NetworkStream stream;

        //Request Line
        private string method;
        private string uri;
        private string version;

        private Dictionary<string, string> commonHeaders = new Dictionary<string, string>();

        public Encoding MessageEncoding
        {
            get
            {
                return messageEncoding;
            }
            set
            {
                messageEncoding = value;
            }
        }

        public HttpRequestMessage()
        {
            messageEncoding = Encoding.UTF8;
            version = "HTTP/1.1";
            uri = "/SM/ui";
            method = "POST";
        }

        public string Method
        {
            get
            {
                return method;
            }
            set
            {
                method = value;
            }
        }

        public string URI
        {
            get
            {
                return uri;
            }
            set
            {
                uri = value;
            }
        }

        public string Version
        {
            get
            {
                return version;
            }
            set
            {
                version = value;
            }
        }

        public Dictionary<string, string> CommonHeaders
        {
            get
            {
                return commonHeaders;
            }
        }

        public string GetHeaderString()
        {
            StringBuilder headerString = new StringBuilder();
            headerString.AppendFormat("{0} {1} {2}\r\n", method, uri, version);

            foreach (KeyValuePair<string, string> keyValuePair in CommonHeaders)
            {
                headerString.AppendFormat("{0}: {1}\r\n", keyValuePair.Key, keyValuePair.Value);
            }
            headerString.Append("\r\n");
            return headerString.ToString();
        }

        public Stream GetRequestStream(IPEndPoint ipAddress)
        {
            if (client == null || !client.Connected)
            {
                client = new TcpClient();
                client. Connect(ipAddress);
                stream = client. GetStream();
            }

            //byte[] headerBytes = Encoding.ASCII.GetBytes(GetHeaderString());
            //stream.Write(headerBytes, 0, headerBytes.Length);

            //Send Header first
            return stream;

        }

        public HttpResponseMessage GetResponse()
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage(client);

            //Init response headers, then user can read http body from response stream.
            return responseMessage;
        }


        public void Dispose()
        {
            if (stream != null)
            {
                stream.Dispose();
            }
        }
    }
}
