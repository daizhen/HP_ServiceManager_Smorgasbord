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

function GetVarValues(variables)
{
	var varValues = new Array();
	
	if(variables!=null)
	{
        for(var i=0;i<variables.length;i++)
        {
    	    var debuggerTemValue;
    	    var currentVarName = variables[i];
        	
    	    var radExpString = "$L.debugger.tem = "+currentVarName;
    	    system.functions.evaluate(system.functions.parse(radExpString));
        	
    	    var assignStatement ="debuggerTemValue = system.vars.$L_debugger_tem";
    	    eval(assignStatement);
    	    if(debuggerTemValue!=null)
    	    {
    		    varValues.push(debuggerTemValue.toString());
    	    }
    	    else
    	    {
    		    varValues.push("NULL");
    	    }
        }
    }
    return varValues;
}

function GetContext(radName, panelName)
{
	var contextObj = new Object();
	contextObj.names = ExtractVars(radName,panelName);
	contextObj.values = GetVarValues(contextObj.names);
	return JsonEncode(contextObj);
}