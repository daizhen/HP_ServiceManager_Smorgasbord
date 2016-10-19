using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;

namespace SM. Smorgasbord. UnloadAnalyzer
{
    public class Dbdict
    {
        private string name;
        private StructFieldDef descriptor;

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

        public StructFieldDef Descriptor
        {
            get
            {
                return descriptor;
            }
            set
            {
                descriptor = value;
            }
        }
    }
}
