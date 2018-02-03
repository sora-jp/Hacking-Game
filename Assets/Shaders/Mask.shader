Shader "UI/Mask"
{
	Properties
	{
		_MainTex("Particle Texture", 2D) = "white" {}

		_Rect("XY=Max, ZW=Min", Vector) = (1,1,0,0)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" "Queue" = "Transparent-100" "PreviewType" = "Plane"}
		LOD 100
		Cull Off
		ZWrite Off
		ZTest Always

		Pass
		{
			Name "Default"
			Blend SrcAlpha OneMinusSrcAlpha

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
				float2 suv : TEXCOORD1;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _Rect;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.suv = ComputeScreenPos(o.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float4 col = tex2D(_MainTex, i.uv);
				float2 v = i.suv;

				clip(v.x - _Rect.x);
				clip(v.y - _Rect.y);
				clip(_Rect.z - v.x);
				clip(_Rect.w - v.y);

				return col;
			}
			ENDCG
		}
	}
}
