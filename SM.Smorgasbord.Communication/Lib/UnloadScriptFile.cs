using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using SM.Smorgasbord.Communication.Actions;
using SM.Smorgasbord.Communication.Common.Http;
using SM.Smorgasbord.Communication.Common;
using SM.Smorgasbord.Communication.SoapEntities.Response;
using SM.Smorgasbord.Communication.Utils;

namespace SM.Smorgasbord.Communication.Lib
{
    public class UnloadScriptFile
    {
        private string location = @"C:\Users\daizhen\Research\GK_Tool\123.script.unl";
        private string unloadScriptName;

        public string FileLocation
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
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

        public void GenerateUnloadScript(DataBus dataBus)
        {
            //1 Service
            ServiceAction serviceAction = new ServiceAction(dataBus);
            serviceAction.CommandLine = "db";

            HttpResponseMessage response = serviceAction.DoAction();
            string responseData = response.GetContent();
            Service serviceEntity = serviceAction.ResponseData as Service;

            if (serviceEntity.Threads.Count > 0)
            {
                dataBus.ThreadId = serviceEntity.Threads[0].ThreadId;
            }
            else
            {
                throw new Exception("Service action Error: no thread returned");
            }

            ////Get Form
            //GetFormAction getFormAction = new GetFormAction(dataBus);
            //response = getFormAction.DoAction();
            //responseData = response.GetContent();

            //Get Data
            GetDataAction getDataAction = new GetDataAction(dataBus);
            getDataAction.FormName = "format.prompt.db.g";
            response = getDataAction.DoAction();
            responseData = response.GetContent();

           // ActionUtil.GetFormAndData(dataBus, "format.prompt.db.g");

            //Execute  in DB form to go to unload file
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

            //Execute Search for specialized unload ticket
            ExecuteAction unloadExecuteAction = new ExecuteAction(dataBus);
            unloadExecuteAction.FormName = "unload.script.g";
            unloadExecuteAction.Type = "detail";
            unloadExecuteAction.EventId = 6;
            unloadExecuteAction.ExecuteData.LoadXml("<modelChanges></modelChanges>");

            XmlElement unloadRootElement = unloadExecuteAction.ExecuteData.DocumentElement;

            XmlElement unloadInstanceElement = unloadExecuteAction.ExecuteData.CreateElement("instance");
            XmlElement unloadNameElement = unloadExecuteAction.ExecuteData.CreateElement("name");
            unloadNameElement.AppendChild(unloadExecuteAction.ExecuteData.CreateTextNode(unloadScriptName));
            unloadInstanceElement.AppendChild(unloadNameElement);
            unloadRootElement.AppendChild(unloadInstanceElement);

            response = unloadExecuteAction.DoAction();
            responseData = response.GetContent();

            ////Get unload Form
            //GetFormAction unloadGetFormAction = new GetFormAction(dataBus);
            //response = unloadGetFormAction.DoAction();
            //responseData = response.GetContent();

            ActionUtil.GetFormAndData(dataBus, "unload.script.g");

            //click unload
            ExecuteAction unloadButtonExecuteAction = new ExecuteAction(dataBus);
            unloadButtonExecuteAction.FormName = "unload.script.g";
            unloadButtonExecuteAction.Type = "detail";
            unloadButtonExecuteAction.EventId = 211;

            unloadButtonExecuteAction.ExecuteData.LoadXml("<modelChanges><focus cursorLine=\"2\" cursorLineAbs=\"2\">instance/name</focus></modelChanges>");

            response = unloadButtonExecuteAction.DoAction();
            responseData = response.GetContent();

            ActionUtil.GetFormAndData(dataBus, "file.prompt.dbu.g");

            //Get message
            GetMessageAction getMessageAction = new GetMessageAction(dataBus);
            response = getMessageAction.DoAction();
            responseData = response.GetContent();

            //Get Unload Script file
            ExecuteAction unloadButtonOneExecuteAction = new ExecuteAction(dataBus);
            unloadButtonOneExecuteAction.FormName = "file.prompt.dbu.g";
            unloadButtonOneExecuteAction.Type = "detail";
            unloadButtonOneExecuteAction.EventId = 1;

            unloadButtonOneExecuteAction.ExecuteData.LoadXml("<modelChanges> <focus cursorLine=\"5\" cursorLineAbs=\"5\">var/dbu.file.name</focus><focusContents>C:\\Users\\daizhen\\Research\\GK_Tool\\12d3.script.unl</focusContents></modelChanges>");
            XmlElement unloadButtonOneRootElement = unloadButtonOneExecuteAction.ExecuteData.DocumentElement;

            XmlElement unloadButtonOneVarElement = unloadButtonOneExecuteAction.ExecuteData.CreateElement("var");
            XmlElement unloadButtonOneFileNameElement = unloadButtonOneExecuteAction.ExecuteData.CreateElement("dbu.file.name");
            unloadButtonOneFileNameElement.AppendChild(unloadButtonOneExecuteAction.ExecuteData.CreateTextNode(location));
            unloadButtonOneVarElement.AppendChild(unloadButtonOneFileNameElement);
            unloadButtonOneRootElement.AppendChild(unloadButtonOneVarElement);

            response = unloadButtonOneExecuteAction.DoAction();
            responseData = response.GetContent();

            Execute executeEntity = unloadButtonOneExecuteAction.ResponseData as Execute;

            int attachmentCount = 0;

            try
            {
                attachmentCount = executeEntity.ClientRequestEntity.File.Attachments.Count;
            }
            catch (Exception ex)
            {
                throw new Exception("Error when get unload script");
            }

            //Handle the messages
            ActionUtil.StoreMessages(executeEntity.ClientRequestEntity);

            //Store Data to local
            int lastSlashIndex = location.LastIndexOf("\\");
            string dirPath = location.Substring(0, lastSlashIndex);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            FileStream fileStream = new FileStream(location, FileMode.Create, FileAccess.Write);

            byte[] attachData = response.GetAttachments()[0].ToArray();
            fileStream.Write(attachData, 0, attachData.Length);

            //Get following parts of the attachments
            FilePutResponseAction filePutResponseAction = new FilePutResponseAction(dataBus);
            response = filePutResponseAction.DoAction();
            responseData = response.GetContent();

            executeEntity = filePutResponseAction.ResponseData as Execute;
            ActionUtil.StoreMessages(executeEntity.ClientRequestEntity);

            //while (response.GetAttachments().Count > 0)
            for (int i = 1; i < attachmentCount;i++ )
            {
                attachData = response. GetAttachments()[0]. ToArray();
                fileStream. Write(attachData, 0, attachData. Length);

                response = filePutResponseAction. DoAction();
                responseData = response. GetContent();

                executeEntity = filePutResponseAction. ResponseData as Execute;
                ActionUtil. StoreMessages(executeEntity. ClientRequestEntity);
            }
            fileStream.Close();

            ActionUtil. GetFormAndData(dataBus, "unload.script.g");
        }
    }
}
