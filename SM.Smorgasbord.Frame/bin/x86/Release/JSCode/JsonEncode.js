function JsonEncodeArray(array)
{
	var result = [];
	
	for(var i= 0;i<array.length;i++)
	{
		var value = array[i];
		
		result.push(JsonEncode(value));
	}
	return '['+result.join(',')+']';
}
function JsonEncodeObject(obj)
{
	var result = [];
	for(var i in obj)
	{
		var value = obj[i];
		var name ='"'+i+'":';
		result.push(name+JsonEncode(value));
	}
	return '{'+result.join(',')+'}';
}

function JsonEncode(obj)
{
	Array.prototype.testABCD123 = function(){};
	
	if(typeof(obj)=='object')
	{
		//Array
		if(obj!=null && obj.testABCD123 !=null)
		{
			return JsonEncodeArray(obj);
		}
		else
		{
			return JsonEncodeObject(obj);
		}
	}
	else
	{
		if(typeof(obj)=='string')
		{ 
			return '"'+Escape(obj)+'"';
		}
		else if(typeof(obj)=='number')
		{
			return obj;
		}
		else
		{
			return obj.toString();
		}
	}
}

function Escape(str)
{
	return str.replace(/\\/g,"\\\\").replace(/"/g,'\\\"').replace(/\r/g,"\\u000d").replace(/\n/g,"\\u000a");
}