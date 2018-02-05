Shader "Unlit/Stencil"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_SRef("Thing", Int) = 1
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" "RenderQueue" = "Transparent-100"}
		Stencil {
			Ref [_SRef]
			Comp Always
			Pass Replace
			ZFail Replace
		}
		Cull Off
		ZWrite Off
		ZTest Always
		Blend One One
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = fixed4(0, 0, 0, 0);
				return col;
			}
			ENDCG
		}
	}
}
