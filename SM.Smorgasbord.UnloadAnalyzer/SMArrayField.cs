using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Collections. ObjectModel;

namespace SM. Smorgasbord. UnloadAnalyzer
{
    public class SMArrayField : SMFieldBase
    {
        private Collection<SMFieldBase> children = new Collection<SMFieldBase>();
        public Collection<SMFieldBase> Children
        {
            get
            {
                return children;
            }
        }
    }
}
