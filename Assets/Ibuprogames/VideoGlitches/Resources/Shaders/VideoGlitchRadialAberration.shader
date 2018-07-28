///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Ibuprogames <hello@ibuprogames.com>. All rights reserved.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR
// IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

Shader "Hidden/Video Glitches/VideoGlitchRadialAberration"
{
  Properties
  {
    _MainTex("Base (RGB)", 2D) = "white" {}
  }

  CGINCLUDE
  #pragma exclude_renderers gles

  #include "UnityCG.cginc"
  #include "VideoGlitchCG.cginc"

  int _Samples;
  float _Blur;
  float _Falloff;
  float2 _Focus;

  inline float3 VideoGlitch(float3 pixel, float2 uv)
  {
    float2 velocity = normalize(uv - _Focus) * _Blur * pow(length(uv - _Focus), _Falloff);
    
    const float inverseSampleCount = 1.0 / float(_Samples);
    float3x2 increments = float3x2(velocity * 1.0 * inverseSampleCount,
                                   velocity * 2.0 * inverseSampleCount,
                                   velocity * 4.0 * inverseSampleCount);

    float3 accumulator = float3(0.0, 0.0, 0.0);
    float3x2 offsets = float3x2(0.0, 0.0, 0.0, 0.0, 0.0, 0.0);

    for (int i = 0; i < _Samples; i++)
    {
      accumulator.r += SampleMainTextureLod(uv + offsets[0]).r;
      accumulator.g += SampleMainTextureLod(uv + offsets[1]).g;
      accumulator.b += SampleMainTextureLod(uv + offsets[2]).b;

      offsets -= increments;
    }

    return accumulator / float(_Samples);
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