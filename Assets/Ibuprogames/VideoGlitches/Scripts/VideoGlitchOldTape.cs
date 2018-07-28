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
    /// Old video tape.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    [AddComponentMenu("Image Effects/Video Glitches/Video Glitch Old Tape")]
    public sealed class VideoGlitchOldTape : VideoGlitchBase
    {
      /// <summary>
      /// Noise speed [0.0 - 1.0]. Default 0.25.
      /// </summary>
      [RangeFloat(0.0f, 1.0f, 0.25f)]
      public float Speed
      {
        get { return noiseSpeed; }
        set { noiseSpeed = value < 0.0f ? 0.0f : value; }
      }

      /// <summary>
      /// Noise amplitude [1.0 - 100.0]. Default 1.
      /// </summary>
      [RangeFloat(1.0f, 100.0f, 1.0f)]
      public float Amplitude
      {
        get { return noiseAmplitude; }
        set { noiseAmplitude = value < 1.0f ? 1.0f : value; }
      }

      [SerializeField]
      private float noiseSpeed = 0.25f;

      [SerializeField]
      private float noiseAmplitude = 1.0f;

      private const string variableNoiseSpeed = @"_NoiseSpeed";
      private const string variableNoiseAmplitude = @"_NoiseAmplitude";

      /// <summary>
      /// Effect description.
      /// </summary>
      public override string ToString()
      {
        return @"Old video tape.";
      }

      /// <summary>
      /// Set the default values of the shader.
      /// </summary>
      public override void ResetDefaultValues()
      {
        noiseSpeed = 0.25f;
        noiseAmplitude = 1.0f;

        base.ResetDefaultValues();
      }

      /// <summary>
      /// Set the values to shader.
      /// </summary>
      protected override void SendValuesToShader()
      {
        material.SetFloat(variableNoiseSpeed, noiseSpeed * 0.0001f);
        material.SetFloat(variableNoiseAmplitude, (101.0f - noiseAmplitude) * 1000.0f);
      }
    }
  }
}
