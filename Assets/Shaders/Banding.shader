Shader "Image Effects/Banding"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_BandTex("Banding texture", 2D) = "black" {}
		_Str ("Strength", Range(0,1)) = 1
		_Size("Effect Size", Float) = 50
		_Col ("Color", Color) = (1,1,1,1)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			sampler2D _BandTex;
			float4 _MainTex_ST;
			float _Str;
			float _Size;
			fixed4 _Col;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col + tex2D(_BandTex, i.uv * _Size * _ScreenParams.y) * _Str * _Col;
			}
			ENDCG
		}
	}
}
