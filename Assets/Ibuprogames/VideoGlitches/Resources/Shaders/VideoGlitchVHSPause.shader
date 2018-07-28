///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Ibuprogames <hello@ibuprogames.com>. All rights reserved.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR
// IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

Shader "Hidden/Video Glitches/VideoGlitchVHSPause"
{
  Properties
  {
    _MainTex("Base (RGB)", 2D) = "white" {}
  }

  CGINCLUDE
  #include "UnityCG.cginc"
  #include "VideoGlitchCG.cginc"

  float _Intensity;
  float _ColorNoise;
  float4 _NoiseColor;

  inline float3 VideoGlitch(float3 pixel, float2 uv)
  {
    float2 uv2 = uv;

    uv2.x += ((Rand(float2(_CustomTime.y, uv.y)) - 0.5) / 64.0) * _Intensity;
    uv2.y += ((Rand(_CustomTime.y) - 0.5) / 32.0) * _Intensity;

    float3 final = (-0.5 + float3(Rand(float2(uv.y, _CustomTime.y)), Rand(float2(uv.y, _CustomTime.y + 1.0)), Rand(float2(uv.y, _CustomTime.y + 2.0)))) * _ColorNoise;

    float noise = Rand(float2(floor(uv2.y * 80.0), floor(uv2.x * 50.0)) + float2(_CustomTime.y, 0.0));

    if (noise > 11.5 - 30.0 * uv2.y || noise < 1.5 - 5.0 * uv2.y)
      final += SampleMainTexture(uv2);
    else
      final = _NoiseColor.rgb;

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