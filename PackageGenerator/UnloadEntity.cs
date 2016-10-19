using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System.Collections.ObjectModel;

namespace SM. Smorgasbord. PackageGenerator
{
    public class UnloadEntity
    {
        private string name;
        private bool unload;
        private bool purge;
        private Collection<UnloadItem> items = new Collection<UnloadItem>();

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public bool Unload
        {
            get
            {
                return unload;
            }
            set
            {
                unload = value;
            }
        }

        public bool Purge
        {
            get
            {
                return purge;
            }
            set
            {
                purge = value;
            }
        }

        public Collection<UnloadItem> Items
        {
            get
            {
                return items;
            }
        }
    }
}
