function ColumnInfo()
{
	//Column name
	this.name = "";
	
	//Column type: array, structure,primitive.
	this.type = "";
	
	//Column level
	this.level= 0;
	
	//Children of this column
	this.children = new Array();
	
	//Parent column.
	this.parent = null;
};

function CallResult()
{
	this.success = false;
	this.message = "";
};


function SearchResult()
{
	this.table="";
	this.key="";
	this.field="";
};

function TableInfo(name)
{
	this.column = null;
	this.primaryKey=new Array();
	this.tableName=name;
};

TableInfo.prototype.Build = function()
{
	var dbdict = new SCFile("dbdict");
	dbdict.doSelect("name =\""+this.tableName+"\"");
	this.column = GetColumnInfo(dbdict);
	for(var i=0;i<dbdict.key.length();i++)
	{
		var flag = dbdict.key[i].flags;
		if(flag == 12)
		{
			for(var j=0;j<dbdict.key[i].name.length();j++)
			{
				this.primaryKey.push(dbdict.key[i].name[j].replace(/\./g,"_"));
			}
		}
	}	
};
var tableNames = new Array();
function BuildTables()
{
    //tableNames.push("application");
	//tableNames.push("Process");
	
	//tableNames.push("ScriptLibrary");
	//tableNames.push("eventregister");

	//tableNames.push("triggers");	


	//tableNames.push("inbox");	

	//tableNames.push("formatctrl");	
	//tableNames.push("notification");

	//tableNames.push("link");
	//tableNames.push("displayoption");
	
	//tableNames.push("schedule");
	//tableNames.push("globallists");
	
	

	//tableNames.push("scripts");
	//tableNames.push("eventmap");

	//tableNames.push("subtotals");
	//tableNames.push("displayscreen");

	//tableNames.push("cm3rcatphase");
	//tableNames.push("activityactions");
	//tableNames.push("validity");
	
	//tableNames.push("scaccess");
	//tableNames.push("cascadeupd");

	//tableNames.push("wizard");

	//tableNames.push("ApprovalDef");
	//tableNames.push("Alert");
	//tableNames.push("category");
	//tableNames.push("querystored");	

	//tableNames.push("format");
	
	var tables = new Array();
	
	for(var i=0;i<tableNames.length;i++)
	{
		var temTable = new TableInfo(tableNames[i]);
		temTable.Build();
		tables.push(temTable);
	}	
	return tables;
}


function FindForSingleTable(searchStr,tableRecord,tableInfo)
{
	
	//Opetimize the formatctrl.

	if(tableInfo.tableName == "format")
	{
		return FindFormatTable(searchStr,tableRecord,tableInfo);
	}

	var searchResults = new Array();
	//print(table);
	do
	{
		var result = CheckSingleRecord(searchStr,tableRecord,tableInfo.column);
		if(result.success)
		{
			var keyValue ="";
			for(var i=0;i<tableInfo.primaryKey.length;i++)
			{
				var currentKey = tableInfo.primaryKey[i];
			 	keyValue= keyValue+ currentKey+"="+eval("tableRecord."+currentKey)+" and ";
			}
			if(keyValue.length>0)
			{
				keyValue = keyValue.substring(0,keyValue.length-4);
			}
			//var temString = "Table:"+tableInfo.tableName+"  key:"+keyValue+"            field:"+result.message;
			var currentSearchResult = new SearchResult();
			currentSearchResult.table = tableInfo.tableName;
			currentSearchResult.key = keyValue;
			currentSearchResult.field = result.message;
			
			searchResults.push(currentSearchResult);
			print("" +currentSearchResult.table + "  " + currentSearchResult.key + "  " + currentSearchResult.field);
		}
	}
	while(tableRecord.getNext()==RC_SUCCESS)
	return searchResults;
}

function CheckSingleRecord(searchStr,record,columnInfo)
{
	if(record!=null)
	{
		if(columnInfo.type=="array")
		{
			var recordLength = system.functions.lng(system.functions.denull(record));
			for(var i=0;i<recordLength;i++)
			{
				var result = CheckSingleRecord(searchStr,record[i],columnInfo.children[0]);
				if(result.success)
				{
					result.message = columnInfo.name+"["+i+"]."+result.message;
					return result;
				}
			}
		}
		else if(columnInfo.type=="structure")
		{
			for(var i =0;i<columnInfo.children.length;i++)
			{
				var childColumn = columnInfo.children[i];	
				var str_record = "record[\""+(childColumn.name.replace(/\./g,'_')+"\"]");
				var result = null;
				//print(childColumn.name);
				var subRecord = eval(str_record);
				if(subRecord == null)
				{
					continue;
				}
				
				result = CheckSingleRecord(searchStr,subRecord,childColumn);
		
				if(result.success)
				{
					if(columnInfo.parent!=null && columnInfo.parent.type == "array")
					{
						//Nothing to do.
					}
					else
					{
						result.message = columnInfo.name+"." + result.message;
					}
					return result;
				}
			}
		}
		else
		{
			if(record!= undefined &&record!=null && record!="" && record.toString().indexOf(searchStr)>=0)
			{
				var result = new CallResult();
				result.success = true;
				result.message = columnInfo.name;
				return result;	
			}
		}
	}
	var defaultResult = new CallResult();
	defaultResult.success = false;
	defaultResult.message = "";
	return defaultResult;
}

function GetColumnInfo(dbdict)
{
	var rootColumn = null;	

	var index = 0;
	while(dbdict.field[index].name!=null)
	{
		var columnName = dbdict.field[index].name;
		var columnType = dbdict.field[index].type;
		var columnLevel = dbdict.field[index].level;
		
		var currentColumn = new ColumnInfo();
		currentColumn.name = columnName;
		if(columnType == 9)
		{
			currentColumn.type="structure";
		}
		else if(columnType ==8)
		{
			currentColumn.type="array";
		}
		else
		{
			currentColumn.type="primitive"; 
		}
		
		currentColumn.level = columnLevel;
		if(currentColumn.name == "descriptor")
		{
			rootColumn = currentColumn;
		}
		else
		{
			var parentColumn = rootColumn;
			var temLevel = 0;
			while(temLevel<currentColumn.level-1)
			{
				parentColumn = parentColumn.children[parentColumn.children.length - 1];
				temLevel++;
			}
			parentColumn.children[parentColumn.children.length] = currentColumn;
			currentColumn.parent = parentColumn;
		}
		index++;
	}
	return rootColumn;
}


function GetAllString(searchStr)
{
	print(new Date());

	var resultStrings = new Array();
	var tablesInfo = BuildTables();
	for(var i=0;i<tablesInfo.length;i++)
	{
		var tableName = tablesInfo[i].tableName;
		print(tableName);
		var currentTable = new SCFile(tableName);
		currentTable.doSelect("true");		
		var currentResult = FindForSingleTable(searchStr,currentTable,tablesInfo[i]);
		
		for(var j=0;j<currentResult.length;j++)
		{
			resultStrings.push(currentResult[j]);
		}
		
	}
	
	system.vars.$MS_search_count = resultStrings.length;
	
	print(new Date());
	
	return JsonEncode(resultStrings);
}

function FindFormatTable(searchStr,tableRecord,tableInfo)
{
	var searchResults = new Array();
	do
	{
		var result = new CallResult();
		var fieldLength = 	system.functions.lng(tableRecord.field);
		for(var i=0;i<fieldLength;i++)
		{
			var currentField = tableRecord.field[i];
			var currentInput = currentField.input;
			var currentProperty = currentField.property;
			
			if(currentInput!=null && currentInput!="" &&　currentInput.toString().indexOf(searchStr)>=0)
			{
				result.success = true;
				result.message = "descriptor.field["+i+"].input";
				break;
			}
			else if(currentProperty!=null && currentProperty!="" &&　currentProperty.toString().indexOf(searchStr)>=0)
			{
				result.success = true;
				result.message = "descriptor.field["+i+"].property";
				break;
			}
			else
			{
			}
		}
	
		if(result.success)
		{
			var keyValue ="";
			for(var i=0;i<tableInfo.primaryKey.length;i++)
			{
				var currentKey = tableInfo.primaryKey[i];
			 	keyValue= keyValue+ currentKey+"="+eval("tableRecord."+currentKey)+" and ";
			}
			if(keyValue.length>0)
			{
				keyValue = keyValue.substring(0,keyValue.length-4);
			}
			
			var currentSearchResult = new SearchResult();
			currentSearchResult.table = tableInfo.tableName;
			currentSearchResult.key = keyValue;
			currentSearchResult.field = result.message;
			
			searchResults.push(currentSearchResult);
		}
	}
	while(tableRecord.getNext()==RC_SUCCESS)

	return searchResults;
	
}

function TestSearch()
{
	var dbdict = new SCFile("dbdict");
	dbdict.doSelect("name =\"formatctrl\"");
	this.column = GetColumnInfo(dbdict);
	var tem = GetColumnInfo(dbdict);

	var str = showColumns(tem);
	print(str);
}

function showColumns(columns)
{
	var str = "";
	
	str+="name:"+columns.name+"\n";
	str+="type:"+columns.type+"\n";
	str+="level:"+columns.level+"\n";
	if(columns.parent!=null)
	{
		str+="parent:"+columns.parent.name+"\n";
	}
	
	for(var i=0;i<columns.children.length;i++)
	{
		str+=showColumns(columns.children[i]);
	}
	
	return str;
}

function getLength(file)
{
	var count = 0;
	while(file.getNext() == RC_SUCCESS)
	{
		print(file.DateFrom);
		count++;
	}
	return count;
}

function GetFullName(column)
{
	var str = "";
	
	var tem = column;
	while(tem!=null)
	{
		str=tem.name+"."+str;
		tem = tem.parent;
	}
	return str;
}
function GetAllUnloadTables()
{
	Array.prototype.indexof=function(element)
	{
		for(var i =0;i<this.length;i++)
		{
			if(element == this[i])
			{
				return i;
			}
		}
		return -1;
		
	};
	var unload = new SCFile("unload");
	var result = unload.doSelect("true");
	var tables = new Array();	
	var count = 0;
	while(unload.getNext()==RC_SUCCESS)
	{
		if(unload.record.filename == "RAD")
		{
			continue;
		}
		if(tables.indexof(unload.record.filename)<0 
		&& unload.record.filename!=null && unload.record.filename!="")
		{
			tables[tables.length] = unload.record.filename;
		}
	}
	return tables;
}