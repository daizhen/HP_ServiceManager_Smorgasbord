using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using SM.Smorgasbord.Communication.Actions;
using SM.Smorgasbord.Communication.Common;
using SM.Smorgasbord.Communication.Common.Http;
using System.IO;
using System.Collections.ObjectModel;
using SM. Smorgasbord. Communication. SoapEntities. Response;
using SM. Smorgasbord. Communication. Utils;

namespace SM.Smorgasbord.Communication.Lib
{
    public class UnloadFile
    {
        private string unloadLocation = @"C:\Users\daizhen\Research\GK_Tool\123.unl";
        private string unloadScriptName = "Test_DaiZhen";

        public string UnloadLocation
        {
            get
            {
                return unloadLocation;
            }
            set
            {
                unloadLocation = value;
            }
        }

        public string UnloadScriptName
        {
            get
            {
                return unloadScriptName;
            }
            set
            {
                unloadScriptName = value;
            }
        }

        public void GenerateUnload(DataBus dataBus)
        {
            //1 Service
            ServiceAction serviceAction = new ServiceAction(dataBus);
            serviceAction.CommandLine = "db";

            HttpResponseMessage response = serviceAction.DoAction();
            string responseData = response.GetContent();

            //dataBus.ThreadId = 2;
            Service serviceEntity = serviceAction. ResponseData as Service;

            if (serviceEntity. Threads. Count > 0)
            {
                dataBus. ThreadId = serviceEntity. Threads[0]. ThreadId;
            }
            else
            {
                throw new Exception("Service action Error: no thread returned");
            }

            //Get Form
            GetFormAction getFormAction = new GetFormAction(dataBus);
            response = getFormAction.DoAction();
            responseData = response.GetContent();

            //Get Data
            GetDataAction getDataAction = new GetDataAction(dataBus);
            getDataAction.FormName = "format.prompt.db.g";
            response = getDataAction.DoAction();
            responseData = response.GetContent();

            //Execute unload
            ExecuteAction executeAction = new ExecuteAction(dataBus);
            executeAction.FormName = "format.prompt.db.g";
            executeAction.Type = "detail";
            executeAction.EventId = 0;

            executeAction.ExecuteData.LoadXml("<modelChanges></modelChanges>");
            XmlElement rootElement = executeAction.ExecuteData.DocumentElement;

            XmlElement instanceElement = executeAction.ExecuteData.CreateElement("instance");
            XmlElement fileNameElement = executeAction.ExecuteData.CreateElement("file.name");
            fileNameElement.AppendChild(executeAction.ExecuteData.CreateTextNode("unload"));
            instanceElement.AppendChild(fileNameElement);
            rootElement.AppendChild(instanceElement);

            response = executeAction.DoAction();
            responseData = response.GetContent();

            //Get  Form : unload
            GetFormAction unloadGetFormAction = new GetFormAction(dataBus);
            response = unloadGetFormAction.DoAction();
            responseData = response.GetContent();

            //Execute unload

            ExecuteAction unloadExecuteAction = new ExecuteAction(dataBus);
            unloadExecuteAction.FormName = "unload.script.g";
            unloadExecuteAction.Type = "detail";
            unloadExecuteAction.EventId = 0;

            unloadExecuteAction.ExecuteData.LoadXml("<modelChanges></modelChanges>");
            XmlElement unloadRootElement = unloadExecuteAction.ExecuteData.DocumentElement;

            XmlElement unloadInstanceElement = unloadExecuteAction.ExecuteData.CreateElement("instance");
            XmlElement unloadNameElement = unloadExecuteAction.ExecuteData.CreateElement("name");
            unloadNameElement.AppendChild(unloadExecuteAction.ExecuteData.CreateTextNode(unloadScriptName));
            unloadInstanceElement.AppendChild(unloadNameElement);
            unloadRootElement.AppendChild(unloadInstanceElement);

            response = unloadExecuteAction.DoAction();
            responseData = response.GetContent();

            //Get unload Form
            unloadGetFormAction = new GetFormAction(dataBus);
            response = unloadGetFormAction.DoAction();
            responseData = response.GetContent();

            //GetFormAndData(dataBus, "unload.script.g");

            //click unload
            ExecuteAction unloadButtonExecuteAction = new ExecuteAction(dataBus);
            unloadButtonExecuteAction.FormName = "unload.script.g";
            unloadButtonExecuteAction.Type = "detail";
            unloadButtonExecuteAction.EventId = 7;

            response = unloadButtonExecuteAction.DoAction();
            responseData = response.GetContent();

            //Get form and Data
            GetFormAndData(dataBus, "us.unload.get.name.g");

            ////Get message
            //GetMessageAction getMessageAction = new GetMessageAction(dataBus);
            //response = getMessageAction.DoAction();
            //responseData = response.GetContent();

            //Get Unload file
            ExecuteAction unloadButtonOneExecuteAction = new ExecuteAction(dataBus);
            unloadButtonOneExecuteAction.FormName = "us.unload.get.name.g";
            unloadButtonOneExecuteAction.Type = "detail";
            unloadButtonOneExecuteAction.EventId = 5;

            unloadButtonOneExecuteAction.ExecuteData.LoadXml("<modelChanges> <focus cursorLine=\"5\" cursorLineAbs=\"5\">var/dbu.file.name</focus><focusContents>C:\\Project\\GateKeeper\\20110909\\123.unl</focusContents></modelChanges>");
            XmlElement unloadButtonOneRootElement = unloadButtonOneExecuteAction.ExecuteData.DocumentElement;

            XmlElement unloadButtonOneVarElement = unloadButtonOneExecuteAction.ExecuteData.CreateElement("var");
            XmlElement unloadButtonOneFileNameElement = unloadButtonOneExecuteAction.ExecuteData.CreateElement("L.unload.file");
            unloadButtonOneFileNameElement.AppendChild(unloadButtonOneExecuteAction.ExecuteData.CreateTextNode(unloadLocation));
            unloadButtonOneVarElement.AppendChild(unloadButtonOneFileNameElement);
            unloadButtonOneRootElement.AppendChild(unloadButtonOneVarElement);

            response = unloadButtonOneExecuteAction.DoAction();
            responseData = response.GetContent();

            Execute executeEntity = unloadButtonOneExecuteAction. ResponseData as Execute;

            int attachmentCount = 0;
            if (executeEntity. ClientRequestEntity. File != null)
            {
                try
                {
                    attachmentCount = executeEntity. ClientRequestEntity. File. Attachments. Count;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error when get unload file");
                }
            }
            ActionUtil. StoreMessages(executeEntity. ClientRequestEntity);

            //Message
            //FilePutResponseAction filePutResponseAction = new FilePutResponseAction(dataBus);
            //response = filePutResponseAction.DoAction();
            //responseData = response.GetContent();


            //MessageResponseAction messageResponseAction = new MessageResponseAction(dataBus);
            //response = messageResponseAction.DoAction();
            //responseData = response.GetContent();

            //Store Data to local
            FileStream fileStream = null;

            Collection<byte[]> attachments = response.GetAttachments();
            if (attachments.Count > 0)
            {
                int lastSlashIndex = unloadLocation.LastIndexOf("\\");
                string dirPath = unloadLocation.Substring(0, lastSlashIndex);
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                fileStream = new FileStream(unloadLocation, FileMode.Create, FileAccess.Write);
                byte[] attachData = attachments[0].ToArray();
                fileStream.Write(attachData, 0, attachData.Length);
            }
            //Get following parts of the attachments
            FilePutResponseAction filePutResponseAction = new FilePutResponseAction(dataBus);
            response = filePutResponseAction.DoAction();
            responseData = response.GetContent();

            while (response.GetAttachments().Count > 0)
            {
                byte[] attachData = response.GetAttachments()[0].ToArray();
                fileStream.Write(attachData, 0, attachData.Length);
                response = filePutResponseAction.DoAction();
                responseData = response.GetContent();
            }
            if (fileStream != null)
            {
                fileStream.Flush();
                fileStream.Close();
            }
            ActionUtil. GetFormAndData(dataBus, "unload.script.g");
        }

        private void GetFormAndData(DataBus dataBus, string formName)
        {
            //Get unload Form
            GetFormAction getFormAction = new GetFormAction(dataBus);
            HttpResponseMessage response = getFormAction.DoAction();
            string responseData = response.GetContent();

            GetDataAction getDataAction = new GetDataAction(dataBus);
            getDataAction.FormName = formName;
            response = getDataAction.DoAction();
            responseData = response.GetContent();
        }
    }
}
