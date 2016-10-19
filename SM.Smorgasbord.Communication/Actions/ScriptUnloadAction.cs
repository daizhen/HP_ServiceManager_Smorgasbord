using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SM.Smorgasbord.Communication.Common;

namespace SM.Smorgasbord.Communication.Actions
{
    public class ScriptUnloadAction : ExecuteAction
    {
        public ScriptUnloadAction(DataBus bus)
            : base(bus)
        {

        }

        public override string BuildXmlCommandString()
        {
            string xmlCommand = "<SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\">" +
            "<SOAP-ENV:Header></SOAP-ENV:Header><SOAP-ENV:Body><execute><thread>2</thread><formname>file.prompt.dbu.g</formname><type>detail</type><event>1</event><modelChanges><focusContents>C:\\Users\\daizhen\\Research\\GK_Tool\\test.script.unl</focusContents><var><dbu.file.name>C:\\Users\\daizhen\\Research\\GK_Tool\\test.script.unl</dbu.file.name></var><focus cursorLine=\"5\" cursorLineAbs=\"5\">var/dbu.file.name</focus></modelChanges></execute></SOAP-ENV:Body></SOAP-ENV:Envelope>";

            return xmlCommand;
        }
    }
}
