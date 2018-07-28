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
    /// Video Glitch Corruption Digital Editor.
    /// </summary>
    [CustomEditor(typeof(VideoGlitchCorruptionDigital))]
    public class VideoGlitchCorruptionDigitalEditor : VideoGlitchEditorBase
    {
      /// <summary>
      /// Inspector.
      /// </summary>
      protected override void Inspector()
      {
        VideoGlitchCorruptionDigital thisTarget = (VideoGlitchCorruptionDigital)target;

        thisTarget.Speed = EditorHelper.Slider(@"Speed", @"Effect speed [0.0 - 10.0]. Default 1.0.", thisTarget.Speed, 0.0f, 10.0f, 1.0f);

        thisTarget.Intensity = EditorHelper.Slider(@"Intensity", @"Corruption strength [0.0 - 1.0]. Default 0.6.", thisTarget.Intensity, 0.0f, 1.0f, 0.6f);

        thisTarget.Posterize = EditorHelper.Slider(@"Posterize", @"Posterize intensity [0.0 - 1.0]. Default 0.25.", thisTarget.Posterize, 0.0f, 1.0f, 0.25f);

        thisTarget.TileSize = EditorHelper.IntSlider(@"Tile size", @"Block size [1 - 256]. Default 128.", thisTarget.TileSize, 1, 256, 128);
      }
    }
  }
}
