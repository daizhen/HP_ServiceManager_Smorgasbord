using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SM.Smorgasbord.Communication.Actions;
using SM.Smorgasbord.Communication.Common.Http;
using SM.Smorgasbord.Communication.Common;
using System.Xml;
using SM.Smorgasbord.Communication.SoapEntities.Response;
using SM.Smorgasbord.Communication.Utils;

namespace SM.Smorgasbord.Communication.Lib
{
    public class ImportFile
    {
        private string unloadLocation = @"C:\Users\daizhen\Research\GK_Tool\Test_DaiZhen.script1.unl";
        //private string name;


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

        public void Import(DataBus dataBus)
        {
            //1 Service
            ServiceAction serviceAction = new ServiceAction(dataBus);
            serviceAction.CommandLine = "db";
            HttpResponseMessage response = serviceAction.DoAction();

            Service serviceEntity = serviceAction.ResponseData as Service;
            if (serviceEntity.Threads.Count > 0)
            {
                dataBus.ThreadId = serviceEntity.Threads[0].ThreadId;
            }
            else
            {
                throw new Exception("Service action Error: no thread returned");
            }
            string responseData = response.GetContent();


            ////Get Form
            //GetFormAction getFormAction = new GetFormAction(dataBus);
            //response = getFormAction.DoAction();
            //responseData = response.GetContent();

            //Get Data
            GetDataAction getDataAction = new GetDataAction(dataBus);
            getDataAction.FormName = "format.prompt.db.g";
            response = getDataAction.DoAction();
            responseData = response.GetContent();

            //Execute inport/load
            ExecuteAction executeAction = new ExecuteAction(dataBus);
            executeAction.FormName = "format.prompt.db.g";
            executeAction.Type = "detail";
            executeAction.EventId = 208;

            executeAction.ExecuteData.LoadXml("<modelChanges><focus cursorLine=\"7\" cursorLineAbs=\"10\">instance/file.name</focus></modelChanges>");
            response = executeAction.DoAction();
            responseData = response.GetContent();

            //Get  Form : import
            GetFormAction unloadGetFormAction = new GetFormAction(dataBus);
            response = unloadGetFormAction.DoAction();
            responseData = response.GetContent();

            //Execute import
            ExecuteAction importExecuteAction = new ExecuteAction(dataBus);
            importExecuteAction.FormName = "database.load.prompt.g";
            importExecuteAction.Type = "detail";
            importExecuteAction.EventId = 1;

            importExecuteAction.ExecuteData.LoadXml("<modelChanges></modelChanges>");
            XmlElement importRootElement = importExecuteAction.ExecuteData.DocumentElement;

            XmlElement importVarElement = importExecuteAction.ExecuteData.CreateElement("var");
            XmlElement importNameElement = importExecuteAction.ExecuteData.CreateElement("dbl.file.name");
            importNameElement.AppendChild(importExecuteAction.ExecuteData.CreateTextNode(unloadLocation));
            importVarElement.AppendChild(importNameElement);
            importRootElement.AppendChild(importVarElement);

            response = importExecuteAction.DoAction();
            responseData = response.GetContent();

            Execute executeEntity = null;
            do
            {
                ImportAction uploadAction = new ImportAction(dataBus);
                uploadAction.FileLocation = unloadLocation;
                response = uploadAction.DoAction();
                responseData = response.GetContent();

                executeEntity = uploadAction.ResponseData as Execute;
                ActionUtil.StoreMessages(executeEntity.ClientRequestEntity);
            }
            while (executeEntity.ClientRequestEntity.Name != "message");


            while (executeEntity.ClientRequestEntity != null)
            {
                try
                {
                    MessageResponseAction messageAction = new MessageResponseAction(dataBus);
                    response = messageAction.DoAction();
                    // responseData = response.GetContent();
                    executeEntity = messageAction.ResponseData as Execute;
                    ActionUtil.StoreMessages(executeEntity.ClientRequestEntity);
                }
                catch (Exception ex)
                {
                    break;
                }
            }
        }
    }
}
