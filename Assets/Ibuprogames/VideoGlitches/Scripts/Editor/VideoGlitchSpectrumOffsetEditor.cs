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
    /// Video Glitch Spectrum Offset Editor.
    /// </summary>
    [CustomEditor(typeof(VideoGlitchSpectrumOffset))]
    public class VideoGlitchSpectrumOffsetEditor : VideoGlitchEditorBase
    {
      /// <summary>
      /// Inspector.
      /// </summary>
      protected override void Inspector()
      {
        VideoGlitchSpectrumOffset thisTarget = (VideoGlitchSpectrumOffset)target;

        thisTarget.Intensity = EditorHelper.Slider("Strength", @"Effect strength [0.0 - 1.0]. Default 0.1.", thisTarget.Intensity, 0.0f, 1.0f, 0.1f);

        thisTarget.Steps = EditorHelper.IntSlider("Steps", @"Effect steps [3 - 10]. Default 5.", thisTarget.Steps, 3, 10, 5);
      }
    }
  }
}
