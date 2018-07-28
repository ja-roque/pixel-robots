///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Ibuprogames <hello@ibuprogames.com>. All rights reserved.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR
// IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

Shader "Hidden/Video Glitches/VideoGlitchNoiseAnalog"
{
  Properties
  {
    _MainTex("Base (RGB)", 2D) = "white" {}
  }

  CGINCLUDE
  #include "UnityCG.cginc"
  #include "VideoGlitchCG.cginc"

  float _StripesSize;
  float _BarsCount;
  float _Distortion;
  float _NoiseIntensity;

  inline float3 VideoGlitch(float3 pixel, float2 uv)
  {
    float3 final = float3(0.0, 0.0, 0.0);
    float2 uv2 = uv;

    float t = mod(_CustomTime.y, 360.0);
    float t2 = floor(t * 0.6);

    float x, y, yt, xt;

    yt = abs(cos(t)) * Rand(float2(t, t)) * 100.0;
    xt = sin(2.0 * _Distortion * Rand(float2(t, t))) * 0.25;
    
	  if (xt < 0.0)
      xt = 0.125;

    x = uv2.x - xt * exp(-pow(uv2.y * 100.0 - yt, 2.0) / 24.0);
    y = uv2.y;
        
    uv2.x = x;
    uv2.y = y;

    yt = 0.5 * cos((yt / 100.0) / 100.0 * 360.0);
    float yr = 0.1 * cos((yt / 100.0) / 100.0 * 360.0);

	  if (uv.y > yt && uv.y < yt + Rand(float2(t2, t)) * _StripesSize)
	  {
      float md = mod(x * 100.0, 10.0);

      if (md * sin(t) > sin(yr * 360.0) || Rand(float2(md, md)) > 0.4)
      {
        float3 stripeColor = SampleMainTexture(uv2);

        float colX = Rand(float2(t2, t2)) * 0.5;
        
		    final = float3(stripeColor.x + colX, stripeColor.y + colX, stripeColor.z + colX);
      }
    }
    else if (y < sin(t) * 0.01 && mod(x * 40.0, 2.0) > Rand(float2(y * t, t * t)) || mod(y * _BarsCount, 2.0) < Rand(float2(x, t)))
	  {
      if (_BarsCount > 0 && Rand(float2(x + t, y + t)) > 0.8)
	      final = float3(Rand(float2(x * t, y * t)), Rand(float2(x * t, y * t)), Rand(float2(x * t, y * t)));
      else 
	      final = SampleMainTexture(uv2);
    }
	  else
	  {
      uv2.x += Rand(float2(t, uv2.y)) * _NoiseIntensity;
      
      final = SampleMainTexture(uv2);
	  }

    return final;
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