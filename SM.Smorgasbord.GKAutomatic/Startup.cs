using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using SM.Smorgasbord.Communication.SoapEntities;
using SM.Smorgasbord.Communication.SoapEntities.Response;


namespace SM.Smorgasbord.GKAutomatic
{
    public class Startup
    {
        public MainForm Start(Form parentForm)
        {

            //SoapMessage message = new SoapMessage();

            //message.Body = new SoapBody();
            //message.Header = new SoapHeader();
            //XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            //ns.Add("SOAP-ENV", "http://schemas.xmlsoap.org/soap/envelope/");
            //XmlSerializer xmlSerializer = new XmlSerializer(typeof(SoapMessage));
            //StreamWriter connectionWriter = new StreamWriter("CRs.xml");

            //xmlSerializer.Serialize(connectionWriter, message, ns);
            //connectionWriter.Close();
            //TestSoap();

            MainForm mainForm = new MainForm();
            //mainForm.MdiParent = parentForm;
            //mainForm.Show();
            return mainForm;
        }

        public void ConfigConnections(Form parentForm)
        {
            ChainForm chainForm = new ChainForm();
            //chainForm. MdiParent = parentForm;
            chainForm. ShowDialog();
        }

        public void ConfigRootPath(Form parentForm)
        {
            RootPathForm rootPathForm = new RootPathForm();
            //rootPathForm.MdiParent = parentForm;
            rootPathForm. ShowDialog();
        }

        private void TestSoap()
        {
            //ExecuteResponseMessage responseMessage = new ExecuteResponseMessage();
            ////responseMessage.RequestMessage.Name=
            //ClientRequest clientRequest = new ClientRequest();
            //clientRequest.Name = "message";

            //MessageItem messageItem = new MessageItem();
            //messageItem.Id = 3;
            //messageItem.Module = "format.cque";
            //messageItem.Severity = 3;
            //messageItem.Text = "'Company' entered is not valid.";

            //ThreadItem threadItem = new ThreadItem();
            //threadItem.ThreadType = "clientRequest";
            //threadItem.ThreadId = 9;

            //responseMessage.Threads = new ThreadItem[1] { threadItem };
            //responseMessage.RequestMessage = clientRequest;
            //clientRequest.MessageItems.Add(messageItem);

            //XmlTypeMapping myTypeMapping = (new SoapReflectionImporter().ImportTypeMapping(typeof(ClientRequest)));
            //XmlSerializer xmlSerializer = new XmlSerializer(myTypeMapping);

            //StreamWriter connectionWriter = new StreamWriter("CRs.xml");

            //xmlSerializer.Serialize(connectionWriter, clientRequest);
            //connectionWriter.Close();

            //IFormatter formatter = new SoapFormatter();


            //Stream stream = new FileStream("CRs.xml", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);

            //formatter.Serialize(stream, responseMessage);

            //stream.Close();

            //Deserialize

            //IFormatter formatter = new SoapFormatter();
            //Stream destream = new FileStream("CRs.xml", FileMode.Open,FileAccess.Read, FileShare.Read);

            //ExecuteResponseMessage reponseMessage2 = (ExecuteResponseMessage)formatter.Deserialize(destream);

            //destream.Close();

        }
    }
}
