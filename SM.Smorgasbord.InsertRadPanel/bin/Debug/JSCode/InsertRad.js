//index is 1 based
function Insert(processName,index,count)
{	
	var ProcessFile = new SCFile("Process");
	var searchResult = ProcessFile.doSelect("process = \""+processName+"\"");
	
	if(searchResult == RC_SUCCESS)
	{
		var radLength = system.functions.lng(ProcessFile.rad);
		var temRad = ProcessFile.rad[0];
		ProcessFile.rad =system.functions.insert(ProcessFile.rad,index,count,temRad);
		
		//Clean the added rad panel
		for(var i=0;i<count;i++)
		{
			ProcessFile.rad[index+i-1].application = "APP";
			ProcessFile.rad[index+i-1].rad_condition = null;
			system.functions.cleanup(ProcessFile.rad[index+i-1].names);
			system.functions.cleanup(ProcessFile.rad[index+i-1].values);
			system.functions.cleanup(ProcessFile.rad[index+i-1].pre_rad_expressions);
			system.functions.cleanup(ProcessFile.rad[index+i-1].post_rad_expressions);
		}
		ProcessFile.doSave();
	}
	print("Done");
}