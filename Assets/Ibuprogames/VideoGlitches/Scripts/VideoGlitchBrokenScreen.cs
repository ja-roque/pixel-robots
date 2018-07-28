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
    /// Broken screen (simulated).
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    [AddComponentMenu("Image Effects/Video Glitches/Video Glitch Broken Screen")]
    public sealed class VideoGlitchBrokenScreen : VideoGlitchBase
    {
      /// <summary>
      /// Impact point [(-1.0, -1.0) - (1.0, 1.0)]. Default [0, 0].
      /// </summary>
      [RangeVector2(-1.0f, 1.0f, 0.0f)]
      public Vector2 Impact
      {
        get { return impact; }
        set { impact = Vector2.ClampMagnitude(value, 1.0f); }
      }

      /// <summary>
      /// Number of splits [2 - 100]. Default 25.
      /// </summary>
      [RangeInt(2, 100, 25)]
      public int Splits
      {
        get { return splits; }
        set { splits = value < 2 ? 2 : value; }
      }

      /// <summary>
      /// Split threshold [0.0 - 1.0]. Default 1.0.
      /// </summary>
      [RangeFloat(0.0f, 1.0f, 0.75f)]
      public float Threshold
      {
        get { return splitThreshold; }
        set { splitThreshold = Mathf.Clamp01(value); }
      }

      /// <summary>
      /// Split color. Default gray.
      /// </summary>
      public Color Color
      {
        get { return splitColor; }
        set { splitColor = value; }
      }

      /// <summary>
      /// Image distortion [0.0 - 1.0]. Default 0.2.
      /// </summary>
      [RangeFloat(0.0f, 1.0f, 0.2f)]
      public float Distortion
      {
        get { return distortion; }
        set { distortion = Mathf.Clamp01(value); }
      }

      [SerializeField]
      private Vector2 impact = Vector2.zero;

      [SerializeField]
      private int splits = 25;

      [SerializeField]
      private float splitThreshold = 1.0f;

      [SerializeField]
      private Color splitColor = Color.gray;

      [SerializeField]
      private float distortion = 0.2f;

      private const string variableCenter = @"_Center";
      private const string variableSplits = @"_Splits";
      private const string variableSplitThreshold = @"_SplitThreshold";
      private const string variableSplitColor = @"_SplitColor";
      private const string variableDistortion = @"_Distortion";

      /// <summary>
      /// Effect description.
      /// </summary>
      public override string ToString()
      {
        return @"Broken screen (simulated).";
      }

      /// <summary>
      /// Set the default values of the shader.
      /// </summary>
      public override void ResetDefaultValues()
      {
        impact = Vector2.zero;
        splits = 25;
        splitThreshold = 1.0f;
        splitColor = Color.gray;
        distortion = 0.2f;

        base.ResetDefaultValues();
      }

      /// <summary>
      /// Set the values to shader.
      /// </summary>
      protected override void SendValuesToShader()
      {
        material.SetVector(variableCenter, impact);
        material.SetInt(variableSplits, splits);
        material.SetFloat(variableSplitThreshold, splitThreshold);
        material.SetColor(variableSplitColor, splitColor);
        material.SetFloat(variableDistortion, distortion * 0.1f);
      }
    }
  }
}
