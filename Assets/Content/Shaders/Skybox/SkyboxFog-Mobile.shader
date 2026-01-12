// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

// Simplified Skybox shader. Differences from regular Skybox one:
// - no tint color

Shader "Mobile/Skybox" {
Properties {
	_FrontTex ("Front (+Z)", 2D) = "white" {}
	_BackTex ("Back (-Z)", 2D) = "white" {}
	_LeftTex ("Left (+X)", 2D) = "white" {}
	_RightTex ("Right (-X)", 2D) = "white" {}
	_UpTex ("Up (+Y)", 2D) = "white" {}
	_DownTex ("Down (-Y)", 2D) = "white" {}
	[Header(Height)]
	_Height ("Height", Float) = 2500.0
	_Blend ("Blend", Float) = 2500.0
	_ColorB ("2nd Color", Color) = (.0, .5, 1.0, 1.0)
	_Opacity ("Opacity", Range(0, 1)) = 1.0
}

SubShader {
	Tags { "Queue"="Background" "RenderType"="Background" "PreviewType"="Skybox" }
	Cull Off ZWrite Off Fog { Mode Linear Color(0.5,1,1,1) Range 0, 50 Density 100 }
	//Fog { Density 100 }
	//Fog { Range 500, 500 }
	
	Pass {
		SetTexture [_FrontTex] { combine texture }
	}
	Pass {
		SetTexture [_BackTex]  { combine texture }
	}
	Pass {
		SetTexture [_LeftTex]  { combine texture }
	}
	Pass {
		SetTexture [_RightTex] { combine texture }
	}
	Pass {
		SetTexture [_UpTex]    { combine texture }
	}
	Pass {
		SetTexture [_DownTex]  { combine texture }
	}
	
}
}
