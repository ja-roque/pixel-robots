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
    /// Video Glitch Broken Screen Editor.
    /// </summary>
    [CustomEditor(typeof(VideoGlitchBrokenScreen))]
    public class VideoGlitchBrokenScreenEditor : VideoGlitchEditorBase
    {
      /// <summary>
      /// Inspector.
      /// </summary>
      protected override void Inspector()
      {
        VideoGlitchBrokenScreen thisTarget = (VideoGlitchBrokenScreen)target;

        thisTarget.Impact = EditorHelper.Vector2(@"Impact", @"Point of impact.", thisTarget.Impact, Vector2.zero);

        thisTarget.Distortion = EditorHelper.Slider("Distortion", @"Image distortion [0.0 - 1.0].", thisTarget.Distortion, 0.0f, 1.0f, 0.2f);

        EditorGUILayout.LabelField(@"Splits");

        EditorGUI.indentLevel++;

        thisTarget.Splits = EditorHelper.IntSlider("Count", @"Number of splits [1 - 100]. Default 25.", thisTarget.Splits, 2, 100, 25);

        thisTarget.Threshold = EditorHelper.Slider("Threshold", @"Split threshold [0.0 - 1.0]. Default 1.", thisTarget.Threshold, 0.0f, 1.0f, 1.0f);

        thisTarget.Color = EditorHelper.Color("Color", @"Split color. Default gray.", thisTarget.Color, Color.gray);

        EditorGUI.indentLevel--;
      }
    }
  }
}
