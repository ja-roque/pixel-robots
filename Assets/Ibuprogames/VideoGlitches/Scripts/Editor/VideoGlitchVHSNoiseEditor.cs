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
    /// Video Glitch VHS Noise Editor.
    /// </summary>
    [CustomEditor(typeof(VideoGlitchVHSNoise))]
    public class VideoGlitchVHSNoiseEditor : VideoGlitchEditorBase
    {
      #region Private functions.    
      /// <summary>
      /// Inspector.
      /// </summary>
      protected override void Inspector()
      {
        VideoGlitchVHSNoise thisTarget = (VideoGlitchVHSNoise)target;

        thisTarget.Speed = EditorHelper.Slider(@"Speed", @"Effect speed [-10.0 - 10.0]. Default 2.", thisTarget.Speed, -10.0f, 10.0f, 2.0f);

        thisTarget.Intensity = EditorHelper.Slider(@"Intensity", @"Noise intensity [0.0 - 1.0]. Default 0.3.", thisTarget.Intensity, 0.0f, 1.0f, 0.3f);

        thisTarget.Size = EditorHelper.Slider(@"Size", @"Noise size [0.0 - 1.0]. Default 0.25.", thisTarget.Size, 0.0f, 1.0f, 0.25f);

        thisTarget.Color = EditorHelper.Color(@"Color", @"Noise color. Default white", thisTarget.Color, Color.white);
      }
      #endregion
    }
  }
}
