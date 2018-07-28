///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Ibuprogames <hello@ibuprogames.com>. All rights reserved.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR
// IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#ifndef VIDEOGLITCHES_CGINC
#define VIDEOGLITCHES_CGINC

// Common.
float _Strength;
float4 _CustomTime;

// Color control.
#ifdef COLOR_CONTROLS
  float _Brightness;
  float _Contrast;
  float _Gamma;
  float _Hue;
  float _Saturation;
#endif

UNITY_DECLARE_SCREENSPACE_TEXTURE(_MainTex);
float4 _MainTex_ST;
float4 _MainTex_TexelSize;

#if defined(MODE_LAYER) || defined(MODE_DISTANCE)
  UNITY_DECLARE_DEPTH_TEXTURE(_CameraDepthTexture);
#endif

#if MODE_DISTANCE
sampler2D _DistanceTex;
#endif

#ifdef MODE_LAYER
float _DepthThreshold;

sampler2D _RTT;
#endif

#if SHADER_API_PS4
inline float2 lerp(float2 a, float2 b, float t)
{
  return lerp(a, b, (float2)t);
}

inline float3 lerp(float3 a, float3 b, float t)
{
  return lerp(a, b, (float3)t);
}

inline float4 lerp(float4 a, float4 b, float t)
{
  return lerp(a, b, (float4)t);
}
#endif

inline float mod(float x, float y)
{
  return x - y * floor(x / y);
}

#define _PI			3.141592653589

/// <summary>
/// Samples MainTex.
/// </summary>
inline float3 SampleMainTexture(float2 uv)
{
#if defined(UNITY_SINGLE_PASS_STEREO)
  return tex2D(_MainTex, UnityStereoScreenSpaceUVAdjust(uv, _MainTex_ST)).rgb;
#else
  return UNITY_SAMPLE_SCREENSPACE_TEXTURE(_MainTex, uv).rgb;
#endif
}

/// <summary>
/// Samples MainTex lod.
/// </summary>
inline float3 SampleMainTextureLod(float2 uv)
{
#if defined(UNITY_SINGLE_PASS_STEREO)
  return tex2Dlod(_MainTex, float4(UnityStereoScreenSpaceUVAdjust(uv, _MainTex_ST), 0.0, 0.0)).rgb;
#else
  return tex2Dlod(_MainTex, float4(uv, 0.0, 0.0)).rgb;
#endif
}

/// <summary>
/// RGB -> YUV.
/// </summary>
inline float3 RGB2YUV(float3 rgb)
{
  float3 yuv;
  yuv.x = dot(rgb, float3(0.299, 0.587, 0.114));
  yuv.y = dot(rgb, float3(-0.14713, -0.28886, 0.436));
  yuv.z = dot(rgb, float3(0.615, -0.51499, -0.10001));

  return yuv;
}

/// <summary>
/// YUV -> RGB.
/// </summary>
inline float3 YUV2RGB(float3 yuv)
{
  float3 rgb;
  rgb.r = yuv.x + yuv.z * 1.13983;
  rgb.g = yuv.x + dot(float2(-0.39465, -0.58060), yuv.yz);
  rgb.b = yuv.x + yuv.y * 2.03211;

  return rgb;
}

/// <summary>
/// RGB -> HSV.
/// </summary>
inline float3 RGB2HSV(float3 c)
{
  float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
  float4 p = lerp(float4(c.bg, K.wz), float4(c.gb, K.xy), step(c.b, c.g));
  float4 q = lerp(float4(p.xyw, c.r), float4(c.r, p.yzx), step(p.x, c.r));

  float d = q.x - min(q.w, q.y);
  float e = 1.0e-10;
    
  return float3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
}

/// <summary>
/// HSV -> RGB.
/// </summary>
inline float3 HSV2RGB(float3 c)
{
  float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
  float3 p = abs(frac(c.xxx + K.xyz) * 6.0 - K.www);
    
  return c.z * lerp((float3)K.xxx, float3(clamp(p - K.xxx, 0.0, 1.0)), c.y);
}

/// <summary>
/// XYZ space.
/// </summary>
inline float3 Coord2XYZ(float2 coord, int size)
{
  float2 uv = float2(int2(float2(coord.x, _ScreenParams.y - coord.y) / (float2(size, size))));
  int block = int(uv.y * _ScreenParams.x / float(size) + uv.x);
  
  float2 fragCoordY = mod(coord, float2(size, size));
  
  return float3(fragCoordY, block);
}

/// <summary>
/// XYZ space.
/// </summary>
inline float2 XYZ2Coord(float3 xyz, int size, bool flip)
{
  int block = int(xyz.z);
  int width = int(_ScreenParams.x) / size;

  float2 fullC = float2(size, size) * float2(block % width, block / width);
  
  return float2(fullC.x + xyz.x, flip ? _ScreenParams.y - fullC.y - xyz.y : fullC.y + xyz.y);
}

#ifdef COLOR_CONTROLS

/// <summary>
/// Color adjust.
/// </summary>
inline float3 ColorAdjust(float3 pixel)
{
  // Brightness.
  pixel += _Brightness;

  // Contrast.
  pixel = (pixel - 0.5) * ((1.015 * (_Contrast + 1.0)) / (1.015 - _Contrast)) + 0.5;

  // Hue & saturation.
  float3 hsv = RGB2HSV(pixel);

  hsv.x += _Hue;
  hsv.y *= _Saturation;

  pixel = HSV2RGB(hsv);

  // Gamma.
  pixel = pow(pixel, _Gamma);

  return pixel;
}

#endif

inline float Rand(float n)
{
  return frac(sin(n) * 43758.5453123);
}

inline float Rand(float2 n)
{
  return frac(sin(dot(n, float2(12.9898, 78.233))) * 43758.5453);
}

inline float SRand(float2 n)
{
  return Rand(n) * 2.0 - 1.0;
}

inline float Trunc(float x, float num_levels)
{
  return floor(x * num_levels) / num_levels;
}

inline float2 Trunc(float2 x, float2 num_levels)
{
  return floor(x * num_levels) / num_levels;
}

// Do not use.
inline float3 PixelDemo(float3 pixel, float3 final, float2 uv)
{
  float separator = (sin(_Time.x * 20.0) * 0.3) + 0.8;
  const float separatorWidth = 0.05;

  if (uv.x > separator)
    final = pixel;
  else if (abs(uv.x - separator) < separatorWidth)
    final = lerp(pixel, final, (separator - uv.x) / separatorWidth);

  return final;
}

struct appdata_t
{
  float4 vertex : POSITION;
  half2 texcoord : TEXCOORD0;
  UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct v2f
{
  float4 vertex : SV_POSITION;
  half2 texcoord : TEXCOORD0;
  UNITY_VERTEX_INPUT_INSTANCE_ID
  UNITY_VERTEX_OUTPUT_STEREO    
};

v2f glitchVert(appdata_t v)
{
  v2f o;
  UNITY_SETUP_INSTANCE_ID(v);
  UNITY_TRANSFER_INSTANCE_ID(v, o);
  UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
  o.vertex = UnityObjectToClipPos(v.vertex);

#if UNITY_UV_STARTS_AT_TOP
  if (_MainTex_TexelSize.y < 0)
    o.texcoord.y = 1.0 - o.texcoord.y;
#endif

  o.texcoord = v.texcoord;

  return o;
}

#endif