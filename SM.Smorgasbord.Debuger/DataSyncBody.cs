using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace SM.Smorgasbord.Debuger
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class DataSyncBody
    {
        public int Flag;
        public int Length;
        public MemoryStream Stream;
    }
}
