using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	[ExecuteInEditMode]
	[AddComponentMenu("Image Effects/Color Adjustments/Grayscale")]
	public class Grayscale : ImageEffectBase
	{
		public Texture textureRamp;

		public float rampOffset;

		public bool useRamp;

		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (!(base.material == null))
			{
				bool flag = useRamp && textureRamp != null;
				base.material.SetFloat("_RampEnabled", flag ? 1f : 0f);
				if (flag)
				{
					base.material.SetTexture("_RampTex", textureRamp);
				}
				base.material.SetFloat("_RampOffset", rampOffset);
				Graphics.Blit(source, destination, base.material);
				return;
			}
			Graphics.Blit(source, destination);
		}
	}
}
