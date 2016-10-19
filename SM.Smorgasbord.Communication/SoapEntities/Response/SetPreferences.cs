using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SM.Smorgasbord.Communication.SoapEntities.Response
{
    [Serializable]
    [XmlRoot("setPreferencesResponse", Namespace = "http://servicecenter.peregrine.com/PWS")]
    public class SetPreferences : SoapBase
    {
        private bool attachmentCompressed;
        [XmlAttribute("attachmentCompressed")]
        public bool AttachmentCompressed
        {
            get
            {
                return attachmentCompressed;
            }
            set
            {
                attachmentCompressed = value;
            }
        }
    }
}
