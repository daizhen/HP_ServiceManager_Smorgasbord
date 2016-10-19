using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace SM.Smorgasbord.Communication.SoapEntities.Response
{
    [Serializable]
    [XmlRoot("getPreferencesResponse", Namespace = "http://servicecenter.peregrine.com/PWS")]
    public class GetPreferences : SoapBase
    {
        private Collection<Preference> preferences = new Collection<Preference>();
        private bool attachmentCompressed;

        [XmlArray("preferences")]
        [XmlArrayItem("preference")]
        public Collection<Preference> Preferences
        {
            get
            {
                return preferences;
            }
        }

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

        public Preference this[string name]
        {
            get
            {
                foreach(Preference tem in preferences)
                  {
                      if (tem.Name.Equals(name))
                      {
                          return tem;
                      }
                }
                return null;
            }
        }
    }
}
