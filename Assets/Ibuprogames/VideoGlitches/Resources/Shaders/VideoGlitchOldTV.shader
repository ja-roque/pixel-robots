///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Ibuprogames <hello@ibuprogames.com>. All rights reserved.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR
// IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

Shader "Hidden/Video Glitches/VideoGlitchOldTV"
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}
	}

	CGINCLUDE
	#include "UnityCG.cginc"
	#include "VideoGlitchCG.cginc"

	float _Scanline;
	float _Slowscan;
	float _VignetteSoftness;
	float _VignetteScale;
	float _GrainOpacity;
	float _SaturationTV;
	float _ScanDistort;
	float _Timer;
	float _Speed;
	float _Distort;
	float _Scale;
	float _StripesCount;
	float _Opacity;
	float _BarsCount;
	float _OpacityMoire;
	float _MoireScale;
	float _TVLines;
	float _TVLinesOpacity;
	float _TVTubeVignetteScale;
	float _TVDots;
	float _TVDotsBlend;

	inline float3 Noise(float2 uv)
	{
		float2 c = (_ScreenParams.x) * float2(1.0, (_ScreenParams.y / _ScreenParams.x));

		float r = Rand(float2((2.0 + _CustomTime.y) * floor(uv.x * c.x) / c.x, (2.0 + _CustomTime.y) * floor(uv.y * c.y) / c.y));
		float g = Rand(float2((5.0 + _CustomTime.y) * floor(uv.x * c.x) / c.x, (5.0 + _CustomTime.y) * floor(uv.y * c.y) / c.y));
		float b = Rand(float2((9.0 + _CustomTime.y) * floor(uv.x * c.x) / c.x, (9.0 + _CustomTime.y) * floor(uv.y * c.y) / c.y));

		return float3(r, g, b);
	}

	inline float Overlay(float s, float d)
	{
		return (d < 0.5) ? 2.0 * s * d : 1.0 - 2.0 * (1.0 - s) * (1.0 - d);
	}

	inline float3 Overlay(float3 s, float3 d)
	{
		float3 pixel;
		pixel.x = Overlay(s.x, d.x);
		pixel.y = Overlay(s.y, d.y);
		pixel.z = Overlay(s.z, d.z);
		
		return pixel;
	}

	inline float Ramp(float y, float start, float end)
	{
		float inside = step(start, y) - step(end, y);
		float fact = (y - start) / (end - start) * inside;

		return (1.0 - fact) * inside;
	}

	inline float Scanline(float2 uv)
	{
		return sin(_ScreenParams.y * uv.y * _Scanline - _CustomTime.y * 10.0);
	}

	inline float SlowScan(float2 uv)
	{
		return sin(_ScreenParams.y * uv.y * _Slowscan + _CustomTime.y * 6.0);
	}

	inline float2 CRT(float2 coord, float bend)
	{
		coord = (coord - 0.5) * 2.0 / _Scale;
		coord *= 0.5;
		coord.x *= 1.0 + pow((abs(coord.y) / bend * _Distort), 2.0);
		coord.y *= 1.0 + pow((abs(coord.x) / bend * _Distort), 2.0);
		coord = (coord / 1.0) + 0.5;

		return coord;
	}

	inline float2 ScanDistort(float2 uv)
	{
		float scan1 = clamp(cos(uv.y * _Speed + _CustomTime.y * _Timer), 0.0, 1.0);
		float scan2 = clamp(cos(uv.y * _Speed + _CustomTime.y * _Timer + 4.0) * 10.0, 0.0, 1.0);
		
		float amount = scan1 * scan2 * uv.x;

		uv.x -= _ScanDistort * lerp(SampleMainTexture(float2(uv.x, amount)).r * amount, amount, 0.9);

		return uv;
	}

	inline float OnOff(float a, float b, float c)
	{
		return step(c, sin(_CustomTime.y + a * cos(_CustomTime.y * b)));
	}

	inline float3 VideoDistortion(float2 uv)
	{
		float2 look = uv;
		float window = 1.0 / (1.0 + 20.0 * (look.y - mod(_CustomTime.y / 4.0, 1.0)) * (look.y - mod(_CustomTime.y / 4.0, 1.0)));
		look.x = look.x + sin(look.y * 10.0 + _CustomTime.y) / 50.0 * OnOff(4.0, 4.0, 0.3) * (1.0 + cos(_CustomTime.y * 80.0)) * window;

		float vShift = 0.4 * OnOff(2.0, 3.0, 0.9) * (sin(_CustomTime.y) * sin(_CustomTime.y * 20.0) + (0.5 + 0.1 * sin(_CustomTime.y * 200.0) * cos(_CustomTime.y)));
		look.y = mod(look.y + vShift, 1.0);

		return SampleMainTexture(look);
	}

	inline float Vignette(float2 uv)
	{
		uv = (uv - 0.5) * 0.98;
		
		return clamp(pow(cos(uv.x * _PI), _VignetteScale) * pow(cos(uv.y * _PI), _VignetteScale) * _VignetteSoftness, 0.0, 1.0);
	}

	inline float Stripes(float2 uv)
	{
		float stripes = Rand(uv * float2(0.5, 1.0) + float2(1.0, 3.0)) * _Opacity;

		return Ramp(mod(uv.y * _StripesCount + _CustomTime.y / 2.0 + sin(_CustomTime.y + sin(_CustomTime.y * 2.0)), 1.0), 0.5, 0.6) * stripes;
	}

  inline float3 VideoGlitch(float3 pixel, float2 uv)
	{
		float2 uv2 = uv * 2.0 - 1.0;

		float3 grain = Noise(uv);

		float2 crtUV = ScanDistort(uv);

		crtUV = CRT(crtUV, 2.0);

		float3 color = SampleMainTexture(crtUV);
		float3 scanlineColor = color;
		float3 slowscanColor = color;

		color = VideoDistortion(crtUV);

		scanlineColor = Scanline(crtUV);

		slowscanColor = SlowScan(crtUV);

    // Moire.
		color *= 1.0 + _TVDotsBlend * 0.2 * sin(crtUV.x * _ScreenParams.x * 5.0 * _TVDots);
		color *= 1.0 + _TVDotsBlend * 0.2 * cos(crtUV.y * _ScreenParams.y) * sin(0.5 + crtUV.x * _ScreenParams.x);

    // VHS stripes.
		color *= (1.0 + Stripes(crtUV));

    // VHS bars.
		color *= (12.0 + fmod(crtUV.y * _BarsCount + _CustomTime.y, 1.0)) / 13.0;

    // Moire.
		color *= (0.45 + (Rand(crtUV * 0.01 * _MoireScale)) * _OpacityMoire);

    // Grain.
		grain = lerp((float3)0.5, grain, _GrainOpacity * 0.1);
		grain = lerp(grain.rrr, grain, _SaturationTV);
		color = Overlay(grain, color);

		color *= Vignette(uv);

    // Tube vignette.
		color *= 1.0 - pow(length(uv2 * uv2 * uv2 * uv2) * 1.0, 6.0 * 1.0 / _TVTubeVignetteScale);

    // TV lines.
		crtUV.y *= _ScreenParams.y / _ScreenParams.y * _TVLines;
		color.r *= (0.55 + abs(0.5 - mod(crtUV.y, 0.021) / 0.021) * _TVLinesOpacity) * 1.2;
		color.g *= (0.55 + abs(0.5 - mod(crtUV.y + 0.007, 0.021) / 0.021) * _TVLinesOpacity) * 1.2;
		color.b *= (0.55 + abs(0.5 - mod(crtUV.y + 0.014, 0.021) / 0.021) * _TVLinesOpacity) * 1.2;

		return lerp(color, lerp(scanlineColor, slowscanColor, 0.5), 0.05);
	}

  #include "VideoGlitchFragCG.cginc"
  ENDCG

	SubShader
	{
    Cull Off
    ZWrite Off
    ZTest Always

    // Pass 0: Effect.
    Pass
    {
      CGPROGRAM
      #pragma target 3.0
      #pragma fragmentoption ARB_precision_hint_fastest
      #pragma exclude_renderers d3d9 d3d11_9x ps3 flash

      #pragma multi_compile ___ MODE_SCREEN MODE_LAYER MODE_DISTANCE
      #pragma multi_compile ___ COLOR_CONTROLS
      #pragma multi_compile ___ VIDEOGLITCH_DEMO
      
      #pragma vertex glitchVert
      #pragma fragment glitchFrag
      ENDCG
    }
	}

	Fallback off
}