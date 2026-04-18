using System.Collections.Generic;
using UnityEngine;

public class FX_generator : MonoBehaviour
{
	private List<QuadTransitionFX> quadList;

	private int _numFX = 2000;

	private void Start()
	{
		GameObject proto = Resources.Load("QuadTransitionFX") as GameObject;
		quadList = new List<QuadTransitionFX>();
		for (int i = 0; i < _numFX; i++)
		{
			QuadTransitionFX item = new QuadTransitionFX(proto, new Vector3(Random.value * 20f - 10f, Random.value * 10f - 5f, Random.value * 20f));
			quadList.Add(item);
		}
	}

	private void Update()
	{
		for (int i = 0; i < _numFX; i++)
		{
			quadList[i].Update();
		}
	}
}
