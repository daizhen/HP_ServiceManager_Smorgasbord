using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;

namespace SM. Smorgasbord. UnloadAnalyzer
{
    public class ArrayFieldDef : BaseFieldDef
    {
        private BaseFieldDef childField;

        public BaseFieldDef ChildField
        {
            get
            {
                return childField;
            }
            set
            {
                childField = value;
            }
        }

        public ArrayFieldDef(string name, int level, int index)
            : base(name,level,index, FieldType.Array)
        {
        }

    }
}
