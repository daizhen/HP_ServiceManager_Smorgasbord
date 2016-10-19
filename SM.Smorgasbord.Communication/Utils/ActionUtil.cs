using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SM.Smorgasbord.Communication.Common;
using SM.Smorgasbord.Communication.Actions;
using SM.Smorgasbord.Communication.Common.Http;
using SM.Smorgasbord.Communication.SoapEntities.Response;

namespace SM.Smorgasbord.Communication.Utils
{
    public class ActionUtil
    {
        public static void GetFormAndData(DataBus dataBus, string formName)
        {
            GetFormAction getFormAction;
            HttpResponseMessage response;
            string responseData;

            //Get unload Form
            getFormAction = new GetFormAction(dataBus);
            response = getFormAction.DoAction();
            responseData = response.GetContent();

            GetDataAction getDataAction = new GetDataAction(dataBus);
            getDataAction.FormName = formName;
            response = getDataAction.DoAction();
            responseData = response.GetContent();
        }

        public static void StoreMessages(ClientRequest clientRequest)
        {
            if (clientRequest != null)
            {
                Messages messages = Messages.Create();
                foreach (MessageItem messageItem in clientRequest.Messages.Messages)
                {
                    messages.AddMessage(messageItem);
                }
            }
        }
    }
}
