Shader "Image Effects/Old TV Shutoff"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Prog ("Progress", Range(0,1)) = 0
		_S ("Min Size", Float) = 0.5
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

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

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			float _Prog;
			float _S;

			fixed4 frag (v2f i) : SV_Target
			{

				float uvY = lerp(i.uv.y, 0.5-_S+(2*_S*i.uv.y), pow(_Prog, 5));

				if (uvY > 1 || uvY < 0) return fixed4(0, 0, 0, 1);

				float b = pow(_Prog, 7);

				float2 uv = float2(i.uv.x, uvY);

				fixed4 col = tex2D(_MainTex, uv);

				return saturate(col + fixed4(b,b,b,0));
			}
			ENDCG
		}
	}
}
