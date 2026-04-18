using System;
using System.Collections.Generic;
using System.Reflection;

public static class Ferr_JSON
{
	public static Exception error;

	public static bool failed;

	public static Ferr_JSONValue Parse(string aText)
	{
		Ferr_JSONValue ferr_JSONValue = null;
		failed = false;
		error = null;
		try
		{
			ferr_JSONValue = new Ferr_JSONValue();
			ferr_JSONValue.FromString(aText);
		}
		catch (Exception ex)
		{
			error = ex;
			failed = true;
		}
		return ferr_JSONValue;
	}

	public static Ferr_JSONValue MakeShallow(object obj)
	{
		Type type = obj.GetType();
		FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
		Ferr_JSONValue ferr_JSONValue = new Ferr_JSONValue();
		for (int i = 0; i < fields.Length; i++)
		{
			object value = fields[i].GetValue(obj);
			if (value is float || value is int || value is double || value is long)
			{
				ferr_JSONValue[fields[i].Name] = Convert.ToSingle(value);
			}
			else if (value is string)
			{
				ferr_JSONValue[fields[i].Name] = (string)value;
			}
			else if (value is bool)
			{
				ferr_JSONValue[fields[i].Name] = (bool)value;
			}
			else
			{
				ferr_JSONValue[fields[i].Name] = value.ToString();
			}
		}
		return ferr_JSONValue;
	}

	public static string[] _Split(string aText, char aDelimeter, bool aOuterOnly = false)
	{
		List<string> list = new List<string>();
		string text = string.Empty;
		bool flag = false;
		int num = 0;
		for (int i = 0; i < aText.Length; i++)
		{
			if (aText[i] == '"')
			{
				flag = !flag;
			}
			if (aText[i] == '{' || aText[i] == '[')
			{
				num++;
			}
			if (aText[i] == '}' || aText[i] == ']')
			{
				num--;
			}
			if ((!aOuterOnly || num <= 0) && !flag && aText[i] == aDelimeter)
			{
				list.Add(text);
				text = string.Empty;
			}
			else
			{
				text += aText[i];
			}
		}
		if (text != string.Empty)
		{
			list.Add(text);
		}
		return list.ToArray();
	}
}
