using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using LiquidTechnologies.FastInfoset;

namespace SM.Smorgasbord.Communication.Utils
{
    public class FastinfosetUtil
    {
        public static string GetFastInfoSet(byte[] data)
        {
            using (MemoryStream mStream = new MemoryStream(data))
            {
                mStream.Position = 0;
                //XmlFastInfosetReader reader = new XmlFastInfosetReader(mStream);
                FIReader reader = new FIReader(mStream);

                StringBuilder resultStr = new StringBuilder();
                string resultString = ReadXml(reader);
                return resultString;
            }
        }

        public static byte[] GenerateFastInfoSet(XmlReader reader)
        {
            MemoryStream stream = new MemoryStream();
            //XmlFastInfosetWriter writer = new XmlFastInfosetWriter(stream);
            FIWriter writer = new FIWriter(stream);


            try
            {
                // 解析文件，并显示每一个节点
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (reader.IsEmptyElement) // 空元素？
                            {
                                writer.WriteStartElement(reader.Name);
                                writer.WriteEndElement();
                            }
                            else
                            {
                                writer.WriteStartElement(reader.Name);
                                if (reader.HasAttributes)   // 属性？
                                {
                                    while (reader.MoveToNextAttribute())
                                    {
                                        writer.WriteAttributeString(reader.Name, Escape(reader.Value));
                                    }
                                }
                                //writer.WriteEndElement();
                            }
                            break;
                        case XmlNodeType.Text:
                            writer.WriteString(reader.Value);
                            break;
                        case XmlNodeType.CDATA:
                            writer.WriteCData(reader.Value);
                            break;
                        case XmlNodeType.ProcessingInstruction:
                            writer.WriteProcessingInstruction(reader.Name, reader.Value);
                            break;
                        case XmlNodeType.Comment:
                            writer.WriteComment(reader.Value);
                            break;
                        case XmlNodeType.XmlDeclaration:

                            // result.AppendFormat("<?xml version='1.0'?>");
                            //Nothing to do.
                            break;
                        case XmlNodeType.Document:

                            break;
                        case XmlNodeType.DocumentType:
                            //Nothing to do.
                            //result.AppendFormat("<!DOCTYPE {0} [{1}]>", reader.Name, reader.Value);
                            break;
                        case XmlNodeType.EntityReference:
                            writer.WriteEntityRef(reader.Name);
                            //result.AppendFormat(reader.Name);
                            break;
                        case XmlNodeType.EndElement:
                            writer.WriteEndElement();
                            //result.AppendFormat("</{0}>", reader.Name);
                            break;

                        default:
                            Console.WriteLine(reader.Name);
                            break;
                    }
                }
            }
            catch (XmlException ex)
            {

            }

            byte[] content = stream.ToArray();
            stream.Close();

            return content;
        }


        public static byte[] GenerateFastInfoSet(string message)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                byte[] rawData = Encoding.UTF8.GetBytes(message);
                memoryStream.Write(rawData, 0, rawData.Length);
                memoryStream.Position = 0;

                try
                {
                    XmlReader reader = XmlReader.Create(memoryStream);
                    return GenerateFastInfoSet(reader);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private static string ReadXml(XmlReader reader)
        {
            StringBuilder result = new StringBuilder();
            Stack<string> xmlTags = new Stack<string>();
            try
            {
                // 解析文件，并显示每一个节点
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (reader.IsEmptyElement) // 空元素？
                            {
                                result.AppendFormat("<{0}/>", reader.Name);
                            }
                            else
                            {
                                xmlTags.Push(reader.Name);
                                result.AppendFormat("<{0}", reader.Name);
                                if (reader.HasAttributes)   // 属性？
                                {
                                    while (reader.MoveToNextAttribute())
                                    {
                                        result.AppendFormat(" {0}=\"{1}\"", reader.Name, Escape(reader.Value));
                                    }
                                }
                                result.Append(">");
                            }
                            break;
                        case XmlNodeType.Text:
                            result.Append(Escape(reader.Value));
                            break;
                        case XmlNodeType.CDATA:
                            result.AppendFormat("<![CDATA[{0}]]>", reader.Value);
                            break;
                        case XmlNodeType.ProcessingInstruction:
                            result.AppendFormat("<?{0} {1}?>", reader.Name, reader.Value);
                            break;
                        case XmlNodeType.Comment:
                            result.AppendFormat("<!--{0}-->", reader.Value);
                            break;
                        case XmlNodeType.XmlDeclaration:
                            result.AppendFormat("<?xml version='1.0'?>");
                            break;
                        case XmlNodeType.Document:
                            break;
                        case XmlNodeType.DocumentType:
                            result.AppendFormat("<!DOCTYPE {0} [{1}]>", reader.Name, reader.Value);
                            break;
                        case XmlNodeType.EntityReference:
                            result.AppendFormat(reader.Name);
                            break;
                        case XmlNodeType.EndElement:
                            result.AppendFormat("</{0}>", xmlTags.Pop());
                            break;

                        default:
                            XmlNodeType type = reader.NodeType;
                            break;
                    }
                }
            }
            catch (XmlException ex)
            {
                result.AppendFormat(ex.Message);
            }

            return result.ToString();
        }

        private static string Escape(string str)
        {
            string resultStr = str;

            resultStr = resultStr.Replace("&", "&amp;");
            resultStr = resultStr.Replace("\"", "&quot;");
            resultStr = resultStr.Replace("<", "&lt;");
            resultStr = resultStr.Replace("<", "&gt;");
            return resultStr;
        }
    }
}
