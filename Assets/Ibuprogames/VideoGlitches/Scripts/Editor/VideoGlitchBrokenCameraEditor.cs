///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Ibuprogames <hello@ibuprogames.com>. All rights reserved.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR
// IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using UnityEditor;

namespace Ibuprogames
{
  namespace VideoGlitchesAsset
  {
    /// <summary>
    /// Video Glitch Broken Camera inspector.
    /// </summary>
    [CustomEditor(typeof(VideoGlitchBrokenCamera))]
    public class VideoGlitchBrokenCameraEditor : VideoGlitchEditorBase
    {
      #region Private functions.    
      /// <summary>
      /// Inspector.
      /// </summary>
      protected override void Inspector()
      {
        VideoGlitchBrokenCamera thisTarget = (VideoGlitchBrokenCamera)target;

        thisTarget.Malfunction = EditorHelper.Slider("Malfunction", @"Malfunction probability [0.0 - 1.0]. Default 0.9.", thisTarget.Malfunction, 0.0f, 1.0f, 0.9f);

        thisTarget.Noise = EditorHelper.Slider("Noise", @"Noise intensity [0.0 - 1.0]. Default 0.5.", thisTarget.Noise, 0.0f, 1.0f, 0.5f);

        EditorGUILayout.LabelField(@"Distortion");

        EditorGUI.indentLevel++;

        thisTarget.DistortionIntensity = EditorHelper.Slider("Intensity", @"Distortion intensity [0.0 - 1.0]. Default 0.3.", thisTarget.DistortionIntensity, 0.0f, 1.0f, 0.3f);

        thisTarget.DistortionSpeed = EditorHelper.Slider("Speed", @"Distortion speed [0.0 - 1.0]. Default 0.1.", thisTarget.DistortionSpeed, 0.0f, 1.0f, 0.1f);

        EditorGUI.indentLevel--;
      }
      #endregion
    }
  }
}
