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
    /// Analog noise.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    [AddComponentMenu("Image Effects/Video Glitches/Video Glitch Noise Analog")]
    public sealed class VideoGlitchNoiseAnalog : VideoGlitchBase
    {
      /// <summary>
      /// Stripes size [0.0 - 1.0]. Default 0.25.
      /// </summary>
      [RangeFloat(0.0f, 1.0f, 0.25f)]
      public float StripesSize
      {
        get { return stripesSize; }
        set { stripesSize = Mathf.Clamp01(value); }
      }

      /// <summary>
      /// Noise bars count [0 - 100]. Default 10.
      /// </summary>
      [RangeInt(0, 100, 10)]
      public int BarsCount
      {
        get { return barsCount; }
        set { barsCount = value < 0 ? 0 : value; }
      }

      /// <summary>
      /// Distortion speed [0.0 - 1.0]. Default 0.25.
      /// </summary>
      [RangeFloat(0.0f, 1.0f, 0.25f)]
      public float Distortion
      {
        get { return distortion; }
        set { distortion = Mathf.Clamp01(value); }
      }

      /// <summary>
      /// Noise intensity [0.0 - 1.0]. Default 0.2.
      /// </summary>
      [RangeFloat(0.0f, 1.0f, 0.2f)]
      public float Noise
      {
        get { return noiseIntensity; }
        set { noiseIntensity = Mathf.Clamp01(value); }
      }

      [SerializeField]
      private float stripesSize = 0.25f;

      [SerializeField]
      private int barsCount = 10;

      [SerializeField]
      private float distortion = 0.25f;

      [SerializeField]
      private float noiseIntensity = 0.2f;

      private const string variableStripesSize = @"_StripesSize";
      private const string variableBarsCount = @"_BarsCount";
      private const string variableDistortion = @"_Distortion";
      private const string variableNoiseIntensity = @"_NoiseIntensity";

      /// <summary>
      /// Effect description.
      /// </summary>
      public override string ToString()
      {
        return @"Analog noise.";
      }

      /// <summary>
      /// Set the default values of the shader.
      /// </summary>
      public override void ResetDefaultValues()
      {
        stripesSize = 0.25f;
        barsCount = 10;
        distortion = 0.1f;
        noiseIntensity = 0.2f;

        base.ResetDefaultValues();
      }

      /// <summary>
      /// Set the values to shader.
      /// </summary>
      protected override void SendValuesToShader()
      {
        material.SetFloat(variableStripesSize, stripesSize);
        material.SetFloat(variableBarsCount, barsCount * 1.0f);
        material.SetFloat(variableDistortion, distortion);
        material.SetFloat(variableNoiseIntensity, noiseIntensity * 0.05f);
      }
    }
  }
}
