using System;

public class Ferr_JSONValue
{
	public Ferr_JSONType type;

	public object data;

	public int Length
	{
		get
		{
			if (type == Ferr_JSONType.Array)
			{
				return ((object[])data).Length;
			}
			if (type == Ferr_JSONType.Object)
			{
				return ((Ferr_JSONObject[])data).Length;
			}
			return 0;
		}
	}

	public Ferr_JSONValue this[string aPath]
	{
		get
		{
			return Get(aPath);
		}
		set
		{
			Set(aPath, value);
		}
	}

	public Ferr_JSONValue this[int aIndex]
	{
		get
		{
			return Get(aIndex);
		}
		set
		{
			Set(aIndex, value);
		}
	}

	public bool this[int aIndex, bool aDefaultValue]
	{
		get
		{
			Ferr_JSONValue ferr_JSONValue = Get(aIndex);
			return (ferr_JSONValue != null && ferr_JSONValue.type == Ferr_JSONType.Bool) ? ((bool)ferr_JSONValue.data) : aDefaultValue;
		}
	}

	public string this[int aIndex, string aDefaultValue]
	{
		get
		{
			Ferr_JSONValue ferr_JSONValue = Get(aIndex);
			return (ferr_JSONValue != null && ferr_JSONValue.type == Ferr_JSONType.String) ? ((string)ferr_JSONValue.data) : aDefaultValue;
		}
	}

	public float this[int aIndex, float aDefaultValue]
	{
		get
		{
			Ferr_JSONValue ferr_JSONValue = Get(aIndex);
			return (ferr_JSONValue != null && ferr_JSONValue.type == Ferr_JSONType.Number) ? ((float)ferr_JSONValue.data) : aDefaultValue;
		}
	}

	public object[] this[int aIndex, object[] aDefaultValue]
	{
		get
		{
			Ferr_JSONValue ferr_JSONValue = Get(aIndex);
			return (ferr_JSONValue != null && ferr_JSONValue.type == Ferr_JSONType.Array) ? ((object[])ferr_JSONValue.data) : aDefaultValue;
		}
	}

	public bool this[string aPath, bool aDefaultValue]
	{
		get
		{
			Ferr_JSONValue ferr_JSONValue = Get(aPath);
			return (ferr_JSONValue != null && ferr_JSONValue.type == Ferr_JSONType.Bool) ? ((bool)ferr_JSONValue.data) : aDefaultValue;
		}
	}

	public string this[string aPath, string aDefaultValue]
	{
		get
		{
			Ferr_JSONValue ferr_JSONValue = Get(aPath);
			return (ferr_JSONValue != null && ferr_JSONValue.type == Ferr_JSONType.String) ? ((string)ferr_JSONValue.data) : aDefaultValue;
		}
	}

	public float this[string aPath, float aDefaultValue]
	{
		get
		{
			Ferr_JSONValue ferr_JSONValue = Get(aPath);
			return (ferr_JSONValue != null && ferr_JSONValue.type == Ferr_JSONType.Number) ? ((float)ferr_JSONValue.data) : aDefaultValue;
		}
	}

	public object[] this[string aPath, object[] aDefaultValue]
	{
		get
		{
			Ferr_JSONValue ferr_JSONValue = Get(aPath);
			return (ferr_JSONValue != null && ferr_JSONValue.type == Ferr_JSONType.Array) ? ((object[])ferr_JSONValue.data) : aDefaultValue;
		}
	}

	public Ferr_JSONValue()
	{
		type = Ferr_JSONType.Object;
		data = null;
	}

	public Ferr_JSONValue(object aData)
	{
		if (aData == null)
		{
			type = Ferr_JSONType.Object;
			data = null;
		}
		else if (aData is string)
		{
			type = Ferr_JSONType.String;
		}
		else if (aData is object[])
		{
			type = Ferr_JSONType.Array;
		}
		else if (aData is bool)
		{
			type = Ferr_JSONType.Bool;
		}
		else if (aData is float || aData is double || aData is int || aData is long)
		{
			type = Ferr_JSONType.Number;
			aData = Convert.ToSingle(aData);
		}
		else
		{
			if (!(aData is Ferr_JSONObject))
			{
				throw new Exception("Can't convert type '" + aData.GetType().Name + "' to a JSON value!");
			}
			type = Ferr_JSONType.Object;
			aData = new Ferr_JSONObject[1] { (Ferr_JSONObject)aData };
		}
		data = aData;
	}

	public void FromString(string aValue)
	{
		string text = aValue.Trim();
		float result = 0f;
		if (text.Length <= 0)
		{
			throw new Exception("Bad JSON value: " + aValue);
		}
		if (text[0] == '{')
		{
			type = Ferr_JSONType.Object;
			text = text.Remove(0, 1);
			text = text.Remove(text.Length - 1, 1);
			string[] array = Ferr_JSON._Split(text, ',', true);
			Ferr_JSONObject[] array2 = new Ferr_JSONObject[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = new Ferr_JSONObject(array[i]);
			}
			data = array2;
			return;
		}
		if (text[0] == '[')
		{
			type = Ferr_JSONType.Array;
			text = text.Remove(0, 1);
			text = text.Remove(text.Length - 1, 1);
			string[] array3 = Ferr_JSON._Split(text, ',', true);
			Ferr_JSONValue[] array4 = new Ferr_JSONValue[array3.Length];
			for (int j = 0; j < array3.Length; j++)
			{
				array4[j] = new Ferr_JSONValue();
				array4[j].FromString(array3[j]);
			}
			data = array4;
			return;
		}
		if (text[0] == '"')
		{
			type = Ferr_JSONType.String;
			text = text.Remove(0, 1);
			text = text.Remove(text.Length - 1, 1);
			data = text;
			return;
		}
		if (float.TryParse(text, out result))
		{
			type = Ferr_JSONType.Number;
			data = result;
			return;
		}
		switch (text.ToLower())
		{
		case "false":
		case "true":
			type = Ferr_JSONType.Bool;
			data = bool.Parse(text);
			break;
		case "null":
			type = Ferr_JSONType.Object;
			data = null;
			break;
		default:
			type = Ferr_JSONType.String;
			data = text;
			break;
		}
	}

	public Ferr_JSONValue Get(string aPath)
	{
		string[] array = aPath.Split('.');
		Ferr_JSONValue ferr_JSONValue = this;
		for (int i = 0; i < array.Length; i++)
		{
			Ferr_JSONObject immidiateChild = ferr_JSONValue.GetImmidiateChild(array[i]);
			if (immidiateChild != null)
			{
				ferr_JSONValue = immidiateChild.val;
				continue;
			}
			return null;
		}
		return ferr_JSONValue;
	}

	private Ferr_JSONObject GetImmidiateChild(string aName)
	{
		if (type == Ferr_JSONType.Object)
		{
			Ferr_JSONObject[] array = (Ferr_JSONObject[])data;
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].name == aName)
					{
						return array[i];
					}
				}
			}
		}
		return null;
	}

	public Ferr_JSONValue Set(string aPath)
	{
		return Set(aPath, new Ferr_JSONValue());
	}

	public Ferr_JSONValue Set(string aPath, Ferr_JSONValue aVal)
	{
		string[] array = aPath.Split('.');
		Ferr_JSONValue ferr_JSONValue = this;
		Ferr_JSONObject ferr_JSONObject = null;
		for (int i = 0; i < array.Length; i++)
		{
			Ferr_JSONObject ferr_JSONObject2 = ferr_JSONValue.GetImmidiateChild(array[i]);
			if (ferr_JSONObject2 == null)
			{
				Ferr_JSONObject[] array2 = null;
				if (ferr_JSONValue.type == Ferr_JSONType.Object)
				{
					array2 = (Ferr_JSONObject[])ferr_JSONValue.data;
					if (array2 == null)
					{
						array2 = new Ferr_JSONObject[1];
					}
					else
					{
						Array.Resize(ref array2, array2.Length + 1);
					}
					ferr_JSONValue.data = array2;
				}
				else
				{
					ferr_JSONValue.type = Ferr_JSONType.Object;
					array2 = (Ferr_JSONObject[])(ferr_JSONValue.data = new Ferr_JSONObject[1]);
				}
				array2[array2.Length - 1] = new Ferr_JSONObject(array[i], new Ferr_JSONValue());
				ferr_JSONObject2 = array2[array2.Length - 1];
			}
			ferr_JSONObject = ferr_JSONObject2;
			ferr_JSONValue = ferr_JSONObject2.val;
		}
		if (ferr_JSONObject != null)
		{
			ferr_JSONObject.val = aVal;
			return ferr_JSONObject.val;
		}
		return null;
	}

	public Ferr_JSONValue Get(int aIndex)
	{
		if (type == Ferr_JSONType.Array)
		{
			object[] array = (object[])data;
			if (aIndex >= 0 && aIndex < array.Length)
			{
				return (Ferr_JSONValue)((object[])data)[aIndex];
			}
			return null;
		}
		return null;
	}

	public Ferr_JSONValue Set(int aIndex, Ferr_JSONValue aVal)
	{
		if (type == Ferr_JSONType.Array)
		{
			object[] array = (object[])data;
			if (array.Length <= aIndex)
			{
				Array.Resize(ref array, aIndex + 1);
				data = array;
			}
			array[aIndex] = aVal;
			return aVal;
		}
		type = Ferr_JSONType.Array;
		object[] array2 = new object[aIndex + 1];
		array2[aIndex] = aVal;
		data = array2;
		return aVal;
	}

	public override string ToString()
	{
		if (type == Ferr_JSONType.Array)
		{
			string text = "[";
			object[] array = (object[])data;
			for (int i = 0; i < array.Length; i++)
			{
				string empty = string.Empty;
				empty = ((!(array[i] is Ferr_JSONValue)) ? new Ferr_JSONValue(array[i]).ToString() : array[i].ToString());
				text = text + empty + ((i != array.Length - 1) ? "," : string.Empty);
			}
			return text + "]";
		}
		if (type == Ferr_JSONType.Object)
		{
			string empty2 = string.Empty;
			Ferr_JSONObject[] array2 = (Ferr_JSONObject[])data;
			if (array2 == null)
			{
				return "null";
			}
			empty2 += "{";
			for (int j = 0; j < array2.Length; j++)
			{
				empty2 = empty2 + array2[j].ToString() + ((j != array2.Length - 1) ? "," : string.Empty);
			}
			return empty2 + "}";
		}
		if (type == Ferr_JSONType.Bool)
		{
			return (!(bool)data) ? "false" : "true";
		}
		if (type == Ferr_JSONType.Number)
		{
			return string.Empty + data;
		}
		return string.Concat("\"", data, "\"");
	}

	public static implicit operator Ferr_JSONValue(float val)
	{
		return new Ferr_JSONValue(val);
	}

	public static implicit operator Ferr_JSONValue(double val)
	{
		return new Ferr_JSONValue(val);
	}

	public static implicit operator Ferr_JSONValue(bool val)
	{
		return new Ferr_JSONValue(val);
	}

	public static implicit operator Ferr_JSONValue(string val)
	{
		return new Ferr_JSONValue(val);
	}

	public static implicit operator Ferr_JSONValue(object[] val)
	{
		return new Ferr_JSONValue(val);
	}
}
