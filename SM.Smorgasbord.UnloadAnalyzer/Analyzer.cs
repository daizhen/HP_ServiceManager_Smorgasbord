using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System.IO;
using System. Collections. ObjectModel;

namespace SM. Smorgasbord. UnloadAnalyzer
{
    public class Analyzer
    {
        public TableDef InitDbdictDef()
        {
            TableDef tableDef = new TableDef();
            StructFieldDef descriptor = new StructFieldDef("descriptor",0,1);
            //descriptor;
            //   field;
            ArrayFieldDef field = new ArrayFieldDef("field", 1, 1);
            descriptor. Children. Add(field);
            //      field;
            StructFieldDef fieldChild = new StructFieldDef("field", 2, 1);
            field. ChildField = fieldChild;
            //         name;
            PrimaryFieldDef fieldName = new PrimaryFieldDef("name", 3, 1, FieldType. Character);
            fieldChild. Children. Add(fieldName);
            //         level;
            PrimaryFieldDef fieldLevel = new PrimaryFieldDef("level", 3, 2, FieldType.Number);
            fieldChild. Children. Add(fieldLevel);
            //         index;
            PrimaryFieldDef fieldIndex = new PrimaryFieldDef("index", 3, 3, FieldType. Number);
            fieldChild. Children. Add(fieldIndex);
            //         type;
            PrimaryFieldDef fieldType = new PrimaryFieldDef("type", 3, 4, FieldType. Number);
            fieldChild. Children. Add(fieldType);
            //         structure;
            PrimaryFieldDef fieldStructure = new PrimaryFieldDef("structure", 3, 5, FieldType.Character);
            fieldChild. Children. Add(fieldStructure);
            //         db2.field.name;
            PrimaryFieldDef fieldDb2FieldName = new PrimaryFieldDef("db2.field.name", 3, 6, FieldType. Character);
            fieldChild. Children. Add(fieldDb2FieldName);
            //         db2.length;
            PrimaryFieldDef fieldDb2Length = new PrimaryFieldDef("db2.length", 3, 7, FieldType.Number);
            fieldChild. Children. Add(fieldDb2Length);
            //         sql.field.options;
            StructFieldDef sqlFieldOptions = new StructFieldDef("sql.field.options", 3, 8);
            fieldChild. Children. Add(sqlFieldOptions);
            //            sql.table.alias;
            PrimaryFieldDef sqlTableAlias = new PrimaryFieldDef("sql.table.alias", 4, 1, FieldType.Character);
            sqlFieldOptions. Children. Add(sqlTableAlias);
            //            sql.column.name;
            PrimaryFieldDef sqlColumnName = new PrimaryFieldDef("sql.column.name", 4, 2, FieldType. Character);
            sqlFieldOptions. Children. Add(sqlColumnName);
            //            sql.data.type;
            PrimaryFieldDef sqlDataType = new PrimaryFieldDef("sql.data.type", 4, 3, FieldType. Character);
            sqlFieldOptions. Children. Add(sqlDataType);
            //            sql.rcformat;
            PrimaryFieldDef sqlRcformat = new PrimaryFieldDef("sql.rcformat", 4, 4, FieldType.Logical);
            sqlFieldOptions. Children. Add(sqlRcformat);
            //         structure.array.options;
            StructFieldDef structureArrayOptions = new StructFieldDef("structure.array.options", 3, 9);
            fieldChild. Children. Add(structureArrayOptions);
            //            sa.unique.key;
            PrimaryFieldDef saUniqueKey = new PrimaryFieldDef("sa.unique.key", 4, 1, FieldType. Logical);
            structureArrayOptions. Children. Add(saUniqueKey);
            //            sa.unique.filename;
            PrimaryFieldDef saUniqueFilename = new PrimaryFieldDef("sa.unique.filename", 4, 2, FieldType.Character);
            structureArrayOptions. Children. Add(saUniqueFilename);
            //            sa.attribute.file;

            PrimaryFieldDef saAttributeFile = new PrimaryFieldDef("sa.attribute.file", 4, 3, FieldType. Character);
            structureArrayOptions. Children. Add(saAttributeFile);
            
            //            temporal;
            PrimaryFieldDef temporal = new PrimaryFieldDef("temporal", 4, 4, FieldType. Logical);
            structureArrayOptions. Children. Add(temporal);
            
            #region dump field

            StructFieldDef temField0 = new StructFieldDef("temField0", 3, 10);
            fieldChild. Children. Add(temField0);
            PrimaryFieldDef temField1 = new PrimaryFieldDef("temField1", 4, 1, FieldType.Number);
            temField0. Children. Add(temField1);
            PrimaryFieldDef temField2 = new PrimaryFieldDef("temField2", 4, 2, FieldType. Number);
            temField0. Children. Add(temField2);

            #endregion
            //   key;
            ArrayFieldDef key = new ArrayFieldDef("key", 1, 2);
            descriptor. Children. Add(key);
            //      key;
            StructFieldDef keyChild = new StructFieldDef("key", 2, 1);
            key. ChildField = keyChild;
            //         flags;
            PrimaryFieldDef keyFlags = new PrimaryFieldDef("flags", 3, 1, FieldType. Number);
            keyChild. Children. Add(keyFlags);
            //         name;
            ArrayFieldDef keyName = new ArrayFieldDef("name", 3, 2);
            keyChild. Children. Add(keyName);
            //            name;
            PrimaryFieldDef keyNameChild = new PrimaryFieldDef("key", 4, 1, FieldType. Character);
            keyName. ChildField = keyNameChild;
            //   name;

            #region 

            ArrayFieldDef temField3 = new ArrayFieldDef("temField3", 3, 3);
            keyChild. Children. Add(temField3);

            StructFieldDef temField4 = new StructFieldDef("temField4", 4, 1);
            temField3. ChildField = temField4;

            PrimaryFieldDef temField5 = new PrimaryFieldDef("temField5", 4, 2, FieldType.Character);
            temField4. Children. Add(temField5);

            PrimaryFieldDef temField6 = new PrimaryFieldDef("temField6", 4, 2, FieldType.Character);
            temField4. Children. Add(temField6);

            PrimaryFieldDef temField7 = new PrimaryFieldDef("temField6", 4, 2, FieldType.Number);
            temField4. Children. Add(temField7);

            #endregion
            PrimaryFieldDef name = new PrimaryFieldDef("name", 1, 3, FieldType. Character);
            descriptor. Children. Add(name);
            //   index.file;
            PrimaryFieldDef indexFile = new PrimaryFieldDef("index.file", 1, 4, FieldType. Number);
            descriptor. Children. Add(indexFile);
            //   index.rec.len;
            PrimaryFieldDef indexRecLen = new PrimaryFieldDef("index.rec.len", 1, 5, FieldType. Number);
            descriptor. Children. Add(indexRecLen);
            //   index.pools;
            ArrayFieldDef indexPools = new ArrayFieldDef("index.pools", 1, 6);
            descriptor. Children. Add(indexPools);
            //      index.pools;
            PrimaryFieldDef indexPoolsChild = new PrimaryFieldDef("index.pools", 2, 1, FieldType. Number);
            indexPools. ChildField = indexPoolsChild;
            //   data.file;

            PrimaryFieldDef dataFile = new PrimaryFieldDef("data.file", 1, 7, FieldType. Number);
            descriptor. Children. Add(dataFile);

            //   data.rec.len;
            PrimaryFieldDef dataRecLen = new PrimaryFieldDef("data.rec.len", 1, 8, FieldType. Number);
            descriptor. Children. Add(dataRecLen);
            //   data.pools;
            ArrayFieldDef dataPools = new ArrayFieldDef("data.pools", 1, 9);
            descriptor. Children. Add(dataPools);
            //      data.pools;
            PrimaryFieldDef dataPoolsChild = new PrimaryFieldDef("data.pools", 2, 1, FieldType. Number);
            dataPools. ChildField = dataPoolsChild;
            //   root.record;
            PrimaryFieldDef rootRecord = new PrimaryFieldDef("root.record", 1, 10, FieldType. Number);
            descriptor. Children. Add(rootRecord);
            //   sql.tables;
            ArrayFieldDef sqlTables = new ArrayFieldDef("sql.tables", 1, 11);
            descriptor. Children. Add(sqlTables);
            //      sql.tables;
            StructFieldDef sqlTablesChild = new StructFieldDef("sql.tables", 2, 1);
            sqlTables. ChildField = sqlTablesChild;
            //         sql.db.type;
            PrimaryFieldDef sqlDbType = new PrimaryFieldDef("sql.db.type", 3, 1, FieldType.Character);
            sqlTablesChild. Children. Add(sqlDbType);
            //         sql.table.name;
            PrimaryFieldDef sqlTableName = new PrimaryFieldDef("sql.table.name", 3, 2, FieldType. Character);
            sqlTablesChild. Children. Add(sqlTableName);
            //         sql.table.alias;
            PrimaryFieldDef sqlTableAlias2 = new PrimaryFieldDef("sql.table.alias", 3, 3, FieldType. Character);
            sqlTablesChild. Children. Add(sqlTableAlias2);
            //         sql.lookup.table.keys;
            ArrayFieldDef sqlLookupTableKeys = new ArrayFieldDef("sql.lookup.table.keys", 3, 4);
            sqlTablesChild. Children. Add(sqlTableAlias2);
            //            sql.lookup.table.keys;
            StructFieldDef sqlLookupTableKeysChild = new StructFieldDef("sql.lookup.table.keys", 4, 1);
            sqlLookupTableKeys. ChildField = sqlLookupTableKeysChild;
            //               sql.lookup.field;
            PrimaryFieldDef sqlLookupField = new PrimaryFieldDef("sql.lookup.field", 5, 1, FieldType. Character);
            sqlLookupTableKeysChild. Children. Add(sqlLookupField);
            //               sql.lookup.column;
            PrimaryFieldDef sqlLookupColumn = new PrimaryFieldDef("sql.lookup.column", 5, 2, FieldType. Character);
            sqlLookupTableKeysChild. Children. Add(sqlLookupColumn);
            //         sql.table.options;
            ArrayFieldDef sqlTableOptions = new ArrayFieldDef("sql.table.options", 3, 5);
            sqlTablesChild. Children. Add(sqlTableOptions);
            //            sql.table.options;
            PrimaryFieldDef sqlTableOptionsChild = new PrimaryFieldDef("sql.table.options", 4, 1, FieldType.Character);
            sqlTableOptions. ChildField = sqlTableOptionsChild;
            //   file.options;
            StructFieldDef fileOptions = new StructFieldDef(" file.options", 1, 12);
            descriptor. Children. Add(fileOptions);
            //      shadow;
            PrimaryFieldDef shadow = new PrimaryFieldDef("shadow", 2, 1, FieldType. Logical);
            fileOptions. Children. Add(shadow);
            //      reselect;
            PrimaryFieldDef reselect = new PrimaryFieldDef("reselect", 2, 2, FieldType. Logical);
            fileOptions. Children. Add(reselect);
            //   case.insensitive;
            PrimaryFieldDef caseInsensitive = new PrimaryFieldDef("case.insensitive", 1, 13, FieldType. Logical);
            descriptor. Children. Add(caseInsensitive);
            //   sysmodtime;
            PrimaryFieldDef sysmodtime = new PrimaryFieldDef("case.insensitive", 1, 14, FieldType. DateTime);
            descriptor. Children. Add(sysmodtime);
            //   sysmoduser;
            PrimaryFieldDef sysmoduser = new PrimaryFieldDef("sysmoduser", 1, 15, FieldType. Character);
            descriptor. Children. Add(sysmoduser);
            //   sysmodcount;
            PrimaryFieldDef sysmodcount = new PrimaryFieldDef("sysmoduser", 1, 16, FieldType. Number);
            descriptor. Children. Add(sysmodcount);

            tableDef. Name = "dbdict";
            tableDef. Descriptor = descriptor;
            return tableDef;
        }

        public TableDef ConvertTableDataToDef(SMTableRecord defRecord)
        {
            TableDef tableDef = new TableDef();
            SMArrayField fieldData = defRecord. Descriptor["field"] as SMArrayField;



            return tableDef;
        }

        private void FillStructDef(StructFieldDef parentFieldDef, SMArrayField fieldData,int recordIndex)
        {
            int currentLevel = parentFieldDef. Level;
            int currentIndex = parentFieldDef. Index;
        }

        public SMTableRecord GetTableRecord(Stream stream, TableDef tableDef)
        {
            SMTableRecord tableRecord = new SMTableRecord();
            tableRecord. TableSchema = tableDef;
            tableRecord. Descriptor = GetStructField(stream, tableDef. Descriptor);
            return tableRecord;
        }

        public SMStructField GetStructField(Stream stream, StructFieldDef fieldDef)
        {
            SMStructField structField = new SMStructField();
            structField. FieldDef = fieldDef;

            int firstByteValue = stream. ReadByte();

            if (firstByteValue == Consts. StartOfStruct)
            {
                int nextByteValue = stream. ReadByte();

                if (nextByteValue == Consts. EndOfStruct)
                {
                    //It should be a null struct
                    structField. IsNull = true;
                }
                else
                {
                    stream. Position--;
                    foreach (BaseFieldDef childDef in fieldDef. Children)
                    {
                        structField. Children. Add(GetField(stream, childDef));
                    }
                    int endByteValue = stream. ReadByte();
                    if (endByteValue != Consts. EndOfStruct)
                    {
                        throw new Exception("Not a valid struct:End");
                    }
                }
            }
            else
            {
                throw new Exception("Not a valid struct:Start");
            }

            return structField;
        }

        public SMArrayField GetArrayField(Stream stream,ArrayFieldDef fieldDef)
        {
            SMArrayField arrayField = new SMArrayField();
            arrayField. FieldDef = fieldDef;

            int firstByteValue = stream. ReadByte();

            if (firstByteValue == Consts. StartOfArray)
            {
                int nextByteValue = stream. ReadByte();

                if (nextByteValue == Consts. EndOfArray)
                {
                    //It should be a null struct
                    arrayField. IsNull = true;
                }
                else
                {
                    stream. Position--;
                    SMFieldBase arrayItem = GetField(stream, fieldDef. ChildField);
                    nextByteValue = stream. ReadByte();
                    while (nextByteValue !=Consts.EndOfArray)
                    {
                        stream. Position--;
                        arrayField. Children. Add(arrayItem);
                        arrayItem = GetField(stream, fieldDef. ChildField);
                        nextByteValue = stream. ReadByte();
                    }
                }
            }
            else
            {
                throw new Exception("Not a valid struct:Start");
            }

            return arrayField;
        }

        public SMCharacterField GetCharField(Stream stream, PrimaryFieldDef fieldDef)
        {
            SMCharacterField characterField = new SMCharacterField();
            characterField.FieldDef = fieldDef;

            int firstByteValue = stream. ReadByte();
            //this means the following byte indicate the length of the string.

            int strLength = 0;
            if (firstByteValue == 0x2D)
            {
                strLength = stream. ReadByte();
            }
            else if (firstByteValue < 0x2D && firstByteValue > 0x28)
            {
                strLength = firstByteValue - 0x28;
            }
            else
            {
                throw new Exception("...");
            }
            characterField. FieldValue = ReadString(stream, strLength);

            return characterField;
        }

        public SMDateTimeField GetDateTimeField(Stream stream, PrimaryFieldDef fieldDef)
        {
            SMDateTimeField dateTimeField = new SMDateTimeField();
            dateTimeField. FieldDef = fieldDef;

            int firstByteValue = stream. ReadByte();

            Collection<byte> dateRowData;
            if (firstByteValue == 0x3D)
            {
                dateRowData = ReadRawData(stream, 9);
            }
            else if (firstByteValue == 0x3C)
            {
                dateRowData = ReadRawData(stream, 4);
            }
            else
            {
                throw new Exception("Not a Date type");
            }
            foreach (byte byteValue in dateRowData)
            {
                dateTimeField. RawData. Add(byteValue);
            }
            return dateTimeField;
        }

        public SMExpressionField GetExpressionField(Stream stream, PrimaryFieldDef fieldDef)
        {
            throw new Exception("Expression");
        }

        public SMLogicalField GetLogicalField(Stream stream, PrimaryFieldDef fieldDef)
        {
            SMLogicalField logicalField = new SMLogicalField();
            logicalField. FieldDef = fieldDef;

            int firstByteValue = stream. ReadByte();
            if (firstByteValue == 0x41)
            {
                logicalField. FieldValue = true;
            }
            else if (firstByteValue == 0x42)
            {
                logicalField. FieldValue = false;
            }
            else
            {
                logicalField. FieldValue = null;
            }

            return logicalField;
        }

        public SMNumberField GetNumberField(Stream stream, PrimaryFieldDef fieldDef)
        {
            SMNumberField numberField = new SMNumberField();
            numberField. FieldDef = fieldDef;

            int firstByteValue = stream. ReadByte();
            if (firstByteValue >= 0x10 && firstByteValue <= 0x18)
            {
                numberField. FieldValue = (double)(firstByteValue - 0x10);
            }
            else
            {
                if (firstByteValue == 0x1D)
                {
                    throw new Exception("Float Value");
                }
                else
                {
                    int numberLength = firstByteValue - 0x18;
                    Collection<byte> rawNumberData = ReadRawData(stream, numberLength);
                    numberField. FieldValue = ConvertToInt(rawNumberData);
                }
            }

            return numberField;
        }

        public SMFieldBase GetField(Stream stream, BaseFieldDef fieldDef)
        {
            int firstByteValue = stream. ReadByte();
            if (firstByteValue == 0x2F || firstByteValue == 0x4F)
            {
                if (fieldDef. Type == FieldType. Array)
                {
                    SMArrayField nullField = new SMArrayField();
                    nullField. IsNull = true;
                    return nullField;
                }
                else if (fieldDef. Type == FieldType. Structure)
                {
                    SMStructField nullField = new SMStructField();
                    nullField. IsNull = true;
                    return nullField;
                }
                else if (fieldDef. Type == FieldType. Character)
                {
                    SMCharacterField nullField = new SMCharacterField();
                    nullField. IsNull = true;
                    return nullField;
                }
                else if (fieldDef. Type == FieldType. DateTime)
                {
                    SMDateTimeField nullField = new SMDateTimeField();
                    nullField. IsNull = true;
                    return nullField;
                }
                else if (fieldDef. Type == FieldType. Expression)
                {
                    SMExpressionField nullField = new SMExpressionField();
                    nullField. IsNull = true;
                    return nullField;
                }
                else if (fieldDef. Type == FieldType. Logical)
                {
                    SMLogicalField nullField = new SMLogicalField();
                    nullField. IsNull = true;
                    return nullField;
                }
                else
                {
                    SMNumberField nullField = new SMNumberField();
                    nullField. IsNull = true;
                    return nullField;
                }
            }
            else
            {
                stream. Position--;
                if (fieldDef. Type == FieldType. Array)
                {
                    return GetArrayField(stream, fieldDef as ArrayFieldDef);
                }
                else if (fieldDef. Type == FieldType. Structure)
                {
                    return GetStructField(stream, fieldDef as StructFieldDef);
                }
                else if (fieldDef. Type == FieldType. Character)
                {
                    return GetCharField(stream, fieldDef as PrimaryFieldDef);
                }
                else if (fieldDef. Type == FieldType. DateTime)
                {
                    return GetDateTimeField(stream, fieldDef as PrimaryFieldDef);
                }
                else if (fieldDef. Type == FieldType. Expression)
                {
                    return GetExpressionField(stream, fieldDef as PrimaryFieldDef);
                }
                else if (fieldDef. Type == FieldType. Logical)
                {
                    return GetLogicalField(stream, fieldDef as PrimaryFieldDef);
                }
                else
                {
                    return GetNumberField(stream, fieldDef as PrimaryFieldDef);
                }
            }
        }


        private string ReadString(Stream stream, int length)
        {
            Collection<byte> rawData = new Collection<byte>();

            for (int i = 0; i < length; i++)
            {
                int returnData = stream. ReadByte();
                rawData. Add((byte)returnData);
            }
            return Encoding. UTF8. GetString(rawData. ToArray());
        }

        private Collection<byte> ReadRawData(Stream stream, int length)
        {
            Collection<byte> rawData = new Collection<byte>();

            for (int i = 0; i < length; i++)
            {
                int returnData = stream. ReadByte();
                rawData. Add((byte)returnData);
            }
            return rawData;
        }

        private int ConvertToInt(Collection<byte> rawNumberData)
        {
            int returnValue = 0;

            for (int i = 0; i < rawNumberData.Count; i++)
            {
                returnValue = returnValue + (((int)rawNumberData[i]) << (8 * i));
            }
            return returnValue;
        }

    }
}
