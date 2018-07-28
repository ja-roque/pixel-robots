///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Ibuprogames <hello@ibuprogames.com>. All rights reserved.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR
// IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#ifndef VIDEOGLITCH_FRAG_CGINC
#define VIDEOGLITCH_FRAG_CGINC

inline float4 glitchFrag(v2f i) : SV_Target
{
  UNITY_SETUP_INSTANCE_ID(i);

  float3 pixel = SampleMainTexture(i.texcoord);

  float3 final = pixel;

#ifdef MODE_SCREEN
  final = VideoGlitch(pixel, i.texcoord);
#elif MODE_LAYER
  float depth = Linear01Depth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, i.texcoord));

  float rtt = tex2D(_RTT, i.texcoord).r;

  if (rtt - depth < _DepthThreshold)
    final = VideoGlitch(pixel, i.texcoord);
#elif MODE_DISTANCE
  final = VideoGlitch(pixel, i.texcoord);

  float depth = Linear01Depth(SAMPLE_RAW_DEPTH_TEXTURE(_CameraDepthTexture, i.texcoord));
  
  float curve = clamp(tex2D(_DistanceTex, float2(depth, 0.0)).x, 0.0, 1.0);

  final = lerp(pixel, final, curve);
#endif

#ifdef COLOR_CONTROLS
  final = ColorAdjust(final);
#endif

#ifdef VIDEOGLITCH_DEMO
  final = PixelDemo(tex2D(_MainTex, i.texcoord).rgb, final, i.texcoord);
#endif

  final = lerp(SampleMainTexture(i.texcoord), final, _Strength);

  return float4(final, 1.0);
}

#endif