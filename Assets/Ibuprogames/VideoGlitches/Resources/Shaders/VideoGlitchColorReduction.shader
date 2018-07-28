///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Ibuprogames <hello@ibuprogames.com>. All rights reserved.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR
// IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

Shader "Hidden/Video Glitches/VideoGlitchColorReduction"
{
  Properties
  {
    _MainTex("Base (RGB)", 2D) = "white" {}
  }

  CGINCLUDE
  #include "UnityCG.cginc"
  #include "VideoGlitchCG.cginc"

  sampler2D _DitheringTex;
  float _PaletteSpace;
  float _Pixelation;

  inline float Quantize(float inp, float period)
  {
    return floor((inp + period / 2.0) / period) * period;
  }

  inline float Bayer4x4(float2 uvScreenSpace)
  {
    float2 bayerCoord = floor(uvScreenSpace / _Pixelation);
    bayerCoord = fmod(bayerCoord, 4.0);
    
    const float4x4 bayerMat = float4x4(1.0, 9.0, 3.0, 11.0,
                                       13.0, 5.0, 15.0, 7.0,
                                       4.0, 12.0, 2.0, 10.0,
                                       16.0, 8.0, 14.0, 6.0) / 16.0;

    int bayerIndex = int(bayerCoord.x + bayerCoord.y * 4.0);
    if (bayerIndex == 0) return bayerMat[0][0];
    if (bayerIndex == 1) return bayerMat[0][1];
    if (bayerIndex == 2) return bayerMat[0][2];
    if (bayerIndex == 3) return bayerMat[0][3];
    if (bayerIndex == 4) return bayerMat[1][0];
    if (bayerIndex == 5) return bayerMat[1][1];
    if (bayerIndex == 6) return bayerMat[1][2];
    if (bayerIndex == 7) return bayerMat[1][3];
    if (bayerIndex == 8) return bayerMat[2][0];
    if (bayerIndex == 9) return bayerMat[2][1];
    if (bayerIndex == 10) return bayerMat[2][2];
    if (bayerIndex == 11) return bayerMat[2][3];
    if (bayerIndex == 12) return bayerMat[3][0];
    if (bayerIndex == 13) return bayerMat[3][1];
    if (bayerIndex == 14) return bayerMat[3][2];
    if (bayerIndex == 15) return bayerMat[3][3];

    return 10.0;
  }

  inline float Bayer8x8(float2 uvScreenSpace)
  {
    return tex2D(_DitheringTex, uvScreenSpace / (_Pixelation * 8.0)).r;
  }

  inline float3 VideoGlitch(float3 pixel, float2 uv)
  {
    float2 fragCoord = uv * _ScreenParams.xy;
    float2 uvPixellated = floor(fragCoord / _Pixelation) * _Pixelation;

    float3 dc = SampleMainTexture(uvPixellated / _ScreenParams.xy);

#ifdef DITHERING_BAYER4x4
    dc += (Bayer4x4(fragCoord) - 0.5) * _PaletteSpace;
#elif DITHERING_BAYER8x8
    dc += (Bayer8x8(fragCoord) - 0.5) * _PaletteSpace;
#elif DITHERING_NOISE
    dc += (Rand(uvPixellated) - 0.5) * _PaletteSpace;
#else
    dc += (Rand(float2(Rand(uvPixellated), _CustomTime.w)) - 0.5) * _PaletteSpace;
#endif

    return float3(Quantize(dc.r, _PaletteSpace), Quantize(dc.g, _PaletteSpace), Quantize(dc.b, _PaletteSpace));
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

      #pragma multi_compile ___ DITHERING_BAYER4x4 DITHERING_BAYER8x8 DITHERING_NOISE
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