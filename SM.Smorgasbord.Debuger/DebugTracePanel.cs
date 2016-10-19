using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SM.Smorgasbord.Debuger
{
    public class DebugTracePanel:DebugTraceBase
    {
        public RADPanel PanelInfo
        {
            get;
            set;
        }

        public override string RADName
        {
            get
            {
                return PanelInfo.RADName;
            }
            set
            {
                PanelInfo.RADName = value;
            }
        }

        public RTContext PreContext
        {
            get;
            set;
        }
        public RTContext PostContext
        {
            get;
            set;
        }
    }
}
