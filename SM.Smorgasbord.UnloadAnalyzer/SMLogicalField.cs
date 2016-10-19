using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;

namespace SM. Smorgasbord. UnloadAnalyzer
{
    public class SMLogicalField : SMFieldBase
    {
        private bool? fieldValue;
        public bool? FieldValue
        {
            get
            {
                return fieldValue;
            }
            set
            {
                fieldValue = value;
            }
        }
    }
}
