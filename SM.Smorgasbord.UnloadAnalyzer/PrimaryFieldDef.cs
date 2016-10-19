using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;

namespace SM. Smorgasbord. UnloadAnalyzer
{
    public class PrimaryFieldDef : BaseFieldDef
    {
        public PrimaryFieldDef(string name, int level, int index,FieldType type)
            : base(name,level,index, type)
        {
        }

    }
}
