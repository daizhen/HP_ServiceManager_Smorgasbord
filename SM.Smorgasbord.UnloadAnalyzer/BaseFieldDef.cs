using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;

namespace SM. Smorgasbord. UnloadAnalyzer
{
    public class BaseFieldDef
    {
        private string name;
        private int level;
        private int index;
        private FieldType type;
        private string structure;
        private string db2FieldName;
        private int db2Length;

        private StructFieldDef sqlFieldOptions;

        private StructFieldDef structArrayOptions;

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

        public int Level
        {
            get
            {
                return level;
            }
            set
            {
                level = value;
            }
        }

        public int Index
        {
            get
            {
                return index;
            }
            set
            {
                index = value;
            }
        }

        public FieldType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        public string Structure
        {
            get
            {
                return structure;
            }
            set
            {
                structure = value;
            }
        }

        public string DB2FieldName
        {
            get
            {
                return db2FieldName;
            }
            set
            {
                db2FieldName = value;
            }
        }

        public int DB2Length
        {
            get
            {
                return db2Length;
            }
            set
            {
                db2Length = value;
            }
        }

        public StructFieldDef SqlFieldOptions
        {
            get
            {
                return sqlFieldOptions;
            }
            set
            {
                sqlFieldOptions = value;
            }
        }

        public StructFieldDef StructArrayOptions
        {
            get
            {
                return structArrayOptions;
            }
            set
            {
                structArrayOptions = value;
            }
        }

        public BaseFieldDef(string name, int level, int index, FieldType type)
        {
            this. name = name;
            this. level = level;
            this. index = index;
            this. type = type;
        }

        public void InitSqlFieldOptions()
        {
            //sqlFieldOptions = new StructFieldDef();

            //PrimaryFieldDef sqlTableAlias = new PrimaryFieldDef();
            //sqlTableAlias. Name = "sql.table.alias";
            //sqlTableAlias. Type = FieldType. Character;
            //sqlFieldOptions. Children. Add(sqlTableAlias);

            //PrimaryFieldDef sqlColumnName = new PrimaryFieldDef();
            //sqlColumnName. Name = "sql.column.name";
            //sqlColumnName. Type = FieldType. Character;
            //sqlFieldOptions. Children. Add(sqlColumnName);

            //PrimaryFieldDef sqlDataType = new PrimaryFieldDef();
            //sqlDataType. Name = "sql.data.type";
            //sqlDataType. Type = FieldType. Character;
            //sqlFieldOptions. Children. Add(sqlDataType);

            //PrimaryFieldDef sqlRCFormat = new PrimaryFieldDef();
            //sqlRCFormat. Name = "sql.rcformat";
            //sqlRCFormat. Type = FieldType.Logical;
            //sqlFieldOptions. Children. Add(sqlRCFormat);
        }

        public void InitStructArrayOptions()
        {
            //structArrayOptions = new StructFieldDef();

            //PrimaryFieldDef saUniqueKey = new PrimaryFieldDef("sa.unique.key",0,0,FieldType. Logical);
           
            //structArrayOptions. Children. Add(saUniqueKey);

            //PrimaryFieldDef saUniqueFilename = new PrimaryFieldDef();
            //saUniqueFilename. Name = "sa.unique.filename";
            //saUniqueFilename. Type = FieldType.Character;
            //structArrayOptions. Children. Add(saUniqueFilename);

            //PrimaryFieldDef  saAttributeFile = new PrimaryFieldDef();
            //saAttributeFile. Name = "sa.attribute.file";
            //saAttributeFile. Type = FieldType. Character;
            //structArrayOptions. Children. Add(saAttributeFile);

            //PrimaryFieldDef temporal = new PrimaryFieldDef();
            //temporal. Name = "temporal";
            //temporal. Type = FieldType. Character;
            //structArrayOptions. Children. Add(temporal);
        }
    }
}
