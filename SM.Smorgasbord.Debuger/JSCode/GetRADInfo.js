function application()
{
	this.application;
	this.label;
	this.comments;
	this.normal;
	this.error;
	this.format;
	this.file;
	this.all_null;
	this.key_null;
	this.key_dupl;
	this.second_file;
	this.target_file;
	this.record;
	this.query;
	this.name;
	this.names = new Array();
	this.values = new Array();
	this.prompt;
	this.condition = new Array();
	this.option = new Array();
	this.description = new Array();
	this.exit = new Array();
	this.index;
	this.empty;
	this.one_rec;
	this.text;
	this.statements = new Array();
	this.cond_input;
	this.sort = new Array();
	this.types = new Array();
	this.levels = new Array();
	this.numbers = new Array();
	this.number1;
	this.string1;
	this.time1;
	this.boolean1;
	this.times = new Array();
	this.expressions = new Array();
	this.comments_more = new Array();
	this.file_variables = new Array();
	this.second_record;
	this.booleans = new Array();
	this.table;
	this.tables = new Array();
	this.sysmodcount;
	this.sysmoduser;
	this.sysmodtime;
}
function GetRADInfo(radName,label)
{
    var applicationObject = new application();
    var fapplication = new SCFile("application");
    var queryStr = "application=\""+radName+"\" and label=\""+label+"\"";
    var queryResult = fapplication.doSelect(queryStr);
    
    var arrayedFile = fapplication.toArray();
    
    applicationObject.application =  fapplication.application;
	applicationObject.label =  arrayedFile[1];
	//fapplication.label;
	applicationObject.comments =  fapplication.comments;
	applicationObject.normal =   arrayedFile[3];
	//fapplication.normal;
	applicationObject.error =  arrayedFile[4];
	//fapplication.error;
	applicationObject.format = fapplication.format;
	applicationObject.file = fapplication.file;
	applicationObject.all_null =  arrayedFile[7];
	//fapplication.all_null;
	applicationObject.key_null = arrayedFile[8];
	//fapplication.key_null;
	applicationObject.key_dupl = arrayedFile[9];
	//fapplication.key_dupl;
	applicationObject.second_file = fapplication.second_file;
	applicationObject.target_file = fapplication.target_file;
	applicationObject.record = fapplication.record;
	applicationObject.query = fapplication.query;
	applicationObject.name = fapplication.name;
	if(system.functions.type(fapplication.names)==8)
	{
		applicationObject.names = fapplication.names.toArray();
	}
	else
	{
		applicationObject.names.push( fapplication.names);
	}
	if(system.functions.type(fapplication.values)==8)
	{
		applicationObject.values = fapplication.values.toArray();
	}
	else
	{
			applicationObject.values.push( fapplication.values);
	}
	applicationObject.prompt = fapplication.prompt;
	if(system.functions.type(fapplication.condition)==8)
	{
		applicationObject.condition = fapplication.condition.toArray();
	}
	else
	{
		applicationObject.condition.push( fapplication.condition);
	}
	if(system.functions.type(fapplication.option)==8)
	{
		applicationObject.option = fapplication.option.toArray();
	}
	else
	{
		applicationObject.option.push(fapplication.option);
	}
	if(system.functions.type(fapplication.description)==8)
	{
		applicationObject.description = fapplication.description.toArray();
	}
	else
	{
		applicationObject.description.push(fapplication.description);
	}
	if(system.functions.type(fapplication.exit)==8)
	{
		applicationObject.exit = fapplication.exit.toArray();
	}
	else
	{
		applicationObject.exit.push(fapplication.exit);
	}
	applicationObject.index = fapplication.index;
	applicationObject.empty = arrayedFile[23];
	//fapplication.empty;
	applicationObject.one_rec = arrayedFile[24];
	//fapplication.one_rec;
	applicationObject.text = fapplication.text;
	if(system.functions.type(fapplication.statements)==8)
	{
		applicationObject.statements = fapplication.statements.toArray();
	}
	else
	{
		applicationObject.statements.push(fapplication.statements);
	}
	applicationObject.cond_input = fapplication.cond_input;
	if(system.functions.type(fapplication.sort)==8)
	{
		applicationObject.sort = fapplication.sort.toArray();
	}
	else
	{
		applicationObject.sort.push( fapplication.sort);
	}
	if(system.functions.type(fapplication.types)==8)
	{
		applicationObject.types = fapplication.types.toArray();
	}
	else
	{
		applicationObject.types.push(fapplication.types);
	}
	if(system.functions.type(fapplication.levels)==8)
	{
		applicationObject.levels = fapplication.levels.toArray();
	}
	else
	{
		applicationObject.levels.push( fapplication.levels);
		
	}
	if(system.functions.type(fapplication.numbers)==8)
	{
		applicationObject.numbers = fapplication.numbers.toArray();
	}
	else
	{
		applicationObject.numbers.push(fapplication.numbers);
	}
	applicationObject.number1 = fapplication.number1;
	applicationObject.string1 = fapplication.string1;
	applicationObject.time1 = fapplication.time1;
	if( fapplication.boolean1)
	{
	    applicationObject.boolean1 = fapplication.boolean1;
	}
	if(system.functions.type(fapplication.times)==8)
	{
		applicationObject.times = fapplication.times.toArray();
	}
	else
	{
		applicationObject.times.push( fapplication.times);
	}
	if(system.functions.type(fapplication.expressions)==8)
	{
		applicationObject.expressions = fapplication.expressions.toArray();
	}
	else
	{
		applicationObject.expressions.push( fapplication.expressions);
		
	}
	if(system.functions.type(fapplication.comments_more)==8)
	{
		applicationObject.comments_more = fapplication.comments_more.toArray();
	}
	else
	{
		applicationObject.comments_more.push(fapplication.comments_more);
	}
	if(system.functions.type(fapplication.file_variables)==8)
	{
		applicationObject.file_variables = fapplication.file_variables.toArray();
	}
	else
	{
		applicationObject.file_variables.push(fapplication.file_variables);
		
	}
	applicationObject.second_record = fapplication.second_record;
	if(system.functions.type(fapplication.booleans)==8)
	{
		applicationObject.booleans = fapplication.booleans.toArray();
	}
	else
	{
		applicationObject.booleans .push(fapplication.booleans);
	}
	applicationObject.table = fapplication.table;
	if(system.functions.type(fapplication.tables)==8)
	{
		applicationObject.tables = fapplication.tables.toArray();
	}
	else
	{
		applicationObject.tables.push(fapplication.tables);
	}
	applicationObject.sysmodcount = fapplication.sysmodcount;
	applicationObject.sysmoduser = fapplication.sysmoduser;
	applicationObject.sysmodtime = fapplication.sysmodtime;
	
	return JsonEncode(applicationObject);
}