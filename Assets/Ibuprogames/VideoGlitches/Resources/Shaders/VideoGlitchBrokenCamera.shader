///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Ibuprogames <hello@ibuprogames.com>. All rights reserved.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR
// IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

Shader "Hidden/Video Glitches/VideoGlitchBrokenCamera"
{
  Properties
  {
    _MainTex("Base (RGB)", 2D) = "white" {}
  }

  CGINCLUDE
  #include "UnityCG.cginc"
  #include "VideoGlitchCG.cginc"

  float _Malfunction;
  float _Noise;
  float _DistortionIntensity;
  float _DistortionSpeed;

  inline float3 VideoGlitch(float3 pixel, float2 uv)
  {
    float3 final = pixel;

    float2 uv1 = uv;
    uv1.y -= Rand(uv.x * _CustomTime.y) * _Noise;
    
    float3 e = SampleMainTexture(uv1);
    float bn = (e.r + e.g + e.b) / 3.0;

    float2 offset = float2(_DistortionSpeed * Rand(_CustomTime.y), sin(_CustomTime.y) * _DistortionIntensity);
    
    e.r = SampleMainTexture(uv + offset.xy).r;
    e.g = SampleMainTexture(uv).g;
    e.b = SampleMainTexture(uv + offset.yx).b;

    uv.y += Rand(_CustomTime.y) / (sin(_CustomTime.y) * 10.0);
    uv.x -= Rand(_CustomTime.y + 2.0) / (sin(_CustomTime.y) * 10.0) / 3.0;

    if (sin(_CustomTime.y * Rand(_CustomTime.y)) < _Malfunction)
      final = lerp(e, (float3)bn, 0.6);

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