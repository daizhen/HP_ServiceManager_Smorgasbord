using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System.Collections.ObjectModel;

namespace SM. Smorgasbord. UnloadAnalyzer
{
    public class StructFieldDef:BaseFieldDef
    {
        private Collection<BaseFieldDef> children = new Collection<BaseFieldDef>();

        public Collection<BaseFieldDef> Children
        {
            get
            {
                return children;
            }
        }

        public BaseFieldDef this[string name]
        {
            get
            {
                foreach (BaseFieldDef childField in children)
                {
                    if (name == childField. Name)
                    {
                        return childField;
                    }
                }
                return null;
            }
        }

        public StructFieldDef(string name, int level, int index)
            : base(name,level,index, FieldType.Structure)
        {
        }

    }
}
