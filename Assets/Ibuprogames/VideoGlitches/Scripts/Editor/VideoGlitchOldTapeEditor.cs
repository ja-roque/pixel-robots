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
    /// Video Glitch Old tape Editor.
    /// </summary>
    [CustomEditor(typeof(VideoGlitchOldTape))]
    public class VideoGlitchOldTapeEditor : VideoGlitchEditorBase
    {
      /// <summary>
      /// Inspector.
      /// </summary>
      protected override void Inspector()
      {
        VideoGlitchOldTape thisTarget = (VideoGlitchOldTape)target;

        thisTarget.Speed = EditorHelper.Slider("Speed", @"Noise speed [0.0 - 1.0]. Default 0.25.", thisTarget.Speed, 0.0f, 1.0f, 0.25f);

        thisTarget.Amplitude = EditorHelper.Slider("Amplitude", @"Noise amplitude [1.0 - 100.0]. Default 1.", thisTarget.Amplitude, 1.0f, 100.0f, 1.0f);
      }
    }
  }
}
