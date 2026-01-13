// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. 
// Modified by Chris Cunningham to achieve fog blending effect.
// MIT license (see license.txt)

Shader "Skybox/Cubemap Fog" {
Properties {
	_Tint ("Tint Color", Color) = (.5, .5, .5, .5)
	[Gamma] _Exposure ("Exposure", Range(0, 8)) = 1.0
	_Rotation ("Rotation", Range(0, 360)) = 0
	[NoScaleOffset] _Tex ("Cubemap   (HDR)", Cube) = "grey" {}
	[Header(Height)]
	_Height ("Height", Float) = 2500.0
	_Blend ("Blend", Float) = 2500.0
	_ColorB ("2nd Color", Color) = (.0, .5, 1.0, 1.0)
	_Opacity ("Opacity", Range(0, 1)) = 1.0
}

SubShader {
	Tags { "Queue"="Background" "RenderType"="Background" "PreviewType"="Skybox" }
	Cull Off ZWrite Off

	Pass {
		
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#pragma target 2.0

		#include "UnityCG.cginc"

		samplerCUBE _Tex;
		half4 _Tex_HDR;
		half4 _Tint;
		half _Exposure;
		float _Rotation;
		float _Height;
		float _Blend;
		half4 _ColorB;
		float _Opacity;
	
		float3 RotateAroundYInDegrees (float3 vertex, float degrees)
		{
			float alpha = degrees * UNITY_PI / 180.0;
			float sina, cosa;
			sincos(alpha, sina, cosa);
			float2x2 m = float2x2(cosa, -sina, sina, cosa);
			return float3(mul(m, vertex.xz), vertex.y).xzy;
		}
		
		struct appdata_t {
			float4 vertex : POSITION;
			UNITY_VERTEX_INPUT_INSTANCE_ID
		};

		struct v2f {
			float4 vertex : SV_POSITION;
			float3 texcoord : TEXCOORD0;
			float3 wPos : TEXCOORD1;
			UNITY_VERTEX_OUTPUT_STEREO
		};

		v2f vert (appdata_t v)
		{
			v2f o;
			UNITY_SETUP_INSTANCE_ID(v);
			UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
			float3 rotated = RotateAroundYInDegrees(v.vertex, _Rotation);
			o.vertex = UnityObjectToClipPos(rotated);
			o.texcoord = v.vertex.xyz;
			o.wPos = mul(unity_ObjectToWorld, v.vertex);
			return o;
		}

		fixed4 frag (v2f i) : SV_Target
		{
			half4 tex = texCUBE (_Tex, i.texcoord);
			half3 c = DecodeHDR (tex, _Tex_HDR);
			c = c * _Tint.rgb * unity_ColorSpaceDouble.rgb;
			c *= _Exposure;
			
			if (i.wPos.y<=_Height) {
				c = lerp(c,_ColorB,saturate((_Height-i.wPos.y)/_Blend));
				c = lerp(c,unity_FogColor,saturate((_Height-i.wPos.y)/_Blend));
			}
			c = lerp(c,tex,1-_Opacity);
			return half4(c, 1);
		}
		ENDCG 
	}
} 	


Fallback Off

}
