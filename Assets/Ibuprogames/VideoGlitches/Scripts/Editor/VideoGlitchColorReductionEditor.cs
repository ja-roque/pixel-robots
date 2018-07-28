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
    /// Video Glitch Color Reduction Editor.
    /// </summary>
    [CustomEditor(typeof(VideoGlitchColorReduction))]
    public class VideoGlitchColorReductionEditor : VideoGlitchEditorBase
    {
      #region Private functions.    
      /// <summary>
      /// Inspector.
      /// </summary>
      protected override void Inspector()
      {
        VideoGlitchColorReduction thisTarget = (VideoGlitchColorReduction)target;

        thisTarget.Dithering = (VideoGlitchColorReduction.Ditherings)EditorHelper.EnumPopup(@"Dithering", @"Dithering method.", thisTarget.Dithering, VideoGlitchColorReduction.Ditherings.Bayer4x4);

        thisTarget.PaletteSize = EditorHelper.IntSlider(@"Palette size", @"Palette color size [0 - 256]. Default 8.", thisTarget.PaletteSize, 0, 256, 8);

        thisTarget.Pixelation = EditorHelper.IntSlider(@"Pixelation", @"Pixel size [1 - 10]. Default 2.", thisTarget.Pixelation, 1, 10, 2);
      }
      #endregion
    }
  }
}
