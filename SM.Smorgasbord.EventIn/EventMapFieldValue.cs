using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace SM.Smorgasbord.EventIn
{
    [DataContract]
    public class EventMapFieldValue
    {
        private string fieldName;
        private string fieldValue;
        private int sequence;
        private int position;
        private int fieldType;

        [DataMember(Name = "fieldName")]
        public string FieldName
        {
            get
            {
                return fieldName;
            }
            set
            {
                fieldName = value;
            }
        }

        [DataMember(Name = "fieldValue")]
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

        [DataMember(Name = "sequence")]
        public int Sequence
        {
            get
            {
                return sequence;
            }
            set
            {
                sequence = value;
            }
        }

        [DataMember(Name = "position")]
        public int Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        [DataMember(Name = "fieldType")]
        public int FieldType
        {
            get
            {
                return fieldType;
            }
            set
            {
                fieldType = value;
            }
        }
    }
}
