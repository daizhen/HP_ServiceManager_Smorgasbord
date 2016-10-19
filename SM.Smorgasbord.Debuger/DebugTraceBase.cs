using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SM.Smorgasbord.Debuger
{
    public class DebugTraceBase
    {
        public DebugTraceBase Parent
        {
            get;
            set;
        }

        public virtual string RADName
        {
            get;
            set;
        }
    }
}
