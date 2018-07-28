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
    /// Video Glitch Black and White Distortion Editor.
    /// </summary>
    [CustomEditor(typeof(VideoGlitchBlackWhiteDistortion))]
    public class VideoGlitchBlackWhiteDistortionEditor : VideoGlitchEditorBase
    {
      #region Private functions.    
      /// <summary>
      /// Inspector.
      /// </summary>
      protected override void Inspector()
      {
        VideoGlitchBlackWhiteDistortion thisTarget = (VideoGlitchBlackWhiteDistortion)target;

        thisTarget.Steps = EditorHelper.IntSlider("Steps", @"Distortion steps [1 - 10]. Default 2.", (int)thisTarget.Steps, 1, 10, 2);

        float minLimit = thisTarget.MinLimit;
        float maxLimit = thisTarget.MaxLimit;
        EditorHelper.MinMaxSlider("Range", @"Distortion range [0.0 - 360.0].", ref minLimit, ref maxLimit, 0.0f, 360.0f, 340.0f, 360.0f);

        thisTarget.MinLimit = minLimit;
        thisTarget.MaxLimit = maxLimit;

        thisTarget.Speed = EditorHelper.Slider("Speed", @"Distortion speed [0.0 - 10.0]. Default 1.", thisTarget.Speed, 0.0f, 10.0f, 1.0f);
      }
      #endregion
    }
  }
}
