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
    /// Video Glitch VHS Pause Editor.
    /// </summary>
    [CustomEditor(typeof(VideoGlitchVHSPause))]
    public class VideoGlitchVHSPauseEditor : VideoGlitchEditorBase
    {
      /// <summary>
      /// Inspector.
      /// </summary>
      protected override void Inspector()
      {
        VideoGlitchVHSPause thisTarget = (VideoGlitchVHSPause)target;

        thisTarget.Intensity = EditorHelper.Slider(@"Strength", @"Effect strength [0.0 - 1.0]. Default 0.5.", thisTarget.Intensity, 0.0f, 1.0f, 0.5f);

        thisTarget.Noise = EditorHelper.Slider(@"Noise", @"Color noise [0.0 - 1.0]. Default 0.1.", thisTarget.Noise, 0.0f, 1.0f, 0.1f);

        thisTarget.Color = EditorHelper.Color(@"Noise color", @"Noise color. Default white", thisTarget.Color, Color.white);
      }
    }
  }
}
