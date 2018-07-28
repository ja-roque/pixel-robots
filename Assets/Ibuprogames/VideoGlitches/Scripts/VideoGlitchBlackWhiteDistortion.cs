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
    /// Black and white with analog distortion.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    [AddComponentMenu("Image Effects/Video Glitches/Video Glitch Black and White Distortion")]
    public sealed class VideoGlitchBlackWhiteDistortion : VideoGlitchBase
    {
      /// <summary>
      /// Distortion steps [1.0 - 10.0]. Default 2.0.
      /// </summary>
      [RangeFloat(1.0f, 10.0f, 2.0f)]
      public float Steps
      {
        get { return distortionSteps; }
        set { distortionSteps = Mathf.Clamp(value, 1.0f, 10.0f); }
      }

      /// <summary>
      /// Distortion min limit [0.0 - 360.0]. Default 340.
      /// </summary>
      [RangeFloat(0.0f, 360.0f, 340.0f)]
      public float MinLimit
      {
        get { return distortionAmountMinLimit; }
        set { distortionAmountMinLimit = Mathf.Clamp(value, 0.0f, 360.0f); }
      }

      /// <summary>
      /// Distortion max limit [0.0 - 360.0]. Default 360.
      /// </summary>
      [RangeFloat(0.0f, 360.0f, 360.0f)]
      public float MaxLimit
      {
        get { return distortionAmountMaxLimit; }
        set { distortionAmountMaxLimit = Mathf.Clamp(value, 0.0f, 360.0f); }
      }

      /// <summary>
      /// Distortion speed [0.0 - 10.0]. Default 1.
      /// </summary>
      [RangeFloat(0.0f, 10.0f, 1.0f)]
      public float Speed
      {
        get { return distortionSpeed; }
        set { distortionSpeed = Mathf.Clamp(value, 0.0f, 10.0f); }
      }

      [SerializeField]
      private float distortionSteps = 2.0f;

      [SerializeField]
      private float distortionAmountMinLimit = 340.0f;

      [SerializeField]
      private float distortionAmountMaxLimit = 360.0f;

      [SerializeField]
      private float distortionSpeed = 1.0f;

      [SerializeField]
      private float distortionAmount = 340.0f;

      private const string variableDistortionSteps = @"_DistortionSteps";
      private const string variableDistortionAmount = @"_DistortionAmount";

      /// <summary>
      /// Effect description.
      /// </summary>
      public override string ToString()
      {
        return @"Black and white with analog distortion.";
      }

      /// <summary>
      /// Set the default values of the shader.
      /// </summary>
      public override void ResetDefaultValues()
      {
        distortionSteps = 2.0f;
        distortionAmountMinLimit = 340.0f;
        distortionAmountMaxLimit = 360.0f;
        distortionSpeed = 1.0f;

        distortionAmount = distortionAmountMinLimit;

        base.ResetDefaultValues();
      }

      /// <summary>
      /// Set the values to shader.
      /// </summary>
      protected override void SendValuesToShader()
      {
        material.SetFloat(variableDistortionSteps, distortionSteps);

        if (distortionAmount > distortionAmountMaxLimit)
          distortionAmount = distortionAmountMinLimit;

        if (distortionAmount < distortionAmountMinLimit)
          distortionAmount = distortionAmountMaxLimit;

        if (distortionSpeed > 0.0f)
          distortionAmount += Time.deltaTime * distortionSpeed;

        material.SetFloat(variableDistortionAmount, distortionAmount);
      }
    }
  }
}
