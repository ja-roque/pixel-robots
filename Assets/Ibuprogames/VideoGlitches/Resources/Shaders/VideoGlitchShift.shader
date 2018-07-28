///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Ibuprogames <hello@ibuprogames.com>. All rights reserved.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR
// IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

Shader "Hidden/Video Glitches/VideoGlitchShift"
{
  Properties
  {
    _MainTex("Base (RGB)", 2D) = "white" {}
  }

  CGINCLUDE
  #include "UnityCG.cginc"
  #include "VideoGlitchCG.cginc"

  sampler2D _NoiseTex;

  float _Amplitude;
  float _Speed;

  inline float4 Pow4(float4 v, float p)
  {
    return float4(pow(v.x, p), pow(v.y, p), pow(v.z, p), v.w);
  }

  inline float4 Noise(float2 p)
  {
    return tex2D(_NoiseTex, p);
  }

  inline float3 ShiftRGB(float2 p, float4 shift)
  {
    shift *= 2.0 * shift.w - 1.0;

    float r = SampleMainTexture(p + float2(shift.x, -shift.y)).r;
    float g = SampleMainTexture(p + float2(shift.y, -shift.z)).g;
    float b = SampleMainTexture(p + float2(shift.z, -shift.x)).b;

    return float3(r, g, b);
  }
  
  inline float3 VideoGlitch(float3 pixel, float2 uv)
  {
    float4 shift = Pow4(Noise(float2(_Speed * _CustomTime.y, 2.0 * _Speed * _CustomTime.y / 25.0)), 8.0) * float4(_Amplitude, _Amplitude, _Amplitude, 1.0);

    return ShiftRGB(uv, shift);
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