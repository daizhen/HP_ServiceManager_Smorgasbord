using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;

using System.Collections.ObjectModel;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace SM.Smorgasbord.Communication.Utils
{
    public class DeflateUtil
    {
        public static byte[] Decompress(byte[] data)
        {
            using (MemoryStream source = new MemoryStream())
            {
                //using (DeflateStream gs = new DeflateStream(new MemoryStream(data), CompressionMode.Decompress, true))
                //{
                //    //从压缩流中读出所有数据
                //    byte[] bytes = new byte[4096];
                //    int n;
                //    while ((n = gs.Read(bytes, 0, bytes.Length)) != 0)
                //    {
                //        source.Write(bytes, 0, n);
                //    }
                //}
                //return source.ToArray();

                using (InflaterInputStream gs = new InflaterInputStream(new MemoryStream(data)))
                {
                    //从压缩流中读出所有数据
                    byte[] bytes = new byte[4096];
                    int n;
                    while ((n = gs.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        source.Write(bytes, 0, n);
                    }
                    bytes = null;
                    GC.Collect();
                }
                return source.ToArray();
            }
        }

        private static byte[] Compress3(byte[] rawContent)
        {
            //java.io.PipedInputStream memoryInputStream = new java.io.PipedInputStream();
            //java.io.PipedOutputStream memoryOutputStream = new java.io.PipedOutputStream(memoryInputStream);
            //java.util.zip.DeflaterOutputStream outputStream = new java.util.zip.DeflaterOutputStream(memoryOutputStream);

            //if (rawContent. Length > 0)
            //{
            //    memoryInputStream. buffer = new sbyte[rawContent. Length];
            //    outputStream. buf = new sbyte[rawContent. Length];
            //}
            //sbyte[] convertedData = new sbyte[rawContent.Length];
            //for (int i = 0; i < rawContent.Length; i++)
            //{
            //    convertedData[i] = (sbyte)rawContent[i];
            //}

            //outputStream.write(convertedData, 0, rawContent.Length);
            //outputStream.flush();
            //outputStream.finish();
            //outputStream.close();
            //Collection<byte> compressedData = new Collection<byte>();

            //int intData = memoryInputStream.read();
            //while (intData >= 0)
            //{
            //    compressedData.Add((byte)intData);
            //    intData = memoryInputStream.read();
            //}
            //return compressedData.ToArray();
            return null;
        }


        public static byte[] Compress2(byte[] rawContent)
        {
            MemoryStream destinationStream = new MemoryStream();
            DeflateStream compressedStream = new DeflateStream(destinationStream, CompressionMode.Compress,true);
            compressedStream.Write(rawContent, 0, rawContent.Length);
            compressedStream.Close();
            byte[] compressedBytes = destinationStream.ToArray();
            destinationStream.Close();
            return compressedBytes;
        }

        public static byte[] Compress(byte[] rawContent)
        {
            MemoryStream destinationStream = new MemoryStream();

            DeflaterOutputStream outputStream = new DeflaterOutputStream(destinationStream);
            outputStream.Write(rawContent, 0, rawContent.Length);
            outputStream.Close();

            byte[] compressedBytes = destinationStream.ToArray();
            destinationStream.Close();
            return compressedBytes;
        }
    }
}
