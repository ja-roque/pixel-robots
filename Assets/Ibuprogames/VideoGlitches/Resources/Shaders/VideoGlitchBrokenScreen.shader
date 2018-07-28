///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Ibuprogames <hello@ibuprogames.com>. All rights reserved.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR
// IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

Shader "Hidden/Video Glitches/VideoGlitchBrokenScreen"
{
  Properties
  {
    _MainTex("Base (RGB)", 2D) = "white" {}
  }

  CGINCLUDE
  #include "UnityCG.cginc"
  #include "VideoGlitchCG.cginc"

  float2 _Center;
  int _Splits;
  float _SplitThreshold;
  float4 _SplitColor;
  float _Distortion;
  
  inline float Rand01(float2 p)
  {
    float3 p3 = frac(p.xyx * 0.1031);
    p3 += dot(p3, p3.yzx + 19.19);

    return frac((p3.x + p3.y) * p3.z) * 2.0 - 1.0;
  }

  inline float3 VideoGlitch(float3 pixel, float2 uv)
  {
    float2 uv2 = (uv * _ScreenParams.xy * 2.0 - _ScreenParams.xy) / _ScreenParams.x;

    float2 v = 1000.0;
    float2 v2 = 10000.0;

    for (int c = 0; c < _Splits; c++)
    {
      float fc = float(c);
      float angle = floor(Rand01(float2(fc, 387.44)) * 16.0) * 3.1415 * 0.4 - 0.5;
      float dist = pow(Rand01(float2(fc, 78.21)), 2.0) * 0.5;

      float2 vc = float2(_Center.x + cos(angle) * dist + Rand01(float2(fc, 349.3)) * 7E-3, _Center.y + sin(angle) * dist + Rand01(float2(fc, 912.7)) * 7E-3);
      if (length(vc - uv2) < length(v - uv2))
      {
        v2 = v;
        v = vc;
      }
      else if (length(vc - uv2) < length(v2 - uv2))
        v2 = vc;
    }

    float col = abs(length(dot(uv2 - v, normalize(v - v2))) - length(dot(uv2 - v2, normalize(v - v2)))) + 0.002 * length(uv2 - _Center);
    col /= 0.0025;

    if (length(v - v2) < 0.0004)
      col = 0.0;

    float3 final = SampleMainTexture(uv + Rand01(v) * _Distortion);

    if (col < _SplitThreshold)
      final *= _SplitColor;

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