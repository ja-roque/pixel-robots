///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Ibuprogames <hello@ibuprogames.com>. All rights reserved.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR
// IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using UnityEditor;

namespace Ibuprogames
{
  namespace VideoGlitchesAsset
  {
    /// <summary>
    /// Video Glitch Editor Base.
    /// </summary>
    [CustomEditor(typeof(VideoGlitchBase))]
    public abstract class VideoGlitchEditorBase : Editor
    {
      #region Private data.
      private VideoGlitchBase baseTarget;

      private bool displayColorControls = false;
      private bool displayAdvancedControls = false;

      private string displayColorControlsKey;
      private string displayAdvancedControlsKey;
      #endregion

      #region Private functions.
      private void OnEnable()
      {
        string productID = this.GetType().ToString().Replace(@"Editor", string.Empty);

        displayColorControlsKey = string.Format("{0}.displayColorControls", productID);
        displayAdvancedControlsKey = string.Format("{0}.displayAdvancedControls", productID);

        displayColorControls = EditorPrefs.GetInt(displayColorControlsKey, 0) == 1;
        displayAdvancedControls = EditorPrefs.GetInt(displayAdvancedControlsKey, 0) == 1;

        baseTarget = this.target as VideoGlitchBase;
      }
      
      /// <summary>
      /// Inspector.
      /// </summary>
      protected virtual void Inspector()
      {
        DrawDefaultInspector();
      }

      /// <summary>
      /// OnInspectorGUI.
      /// </summary>
      public override void OnInspectorGUI()
      {
        EditorHelper.Reset(0, 0.0f, 125.0f);

        Undo.RecordObject(baseTarget, baseTarget.GetType().Name);

        EditorHelper.BeginVertical();
        {
          /////////////////////////////////////////////////
          // Common.
          /////////////////////////////////////////////////

          EditorHelper.Separator();

          baseTarget.Strength = EditorHelper.Slider(@"Strength", "The strength of the effect.\nFrom 0.0 (no effect) to 1.0 (full effect).", baseTarget.Strength, 0.0f, 1.0f, 1.0f);

          baseTarget.EffectMode = (EffectModes)EditorHelper.EnumPopup(@"Mode", @"Screen, Layer or Depth mode. Default Screen.", baseTarget.EffectMode, EffectModes.Screen);

          if (baseTarget.EffectMode == EffectModes.Layer)
          {
            EditorHelper.IndentLevel++;

            baseTarget.Layer = EditorHelper.LayerMask(@"Layer mask", baseTarget.Layer, LayerMask.NameToLayer(@"Everything"));

            EditorHelper.IndentLevel--;
          }
          else if (baseTarget.EffectMode == EffectModes.Distance)
            baseTarget.DistanceCurve = EditorHelper.Curve(@"    Curve", baseTarget.DistanceCurve);

          EditorHelper.Separator();

          Inspector();

          /////////////////////////////////////////////////
          // Color controls.
          /////////////////////////////////////////////////

          EditorHelper.Separator();

          baseTarget.EnableColorControls = EditorHelper.Header(ref displayColorControls, baseTarget.EnableColorControls, @"Color");
          if (displayColorControls == true)
          {
            EditorHelper.Enabled = baseTarget.EnableColorControls;

            EditorGUI.indentLevel++;

            baseTarget.Brightness = EditorHelper.Slider(@"Brightness", "Brightness [-1.0, 1.0]. Default 0.", baseTarget.Brightness, -1.0f, 1.0f, 0.0f);

            baseTarget.Contrast = EditorHelper.Slider(@"Contrast", "Contrast [-1.0, 1.0]. Default 0.", baseTarget.Contrast, -1.0f, 1.0f, 0.0f);

            baseTarget.Gamma = EditorHelper.Slider(@"Gamma", "Gamma [0.1, 10.0]. Default 1.", baseTarget.Gamma, 0.01f, 10.0f, 1.0f);

            if (baseTarget.GetType() != typeof(VideoGlitchBlackWhiteDistortion))
            {
              baseTarget.Hue = EditorHelper.Slider(@"Hue", "The color wheel [0.0, 1.0]. Default 0.", baseTarget.Hue, 0.0f, 1.0f, 0.0f);

              baseTarget.Saturation = EditorHelper.Slider(@"Saturation", "Intensity of a colors [0.0, 2.0]. Default 1.", baseTarget.Saturation, 0.0f, 2.0f, 1.0f);
            }

            EditorHelper.IndentLevel--;

            EditorHelper.Enabled = true;
          }

          /////////////////////////////////////////////////
          // Advanced controls.
          /////////////////////////////////////////////////

          EditorHelper.Separator();

          displayAdvancedControls = EditorHelper.Foldout(displayAdvancedControls, @"Advanced settings");
          if (displayAdvancedControls == true)
          {
            EditorHelper.IndentLevel++;

            baseTarget.DepthThreshold = EditorHelper.Slider(@"Depth threshold", "Accuracy of depth texture.", baseTarget.DepthThreshold, 0.0f, 0.01f, 0.004f);

            baseTarget.UseScaledTime =  EditorGUILayout.Toggle(@"Use scaled time", baseTarget.UseScaledTime);

            baseTarget.CustomTimeScale = EditorHelper.Slider(@"Time scale", "Custom time scale factor.", baseTarget.CustomTimeScale, 0.0f, 10.0f, 1.0f);

            EditorHelper.IndentLevel--;
          }

          /////////////////////////////////////////////////
          // Description.
          /////////////////////////////////////////////////

          EditorHelper.Separator();

          EditorGUILayout.HelpBox(baseTarget.ToString(), MessageType.Info);

          /////////////////////////////////////////////////
          // Misc.
          /////////////////////////////////////////////////

          EditorHelper.Separator();

          EditorHelper.BeginHorizontal();
          {
            if (GUILayout.Button(new GUIContent(@"[doc]", @"Online documentation"), GUI.skin.label) == true)
              Application.OpenURL(@"http://www.ibuprogames.com/2015/07/03/video-glitches/");

            EditorHelper.FlexibleSpace();

            if (EditorHelper.Button(@"Reset") == true)
              baseTarget.ResetDefaultValues();
          }
          EditorHelper.EndHorizontal();
        }
        EditorHelper.EndVertical();

        EditorHelper.Separator();

        if (EditorHelper.Changed == true)
        {
          EditorPrefs.SetInt(displayColorControlsKey, displayColorControls == true ? 1 : 0);
          EditorPrefs.SetInt(displayAdvancedControlsKey, displayAdvancedControls == true ? 1 : 0);

          EditorHelper.SetDirty(target);
        }
      }
      #endregion
    }
  }
}
