using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Collections.ObjectModel;
using System.Linq;
using SM.Smorgasbord.Communication.Utils;
using SM.Smorgasbord.Communication.SoapEntities;

namespace SM.Smorgasbord.Communication.Common.Http
{
    public class HttpResponseMessage
    {
        public HttpResponseMessage(TcpClient client)
        {
            //MemoryStream temStream = new MemoryStream();
            //client.GetStream().ReadTimeout = 1000;
            //int currentValue = client.GetStream().ReadByte();
            //byte currentByte = (byte)currentValue;
            //while (currentValue != -1)
            //{
            //    temStream.WriteByte(currentByte);
            //    try
            //    {
            //        currentValue = client.GetStream().ReadByte();
            //        currentByte = (byte)currentValue;
            //    }
            //    catch
            //    {
            //        currentValue = -1;
            //        currentByte = (byte)currentValue;
            //    }
            //}
            //temStream.Position = 0;

            this.client = client;
            this.stream = client.GetStream();
            //client.GetStream();
            BuildHeaders();
            headerLength = streamIndex;
        }
        public HttpResponseMessage(Stream stream)
        {
            this.client = null;
            this.stream = stream;
            BuildHeaders();
        }
        #region Fields

        private MemoryStream copiedStream = new MemoryStream();
        private int streamIndex = 0;
        private int headerLength;
        private Stream stream;
        private TcpClient client;
        private string version;
        private int statusCode;
        private string reasonPhrase;
        private int contentLength = -1;
        private byte[] rawData;
        private byte[] contentData;
        private Collection<byte[]> attachments = new Collection<byte[]>();
        private Dictionary<string, string> commonHeaders = new Dictionary<string, string>();
        private Collection<int> chunkIndexes = new Collection<int>();

        #endregion

        private bool HasAttachments
        {
            get
            {
                if (commonHeaders.Keys.Contains("Content-Type") &&
            commonHeaders["Content-Type"].Contains("multipart/related"))
                {
                    return true;
                }
                return false;
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

        public int StatusCode
        {
            get
            {
                return statusCode;
            }
            set
            {
                statusCode = value;
            }
        }

        public string ReasonPhrase
        {
            get
            {
                return reasonPhrase;
            }
            set
            {
                reasonPhrase = value;
            }
        }

        public int ContentLength
        {
            get
            {
                return contentLength;
            }
            set
            {
                contentLength = value;
            }
        }

        public Collection<int> ChunkIndexes
        {
            get
            {
                return chunkIndexes;
            }
        }

        public Dictionary<string, string> CommonHeaders
        {
            get
            {
                return commonHeaders;
            }
        }

        public Stream GetResponseStream()
        {
            return stream;
        }

        public MemoryStream CopiedStream
        {
            get
            {
                return copiedStream;
            }
        }

        public Collection<byte> ReadChunks()
        {
            chunkIndexes.Add(streamIndex);
            Collection<byte> content = new Collection<byte>();
            string hexValueString = ReadLine(stream);
            int chunkSize = Convert.ToInt32(hexValueString, 16);

            while (chunkSize > 0)
            {
                ////Merge data to content.
                //for (int i = 0; i < hexValueString.Length; i++)
                //{
                //    copiedStream.WriteByte((byte)hexValueString[i]);
                //}
                //streamIndex += hexValueString.Length;

                byte[] currentData = new byte[chunkSize];
                //Read data to byte array.
                int totalLength = 0;
                while (totalLength < chunkSize)
                {
                    int acturalLength = stream.Read(currentData, totalLength, chunkSize - totalLength);
                    streamIndex += acturalLength;
                    totalLength += acturalLength;
                    if (totalLength < chunkSize && acturalLength == 0)
                    {
                        throw new Exception("Chunk data is not completed");
                    }
                }
                //Merge data to content.
                for (int i = 0; i < chunkSize; i++)
                {
                    copiedStream.WriteByte(currentData[i]);
                    content.Add(currentData[i]);
                }

                //Skip the footer.
                ReadByte(stream);
                ReadByte(stream);
                chunkIndexes.Add(streamIndex);
                hexValueString = ReadLine(stream);
                chunkSize = Convert.ToInt32(hexValueString, 16);
                currentData = null;
            }

            //Skip the last footer.
            ReadByte(stream);
            ReadByte(stream);

            return content;
        }

        public byte[] GetRawData()
        {
            Stream networkStream = stream;
            if (this.rawData != null)
            {
                return this.rawData;
            }

            Collection<byte> content = new Collection<byte>();

            if (commonHeaders.Keys.Contains("Transfer-Encoding") &&
                commonHeaders["Transfer-Encoding"].Equals("chunked"))
            {
                content = ReadChunks();
            }
            else
            {
                byte[] temContent = new byte[10];
                while (true)
                {
                    int temLength = networkStream.Read(temContent, 0, temContent.Length);
                    streamIndex += temLength;
                    for (int i = 0; i < temLength; i++)
                    {
                        copiedStream.WriteByte(temContent[i]);
                        content.Add(temContent[i]);
                    }
                    if (temLength != temContent.Length)
                    {
                        break;
                    }
                }
                temContent = null;
            }

            byte[] rawData = content.ToArray();
            byte[] decompressedData = null;

            if (commonHeaders.Keys.Contains("Content-Encoding") &&
                commonHeaders["Content-Encoding"].Equals("gzip"))
            {
                decompressedData = GzipUtil.Decompress(rawData);
            }
            else
            {
                decompressedData = rawData;
            }

            this.rawData = decompressedData;
            return this.rawData;
        }

        public Collection<byte[]> GetAttachments()
        {
            if (HasAttachments == true && attachments.Count == 0)
            {
                BuildAttachments();
            }
            return attachments;

        }

        public string GetContent()
        {
            byte[] decompressedData = null;

            decompressedData = GetRawData();

            if (this.client != null && !Connected)
            {
                this.client.Close();
            }

            if (HasAttachments)
            {
                if (contentData == null)
                {
                    BuildAttachments();
                }
                decompressedData = contentData;
            }

            if (decompressedData == null || decompressedData.Length == 0)
            {
                return string.Empty;
            }

            if (commonHeaders.Keys.Contains("Content-Type") &&
               commonHeaders["Content-Type"].Contains("application/fastinfoset"))
            {
                return FastinfosetUtil.GetFastInfoSet(decompressedData);
            }
            else
            {
                return Encoding.UTF8.GetString(decompressedData);
            }
        }

        public bool Connected
        {
            get
            {
                if (commonHeaders.ContainsKey("Connection") && commonHeaders["Connection"] == "close")
                {
                    return false;
                }
                return true;
            }
        }

        #region Private Methods

        private int ReadByte(Stream stream)
        {
            int result = stream.ReadByte();
            if (result != -1)
            {
                copiedStream.WriteByte((byte)result);
                streamIndex++;
            }
            return result;
        }
        private string ReadLine(Stream stream)
        {
            Collection<byte> data = new Collection<byte>();
            //byte currentByte =(byte) stream.ReadByte();
            int streamReturnValue = ReadByte(stream);
            byte currentByte = (byte)streamReturnValue;

            while (streamReturnValue != -1 && currentByte != '\n')
            {
                if (currentByte != '\r')
                {
                    data.Add(currentByte);
                }
                streamReturnValue = ReadByte(stream);
                currentByte = (byte)streamReturnValue;
            }
            return Encoding.ASCII.GetString(data.ToArray());
        }

        private byte[] ReadLineBytes(Stream stream)
        {
            Collection<byte> data = new Collection<byte>();

            int streamReturnValue = ReadByte(stream);
            byte currentByte = (byte)streamReturnValue;

            byte preByte = 0;
            while (streamReturnValue != -1 && !(currentByte == '\n' && preByte == '\r'))
            {
                preByte = currentByte;
                data.Add(currentByte);
                streamReturnValue = ReadByte(stream);
                currentByte = (byte)streamReturnValue;
            }

            if (currentByte == '\n' && preByte == '\r')
            {
                data.RemoveAt(data.Count - 1);
            }
            return data.ToArray();
        }

        private byte[] ReadBoundaryBytes(Stream stream, string boundary, out bool isEnd)
        {
            Collection<byte> data = new Collection<byte>();

            byte[] lastBytes = new byte[boundary.Length];
            int streamReturnValue = ReadByte(stream);
            byte currentByte = (byte)streamReturnValue;

            while (streamReturnValue != -1)
            {
                data.Add(currentByte);

                if (data.Count >= boundary.Length)
                {
                    int startIndex = data.Count - boundary.Length;

                    //Asign value to lastBytes;
                    for (int i = 0; i < boundary.Length; i++)
                    {
                        lastBytes[i] = data[i + startIndex];
                    }

                    if (Encoding.ASCII.GetString(lastBytes) == boundary)
                    {
                        for (int i = 0; i < boundary.Length; i++)
                        {
                            data.RemoveAt(data.Count - 1);
                        }
                        break;
                    }
                }

                streamReturnValue = ReadByte(stream);
                currentByte = (byte)streamReturnValue;
            }
            if (streamReturnValue == -1)
            {
                isEnd = true;
            }
            else
            {
                isEnd = false;
            }
            lastBytes = null;
            return data.ToArray();
        }

        private void BuildHeaders()
        {
            string responseHeaderString = ReadLine(stream);
            string[] responseHeaders = responseHeaderString.Split(' ');
            version = responseHeaders[0];
            statusCode = Convert.ToInt32(responseHeaders[1]);
            reasonPhrase = responseHeaders[2];

            string commonHeader = ReadLine(stream);
            while (commonHeader.Length > 0)
            {
                int spliterIndex = commonHeader.IndexOf(':');
                // string[] namedValue = commonHeader.Split(':');
                string headerName = commonHeader.Substring(0, spliterIndex).Trim();
                string headerValue = commonHeader.Substring(spliterIndex + 1).Trim();
                if (!commonHeaders.Keys.Contains(headerName))
                {
                    commonHeaders.Add(headerName, headerValue.Trim());
                }
                else
                {
                    commonHeaders[headerName] = headerValue.Trim();
                }
                if (headerName.Equals("Content-Length"))
                {
                    contentLength = Convert.ToInt32(headerValue);
                }
                commonHeader = ReadLine(stream);
            }
        }

        /// <summary>
        /// Build attachments and contentData.
        /// </summary>
        private void BuildAttachments()
        {
            if (HasAttachments)
            {
                //Get the boundary
                string boundary = string.Empty;
                string contentTypeString = commonHeaders["Content-Type"].Trim();
                string[] contentTypeItems = contentTypeString.Split(';');

                for (int i = 1; i < contentTypeItems.Length; i++)
                {
                    string currentItem = contentTypeItems[i];

                    int spliterIndex = currentItem.IndexOf("=");
                    string contentTypeName = currentItem.Substring(0, spliterIndex).Trim();
                    string contentTypeValue = currentItem.Substring(spliterIndex + 1).Trim();
                    contentTypeValue = "--" + contentTypeValue;
                    if (contentTypeName == "boundary")
                    {
                        boundary = contentTypeValue.Replace("\"", "");
                    }
                }

                using (MemoryStream memoryStream = new MemoryStream(GetRawData()))
                {
                    //StreamReader streamReader = new StreamReader(memoryStream);

                    memoryStream.Position = 0;
                    //boundary
                    byte[] testData = ReadLineBytes(memoryStream);
                    //string str = streamReader.ReadLine();
                    //ContentType
                    testData = ReadLineBytes(memoryStream);
                    //Empty line
                    testData = ReadLineBytes(memoryStream);
                    //Content
                    bool isEnd;
                    contentData = ReadBoundaryBytes(memoryStream, boundary, out isEnd);

                    while (!isEnd)
                    {
                        //Skip headers
                        // string temLine = streamReader.ReadLine();
                        //ContentType
                        ReadLineBytes(memoryStream);
                        //Empty line
                        ReadLineBytes(memoryStream);

                        byte[] temBytes = ReadLineBytes(memoryStream);
                        while (temBytes.Length != 0)
                        {
                            temBytes = ReadLineBytes(memoryStream);
                        }
                        byte[] temData = ReadBoundaryBytes(memoryStream, boundary, out isEnd);
                        byte[] attachmentData = temData;

                        //Remove the \r\n from attachment
                        if (temData.Length >= 2 && temData[temData.Length - 2] == 13 && temData[temData.Length - 1] == 10)
                        {
                            attachmentData = new byte[temData.Length - 2];
                            for (int i = 0; i < attachmentData.Length; i++)
                            {
                                attachmentData[i] = temData[i];
                            }
                        }
                        if (temData.Length > 0)
                        {
                            attachments.Add(attachmentData);
                        }
                    }
                }
            }
        }

        #endregion
    }
}
