using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using SM.Smorgasbord.GKAutomatic.Connections;

namespace SM.Smorgasbord.GKAutomatic.Utils
{
    public class PackageUtil
    {
        public static PackageChains LoadChains()
        {
            FileStream connectionStream = new FileStream(@"XmlData\PackageChains.xml", FileMode.Open);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(PackageChains));
            PackageChains chains = (PackageChains)xmlSerializer.Deserialize(connectionStream);
            connectionStream.Close();
            return chains;
        }

        public static GeneralConfig LoadConfig()
        {
            FileStream connectionStream = new FileStream(@"XmlData\GeneralConfig.xml", FileMode.Open);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(GeneralConfig));
            GeneralConfig config = null;
            try
            {
                config = (GeneralConfig)xmlSerializer.Deserialize(connectionStream);
            }
            catch
            {
                config = new GeneralConfig();
            }
            connectionStream.Close();
            return config;
        }

        public static void SaveConfig(GeneralConfig configInfo)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(GeneralConfig));
            StreamWriter connectionWriter = new StreamWriter(@"XmlData\GeneralConfig.xml");
            xmlSerializer.Serialize(connectionWriter, configInfo);
            connectionWriter.Close();
        }
    }
}