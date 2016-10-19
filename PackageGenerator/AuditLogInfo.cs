using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System.Collections.ObjectModel;

namespace SM. Smorgasbord. PackageGenerator
{
    public class AuditLogInfo
    {
        private string title;
        private string author;
        private Collection<AuditItem> items = new Collection<AuditItem>();


        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }

        public string Author
        {
            get
            {
                return author;
            }
            set
            {
                author = value;
            }
        }
        public Collection<AuditItem> Items
        {
            get
            {
                return items;
            }
        }

    }
}
