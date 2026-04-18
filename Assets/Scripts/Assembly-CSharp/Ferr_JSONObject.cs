using System;

public class Ferr_JSONObject
{
	public string name;

	public Ferr_JSONValue val;

	public Ferr_JSONObject(string aText)
	{
		FromString(aText);
	}

	public Ferr_JSONObject(string aName, Ferr_JSONValue aVal)
	{
		name = aName;
		val = aVal;
	}

	public void FromString(string aText)
	{
		aText = aText.Trim();
		string[] array = Ferr_JSON._Split(aText, ':', true);
		if (array.Length != 2)
		{
			throw new Exception("Bad JSON Object: " + aText);
		}
		name = array[0].Trim().Replace("\"", string.Empty);
		val = new Ferr_JSONValue();
		val.FromString(array[1]);
	}

	public override string ToString()
	{
		return "\"" + name + "\":" + ((val != null) ? val.ToString() : "null");
	}
}
