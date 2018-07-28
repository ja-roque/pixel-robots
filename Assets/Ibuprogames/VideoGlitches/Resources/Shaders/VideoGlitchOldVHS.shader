///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Ibuprogames <hello@ibuprogames.com>. All rights reserved.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR
// IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

Shader "Hidden/Video Glitches/VideoGlitchOldVHS"
{
  Properties
  {
    _MainTex("Base (RGB)", 2D) = "white" {}
  }

  CGINCLUDE
  #include "UnityCG.cginc"
  #include "VideoGlitchCG.cginc"

  float _Waving;
  float _Noise;
  float _StripeCount;
  float _StripeVelocity;
  float _StripeStrength;
  float _StripeNoise;
  float _SwitchingNoise;
  float _ACBeatWidth;
  float _ACBeatVelocity;
  float _BloomPasses;

  inline float Hash(float2 _v, float2 _r)
  {
    float h00 = Rand(float2(floor(_v * _r + float2(0.0, 0.0)) / _r));
    float h10 = Rand(float2(floor(_v * _r + float2(1.0, 0.0)) / _r));
    float h01 = Rand(float2(floor(_v * _r + float2(0.0, 1.0)) / _r));
    float h11 = Rand(float2(floor(_v * _r + float2(1.0, 1.0)) / _r));

    float2 ip = float2(smoothstep(0.0, 1.0, fmod(_v * _r, 1.0)));

    return (h00 * (1.0 - ip.x) + h10 * ip.x) * (1.0 - ip.y) + (h01 * (1.0 - ip.x) + h11 * ip.x) * ip.y;
  }
  
  inline float Noise(float2 _v)
  {
    float sum = 0.0;

    for (int i = 1; i < 9; i++)
    {
	    float fi = float(i);
	    float ft = pow(2.0, fi);
	    sum += Hash(_v + fi, 2.0 * ft) / ft;
    }

    return sum;
  }

  inline float3 HueShift(float3 _i, float _p)
  {
    float3 p = 0.0;
    p.x = clamp(cos(_p), 0.0, 1.0);
    p.y = clamp(cos(_p - _PI / 3.0 * 2.0), 0.0, 1.0);
    p.z = clamp(cos(_p - _PI / 3.0 * 4.0), 0.0, 1.0);

    return float3(dot(_i, p.xyz), dot(_i, p.zxy), dot(_i, p.yzx));
  }

  inline float3 VideoGlitch(float3 pixel, float2 uv)
  {
    float3 final = pixel;

    float huen = 0.0;
    float3 tex = pixel;

    // Waving.
    uv.x += (Noise(float2(uv.y, _CustomTime.y)) - 0.5) * 0.04 * _Waving;

    // Noise.
    uv.x += (Noise(float2(uv.y * 100.0, _CustomTime.y * 10.0)) - 0.5) * 0.1 * _Noise;

    // Stripe.
    float tcPhase = clamp((sin(uv.y * _StripeCount - _CustomTime.y * _PI * _StripeVelocity) - 0.92) * Noise(_CustomTime.y), 0.0, 0.01) * _StripeStrength;
    float tcNoise = max(Noise(float2(uv.y * _StripeNoise, _CustomTime.y * 10.0)) - 0.5, 0.0);
    uv.x = uv.x - tcNoise * tcPhase;
    huen += (tcNoise + 5.0) * tcPhase;

    // Switching noise.
    float snPhase = clamp(pow(1.0 - uv.y, 500.0) * 1000.0, 0.0, 1.0) * _SwitchingNoise;
    uv.x *= (1.0 - snPhase * 0.5) + snPhase * (Noise(float2(uv.y * 100.0, _CustomTime.y * 10.0)) * 0.2 + 0.1);
    huen += snPhase;
    final = HueShift(SampleMainTexture(uv), huen);

    // Bloom
#ifdef USE_BLOOM
    if (_BloomPasses > 0.0)
    {
      float bloomFactor = 0.3 / _BloomPasses;
      for (float x = 0.0; x < _BloomPasses; x += 1.0)
        final += SampleMainTextureLod(uv + float2(x, 0.0) * 7E-3) * bloomFactor;
    }
#endif

    // AC beat
    final *= 1.0 + clamp(Noise(float2(0.0, uv.y + _CustomTime.y * _ACBeatVelocity)) * _ACBeatWidth - 0.25, 0.0, 0.1);

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
      #pragma multi_compile ___ USE_BLOOM
      
      #pragma vertex glitchVert
      #pragma fragment glitchFrag
      ENDCG
    }
  }

  Fallback off
}