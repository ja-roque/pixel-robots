///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Ibuprogames <hello@ibuprogames.com>. All rights reserved.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR
// IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

Shader "Hidden/Video Glitches/VideoGlitchVHSNoise"
{
  Properties
  {
    _MainTex("Base (RGB)", 2D) = "white" {}
  }

  CGINCLUDE
  #include "UnityCG.cginc"
  #include "VideoGlitchCG.cginc"

  float _Speed;
  float _Intensity;
  float _Size;
  float3 _Color;

  inline float4 Hash42(float2 n)
  {
    float4 p4 = frac(n.xyxy * float4(443.8975,397.2973, 491.1871, 470.7827));
    p4 += dot(p4.wzxy, p4 + 19.19);
      
    return frac(float4(p4.x * p4.y, p4.x * p4.z, p4.y * p4.w, p4.x * p4.w));
  }

  // 3D noise function (www.iquilezles.org).
  inline float Noise3D(float3 x)
  {
    float3 p = floor(x);
    float3 f = frac(x);
    
    f = f * f * (3.0 - 2.0 * f);

    float n = p.x + p.y * 57.0 + 113.0 * p.z;
    float res = lerp(lerp(lerp(Rand(n + 0.0), Rand(n + 1.0), f.x),
                lerp(Rand(n + 57.0), Rand(n + 58.0), f.x), f.y),
                lerp(lerp(Rand(n + 113.0), Rand(n + 114.0), f.x),
                lerp(Rand(n + 170.0), Rand(n + 171.0), f.x), f.y), f.z);

    return res;
  }

  inline float TapeNoise(float2 p)
  {
    float y = p.y;
    float s = _CustomTime.y * _Speed;

    float v = (Noise3D(float3(y * 0.01 + s, 1.0, 1.0)) + 0.0) *
              (Noise3D(float3(y * 0.011 + 1000.0 + s, 1.0, 1.0)) + 0.0) * 
              (Noise3D(float3(y * 0.51 + 421.0 + s, 1.0, 1.0)) + 0.0);

    v *= Hash42(float2(p.x + _CustomTime.y * 0.01, p.y)).x + _Intensity;
    v = pow(v + _Intensity, 1.0);

    return v < 0.7 ? 0.0 : v;
  }

  inline float3 VideoGlitch(float3 pixel, float2 uv)
  {
    if (_Size > 0.0)
    {
      float oneY = _ScreenParams.y / _Size;
      
      uv = floor(uv * _ScreenParams.xy / oneY) * oneY;

      pixel += _Color * TapeNoise(uv);
    }

    return pixel;
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