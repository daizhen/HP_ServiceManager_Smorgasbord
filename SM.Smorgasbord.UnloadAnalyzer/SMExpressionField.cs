using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Collections. ObjectModel;

namespace SM. Smorgasbord. UnloadAnalyzer
{
    public class SMExpressionField : SMFieldBase
    {
        private Collection<byte> fieldValue;
        public Collection<byte> FieldValue
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
