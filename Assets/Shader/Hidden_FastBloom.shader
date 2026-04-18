Shader "Hidden/FastBloom"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Bloom ("Bloom (RGB)", 2D) = "black" {}
	}

	SubShader
	{
		Cull Off ZWrite Off ZTest Always

		CGINCLUDE
		#include "UnityCG.cginc"

		sampler2D _MainTex;
		sampler2D _Bloom;
		float4 _MainTex_TexelSize;
		float4 _Parameter;

		fixed4 SampleBright(float2 uv)
		{
			fixed4 color = tex2D(_MainTex, uv);
			fixed luminance = dot(color.rgb, fixed3(0.2126, 0.7152, 0.0722));
			fixed mask = saturate((luminance - _Parameter.z) / max(1e-5, 1.0 - _Parameter.z));
			return fixed4(color.rgb * mask, color.a);
		}

		fixed4 fragComposite(v2f_img i) : SV_Target
		{
			fixed4 color = tex2D(_MainTex, i.uv);
			fixed3 bloom = tex2D(_Bloom, i.uv).rgb * _Parameter.w;
			return fixed4(color.rgb + bloom, color.a);
		}

		fixed4 fragPrefilter(v2f_img i) : SV_Target
		{
			return SampleBright(i.uv);
		}

		fixed4 Blur1D(float2 uv, float2 dir)
		{
			float2 stepUv = dir * _MainTex_TexelSize.xy * _Parameter.x;
			fixed4 sum = tex2D(_MainTex, uv) * 0.4026;
			sum += tex2D(_MainTex, uv + stepUv * 1.0) * 0.2442;
			sum += tex2D(_MainTex, uv - stepUv * 1.0) * 0.2442;
			sum += tex2D(_MainTex, uv + stepUv * 2.0) * 0.0545;
			sum += tex2D(_MainTex, uv - stepUv * 2.0) * 0.0545;
			return sum;
		}

		fixed4 fragBlurH(v2f_img i) : SV_Target { return Blur1D(i.uv, float2(1, 0)); }
		fixed4 fragBlurV(v2f_img i) : SV_Target { return Blur1D(i.uv, float2(0, 1)); }

		fixed4 Blur1DWide(float2 uv, float2 dir)
		{
			float2 stepUv = dir * _MainTex_TexelSize.xy * (_Parameter.x * 1.5);
			fixed4 sum = tex2D(_MainTex, uv) * 0.28;
			sum += tex2D(_MainTex, uv + stepUv * 1.0) * 0.24;
			sum += tex2D(_MainTex, uv - stepUv * 1.0) * 0.24;
			sum += tex2D(_MainTex, uv + stepUv * 2.0) * 0.12;
			sum += tex2D(_MainTex, uv - stepUv * 2.0) * 0.12;
			return sum;
		}

		fixed4 fragBlurHWide(v2f_img i) : SV_Target { return Blur1DWide(i.uv, float2(1, 0)); }
		fixed4 fragBlurVWide(v2f_img i) : SV_Target { return Blur1DWide(i.uv, float2(0, 1)); }
		ENDCG

		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment fragComposite
			ENDCG
		}

		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment fragPrefilter
			ENDCG
		}

		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment fragBlurH
			ENDCG
		}

		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment fragBlurV
			ENDCG
		}

		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment fragBlurHWide
			ENDCG
		}

		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment fragBlurVWide
			ENDCG
		}
	}

	Fallback Off
}
