using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

namespace SM.Smorgasbord.Frame
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TopForm());
            
            //MenuEtity entity = new MenuEtity();

            //MenuEtity childEntity = new MenuEtity();
            //childEntity.DllName = "DllName";
            //childEntity.ClassName = "ClassName";
            //childEntity.Method = "Method";
            //entity.Children.Add(childEntity);

            //MenuHandler handler = new MenuHandler();

            //handler.SaveMenu(entity);
        }
    }
}
