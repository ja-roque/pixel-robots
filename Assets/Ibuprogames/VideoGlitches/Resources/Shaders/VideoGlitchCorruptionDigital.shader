///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Ibuprogames <hello@ibuprogames.com>. All rights reserved.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR
// IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

Shader "Hidden/Video Glitches/VideoGlitchCorruptionDigital"
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
  float _TileSize;
  float _Posterize;
 
  inline float3 Posterize(float3 color, float steps)
  {
    return floor(color * steps) / steps;
  }

  inline float Quantize(float n, float steps)
  {
    return floor(n * steps) / steps;
  }

  float3 Downsample(float2 uv, float pixelSize)
  {
    return SampleMainTexture(uv - mod(uv, pixelSize / _ScreenParams.xy));
  }

  inline float Noise(float p)
  {
    float fl = floor(p);
    float fc = frac(p);
  
    return lerp(Rand(fl), Rand(fl + 1.0), fc);
  }

  inline float Noise(float2 p)
  {
    float2 ip = floor(p);
    float2 u = frac(p);
    u = u * u * (3.0 - 2.0 * u);

    float res = lerp(lerp(Rand(ip), Rand(ip + float2(1.0, 0.0)), u.x),
                     lerp(Rand(ip + float2(0.0, 1.0)), Rand(ip + float2(1.0, 1.0)), u.x), u.y);
  
    return res * res;
  }

  inline float3 Edge(float2 uv, float sampleSize)
  {
    float dx = sampleSize / _ScreenParams.x;
    float dy = sampleSize / _ScreenParams.y;

    return (lerp(Downsample(uv - float2(dx, 0.0), sampleSize), Downsample(uv + float2(dx, 0.0), sampleSize), mod(uv.x, dx) / dx) +
            lerp(Downsample(uv - float2(0.0, dy), sampleSize), Downsample(uv + float2(0.0, dy), sampleSize), mod(uv.y, dy) / dy)).rgb / 2.0 - SampleMainTexture(uv);
  }

  inline float3 Distort(float2 uv, float edgeSize)
  {
    float2 pixel = 1.0 / _ScreenParams.xy;
    float3 field = RGB2HSV(Edge(uv, edgeSize));
    float2 distort = pixel * sin((field.rb) * _PI * 2.0);
    
    float speed = _CustomTime.y * _Speed;
    float shiftx = Noise(float2(Quantize(uv.y + 31.5, _ScreenParams.y / _TileSize) * speed, frac(speed) * 300.0));
    float shifty = Noise(float2(Quantize(uv.x + 11.5, _ScreenParams.x / _TileSize) * speed, frac(speed) * 100.0));
  
    float3 rgb = SampleMainTexture(uv + (distort + (pixel - pixel / 2.0) * float2(shiftx, shifty) * (50.0 + 100.0 * _Intensity)) * _Intensity);

    return Posterize(rgb, floor(lerp(256.0, pow(1.0 - rgb.b - 0.5, 2.0) * 64.0 * shiftx + 4.0, 1.0 - pow(_Posterize, 5.0))));
  }

  inline float3 VideoGlitch(float3 pixel, float2 uv)
  {
    return Distort(uv, 8.0);
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