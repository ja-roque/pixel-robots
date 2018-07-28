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
    /// Broken camera.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    [AddComponentMenu("Image Effects/Video Glitches/Video Glitch Broken Camera")]
    public sealed class VideoGlitchBrokenCamera : VideoGlitchBase
    {
      /// <summary>
      /// Malfunction probability [0.0 - 1.0]. Default 0.9.
      /// </summary>
      [RangeFloat(0.0f, 1.0f, 0.9f)]
      public float Malfunction
      {
        get { return malfunction; }
        set { malfunction = Mathf.Clamp01(value); }
      }

      /// <summary>
      /// Noise intensity [0.0 - 1.0]. Default 0.5.
      /// </summary>
      [RangeFloat(0.0f, 1.0f, 0.5f)]
      public float Noise
      {
        get { return noise; }
        set { noise = value < 0.0f ? 0.0f : value; }
      }

      /// <summary>
      /// Distortion intensity [0.0 - 1.0]. Default 0.3.
      /// </summary>
      [RangeFloat(0.0f, 1.0f, 0.3f)]
      public float DistortionIntensity
      {
        get { return distortionIntensity; }
        set { distortionIntensity = value < 0.0f ? 0.0f : value; }
      }

      /// <summary>
      /// Distortion speed [0.0 - 1.0]. Default 0.1.
      /// </summary>
      [RangeFloat(0.0f, 1.0f, 0.1f)]
      public float DistortionSpeed
      {
        get { return distortionSpeed; }
        set { distortionSpeed = value < 0.0f ? 0.0f : value; }
      }

      [SerializeField]
      private float malfunction = 0.9f;

      [SerializeField]
      private float noise = 0.5f;

      [SerializeField]
      private float distortionIntensity = 0.3f;

      [SerializeField]
      private float distortionSpeed = 0.1f;

      private const string variableMalfunction = @"_Malfunction";
      private const string variableNoise = @"_Noise";
      private const string variableDistortionIntensity = @"_DistortionIntensity";
      private const string variableDistortionSpeed = @"_DistortionSpeed";

      /// <summary>
      /// Effect description.
      /// </summary>
      public override string ToString()
      {
        return @"Broken camera.";
      }

      /// <summary>
      /// Set the default values of the shader.
      /// </summary>
      public override void ResetDefaultValues()
      {
        malfunction = 0.9f;
        noise = 0.5f;
        distortionIntensity = 0.3f;
        distortionSpeed = 0.1f;

        base.ResetDefaultValues();
      }

      /// <summary>
      /// Set the values to shader.
      /// </summary>
      protected override void SendValuesToShader()
      {
        material.SetFloat(variableMalfunction, (malfunction * 2.0f) - 1.0f);
        material.SetFloat(variableNoise, noise * 0.025f);
        material.SetFloat(variableDistortionIntensity, distortionIntensity * 0.1f);
        material.SetFloat(variableDistortionSpeed, distortionSpeed * 0.1f);
      }
    }
  }
}
