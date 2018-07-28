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
    /// Video Glitch Shift Editor.
    /// </summary>
    [CustomEditor(typeof(VideoGlitchShift))]
    public class VideoGlitchShiftEditor : VideoGlitchEditorBase
    {
      /// <summary>
      /// Inspector.
      /// </summary>
      protected override void Inspector()
      {
        VideoGlitchShift thisTarget = (VideoGlitchShift)target;

        thisTarget.Amplitude = EditorHelper.Slider("Amplitude", @"Offset amount [0.0 - 1.0]. Default 0.5.", thisTarget.Amplitude, 0.0f, 1.0f, 0.5f);

        thisTarget.Speed = EditorHelper.Slider(@"Speed", @"Speed of change [0.0 - 1.0]. Default 0.25.", thisTarget.Speed, 0.0f, 1.0f, 0.25f);
      }
    }
  }
}
