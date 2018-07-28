///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Ibuprogames <hello@ibuprogames.com>. All rights reserved.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR
// IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using UnityEngine;

namespace Ibuprogames
{
  namespace VideoGlitchesAsset
  {
    /// <summary>
    /// VHS pause noise.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    [AddComponentMenu("Image Effects/Video Glitches/Video Glitch VHS Pause")]
    public sealed class VideoGlitchVHSPause : VideoGlitchBase
    {
      /// <summary>
      /// Effect strength [0.0 - 1.0]. Default 1.
      /// </summary>
      [RangeFloat(0.0f, 1.0f, 1.0f)]
      public float Intensity
      {
        get { return intensity; }
        set { intensity = Mathf.Clamp01(value); }
      }

      /// <summary>
      /// Noise [0.0 - 1.0]. Default 0.1.
      /// </summary>
      [RangeFloat(0.0f, 1.0f, 0.1f)]
      public float Noise
      {
        get { return noise; }
        set { noise = Mathf.Clamp01(value); }
      }

      /// <summary>
      /// Noise color. Default white.
      /// </summary>
      public Color Color
      {
        get { return noiseColor; }
        set { noiseColor = value; }
      }

      [SerializeField]
      private float intensity = 1.0f;

      [SerializeField]
      private float noise = 0.1f;

      [SerializeField]
      private Color noiseColor = Color.white;

      private const string variableIntensity = @"_Intensity";
      private const string variableColorNoise = @"_ColorNoise";
      private const string variableNoiseColor = @"_NoiseColor";

      /// <summary>
      /// Effect description.
      /// </summary>
      public override string ToString()
      {
        return @"VHS pause noise.";
      }

      /// <summary>
      /// Set the default values of the shader.
      /// </summary>
      public override void ResetDefaultValues()
      {
        intensity = 1.0f;
        noise = 0.1f;
        noiseColor = Color.white;

        base.ResetDefaultValues();
      }

      /// <summary>
      /// Set the values to shader.
      /// </summary>
      protected override void SendValuesToShader()
      {
        material.SetFloat(variableIntensity, intensity);
        material.SetFloat(variableColorNoise, noise);
        material.SetColor(variableNoiseColor, noiseColor);
      }
    }
  }
}
