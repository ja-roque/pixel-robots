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
    /// Digital noise.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    [AddComponentMenu("Image Effects/Video Glitches/Video Glitch Noise Digital")]
    public sealed class VideoGlitchNoiseDigital : VideoGlitchBase
    {
      /// <summary>
      /// Threshold [0.0 - 1.0]. Default 0.1.
      /// </summary>
      [RangeFloat(0.0f, 1.0f, 0.1f)]
      public float Threshold
      {
        get { return threshold; }
        set { threshold = Mathf.Clamp01(value); }
      }

      /// <summary>
      /// Max offset [0.0 - 1.0]. Default 0.1.
      /// </summary>
      [RangeFloat(0.0f, 1.0f, 0.1f)]
      public float MaxOffset
      {
        get { return maxOffset; }
        set { maxOffset = Mathf.Clamp01(value); }
      }

      /// <summary>
      /// Threshold YUV [0.0 - 1.0]. Default 0.5.
      /// </summary>
      [RangeFloat(0.0f, 1.0f, 0.5f)]
      public float ThresholdYUV
      {
        get { return thresholdYUV; }
        set { thresholdYUV = Mathf.Clamp01(value); }
      }

      [SerializeField]
      private float threshold = 0.1f;

      [SerializeField]
      private float maxOffset = 0.1f;

      [SerializeField]
      private float thresholdYUV = 0.5f;

      private const string variableThreshold = @"_Threshold";
      private const string variableMaxOffset = @"_MaxOffset";
      private const string variableThresholdYUV = @"_ThresholdYUV";

      /// <summary>
      /// Effect description.
      /// </summary>
      public override string ToString()
      {
        return @"Digital noise.";
      }

      /// <summary>
      /// Set the default values of the shader.
      /// </summary>
      public override void ResetDefaultValues()
      {
        threshold = 0.1f;
        maxOffset = 0.1f;
        thresholdYUV = 0.5f;

        base.ResetDefaultValues();
      }

      /// <summary>
      /// Set the values to shader.
      /// </summary>
      protected override void SendValuesToShader()
      {
        material.SetFloat(variableThreshold, threshold);
        material.SetFloat(variableMaxOffset, maxOffset);
        material.SetFloat(variableThresholdYUV, thresholdYUV);
      }
    }
  }
}
