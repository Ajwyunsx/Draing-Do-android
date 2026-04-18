Shader "Hidden/SunShaftsComposite"
{
	Properties
	{
		_MainTex ("Base", 2D) = "white" {}
		_ColorBuffer ("Color Buffer", 2D) = "black" {}
		_Skybox ("Skybox", 2D) = "black" {}
	}

	SubShader
	{
		Cull Off ZWrite Off ZTest Always

		CGINCLUDE
		#include "UnityCG.cginc"

		sampler2D _MainTex;
		sampler2D _ColorBuffer;
		sampler2D _Skybox;
		sampler2D _CameraDepthTexture;
		float4 _MainTex_TexelSize;
		float4 _SunPosition;
		float4 _SunThreshold;
		float4 _BlurRadius4;
		float4 _SunColor;

		fixed BrightMask(fixed3 color)
		{
			fixed3 diff = max(color - _SunThreshold.rgb, 0);
			return saturate(dot(diff, fixed3(0.3333, 0.3333, 0.3333)) * 4.0);
		}

		fixed SkyDepthMask(float2 uv)
		{
			#if defined(UNITY_REVERSED_Z)
				float rawDepth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, uv);
				return saturate((0.02 - rawDepth) * 1000.0);
			#else
				float rawDepth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, uv);
				return saturate((rawDepth - 0.98) * 50.0);
			#endif
		}

		fixed4 fragScreen(v2f_img i) : SV_Target
		{
			fixed4 src = tex2D(_MainTex, i.uv);
			fixed3 shafts = tex2D(_ColorBuffer, i.uv).rgb * _SunColor.rgb;
			fixed3 screenBlend = 1 - (1 - src.rgb) * (1 - shafts);
			return fixed4(screenBlend, src.a);
		}

		fixed4 fragRadialBlur(v2f_img i) : SV_Target
		{
			float2 dir = (_SunPosition.xy - i.uv) * _BlurRadius4.xy;
			fixed4 sum = 0;
			sum += tex2D(_MainTex, i.uv);
			sum += tex2D(_MainTex, i.uv + dir * 0.25);
			sum += tex2D(_MainTex, i.uv + dir * 0.5);
			sum += tex2D(_MainTex, i.uv + dir * 0.75);
			sum += tex2D(_MainTex, i.uv + dir * 1.0);
			return sum / 5.0;
		}

		fixed4 fragDepthMask(v2f_img i) : SV_Target
		{
			fixed4 src = tex2D(_MainTex, i.uv);
			fixed mask = BrightMask(src.rgb) * SkyDepthMask(i.uv);
			return fixed4(mask, mask, mask, 1);
		}

		fixed4 fragSkyboxMask(v2f_img i) : SV_Target
		{
			fixed4 sky = tex2D(_Skybox, i.uv);
			fixed mask = BrightMask(sky.rgb);
			return fixed4(mask, mask, mask, 1);
		}

		fixed4 fragAdd(v2f_img i) : SV_Target
		{
			fixed4 src = tex2D(_MainTex, i.uv);
			fixed3 shafts = tex2D(_ColorBuffer, i.uv).rgb * _SunColor.rgb;
			return fixed4(src.rgb + shafts, src.a);
		}
		ENDCG

		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment fragScreen
			ENDCG
		}

		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment fragRadialBlur
			ENDCG
		}

		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment fragDepthMask
			ENDCG
		}

		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment fragSkyboxMask
			ENDCG
		}

		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment fragAdd
			ENDCG
		}
	}

	Fallback Off
}
