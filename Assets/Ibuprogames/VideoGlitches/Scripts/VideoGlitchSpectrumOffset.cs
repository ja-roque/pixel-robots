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
    /// Spectrum Offset.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    [AddComponentMenu("Image Effects/Video Glitches/Video Glitch Spectrum Offset")]
    public sealed class VideoGlitchSpectrumOffset : VideoGlitchBase
    {
      /// <summary>
      /// Effect intensity [0.0 - 1.0]. Default 0.1.
      /// </summary>
      [RangeFloat(0.0f, 1.0f, 0.1f)]
      public float Intensity
      {
        get { return intensity; }
        set { intensity = Mathf.Clamp01(value); }
      }

      /// <summary>
      /// Effect steps [3 - 10]. Default 5.
      /// </summary>
      [RangeInt(3, 10, 5)]
      public int Steps
      {
        get { return steps; }
        set { steps = value < 3 ? 3 : value; }
      }

      [SerializeField]
      private float intensity = 0.1f;

      [SerializeField]
      private int steps = 5;

      private const string variableIntensity = @"_Intensity";
      private const string variableSteps = @"_Steps";

      /// <summary>
      /// Effect description.
      /// </summary>
      public override string ToString()
      {
        return @"Spectrum offset.";
      }

      /// <summary>
      /// Set the default values of the shader.
      /// </summary>
      public override void ResetDefaultValues()
      {
        intensity = 0.1f;
        steps = 5;

        base.ResetDefaultValues();
      }

      /// <summary>
      /// Set the values to shader.
      /// </summary>
      protected override void SendValuesToShader()
      {
        material.SetFloat(variableIntensity, intensity);
        material.SetInt(variableSteps, steps);
      }
    }
  }
}
