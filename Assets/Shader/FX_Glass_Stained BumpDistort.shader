Shader "FX/Glass/Stained BumpDistort"
{
	Properties
	{
		_BumpAmt ("Distortion", Range(0, 128)) = 10
		_MainTex ("Tint Color (RGB)", 2D) = "white" {}
		_BumpMap ("Normalmap", 2D) = "bump" {}
		_Color ("Color", Color) = (1,1,1,0.5)
	}

	SubShader
	{
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }
		GrabPass { "_GrabTexture" }

		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			Cull Off
			ZWrite Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			sampler2D _BumpMap;
			sampler2D _GrabTexture;
			float4 _MainTex_ST;
			float4 _BumpMap_ST;
			float4 _Color;
			float _BumpAmt;

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uvMain : TEXCOORD0;
				float2 uvBump : TEXCOORD1;
				float4 grabPos : TEXCOORD2;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uvMain = TRANSFORM_TEX(v.uv, _MainTex);
				o.uvBump = TRANSFORM_TEX(v.uv, _BumpMap);
				o.grabPos = ComputeGrabScreenPos(o.pos);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 tint = tex2D(_MainTex, i.uvMain) * _Color;
				fixed3 normal = UnpackNormal(tex2D(_BumpMap, i.uvBump));
				float2 offset = normal.xy * (_BumpAmt * 0.0015);
				float4 grabPos = i.grabPos;
				grabPos.xy += offset * grabPos.w;
				fixed4 scene = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(grabPos));
				scene.rgb *= tint.rgb;
				scene.a = tint.a;
				return scene;
			}
			ENDCG
		}
	}

	Fallback Off
}
