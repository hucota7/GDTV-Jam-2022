Shader "Kaima/Depth/IntersectionHighlight"
{
	Properties
	{
		[HDR]_IntersectionColor("Intersection Color", Color) = (1,1,0,0)
		_IntersectionWidth("Intersection Width", Range(0, 1)) = 0.1
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue" = "Transparent" }
		Blend DstColor SrcColor
		ZWrite Off
		Cull Off
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
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 screenPos : TEXCOORD1;
				float eyeZ : TEXCOORD2;
			};

			sampler2D _CameraDepthTexture;
			fixed4 _IntersectionColor;
			float _IntersectionWidth;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.screenPos = ComputeScreenPos(o.vertex);
				COMPUTE_EYEDEPTH(o.eyeZ);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = fixed4(0.5,0.5,0.5,1);

				float screenZ = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.screenPos)));
				
				float halfWidth = _IntersectionWidth / 2;
				float diff = saturate(abs(i.eyeZ - screenZ) / halfWidth);
				//clip(0.99 - diff);
				fixed4 finalColor = lerp(_IntersectionColor, col, diff);
				return finalColor;
				//return _IntersectionColor;
			}
			ENDCG
		}
	}
}
