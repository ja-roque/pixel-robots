///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Ibuprogames <hello@ibuprogames.com>. All rights reserved.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR
// IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

Shader "Hidden/Video Glitches/VideoGlitchNoiseDigital"
{
  Properties
  {
    _MainTex("Base (RGB)", 2D) = "white" {}
  }

  CGINCLUDE
  #include "UnityCG.cginc"
  #include "VideoGlitchCG.cginc"

  float _Threshold;
  float _MaxOffset;
  float _ThresholdYUV;

  inline float3 VideoGlitch(float3 pixel, float2 uv)
  {
    float modTime = mod(_CustomTime.y, 32.0);
    
    float thresholdGlitch = 1.0 - _Threshold;
    const float timeFreq = 16.0;

    const float minChangeFreq = 4.0;
    float ct = Trunc(modTime, minChangeFreq);
    float randChange = Rand(Trunc(uv.yy, float2(16.0, 16.0)) + 150.0 * ct);

    float tf = timeFreq * randChange;

    float t = 5.0 * Trunc(modTime, tf);
    float randVT = 0.5 * Rand(Trunc(uv.yy + t, float2(11.0, 11.0)));
    randVT += 0.5 * Rand(Trunc(uv.yy + t, float2(7.0, 7.0)));
    randVT = randVT * 2.0 - 1.0;
    randVT = sign(randVT) * clamp((abs(randVT) - thresholdGlitch) / (1.0 - thresholdGlitch), 0.0, 1.0);

    float2 uvNM = uv;
    uvNM = clamp(uvNM + float2(_MaxOffset * randVT, 0.0), 0.0, 1.0);

    float rnd = Rand(float2(Trunc(modTime, 8.0), Trunc(modTime, 8.0)));
    uvNM.y = (rnd > lerp(1.0, 0.975, clamp(_Threshold, 0.0, 1.0))) ? 1.0 - uvNM.y : uvNM.y;

    float3 final = SampleMainTexture(uvNM).rgb;

    final = RGB2YUV(final);

    final.g /= 1.0 - 3.0 * abs(randVT) * clamp(_ThresholdYUV - randVT, 0.0, 1.0);
    final.b += 0.125 * randVT * clamp(randVT - _ThresholdYUV, 0.0, 1.0);

    final = YUV2RGB(final);

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