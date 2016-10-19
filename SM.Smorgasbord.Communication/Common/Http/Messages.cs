using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using SM.Smorgasbord.Communication.SoapEntities;
using SM.Smorgasbord.Communication.SoapEntities.Response;

namespace SM.Smorgasbord.Communication.Common.Http
{

    public delegate void ItemAddHandler(Object sender, MessageItem item);

    /// <summary>
    /// This class used to store all messages
    /// </summary>
    public class Messages
    {
        private static Messages messages;
        public event ItemAddHandler OnItemAdded;
       
        private Messages()
        {
        }

        public static Messages Create()
        {
            if (messages == null)
            {
                messages = new Messages();
            }
            return messages;
        }


        private Collection<MessageItem> items = new Collection<MessageItem>();


        public void AddMessage(MessageItem item)
        {
            items. Add(item);
            if (OnItemAdded != null)
            {
                OnItemAdded(this, item);
            }
        }
        public Collection<MessageItem> Items
        {
            get
            {
                return items;
            }
        }

    }
}
