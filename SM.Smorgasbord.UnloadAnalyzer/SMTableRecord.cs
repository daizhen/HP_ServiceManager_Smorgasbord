using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;

namespace SM. Smorgasbord. UnloadAnalyzer
{
    public class SMTableRecord
    {
        private TableDef tableSchema;

        private SMStructField descriptor;

        public TableDef TableSchema
        {
            get
            {
                return tableSchema;
            }
            set
            {
                tableSchema = value;
            }
        }

        public SMStructField Descriptor
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
