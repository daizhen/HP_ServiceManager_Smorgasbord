using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Collections. ObjectModel;

namespace SM. Smorgasbord. UnloadAnalyzer
{
    public class SMStructField : SMFieldBase
    {
        private Collection<SMFieldBase> children = new Collection<SMFieldBase>();
        public Collection<SMFieldBase> Children
        {
            get
            {
                return children;
            }
        }

        public SMFieldBase this[string name]
        {
            get
            {
                foreach(SMFieldBase item in children)
                {
                    if (item. FieldName == name)
                    {
                        return item;
                    }
                }
                return null;
            }
        }
    }
}
