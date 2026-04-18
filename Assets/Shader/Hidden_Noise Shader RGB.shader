Shader "Hidden/Noise Shader RGB"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_GrainTex ("Grain", 2D) = "gray" {}
		_ScratchTex ("Scratch", 2D) = "gray" {}
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
			sampler2D _GrainTex;
			sampler2D _ScratchTex;
			float4 _GrainOffsetScale;
			float4 _ScratchOffsetScale;
			float4 _Intensity;

			fixed4 frag(v2f_img i) : SV_Target
			{
				fixed4 color = tex2D(_MainTex, i.uv);
				float2 grainUv = i.uv * _GrainOffsetScale.zw + _GrainOffsetScale.xy;
				float2 scratchUv = i.uv * _ScratchOffsetScale.zw + _ScratchOffsetScale.xy;

				fixed3 grain = tex2D(_GrainTex, grainUv).rgb - 0.5;
				fixed scratch = tex2D(_ScratchTex, scratchUv).r - 0.5;

				color.rgb += grain * _Intensity.x;
				color.rgb += scratch.xxx * _Intensity.y;
				return color;
			}
			ENDCG
		}
	}

	Fallback Off
}
