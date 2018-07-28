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
    /// Video Glitch Radial Aberration Editor.
    /// </summary>
    [CustomEditor(typeof(VideoGlitchRadialAberration))]
    public class VideoGlitchRadialAberrationEditor : VideoGlitchEditorBase
    {
      #region Private functions.
      /// <summary>
      /// Inspector.
      /// </summary>
      protected override void Inspector()
      {
        VideoGlitchRadialAberration thisTarget = (VideoGlitchRadialAberration)target;

        thisTarget.Focus = EditorHelper.Vector2(@"Focus", @"Point focus", thisTarget.Focus, Vector2.one * 0.5f);

        thisTarget.Samples = EditorHelper.IntSlider(@"Samples", @"Blur samples [1 - 30]. Default 15.", thisTarget.Samples, 1, 30, 15);

        thisTarget.Blur = EditorHelper.Slider(@"Blur", @"Blur [0.0 - 1.0]. Default 0.25.", thisTarget.Blur, 0.0f, 1.0f, 0.25f);

        thisTarget.Falloff = EditorHelper.Slider(@"Falloff", @"Falloff [0.0 - 5.0]. Default 3.0.", thisTarget.Falloff, 0.0f, 5.0f, 3.0f);
      }
      #endregion
    }
  }
}
