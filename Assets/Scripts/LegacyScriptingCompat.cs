using System;
using System.Collections;
using UnityEngine;

namespace UnityScript.Lang
{
	public class Array : ArrayList
	{
		public int length
		{
			get
			{
				return Count;
			}
		}

		public Array()
		{
		}

		public Array(IEnumerable collection)
		{
			if (collection == null)
			{
				return;
			}
			foreach (object item in collection)
			{
				Add(item);
			}
		}

		public object Push(object item)
		{
			Add(item);
			return item;
		}

		public object ToBuiltin(Type elementType)
		{
			if (elementType == null)
			{
				return this;
			}
			System.Array typedArray = System.Array.CreateInstance(elementType, Count);
			for (int i = 0; i < Count; i++)
			{
				object value = this[i];
				if (value != null && !elementType.IsAssignableFrom(value.GetType()))
				{
					value = Convert.ChangeType(value, elementType);
				}
				typedArray.SetValue(value, i);
			}
			return typedArray;
		}
	}

	public static class Extensions
	{
		public static int get_length(Array array)
		{
			return (array != null) ? array.length : 0;
		}

		public static int get_length(System.Array array)
		{
			return (array != null) ? array.Length : 0;
		}

		public static int get_length(string value)
		{
			return string.IsNullOrEmpty(value) ? 0 : value.Length;
		}
	}
}

namespace Boo.Lang.Runtime
{
	public static class RuntimeServices
	{
		public static bool EqualityOperator(object lhs, object rhs)
		{
			if (lhs == null || rhs == null)
			{
				return lhs == rhs;
			}
			return Equals(lhs, rhs);
		}

		public static object Coerce(object value, Type toType)
		{
			if (toType == null)
			{
				return value;
			}
			if (value == null)
			{
				return toType.IsValueType ? Activator.CreateInstance(toType) : null;
			}
			Type valueType = value.GetType();
			if (toType.IsAssignableFrom(valueType))
			{
				return value;
			}
			if (toType.IsEnum)
			{
				return Enum.ToObject(toType, value);
			}
			return Convert.ChangeType(value, toType);
		}

		public static int UnboxInt32(object value)
		{
			return Convert.ToInt32(value);
		}

		public static bool ToBool(object value)
		{
			if (value == null)
			{
				return false;
			}
			if (value is bool flag)
			{
				return flag;
			}
			if (value is string text)
			{
				if (bool.TryParse(text, out bool parsed))
				{
					return parsed;
				}
				return !string.IsNullOrEmpty(text);
			}
			if (value is IConvertible)
			{
				try
				{
					return Convert.ToDouble(value) != 0.0;
				}
				catch
				{
				}
			}
			return true;
		}
	}

	public static class UnityRuntimeServices
	{
		private sealed class EmptyEnumerator : IEnumerator
		{
			public object Current
			{
				get
				{
					return null;
				}
			}

			public bool MoveNext()
			{
				return false;
			}

			public void Reset()
			{
			}
		}

		public static IEnumerator GetEnumerator(object enumerable)
		{
			if (enumerable is IEnumerable sequence)
			{
				return sequence.GetEnumerator();
			}
			return new EmptyEnumerator();
		}

		public static void Update(IEnumerator enumerator, object current)
		{
		}
	}
}

namespace UnityEngine
{
	public class ParticleEmitter : Component
	{
		public bool emit;

		public Vector3 worldVelocity;

		public float maxEmission;
	}
}

namespace LegacyCompat
{
	public class GUITexture : Behaviour
	{
		public Rect pixelInset;

		public Texture texture;

		public Color color = Color.white;

		public bool HitTest(Vector3 screenPosition)
		{
			return pixelInset.Contains(new Vector2(screenPosition.x, screenPosition.y));
		}
	}

	public class MovieTexture : Texture
	{
		public bool isPlaying;

		public void Play()
		{
			isPlaying = true;
		}

		public void Stop()
		{
			isPlaying = false;
		}
	}

	public class GUIText : Behaviour
	{
		public Material material;
	}
}

public static class UnityBuiltins
{
	public static float parseFloat(object value)
	{
		return Convert.ToSingle(value);
	}
}
