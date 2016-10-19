using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SM.Smorgasbord.Debuger
{
    public enum TraceChangedType
    {
        NewPanel = 1,
        NewRADCall,
        RADCallReturn
    }

    public class TraceChangedInfo
    {
        private DebugTraceRADCall oldRad;
        private DebugTraceRADCall newRad;
        private DebugTracePanel oldPanel;
        private DebugTracePanel newPanel;

        public DebugTraceRADCall OldRad
        {
            get 
            {
                return oldRad;
            }
            set
            {
                oldRad = value;
            }
        }
        public DebugTraceRADCall NewRad
        {
            get
            {
                return newRad;
            }
            set
            {
                newRad = value;
            }
        }
        public DebugTracePanel OldPanel
        {
            get
            {
                return oldPanel;
            }
            set
            {
                oldPanel = value;
            }
        }
        public DebugTracePanel NewPanel
        {
            get
            {
                return newPanel;
            }
            set
            {
                newPanel = value;
            }
        }

        public TraceChangedType Type
        {
            get;
            set;
        }
    }
}
