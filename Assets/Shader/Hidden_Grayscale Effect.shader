Shader "Hidden/Grayscale Effect"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_RampTex ("Ramp", 2D) = "gray" {}
		_RampOffset ("Ramp Offset", Range(-1, 1)) = 0
		_RampEnabled ("Ramp Enabled", Float) = 1
	}

	SubShader
	{
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			sampler2D _RampTex;
			float _RampOffset;
			float _RampEnabled;

			fixed4 frag(v2f_img i) : SV_Target
			{
				fixed4 color = tex2D(_MainTex, i.uv);
				fixed luminance = dot(color.rgb, fixed3(0.299, 0.587, 0.114));
				fixed3 grayscale = luminance.xxx;
				if (_RampEnabled < 0.5)
				{
					return fixed4(grayscale, color.a);
				}
				fixed ramp = saturate(luminance + _RampOffset);
				fixed3 graded = tex2D(_RampTex, float2(ramp, 0.5)).rgb;
				return fixed4(graded, color.a);
			}
			ENDCG
		}
	}

	Fallback Off
}
