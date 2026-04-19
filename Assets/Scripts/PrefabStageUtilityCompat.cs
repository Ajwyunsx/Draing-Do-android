#if UNITY_EDITOR && !UNITY_2021_2_OR_NEWER
using UnityEditor.Experimental.SceneManagement;
using UnityEngine;

public static class PrefabStageUtility
{
	public static PrefabStage GetPrefabStage(GameObject gameObject)
	{
		return UnityEditor.Experimental.SceneManagement.PrefabStageUtility.GetPrefabStage(gameObject);
	}
}

namespace UnityEditor.SceneManagement
{
	public static class PrefabStageUtility
	{
		public static PrefabStage GetPrefabStage(GameObject gameObject)
		{
			return UnityEditor.Experimental.SceneManagement.PrefabStageUtility.GetPrefabStage(gameObject);
		}
	}
}
#endif
