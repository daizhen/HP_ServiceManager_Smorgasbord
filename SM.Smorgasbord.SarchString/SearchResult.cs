using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SM.Smorgasbord.SearchString
{
    [DataContract]
    public class SearchResult
    {
        private string tableName;
        private string tableKeyValue;
        private string field;

        [DataMember(Name = "table")]
        public string TableName
        {
            get
            {
                return tableName;
            }
            set
            {
                tableName = value;
            }
        }
        [DataMember(Name = "key")]
        public string TableKeyValue
        {
            get
            {
                return tableKeyValue;
            }
            set
            {
                tableKeyValue = value;
            }
        }
        [DataMember(Name = "field")]
        public string Field
        {
            get
            {
                return field;
            }
            set
            {
                field = value;
            }
        }

    }
}
