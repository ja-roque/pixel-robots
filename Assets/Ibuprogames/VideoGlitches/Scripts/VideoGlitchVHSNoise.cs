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
    /// VHS Noise.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    [AddComponentMenu("Image Effects/Video Glitches/Video Glitch VHS Noise")]
    public sealed class VideoGlitchVHSNoise : VideoGlitchBase
    {
      /// <summary>
      /// Speed [-10.0 - 10.0]. Default 2.
      /// </summary>
      [RangeFloat(-10.0f, 10.0f, 2.0f)]
      public float Speed
      {
        get { return speed; }
        set { speed = value; }
      }

      /// <summary>
      /// Noise intensity [0.0 - 1.0]. Default 0.3.
      /// </summary>
      [RangeFloat(0.0f, 1.0f, 0.3f)]
      public float Intensity
      {
        get { return intensity; }
        set { intensity = value < 0.0f ? 0.0f : value; }
      }

      /// <summary>
      /// Noise size [0.0 - 1.0]. Default 0.25.
      /// </summary>
      [RangeFloat(0.0f, 1.0f, 0.25f)]
      public float Size
      {
        get { return size; }
        set { size = value < 0.0f ? 0.0f : value; }
      }

      /// <summary>
      /// Noise color. Default white.
      /// </summary>
      public Color Color
      {
        get { return color; }
        set { color = value; }
      }

      [SerializeField]
      private float speed = 2.0f;

      [SerializeField]
      private float intensity = 0.3f;

      [SerializeField]
      private float size = 0.25f;

      [SerializeField]
      private Color color = Color.white;

      private const string variableSpeed = @"_Speed";
      private const string variableIntensity = @"_Intensity";
      private const string variableSize = @"_Size";
      private const string variableColor = @"_Color";

      /// <summary>
      /// Effect description.
      /// </summary>
      public override string ToString()
      {
        return @"VHS noise.";
      }

      /// <summary>
      /// Set the default values of the shader.
      /// </summary>
      public override void ResetDefaultValues()
      {
        speed = 2.0f;
        intensity = 0.3f;
        size = 0.25f;
        color = Color.white;

        base.ResetDefaultValues();
      }

      /// <summary>
      /// Set the values to shader.
      /// </summary>
      protected override void SendValuesToShader()
      {
        material.SetFloat(variableSpeed, speed);
        material.SetFloat(variableIntensity, intensity);
        material.SetFloat(variableSize, size * 1000.0f);
        material.SetColor(variableColor, color);
      }
    }
  }
}
