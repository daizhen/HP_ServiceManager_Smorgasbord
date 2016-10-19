using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.IO.Compression;
using System.IO;
using System.Linq;

namespace SM.Smorgasbord.Communication.Utils
{
    public class GzipUtil
    {
        //public static byte[] Decompress(byte[] rawContent)
        //{
        //    Collection<byte> result = new Collection<byte>();

        //    GZipStream zipDecompressStream = new GZipStream(new MemoryStream(rawContent), CompressionMode.Decompress, true);

        //    byte[] temBytes = new byte[256];

        //    int readLength = 0;
        //    do
        //    {
        //        readLength = zipDecompressStream.Read(temBytes, 0, temBytes.Length);
        //        for (int i = 0; i < readLength; i++)
        //        {
        //            result.Add(temBytes[i]);
        //        }
        //    } while (readLength == temBytes.Length);

        //    return result.ToArray<byte>();
        //}
        public static byte[] Decompress(byte[] data)
        {
            using (MemoryStream source = new MemoryStream())
            {
                using (GZipStream gs = new GZipStream(new MemoryStream(data), CompressionMode.Decompress, true))
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

        public static byte[] Compress(byte[] rawContent)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Position = 0;

                GZipStream gZipStream = new GZipStream(stream, CompressionMode.Compress);
                gZipStream.Write(rawContent, 0, rawContent.Length);
                gZipStream.Close();
                return stream.ToArray();
            }
        }

        //public static byte[] Compress(Stream stream)
        //{
        //    Collection<byte> result = new Collection<byte>();

        //    GZipStream zipDecompressStream = new GZipStream(stream, CompressionMode.Compress, true);

        //    byte[] temBytes = new byte[256];

        //    int readLength = zipDecompressStream.Read(temBytes, 0, temBytes.Length);
        //    do
        //    {
        //        for (int i = 0; i < readLength; i++)
        //        {
        //            result.Add(temBytes[i]);
        //        }
        //        readLength = zipDecompressStream.Read(temBytes, 0, temBytes.Length);
        //    } while (readLength == temBytes.Length);

        //    zipDecompressStream.Close();

        //    return result.ToArray<byte>();
        //}
    }
}
