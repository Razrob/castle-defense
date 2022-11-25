
Shader "Sprites/AdvancedSprite"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        [Toggle] _Inverse("Inverse", int) = 0
        _Saturation("Saturation", Range(0.0, 1.0)) = 1
        _Offcet("Offcet", Range(0.0, 1.0)) = 0
        _RadialFill("RadialFill", Range(0.0, 1.0)) = 1
        _VerticalFill("VerticalFill", Range(0.0, 1.0)) = 1
        _HorizontalFill("HorizontalFill", Range(0.0, 1.0)) = 1
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON
			#include "UnityCG.cginc"
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord  : TEXCOORD0;
			};
			
            float _Saturation;
            int _Inverse;
            float _Offcet;
            float _RadialFill;
            float _VerticalFill;
            float _HorizontalFill;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color;

				return OUT;
			}

			sampler2D _MainTex;

            static fixed4 GetSaturationColor(fixed4 color);
            static float GetColorMultiplier(float2 uv);
            static float GetRadialMultiplier(float2 uv);
            static float GetHorizontalMultiplier(float2 uv);
            static float GetVerticalMultiplier(float2 uv);

			fixed4 SampleSpriteTexture (float2 uv)
			{
				fixed4 color = tex2D (_MainTex, uv);
				return color;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
				fixed4 color = SampleSpriteTexture (IN.texcoord) * IN.color;
				color.rgb *= color.a;

                color *= GetColorMultiplier(IN.texcoord);
                color = GetSaturationColor(color);

				return color;
			}

            static fixed4 GetSaturationColor(fixed4 color)
            {
                fixed averageColorChannel = (color.r + color.g + color.b) / 3.0;
                return lerp(color, fixed4(averageColorChannel, averageColorChannel, averageColorChannel, color.a), abs(_Saturation - 1.0));
            }

            static float GetColorMultiplier(float2 uv)
            {
                return GetRadialMultiplier(uv) * GetHorizontalMultiplier(uv) * GetVerticalMultiplier(uv);
            }

            static float GetRadialMultiplier(float2 uv)
            {
                uv = uv - float2(0.5, 0.5);

                float pi = 3.1415926535;

                float2 zeroDirection = float2(0, 1);
                float2 uvDirection = normalize(uv);

                float offcet = _Offcet * pi;

                zeroDirection = float2(zeroDirection.x * cos(offcet) - zeroDirection.y * sin(offcet), zeroDirection.x * sin(offcet) + zeroDirection.y * cos(offcet));
                uvDirection = float2(uvDirection.x * cos(-offcet) - uvDirection.y * sin(-offcet), uvDirection.x * sin(-offcet) + uvDirection.y * cos(-offcet));

                float2 vec = uvDirection - zeroDirection;
                float angle = -atan2(vec.y, vec.x);

                float fill = _RadialFill;

                if (_Inverse == 0)
                    return float(angle + pi / 2 < (fill + 0.5) * pi - offcet);

                fill = abs(fill - 1.0);
                return float(angle + pi / 2 > (fill + 0.5) * pi - offcet);
            }

            static float GetHorizontalMultiplier(float2 uv)
            {
                float fill = _Offcet + (1 - _Offcet) * _HorizontalFill;
                
                if (_Inverse == 0)
                {
                    return float(uv.x < fill);
                }
                
                fill = abs(fill - 1.0);
                return float(uv.x > fill);
            }

            static float GetVerticalMultiplier(float2 uv)
            {
                float fill = _Offcet + (1 - _Offcet) * _VerticalFill;
                
                if (_Inverse == 0)
                {
                    return float(uv.y < fill);
                }
                
                fill = abs(fill - 1.0);
                return float(uv.y > fill);
            }

		ENDCG
		}
	}
}