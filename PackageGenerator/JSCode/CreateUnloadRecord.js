function AuditLogInfo()
{
    this.Title = "";
    this.Author = "";
    this.Items = new Array();
}

function AuditItem()
{
    this.ElementName = "";
    this.Query = "";
    this.Date = "";
    this.Description = "";
}

function GetAuditInfo(releaseName)
{
    var auditLogInfo = new AuditLogInfo();
    var queryStr = "release=\""+releaseName+"\"";
    
    print("queryStr:"+queryStr);
    var MSRCItemDefFile = new SCFile("MSRCItemDef");
    var selectResult = MSRCItemDefFile.doSelect(queryStr);
    while(selectResult == RC_SUCCESS)
    {
        var auditItem = new AuditItem();
        auditItem.ElementName = MSRCItemDefFile.filename;
        auditItem.Query =  MSRCItemDefFile.query;
        auditItem.Description = MSRCItemDefFile.description.join('\r\n');
        auditLogInfo.Items.push(auditItem);
        selectResult = MSRCItemDefFile.getNext();
    }
    return auditLogInfo;
}

function CreateUnloadRecord(unloadName,releaseName)
{
	var unloadFile = new SCFile("unload");
	if(unloadFile.doSelect("name=\""+unloadName+"\"")==RC_SUCCESS)
	{
		unloadFile.doRemove();
		unloadFile = new SCFile("unload");
	}
	
	unloadFile.unload=true;
	unloadFile.name=unloadName;
	var auditLogInfo = GetAuditInfo(releaseName);
	
	var firstRecord = unloadFile.record[0];
	var firstElement;
	var firstQuery;
	
	for(var i=0;i<auditLogInfo.Items.length;i++)
	{
		var currentFileName = auditLogInfo.Items[i].ElementName;
		var currentQuery =  auditLogInfo.Items[i].Query;
		
		firstRecord.filename = currentFileName;
		firstRecord.query = currentQuery;
		if(i==0)
		{
			firstElement = currentFileName;
			firstQuery = currentQuery;
		}
		else
		{
			unloadFile.record.push(firstRecord);
		}
	}
	if(auditLogInfo.Items.length > 1)
	{
		firstRecord.filename = firstElement;
		firstRecord.query = firstQuery;
	}
	unloadFile.doInsert();
	return 1;
}
