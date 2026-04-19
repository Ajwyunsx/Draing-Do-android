using UnityEngine;

public static class FrameRateBoot
{
	private const int MaxFrameRate = 120;

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void Apply()
	{
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = MaxFrameRate;
	}
}
