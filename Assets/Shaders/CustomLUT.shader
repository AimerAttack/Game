Shader "Custom/LUT"
{
	Properties
	{
		_MainTex("Main Texture", 2D) = "white" {}
		_Color("Color",Color) = (1,1,1,1)
		_Brightness("Brightness",Range(1,5)) = 1
	}
	SubShader
	{
		Tags{ "Queue" = "Geometry" "RenderType" = "Opaque" "IgnoreProjector" = "True" }
		Cull Off
		ZWrite Off
		ZTest Always

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
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				fixed4 color : COLOR;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _Color;
			float _Brightness;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}

			float4 frag(v2f i) : SV_Target
			{
				fixed4 mainTexColor = tex2D(_MainTex,i.uv);
				float step = mainTexColor.r * _Brightness;
				return fixed4(step, step, step, 1) * _Color;
			}
			ENDCG
		}
	}
	Fallback Off
}