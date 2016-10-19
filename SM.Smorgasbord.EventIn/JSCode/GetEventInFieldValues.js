function EventMapFieldValue()
{
	this.fieldName;
	this.fieldValue;
	this.sequence;
	this.position;
	this.fieldType;
}

function FieldMappings()
{
	this.mappings = new Array();
}

//The following code is coming from SL: SeventinstrGenerate  and do some modification.

function generateEventInFieldValues(fileName, queryStr, eventMapName)
{

	var resultFields = new FieldMappings();
	
	var evMapFieldListPointor = getFields(eventMapName);
	if (evMapFieldListPointor == null) 
	{
		system.vars.$eventinStr = null;
		print("terminate 1");
		return;
	}
	
	var obj = getXMLObject(fileName, queryStr);
	var documentElement;
	if (obj != null) 
	{
		documentElement = obj.getDocumentElement();
		var tempEventinStr = "";
		var tempCounter = 0;
		var isSuccess = RC_SUCCESS;
		
		while (RC_SUCCESS == isSuccess) 
		{
			var foundValue = paraseXMLObject(documentElement.getFirstChildElement().getNextSiblingElement(), evMapFieldListPointor.evfield);
			
			var fieldValuePair = new EventMapFieldValue();
			fieldValuePair.fieldName = evMapFieldListPointor.evfield;
			if(foundValue == null)
			{
				fieldValuePair.fieldValue = "";
			}
			else
			{
				fieldValuePair.fieldValue = foundValue.toString();
			}
			print(foundValue);
			fieldValuePair.sequence = evMapFieldListPointor.evseq;
			fieldValuePair.position = evMapFieldListPointor.evindex;
			fieldValuePair.fieldType = evMapFieldListPointor.evdtype;
			resultFields.mappings.push(fieldValuePair);
			
			isSuccess = evMapFieldListPointor.getNext();
			tempCounter++;
		}
	} 
	
	return JsonEncode(resultFields.mappings);
}

function getFields(eventMapName) {
	var fieldsList = new SCFile("eventmap");
	var queryStr = "evmap = \"" + eventMapName + "\"";
	var isSuccess = fieldsList.doSelect(queryStr);
	if (isSuccess == RC_SUCCESS) {
		return fieldsList;
	} else {
		print("terminate 3");
		print("Could not find eventMapName : " + eventMapName + ". " + RCtoString(isSuccess));
		return null;
	}
}

function paraseXMLObject(documentElement, fieldName) {
	var childNode;
	while(documentElement != null && documentElement.getNodeName() != fieldName) {
		childNode = documentElement.getFirstChildElement();
		if (childNode != null) {
			documentElement = childNode;
		} else if (documentElement.getNextSiblingElement() != null) {
			documentElement = documentElement.getNextSiblingElement();
		} else {
			while(childNode == null) {
				documentElement = documentElement.getParentNode();
				if( documentElement == null) {
					print("fieldName " + fieldName + " could not be found in the xml object.");
					return null;
				}
				//documentElement = childNode;
				childNode = documentElement.getNextSiblingElement();
			}
			documentElement = childNode;
		}
	}
	if (documentElement != null) {
		return documentElement.getNodeValue();
	} else {
		return null;
	}
}

function getXMLObject(fileName, queryStr) {
	var objectList = new SCFile(fileName);
	var isSuccess = objectList.doSelect(queryStr);
	if (isSuccess == RC_SUCCESS) {
		var xmlObject = objectList.getXML();
		return xmlObject;
	} else {
		print("terminate 4");
		print("Could not find" + fileName + ". " + RCtoString(isSuccess));
		return null;
	}
}




