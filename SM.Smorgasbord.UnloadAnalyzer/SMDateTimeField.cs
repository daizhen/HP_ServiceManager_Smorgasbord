using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System.Collections.ObjectModel;

namespace SM. Smorgasbord. UnloadAnalyzer
{
    public class SMDateTimeField : SMFieldBase
    {
        private Collection<byte> rawData = new Collection<byte>();

        private DateTime fieldValue;

        public Collection<byte> RawData
        {
            get
            {
                return rawData;
            }
        }

        public DateTime FieldValue
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
