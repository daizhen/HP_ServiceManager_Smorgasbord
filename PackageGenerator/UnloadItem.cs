using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;

namespace SM. Smorgasbord. PackageGenerator
{
    public class UnloadItem
    {
        private string elementName;
        private string query;


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
    }
}
