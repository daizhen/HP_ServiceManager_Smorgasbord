using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.IO;
using SM.Smorgasbord.Communication.Common;

namespace SM.Smorgasbord.Communication.Lib
{
    public class JSCodeRunner
    {
        private Collection<string> includedFiles = new Collection<string>();

        public void Include(string fileName)
        {
            includedFiles.Add(fileName);
        }
        public T Run<T>(DataBus dataBus, string jscode) where T : class
        {
            RunCode runCode = new RunCode();
            //runCode.RunJSCodeFromFile<AuditLogInfo> (dataBus,

            StringBuilder codeStr = new StringBuilder();
            foreach (string fileName in includedFiles)
            {
                using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    StreamReader reader = new StreamReader(fileStream);
                    string currentJSCode = reader.ReadToEnd();
                    reader.Close();
                    codeStr.AppendLine(currentJSCode);
                }
            }

            codeStr.AppendLine(jscode);
            return runCode.RunJSCode<T>(dataBus, codeStr.ToString());
        }
    }
}
