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
    
    var str = JsonEncode(auditLogInfo);
    //print(str);
    return str;
}