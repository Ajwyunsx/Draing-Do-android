using System;
using System.Collections.Generic;

public class Ferr2DT_Comparer<T> : IComparer<T>
{
	private readonly Func<T, T, int> func;

	public Ferr2DT_Comparer(Func<T, T, int> comparerFunc)
	{
		func = comparerFunc;
	}

	public int Compare(T x, T y)
	{
		return func(x, y);
	}
}
