function field()
{
	this.flags;
	this.line;
	this.column;
	this.length;
	this.window;
	this.attribute;
	this.graph = new Array();
	this.input;
	this.output;
	this.format;
	this.property;
}

function format()
{
	this.name;
	this.field = new Array();
	this.file_name;
	this.editor;
	this.last_update;
	this.help_topic;
	this.syslanguage;
	this.sysmodcount;
	this.sysmoduser;
	this.sysmodtime;
	this.description;
}


function GetFormatInfo(formatName,language)
{
    var formatObject = new format();
    var fformat = new SCFile("format");
    var queryStr = "name=\""+formatName+"\" and syslanguage=\""+language+"\"";
    var queryResult = fformat.doSelect(queryStr);
    formatObject.name = fformat.name;
    var fieldLength = system.functions.lng(fformat.field);
    for(var i=0;i<fieldLength;i++)
    {
        var currentField = new field();
       	currentField.flags = fformat.field[i].flags;
	    currentField.line = fformat.field[i].line;
	    currentField.column = fformat.field[i].column;
	    currentField.length = fformat.field[i][3];
	    currentField.window = fformat.field[i].window;
	    currentField.attribute = fformat.field[i].attribute;
	    if(fformat.field[i].graph)
	    {
	        currentField.graph  = fformat.field[i].graph.toArray();
	    }
	    else
	    {
	        currentField.graph = null;
	    }
	    currentField.input = fformat.field[i].input;
	    currentField.output = fformat.field[i].output;
	    currentField.format = fformat.field[i].format;
	    currentField.property = fformat.field[i].property;
	    formatObject.field.push(currentField);
    }
	formatObject.file_name = fformat.file_name;
	formatObject.editor = fformat.editor;
	formatObject.last_update = fformat.last_update;
	formatObject.help_topic = fformat.help_topic ;
	formatObject.syslanguage = fformat.syslanguage;
	formatObject.sysmodcount = fformat.sysmodcount;
	formatObject.sysmoduser = fformat.sysmoduser;
	formatObject.sysmodtime = fformat.sysmodtime;
	formatObject.description = fformat.description;
    
	
	return JsonEncode(formatObject);
}