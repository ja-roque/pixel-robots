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
    /// Digital signal corruption.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    [AddComponentMenu("Image Effects/Video Glitches/Video Glitch Corruption Digital")]
    public sealed class VideoGlitchCorruptionDigital : VideoGlitchBase
    {
      /// <summary>
      /// Speed [0.0 - 10.0]. Default 1.0.
      /// </summary>
      [RangeFloat(0.0f, 10.0f, 1.0f)]
      public float Speed
      {
        get { return speed; }
        set { speed = value < 0.0f ? 0.0f : value; }
      }

      /// <summary>
      /// Effect strength [0.0 - 1.0]. Default 0.6.
      /// </summary>
      [RangeFloat(0.0f, 1.0f, 0.6f)]
      public float Intensity
      {
        get { return intensity; }
        set { intensity = Mathf.Clamp01(value); }
      }

      /// <summary>
      /// Posterize intensity [0.0 - 1.0]. Default 0.25.
      /// </summary>
      [RangeFloat(0.0f, 1.0f, 0.5f)]
      public float Posterize
      {
        get { return posterize; }
        set { posterize = Mathf.Clamp01(value); }
      }

      /// <summary>
      /// Tile size [1 - 256]. Default 128.
      /// </summary>
      [RangeInt(1, 256, 64)]
      public int TileSize
      {
        get { return tileSize; }
        set { tileSize = value < 1 ? 1 : value; }
      }

      [SerializeField]
      private float speed = 1.0f;

      [SerializeField]
      private float intensity = 0.6f;

      [SerializeField]
      private float posterize = 0.25f;

      [SerializeField]
      private int tileSize = 128;

      private const string variableSpeed = @"_Speed";
      private const string variableIntensity = @"_Intensity";
      private const string variableTileSize = @"_TileSize";
      private const string variablePosterize = @"_Posterize";

      /// <summary>
      /// Effect description.
      /// </summary>
      public override string ToString()
      {
        return @"Digital signal corruption.";
      }

      /// <summary>
      /// Set the default values of the shader.
      /// </summary>
      public override void ResetDefaultValues()
      {
        speed = 1.0f;
        intensity = 0.6f;
        tileSize = 128;
        posterize = 0.25f;

        base.ResetDefaultValues();
      }

      /// <summary>
      /// Set the values to shader.
      /// </summary>
      protected override void SendValuesToShader()
      {
        material.SetFloat(variableSpeed, speed);
        material.SetFloat(variableIntensity, intensity);
        material.SetFloat(variableTileSize, tileSize);
        material.SetFloat(variablePosterize, 1.0f - posterize);
      }
    }
  }
}
