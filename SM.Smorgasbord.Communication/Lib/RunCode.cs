using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using SM. Smorgasbord. Communication. Actions;
using SM.Smorgasbord.Communication.Common;
using SM. Smorgasbord. Communication. Common. Http;
using SM. Smorgasbord. Communication. SoapEntities. Response;
using SM. Smorgasbord. Communication. Utils;
using System. Runtime. Serialization. Json;
using System. IO;

namespace SM. Smorgasbord. Communication. Lib
{
    public class RunCode
    {
        public T RunJSCode<T>(DataBus dataBus, string jscode) where T:class
        {
            const string codeFormat = "x js(\"{0}\")";
            string serializedCode = SerializeCode(jscode);
            string formatedCode = string.Format(codeFormat, serializedCode);

            return Run<T>(dataBus, formatedCode);
        }

        public T Run<T>(DataBus dataBus, string code) where T : class
        {
            //string serializedCode = SerializeCode(code);
            string serializedCode = code;
            DebugAction debugAction = new DebugAction(dataBus);
            debugAction.CommandLine = serializedCode;
                //string.Format(codeFormat, serializedCode);
            HttpResponseMessage response = debugAction.DoAction();

            Execute excuteObject = debugAction.ResponseData as Execute;
            ActionUtil.StoreMessages(excuteObject.ClientRequestEntity);
            while (excuteObject.ClientRequestEntity.Messages.Rearm)
            {
                //Get messages
                MessageResponseAction messageResponseAction = new MessageResponseAction(dataBus);

                response = messageResponseAction.DoAction();
                excuteObject = messageResponseAction.ResponseData as Execute;
                ActionUtil.StoreMessages(excuteObject.ClientRequestEntity);
            }
            if (typeof(T) == typeof(JsonRaw))
            {
                JsonRaw jsonRaw = new JsonRaw();
                jsonRaw.Text = excuteObject.ClientRequestEntity.Messages[0].Text;
                //response. GetContent();
                return jsonRaw as T;
            }
            string resultStr = excuteObject.ClientRequestEntity.Messages[0].Text;
            return JsonToObject<T>(resultStr);
        }

        public T RunJSCodeFromFile<T>(DataBus dataBus, string filePath) where T : class
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                StreamReader reader = new StreamReader(fileStream);
                string jsCode = reader.ReadToEnd();
                reader.Close();
                return RunJSCode<T>(dataBus, jsCode);
            }
        }

        private string SerializeCode(string rawJsCode)
        {
            StringBuilder newCode = new StringBuilder();
            string[] splitedCodes = rawJsCode. Split('\r', '\n');

            foreach (string codeItem in splitedCodes)
            {
                string currentCode = codeItem. Trim();
                //Ignore the comments
                if (!currentCode. StartsWith("//"))
                {
                    newCode. Append(currentCode. Replace("\\", "\\\\"). Replace("\"", "\\\"")). Append(" ");
                }
            }
            return newCode. ToString();
        }

        public static string ObjectToJson(object obj)
        {
            string output = string. Empty;
            DataContractJsonSerializer dataSerializer = new DataContractJsonSerializer(obj. GetType());

            using (MemoryStream ms = new MemoryStream())
            {
                dataSerializer. WriteObject(ms, obj);
                output = Encoding. UTF8. GetString(ms. ToArray());
            }
            return output;
        }

        public static T JsonToObject<T>(string strJson) where T:class
        {
            DataContractJsonSerializer deSerializer = new DataContractJsonSerializer(typeof(T));
            T result=null;

            using (MemoryStream memoryStre = new MemoryStream(Encoding. UTF8. GetBytes(strJson)))
            {
                try
                {
                    result = deSerializer.ReadObject(memoryStre) as T;
                }
                catch (Exception ex)
                {
 
                }
            }
            return result;
        }
    }
}
