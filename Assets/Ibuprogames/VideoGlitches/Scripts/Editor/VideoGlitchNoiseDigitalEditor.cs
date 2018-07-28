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
    /// Video Glitch Digital Editor.
    /// </summary>
    [CustomEditor(typeof(VideoGlitchNoiseDigital))]
    public class VideoGlitchDigitalEditor : VideoGlitchEditorBase
    {
      /// <summary>
      /// Inspector.
      /// </summary>
      protected override void Inspector()
      {
        VideoGlitchNoiseDigital thisTarget = (VideoGlitchNoiseDigital)target;

        thisTarget.Threshold = EditorHelper.Slider("Threshold", @"Strength of the effect [0.0 - 1.0]. Default 0.1.", thisTarget.Threshold, 0.0f, 1.0f, 0.1f);

        thisTarget.MaxOffset = EditorHelper.Slider("Max offset", @"Max displacement [0.0 - 1.0]. Default 0.1.", thisTarget.MaxOffset, 0.0f, 1.0f, 0.1f);

        thisTarget.ThresholdYUV = EditorHelper.Slider("Threshold YUV", @"Color change [0.0 - 1.0]. Default 0.5.", thisTarget.ThresholdYUV, 0.0f, 1.0f, 0.5f);
      }
    }
  }
}
