using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
namespace SM.Smorgasbord.Debuger
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 2)]
    public class DataSyncHeader
    {
        //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public long IPAddress;
        public int Port;
        public int CookieLength;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 200)]
        public string Cookie;
        public int AuthLength;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 200)]
        public string Authorization;
        public int MonitoredSocket;
        //Indicate if debugger enabled or not.
        public int Enable;
    }   
}