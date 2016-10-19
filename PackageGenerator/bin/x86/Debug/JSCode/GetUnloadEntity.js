function UnloadEntity()
{
    this.Name = "";
    this.Unload = true;
    this.Purge = false;
    this.Items = new Array();
}

function UnloadItem()
{
    this.ElementName = "";
    this.Query = "";
}

function GetUnloadEntity(unloadName)
{
    var unloadEntity = new UnloadEntity();
    unloadEntity.Name = unloadName;
    var queryStr = "name=\""+unloadName+"\"";
    var unloadFile = new SCFile("unload");
    var selectResult = unloadFile.doSelect(queryStr);
    if(selectResult == RC_SUCCESS)
    {
    	if(unloadFile.unload)
    	{
    		unloadEntity.Unload = true;
    	}
    	else
    	{
    		unloadEntity.Unload = false;
    	}
    	
    	if(unloadFile.purge)
    	{
    		unloadEntity.Purge = true;
    	}
    	else
    	{
    		unloadEntity.Purge = false;
    	}
    	
        var itemCount = system.functions.lng(unloadFile.record);
        for(var i=0;i<itemCount;i++)
        {
            var currentRecord = unloadFile.record[i];
            var unloadItem = new UnloadItem();  
            unloadItem.ElementName = currentRecord.filename;
            unloadItem.Query = currentRecord.query;
            unloadEntity.Items.push(unloadItem);            
        }
    }
    
    var str = JsonEncode(unloadEntity);
    //print(str);
    return str;
}