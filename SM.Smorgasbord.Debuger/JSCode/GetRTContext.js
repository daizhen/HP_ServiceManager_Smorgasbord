Array.prototype.indexOf = function(item)
{  
	for(var i = 0;i<this.length;i++)
    {
		if(this[i] == item)
        {  
           return i;  
        }
    }
    return -1;
};

function ExtractVars(radName, panelName)
{
    var fapplication = new SCFile("application");
    var queryStr = "application=\""+radName+"\" and label=\""+panelName+"\"";
    var queryResult = fapplication.doSelect(queryStr);
    var stringedFile = fapplication.toString();

    var reg = /\$(\d|\.|\w)+/gi;
    var variables =  stringedFile.match(reg);
    
    var varNames = new Array();
    if(variables!=null)
    {
        for(var i=0;i<variables.length;i++)
        {
    	    if(varNames.indexOf(variables[i])<0)
    	    {
    		    varNames.push(variables[i]);
    	    }
        }
    }
    return varNames;
}

function ExtractVarsFromString( varStr,existedVars)
{
    var varNames = new Array();
    if(varStr!=null)
    {
        var reg = /\$(\d|\.|\w)+/gi;
        var variables =  varStr.match(reg);
        
        if(variables!=null)
        {
            for(var i=0;i<variables.length;i++)
            {
    	        if(!existedVars[variables[i]] && varNames.indexOf(variables[i])<0)
    	        {
    		        varNames.push(variables[i]);
    		        existedVars[variables[i]] = true;
    	        }
            }
        }
    }
    return varNames;
}

function GetVarValues(varNames,varValues,existedVars,rawString,refRecords)
{	
    var currentVars = ExtractVarsFromString(rawString,existedVars);
    var currentValueStrs = new Array();
    
	if(currentVars!=null)
	{
        for(var i=0;i<currentVars.length;i++)
        {
    	    var currentVarName = currentVars[i];
    	    varNames.push(currentVarName);
        	
    	    var radExpString = "$L.debugger.tem = "+currentVarName;
    	    system.functions.evaluate(system.functions.parse(radExpString));
        	
    	    var assignStatement ="debuggerTemValue = system.vars.$L_debugger_tem";
    	    eval(assignStatement);
    	    if(debuggerTemValue!=null)
    	    {
    	        var temStr = debuggerTemValue.toString();
    		    varValues.push(temStr);
    		    currentValueStrs.push(temStr);
    		    var objectType = system.functions.type(debuggerTemValue);
    		    if(objectType ==6)
    		    {
    		        var fileName =  system.functions.filename(debuggerTemValue);
    		        
    		        if(fileName!=null && fileName !="" && !refRecords[fileName])
    		        {
    		            var dbdict = new SCFile("dbdict");
	                    dbdict.doSelect("name =\""+fileName+"\"");
    		            refRecords[fileName] = GetColumnInfo(dbdict);
    		        }
    		    }
    	    }
    	    else
    	    {
    		    varValues.push("NULL");
    	    }
        }
    }
    
    for(var i=0;i<currentValueStrs.length;i++)
    {
        GetVarValues(varNames,varValues,existedVars,currentValueStrs[i],refRecords);
    }
}

function GetContext(radName, panelName)
{
    var fapplication = new SCFile("application");
    var queryStr = "application=\""+radName+"\" and label=\""+panelName+"\"";
    var queryResult = fapplication.doSelect(queryStr);
    var stringedFile = fapplication.toString();
    var varNames = new Array();
    var varValues = new Array();
    var refRecords = new Object();
     GetVarValues(varNames,varValues,new Object(),stringedFile,refRecords);

	var contextObj = new Object();
	contextObj.names = varNames;
	contextObj.values = varValues;
	contextObj.refs = new Array();
	
	for(var i in refRecords)
	{
		if(refRecords[i]!=undefined)
		{
		    var item = new Object();
		    item.name=i;
		    item.value =  refRecords[i];
		    contextObj.refs.push(item);
		}
	}
	return JsonEncode(contextObj);
}

function ColumnInfo()
{
	//Column name
	this.name = "";
	
	//Column type: 8: array, 9: structure,and other primitive types.
	this.type = "";
	
	//Column level
	this.level= 0;
	
	//Children of this column
	this.children = new Array();
};
function GetColumnInfo(dbdict)
{
	var rootColumn = null;	

	var index = 0;
	var lastIndex = 0;
	while(dbdict.field[index].name!=null)
	{
		var columnName = dbdict.field[index].name;
		var columnType = dbdict.field[index].type;
		var columnLevel = dbdict.field[index].level;
		var columnIndex = dbdict.field[index].index;
		
		var currentColumn = new ColumnInfo();
		currentColumn.name = columnName;
		currentColumn.type = columnType;
		currentColumn.level = columnLevel;
		if(currentColumn.name == "descriptor")
		{
			rootColumn = currentColumn;
		}
		else
		{
			//Gets the parent column
			var parentColumn = rootColumn;
			var temLevel = 0;
			while(temLevel<currentColumn.level-1)
			{
				parentColumn = parentColumn.children[parentColumn.children.length - 1];
				temLevel++;
			}
			if(parentColumn.children.length < columnIndex)
			{
				parentColumn.children.push(currentColumn);
			}
		}
		index++;
	}
	return rootColumn;
};