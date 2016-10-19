using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;

namespace SM. Smorgasbord. UnloadAnalyzer
{
    public class SMNumberField : SMFieldBase
    {
        private double? fieldValue;
        public double? FieldValue
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
