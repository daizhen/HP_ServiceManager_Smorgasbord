using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;

namespace SM. Smorgasbord. UnloadAnalyzer
{
    public class SMFieldBase
    {
        private BaseFieldDef fieldDef;
        private bool isNUll = false;

        public BaseFieldDef FieldDef
        {
            get
            {
                return fieldDef;
            }
            set
            {
                fieldDef = value;
            }
        }

        public bool IsNull
        {
            get
            {
                return isNUll;
            }
            set
            {
                isNUll = value;
            }
        }

        public string FieldName
        {
            get
            {
                return fieldDef. Name;
            }
        }
    }
}
