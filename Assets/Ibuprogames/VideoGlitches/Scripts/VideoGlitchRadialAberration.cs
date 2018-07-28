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
    /// Radial color aberration.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    [AddComponentMenu("Image Effects/Video Glitches/Video Glitch Radial Aberration")]
    public sealed class VideoGlitchRadialAberration : VideoGlitchBase
    {
      /// <summary>
      /// Screen focus [(0.0, 0.0) - (1.0, 1.0)]. Default (0.5, 0.5).
      /// </summary>
      [RangeVector2(0.0f, 1.0f, 0.5f)]
      public Vector2 Focus
      {
        get { return focus; }
        set { focus = value; }
      }

      /// <summary>
      /// Blur samples [1 - 32]. Default 15.
      /// </summary>
      [RangeInt(1, 32, 15)]
      public int Samples
      {
        get { return samples; }
        set { samples = value < 1 ? 1 : value; }
      }

      /// <summary>
      /// Blur [0.0 - 1.0]. Default 0.25.
      /// </summary>
      [RangeFloat(0.0f, 1.0f, 0.25f)]
      public float Blur
      {
        get { return blur; }
        set { blur = Mathf.Clamp01(value); }
      }

      /// <summary>
      /// Falloff [0.0 - 5.0]. Default 3.
      /// </summary>
      [RangeFloat(0.0f, 5.0f, 3.0f)]
      public float Falloff
      {
        get { return falloff; }
        set { falloff = Mathf.Clamp(value, 0.0f, 5.0f); }
      }

      [SerializeField]
      private Vector2 focus = new Vector2(0.5f, 0.5f);

      [SerializeField]
      private int samples = 15;

      [SerializeField]
      private float blur = 0.25f;

      [SerializeField]
      private float falloff = 3.0f;

      private const string variableFocus = @"_Focus";
      private const string variableSamples = @"_Samples";
      private const string variableBlur = @"_Blur";
      private const string variableFalloff = @"_Falloff";

      /// <summary>
      /// Effect description.
      /// </summary>
      public override string ToString()
      {
        return @"Radial color aberration.";
      }

      /// <summary>
      /// Set the default values of the shader.
      /// </summary>
      public override void ResetDefaultValues()
      {
        focus = new Vector2(0.5f, 0.5f);
        samples = 15;
        blur = 0.25f;
        falloff = 3.0f;

        base.ResetDefaultValues();
      }

      /// <summary>
      /// Set the values to shader.
      /// </summary>
      protected override void SendValuesToShader()
      {
        material.SetVector(variableFocus, focus);
        material.SetInt(variableSamples, samples);
        material.SetFloat(variableBlur, blur);
        material.SetFloat(variableFalloff, falloff);
      }
    }
  }
}
