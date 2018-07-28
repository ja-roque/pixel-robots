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
    /// Video Glitch Noise Editor.
    /// </summary>
    [CustomEditor(typeof(VideoGlitchNoiseAnalog))]
    public class VideoGlitchNoiseEditor : VideoGlitchEditorBase
    {
      /// <summary>
      /// Inspector.
      /// </summary>
      protected override void Inspector()
      {
        VideoGlitchNoiseAnalog thisTarget = (VideoGlitchNoiseAnalog)target;

        thisTarget.StripesSize = EditorHelper.Slider(@"Stripes size", @"Stripes size [0.0 - 1.0]. Default 0.25.", thisTarget.StripesSize, 0.0f, 1.0f, 0.25f);

        thisTarget.BarsCount = EditorHelper.IntSlider("Bars count", @"Noise bars count [0 - 100]. Default 10.", thisTarget.BarsCount, 0, 100, 10);

        thisTarget.Distortion = EditorHelper.Slider("Distortion", @"Distortion intensity [0.0 - 1.0]. Default 0.25.", thisTarget.Distortion, 0.0f, 1.0f, 0.25f);

        thisTarget.Noise = EditorHelper.Slider("Noise", @"Noise intensity [0.0 - 1.0]. Default 0.2.", thisTarget.Noise, 0.0f, 1.0f, 0.2f);
      }
    }
  }
}
