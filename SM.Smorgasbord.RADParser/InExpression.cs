using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SM.Smorgasbord.RADParser
{
    public class InExpression:BaseExpression
    {
        private BaseExpression head;
        //This should be InEnd object.
        private BaseExpression end;

        public BaseExpression Head
        {
            get
            {
                return head;
            }
            set
            {
                head = value;
            }
        }
        public BaseExpression End
        {
            get
            {
                return end;
            }
            set
            {
                end = value;
            }
        }
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            if (head != null)
            {
                str.Append(head.ToString());
            }

            if (end != null)
            {
                str.Append(" ");
                str.Append(end.ToString());
            }
            return str.ToString();
        }
        public override string ToJavaScriptString()
        {
            StringBuilder str = new StringBuilder();
            if (end != null)
            {
                str.Append(end.ToJavaScriptString());
            }
            if (head != null)
            {
                if (str.Length == 0)
                {
                    str.Append(head.ToJavaScriptString());
                }
                else
                {
                    string headStr = head.ToJavaScriptString();
                    if (headStr[0] >= '0' && headStr[0] <= '9')
                    {
                        str.AppendFormat("[{0}]", Convert.ToInt32(headStr) - 1);
                    }
                    else
                    {
                        str.AppendFormat("[\"{0}\"]", headStr);
                    }
                }
            }
            return str.ToString();
        }
    }
}
