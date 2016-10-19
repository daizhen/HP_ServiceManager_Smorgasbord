using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;

namespace SM. Smorgasbord. PackageGenerator
{
    public class AuditItem
    {
        private string elementName;
        private string query;
        private string date;
        private string description;

        public string ElementName
        {
            get
            {
                return elementName;
            }
            set
            {
                elementName = value;
            }
        }

        public string Query
        {
            get
            {
                return query;
            }
            set
            {
                query = value;
            }
        }

        public string Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

    }
}
