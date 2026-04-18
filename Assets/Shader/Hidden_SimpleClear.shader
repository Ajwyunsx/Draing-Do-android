Shader "Hidden/SimpleClear"
{
	SubShader
	{
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#include "UnityCG.cginc"

			fixed4 frag(v2f_img i) : SV_Target
			{
				return 0;
			}
			ENDCG
		}
	}

	Fallback Off
}
