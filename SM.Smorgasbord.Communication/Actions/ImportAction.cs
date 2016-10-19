using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Xml;
using SM.Smorgasbord.Communication.Common;
using System.IO;
using SM.Smorgasbord.Communication.Utils;
using SM.Smorgasbord.Communication.Common.Http;
using SM.Smorgasbord.Communication.SoapEntities;
using SM.Smorgasbord.Communication.SoapEntities.Response;

namespace SM.Smorgasbord.Communication.Actions
{
    public class ImportAction : ActionBase
    {
        private string fileLocation;

        public string FileLocation
        {
            get
            {
                return fileLocation;
            }
            set
            {
                fileLocation = value;
            }
        }

        public ImportAction(DataBus bus)
            : base("execute", bus)
        {
            IsGzip = true;
            //IsFastinfoset = true;
        }

        public override Dictionary<string, string> BuildHeaders()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();

            headers["Connection"] = "keep-alive";
            headers["Content-Type"] = "multipart/related; type=\"text/xml\"; boundary=\"----=_Part_28_4424719.1316678863847\"";

            headers["Accept-Encoding"] = "gzip";
            headers["Content-Encoding"] = "gzip";

            headers["Accept"] = "application/fastinfoset, text/xml, text/html, image/gif, image/jpeg, *; q=.2, */*; q=.2";

            return headers;
        }

        public override byte[] BuildMessage()
        {
            Collection<byte> totalData = new Collection<byte>();

            string spliter = "------=_Part_28_4424719.1316678863847";
            StringBuilder contentString = new StringBuilder();
            AttachmentHeaders contentHeaders = new AttachmentHeaders();
            // contentHeaders.Headers.Add("Content-Type", "application/fastinfoset");
            contentHeaders.Headers.Add("Content-Type", "text/xml");
            contentString.AppendLine(spliter);
            contentString.Append(contentHeaders.ToString());

            string xmlData = "<SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\"><SOAP-ENV:Header></SOAP-ENV:Header><SOAP-ENV:Body><execute attachmentCompressed=\"true\" attachmentData=\"true\"><thread>"
                + ThreadId + "</thread><fileGetResponse></fileGetResponse></execute></SOAP-ENV:Body></SOAP-ENV:Envelope>";
            XmlDocument document = new XmlDocument();
            document.LoadXml(xmlData);
            contentString.AppendLine(document.InnerXml);

            byte[] contentData = Encoding.ASCII.GetBytes(contentString.ToString());

            //Merge Data
            for (int i = 0; i < contentData.Length; i++)
            {
                totalData.Add(contentData[i]);
            }

            //byte[] testBytes = new byte[] { 0xE0, 0x00, 0x00, 0x01, 0x00, 0x38, 0xCF, 0x07, 0x53, 0x4F, 0x41, 0x50, 0x2D, 0x45, 0x4E, 0x56, 0x28, 0x68, 0x74, 0x74, 0x70, 0x3A, 0x2F, 0x2F, 0x73, 0x63, 0x68, 0x65, 0x6D, 0x61, 0x73, 0x2E, 0x78, 0x6D, 0x6C, 0x73, 0x6F, 0x61, 0x70, 0x2E, 0x6F, 0x72, 0x67, 0x2F, 0x73, 0x6F, 0x61, 0x70, 0x2F, 0x65, 0x6E, 0x76, 0x65, 0x6C, 0x6F, 0x70, 0x65, 0x2F, 0xF0, 0x3F, 0x81, 0x81, 0x07, 0x45, 0x6E, 0x76, 0x65, 0x6C, 0x6F, 0x70, 0x65, 0x3F, 0x81, 0x81, 0x05, 0x48, 0x65, 0x61, 0x64, 0x65, 0x72, 0xF0, 0x3F, 0x81, 0x81, 0x03, 0x42, 0x6F, 0x64, 0x79, 0x7C, 0x06, 0x65, 0x78, 0x65, 0x63, 0x75, 0x74, 0x65, 0x78, 0x13, 0x61, 0x74, 0x74, 0x61, 0x63, 0x68, 0x6D, 0x65, 0x6E, 0x74, 0x43, 0x6F, 0x6D, 0x70, 0x72, 0x65, 0x73, 0x73, 0x65, 0x64, 0x43, 0x74, 0x72, 0x75, 0x65, 0x78, 0x0D, 0x61, 0x74, 0x74, 0x61, 0x63, 0x68, 0x6D, 0x65, 0x6E, 0x74, 0x44, 0x61, 0x74, 0x61, 0x80, 0xF0, 0x3C, 0x05, 0x74, 0x68, 0x72, 0x65, 0x61, 0x64, 0x90, 0x32, 0xF0, 0x3C, 0x0E, 0x66, 0x69, 0x6C, 0x65, 0x47, 0x65, 0x74, 0x52, 0x65, 0x73, 0x70, 0x6F, 0x6E, 0x73, 0x65, 0xFF, 0xFF, 0xF0, 0xE0, 0x00, 0x00, 0x01, 0x00, 0x3C, 0x08, 0x23, 0x64, 0x6F, 0x63, 0x75, 0x6D, 0x65, 0x6E, 0x74, 0x38, 0xCF, 0x07, 0x53, 0x4F, 0x41, 0x50, 0x2D, 0x45, 0x4E, 0x56, 0x28, 0x68, 0x74, 0x74, 0x70, 0x3A, 0x2F, 0x2F, 0x73, 0x63, 0x68, 0x65, 0x6D, 0x61, 0x73, 0x2E, 0x78, 0x6D, 0x6C, 0x73, 0x6F, 0x61, 0x70, 0x2E, 0x6F, 0x72, 0x67, 0x2F, 0x73, 0x6F, 0x61, 0x70, 0x2F, 0x65, 0x6E, 0x76, 0x65, 0x6C, 0x6F, 0x70, 0x65, 0x2F, 0xF0, 0x3F, 0x81, 0x81, 0x07, 0x45, 0x6E, 0x76, 0x65, 0x6C, 0x6F, 0x70, 0x65, 0x3F, 0x81, 0x81, 0x05, 0x48, 0x65, 0x61, 0x64, 0x65, 0x72, 0xF0, 0x3F, 0x81, 0x81, 0x03, 0x42, 0x6F, 0x64, 0x79, 0x7C, 0x06, 0x65, 0x78, 0x65, 0x63, 0x75, 0x74, 0x65, 0x78, 0x13, 0x61, 0x74, 0x74, 0x61, 0x63, 0x68, 0x6D, 0x65, 0x6E, 0x74, 0x43, 0x6F, 0x6D, 0x70, 0x72, 0x65, 0x73, 0x73, 0x65, 0x64, 0x43, 0x74, 0x72, 0x75, 0x65, 0x78, 0x0D, 0x61, 0x74, 0x74, 0x61, 0x63, 0x68, 0x6D, 0x65, 0x6E, 0x74, 0x44, 0x61, 0x74, 0x61, 0x80, 0xF0, 0x3C, 0x05, 0x74, 0x68, 0x72, 0x65, 0x61, 0x64, 0x90, 0x32, 0xF0, 0x3C, 0x0E, 0x66, 0x69, 0x6C, 0x65, 0x47, 0x65, 0x74, 0x52, 0x65, 0x73, 0x70, 0x6F, 0x6E, 0x73, 0x65, 0xFF, 0xFF, 0xFF };

            ////Merge Data
            //for (int i = 0; i < testBytes.Length; i++)
            //{
            //    totalData.Add(testBytes[i]);
            //}
            //totalData.Add(0x0d);
            //totalData.Add(0x0A);

            //Attachment
            StringBuilder attachmentHeaderString = new StringBuilder();
            byte[] rawAttachData = GetContentOfFile();
            attachmentHeaderString.AppendLine(spliter);
            AttachmentHeaders attachmentHeaders = new AttachmentHeaders();
            attachmentHeaders.Headers.Add("uncompressedSize", rawAttachData.Length.ToString());
            attachmentHeaders.Headers.Add("Content-Type", "application/x-gzip");
            attachmentHeaders.Headers.Add("Content-Location", fileLocation);
            attachmentHeaders.Headers.Add("Content-ID", "397e9e76328ee70a0132902d0be70033");
            attachmentHeaderString.Append(attachmentHeaders.ToString());
            byte[] attachmentHeaderData = Encoding.ASCII.GetBytes(attachmentHeaderString.ToString());

            //Merge Data
            for (int i = 0; i < attachmentHeaderData.Length; i++)
            {
                totalData.Add(attachmentHeaderData[i]);
            }

            //Compressed Data
            byte[] attachmentContentData = DeflateUtil.Compress(rawAttachData);

            //Merge Data
            for (int i = 0; i < attachmentContentData.Length; i++)
            {
                totalData.Add(attachmentContentData[i]);
            }

            //Add last spliter

            string lastString = "\r\n" + spliter + "--";

            byte[] lastBytes = Encoding.ASCII.GetBytes(lastString);

            //Merge Data
            for (int i = 0; i < lastBytes.Length; i++)
            {
                totalData.Add(lastBytes[i]);
            }
            return totalData.ToArray();
        }


        private byte[] GetContentOfFile()
        {
            Collection<byte> fileData = new Collection<byte>();
            using (FileStream fileStream = new FileStream(fileLocation, FileMode.Open, FileAccess.Read))
            {
                int intData = fileStream.ReadByte();
                while (intData >= 0)
                {
                    fileData.Add((byte)intData);
                    intData = fileStream.ReadByte();
                }
                byte[] rawData = fileData.ToArray();
                return rawData;
            }
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
