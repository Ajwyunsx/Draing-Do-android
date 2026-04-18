Shader "AlkemiShaders/fx_shader_transition"
{
	Properties
	{
		_Emission ("Emission", Float) = 3
		_Color ("Main Color", Color) = (1,1,1,1)
		_RampMap ("Ramp Map", 2D) = "white" {}
		_EffectMap ("Effect Map", 2D) = "white" {}
		_RampUV ("Ramp UV", Vector) = (0,0,0,0)
		_LifeColor ("Life Color", Color) = (1,1,1,1)
	}

	SubShader
	{
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }
		Blend SrcAlpha One
		Cull Off
		Lighting Off
		ZWrite Off

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _RampMap;
			sampler2D _EffectMap;
			float4 _EffectMap_ST;
			fixed4 _Color;
			fixed4 _LifeColor;
			float4 _RampUV;
			float _Emission;

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				fixed4 color : COLOR;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				fixed4 color : COLOR;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _EffectMap);
				o.color = v.color;
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 effect = tex2D(_EffectMap, i.uv);
				fixed rampU = saturate(effect.r + _RampUV.x);
				fixed4 ramp = tex2D(_RampMap, float2(rampU, 0.5));
				fixed4 tint = lerp(_Color, _LifeColor, saturate(i.color.a));
				fixed3 rgb = effect.rgb * ramp.rgb * tint.rgb * max(_Emission, 0);
				fixed alpha = saturate(effect.a * tint.a);
				return fixed4(rgb, alpha);
			}
			ENDCG
		}
	}

	Fallback Off
}
