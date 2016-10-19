using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace SM.Smorgasbord.Debuger
{
    public class DebugTraceRADCall : DebugTraceBase
    {
        private string radName;
        private Collection<DebugTraceBase> traces = new Collection<DebugTraceBase>();

        public Collection<DebugTraceBase> Traces
        {
            get
            {
                return traces;
            }
        }

        public override string RADName
        {
            get
            {
                return radName;
            }
            set
            {
                radName = value;
            }
        }
    }
}
