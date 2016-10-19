using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SM.Smorgasbord.Debuger
{
    public class DebugCommandEntity
    {
        public string Command
        {
            get;
            set;
        }

        public bool IsAutoStep
        {
            get;
            set;
        }
    }
}
