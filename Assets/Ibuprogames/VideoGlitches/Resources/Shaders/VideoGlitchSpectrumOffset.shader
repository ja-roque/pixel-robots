///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Ibuprogames <hello@ibuprogames.com>. All rights reserved.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR
// IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

Shader "Hidden/Video Glitches/VideoGlitchSpectrumOffset"
{
  Properties
  {
    _MainTex("Base (RGB)", 2D) = "white" {}
  }

  CGINCLUDE
  #include "UnityCG.cginc"
  #include "VideoGlitchCG.cginc"

  float _Intensity;
  int _Steps;

  inline float Clamp01(float t)
  {
    return clamp(t, 0.0, 1.0);
  }

  inline float2 Clamp01(float2 t)
  {
    return clamp(t, 0.0, 1.0);
  }

  // Remaps [a; b] to [0; 1].
  inline float Remap(float t, float a, float b)
  {
    return Clamp01((t - a) / (b - a));
  }

  // t = [0; 0.5; 1], y = [0; 1; 0]
  inline float Linterp(float t)
  {
    return Clamp01(1.0 - abs(2.0 * t - 1.0));
  }

  inline float3 SpectrumOffset(float t)
  {
    float3 ret;

    float lo = step(t, 0.5);
    float hi = 1.0 - lo;
    float w = Linterp(Remap(t, 0.1666666666, 0.8333333333)); // 1 / 6, 5 / 6.
    
    float neg_w = 1.0 - w;
    ret = float3(lo, 1.0, hi) * float3(neg_w, w, neg_w);
	
    return pow(ret, 0.4545454545); // 1.0 / 2.2
  }

  inline float3 VideoGlitch(float3 pixel, float2 uv)
  {
    float time = mod(_CustomTime.y, 32.0);
    	
    float gnm = Clamp01(_Intensity);
    float rnd0 = Rand(Trunc(float2(time, time), 6.0));
    float r0 = Clamp01((1.0 - gnm) * 0.7 + rnd0);

	  // Horizontal.
    float rnd1 = Rand(float2(Trunc(uv.x, 10.0 * r0), time));
    float r1 = 0.5 - 0.5 * gnm + rnd1;

    // Vertical.
    float rnd2 = Rand(float2(Trunc(uv.y, 40.0 * r1), time));
    float r2 = Clamp01(rnd2);
    
    float rnd3 = Rand(float2(Trunc(uv.y, 10.0 * r0), time));
    float r3 = (1.0 - Clamp01(rnd3 + 0.8)) - 0.1;
    
    float pxrnd = Rand(uv + time);
    
    float ofs = 0.05 * r2 * _Intensity * (rnd0 > 0.5 ? 1.0 : -1.0);
    ofs += 0.5 * pxrnd * ofs;
    
    uv.y += 0.1 * r3 * _Intensity;

    const float fSamples = 1.0 / float(_Steps);
        
    float3 final = float3(0.0, 0.0, 0.0);
    float3 wsum = 0.0;
    for (int i = 0; i < _Steps; ++i)
    {
      float t = float(i) * fSamples;
      uv.x = Clamp01(uv.x + ofs * t);

      float3 samplecol = SampleMainTextureLod(uv);
      
      float3 s = SpectrumOffset(t);
      samplecol *= s;
      final += samplecol;
      wsum += s;
    }

    final.rgb /= wsum;

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