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
    /// Color channels shift.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    [AddComponentMenu("Image Effects/Video Glitches/Video Glitch Shift")]
    public sealed class VideoGlitchShift : VideoGlitchBase
    {
      /// <summary>
      /// Shift amplitude [0.0 - 1.0]. Default 0.5.
      /// </summary>
      [RangeFloat(0.0f, 1.0f, 0.5f)]
      public float Amplitude
      {
        get { return amplitude; }
        set { amplitude = value < 0.0f ? 0.0f : value; }
      }

      /// <summary>
      /// Shift speed [0.0 - 1.0]. Default 0.25.
      /// </summary>
      [RangeFloat(0.0f, 1.0f, 0.25f)]
      public float Speed
      {
        get { return speed; }
        set { speed = value < 0.0f ? 0.0f : value; }
      }

      [SerializeField]
      private float amplitude = 0.5f;

      [SerializeField]
      private float speed = 0.25f;

      private Texture noiseTex;

      private const string variableNoise = @"_NoiseTex";
      private const string variableAmplitude = @"_Amplitude";
      private const string variableSpeed = @"_Speed";

      /// <summary>
      /// Effect description.
      /// </summary>
      public override string ToString()
      {
        return @"Color channels shift.";
      }

      /// <summary>
      /// Load custom resources.
      /// </summary>
      protected override void LoadCustomResources()
      {
        noiseTex = LoadTextureFromResources(@"Textures/Noise256");
      }

      /// <summary>
      /// Set the default values of the shader.
      /// </summary>
      public override void ResetDefaultValues()
      {
        amplitude = 0.5f;
        speed = 0.25f;

        base.ResetDefaultValues();
      }

      /// <summary>
      /// Set the values to shader.
      /// </summary>
      protected override void SendValuesToShader()
      {
        material.SetFloat(variableAmplitude, amplitude);
        material.SetFloat(variableSpeed, speed * 0.1f);
        material.SetTexture(variableNoise, noiseTex);
      }
    }
  }
}
