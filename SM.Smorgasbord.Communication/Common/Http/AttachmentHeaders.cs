using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SM.Smorgasbord.Communication.Common.Http
{
    public class AttachmentHeaders
    {
        private Dictionary<string, string> headers = new Dictionary<string, string>();

        public Dictionary<string, string> Headers
        {
            get
            {
                return headers;
            }
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            foreach (KeyValuePair<string, string> keyValuePair in headers)
            {
                str.AppendLine(keyValuePair.Key + ": " + keyValuePair.Value);
            }
            str.AppendLine();
            return str.ToString();
        }
    }
}
