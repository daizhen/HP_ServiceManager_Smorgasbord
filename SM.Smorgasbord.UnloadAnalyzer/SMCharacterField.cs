using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;

namespace SM. Smorgasbord. UnloadAnalyzer
{
    public class SMCharacterField: SMFieldBase
    {
        private string fieldValue;
        public string FieldValue
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
