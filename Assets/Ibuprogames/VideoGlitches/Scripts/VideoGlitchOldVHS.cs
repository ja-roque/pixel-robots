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
    /// Old broken VHS recorder.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    [AddComponentMenu("Image Effects/Video Glitches/Video Glitch Old VHS")]
    public sealed class VideoGlitchOldVHS : VideoGlitchBase
    {
      /// <summary>
      /// Wave distortion [0.0 - 1.0]. Default 0.5.
      /// </summary>
      [RangeFloat(0.0f, 1.0f, 0.5f)]
      public float Waving
      {
        get { return waving; }
        set { waving = Mathf.Clamp01(value); }
      }

      /// <summary>
      /// Noise distortion [0.0 - 1.0]. Default 0.25.
      /// </summary>
      [RangeFloat(0.0f, 1.0f, 0.25f)]
      public float Noise
      {
        get { return noise; }
        set { noise = Mathf.Clamp01(value); }
      }

      /// <summary>
      /// Stripes count [0 - 32]. Default 8.
      /// </summary>
      [RangeFloat(0.0f, 32.0f, 8.0f)]
      public float StripeCount
      {
        get { return stripeCount; }
        set { stripeCount = value < 0 ? 0 : value; }
      }

      /// <summary>
      /// Stripes velocity [-10.0 - 10.0]. Default 1.2.
      /// </summary>
      [RangeFloat(-10.0f, 10.0f, 1.2f)]
      public float StripeVelocity
      {
        get { return stripeVelocity; }
        set { stripeVelocity = Mathf.Clamp(value, -10.0f, 10.0f); }
      }

      /// <summary>
      /// Stripes strength [0.0 - 1.0]. Default 1.
      /// </summary>
      [RangeFloat(0.0f, 1.0f, 1.0f)]
      public float StripeStrength
      {
        get { return stripeStrength; }
        set { stripeStrength = Mathf.Clamp01(value); }
      }

      /// <summary>
      /// Stripes noise [0.0 - 1.0]. Default 0.5.
      /// </summary>
      [RangeFloat(0.0f, 1.0f, 0.5f)]
      public float StripeNoise
      {
        get { return stripeNoise; }
        set { stripeNoise = Mathf.Clamp01(value); }
      }

      /// <summary>
      /// Head switching noise [0.0 - 1.0]. Default 0.5.
      /// </summary>
      [RangeFloat(0.0f, 1.0f, 0.5f)]
      public float SwitchingNoise
      {
        get { return switchingNoise; }
        set { switchingNoise = Mathf.Clamp01(value); }
      }

      /// <summary>
      /// Ground loop interference width [0.0 - 1.0]. Default 0.6.
      /// </summary>
      [RangeFloat(0.0f, 1.0f, 0.6f)]
      public float ACBeatWidth
      {
        get { return acBeatWidth; }
        set { acBeatWidth = Mathf.Clamp01(value); }
      }

      /// <summary>
      /// Ground loop interference velocity [-10.0 - 10.0]. Default 0.2.
      /// </summary>
      [RangeFloat(-10.0f, 10.0f, 0.2f)]
      public float ACBeatVelocity
      {
        get { return acBeatVelocity; }
        set { acBeatVelocity = Mathf.Clamp(value, -10.0f, 10.0f); }
      }

      /// <summary>
      /// Bloom passes [0 - 10]. Default 5.
      /// </summary>
      [RangeFloat(0.0f, 10.0f, 5.0f)]
      public float BloomPasses
      {
        get { return bloomPasses; }
        set { bloomPasses = Mathf.Clamp(value, 0.0f, 10.0f); }
      }

      [SerializeField]
      private float waving = 0.5f;

      [SerializeField]
      private float noise = 0.25f;

      [SerializeField]
      private float stripeCount = 8.0f;

      [SerializeField]
      private float stripeVelocity = 1.2f;

      [SerializeField]
      private float stripeStrength = 1.0f;

      [SerializeField]
      private float stripeNoise = 0.5f;

      [SerializeField]
      private float switchingNoise = 0.5f;

      [SerializeField]
      private float acBeatWidth = 0.6f;

      [SerializeField]
      private float acBeatVelocity = 0.2f;

      [SerializeField]
      private float bloomPasses = 5.0f;

      private const string variableWaving = @"_Waving";
      private const string variableNoise = @"_Noise";
      private const string variableStripeCount = @"_StripeCount";
      private const string variableStripeVelocity = @"_StripeVelocity";
      private const string variableStripeStrength = @"_StripeStrength";
      private const string variableStripeNoise = @"_StripeNoise";
      private const string variableSwitchingNoise = @"_SwitchingNoise";
      private const string variableACBeatWidth = @"_ACBeatWidth";
      private const string variableACBeatVelocity = @"_ACBeatVelocity";
      private const string variableBloomPasses = @"_BloomPasses";

      /// <summary>
      /// Effect description.
      /// </summary>
      public override string ToString()
      {
        return @"Old broken VHS recorder.";
      }

      /// <summary>
      /// Set the default values of the shader.
      /// </summary>
      public override void ResetDefaultValues()
      {
        waving = 0.5f;
        noise = 0.25f;
        stripeCount = 2.0f;
        stripeVelocity = 1.2f;
        stripeStrength = 1.0f;
        stripeNoise = 0.5f;
        switchingNoise = 0.5f;
        acBeatWidth = 0.6f;
        acBeatVelocity = 0.2f;
        bloomPasses = 5.0f;

        base.ResetDefaultValues();
      }

      /// <summary>
      /// Set the values to shader.
      /// </summary>
      protected override void SendValuesToShader()
      {
        material.SetFloat(variableWaving, waving);
        material.SetFloat(variableNoise, noise);
        material.SetFloat(variableStripeCount, Mathf.Floor(stripeCount) * 6.0f);
        material.SetFloat(variableStripeVelocity, stripeVelocity);
        material.SetFloat(variableStripeStrength, stripeStrength * 10.0f);
        material.SetFloat(variableStripeNoise, stripeNoise * 500.0f);
        material.SetFloat(variableSwitchingNoise, switchingNoise);
        material.SetFloat(variableACBeatWidth, acBeatWidth);
        material.SetFloat(variableACBeatVelocity, acBeatVelocity);
        material.SetFloat(variableBloomPasses, bloomPasses);
      }
    }
  }
}
