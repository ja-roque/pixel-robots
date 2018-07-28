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
    /// Video Glitch Old TV Editor.
    /// </summary>
    [CustomEditor(typeof(VideoGlitchOldTV))]
    public class VideoGlitchOldTVEditor : VideoGlitchEditorBase
    {
      /// <summary>
      /// Inspector.
      /// </summary>
      protected override void Inspector()
      {
        VideoGlitchOldTV thisTarget = (VideoGlitchOldTV)target;

        EditorGUILayout.LabelField(@"Slow scan");

        EditorGUI.indentLevel++;

        thisTarget.SlowScan = EditorHelper.Slider(@"Slow scan", thisTarget.SlowScan, 0.0f, 1.0f, 0.01f);
        thisTarget.ScanLine = EditorHelper.Slider(@"Scanlines", thisTarget.ScanLine, 0.0f, 2.0f, 0.5f);

        EditorGUI.indentLevel--;

        EditorGUILayout.LabelField(@"Vignette");

        EditorGUI.indentLevel++;

        thisTarget.VignetteSoftness = EditorHelper.Slider(@"Softness", thisTarget.VignetteSoftness, 0.0f, 1.0f, 0.9f);
        thisTarget.VignetteScale = EditorHelper.Slider(@"Scale", thisTarget.VignetteScale, 0.0f, 1.0f, 0.14f);
        thisTarget.VignetteTubeScale = EditorHelper.Slider(@"Tube scale", thisTarget.VignetteTubeScale, 0.01f, 10.0f, 0.7f);

        EditorGUI.indentLevel--;

        EditorGUILayout.LabelField(@"Grain");

        EditorGUI.indentLevel++;

        thisTarget.GrainSaturation = EditorHelper.Slider(@"Saturation", thisTarget.GrainSaturation, 0.0f, 1.0f, 0.0f);
        thisTarget.GrainOpacity = EditorHelper.Slider(@"Strength", thisTarget.GrainOpacity, 0.0f, 100.0f, 5.0f);

        EditorGUI.indentLevel--;

        EditorGUILayout.LabelField(@"Distortion");

        EditorGUI.indentLevel++;

        thisTarget.ScanDistort = EditorHelper.Slider(@"Scanline", thisTarget.ScanDistort, 0.0f, 5.0f, 0.03f);
        thisTarget.Timer = EditorHelper.Slider(@"Frequency", thisTarget.Timer, 0.0f, 5.0f, 0.85f);
        thisTarget.Speed = EditorHelper.Slider(@"Speed", thisTarget.Speed, 0.0f, 5.0f, 0.85f);

        EditorGUI.indentLevel--;

        EditorGUILayout.LabelField(@"VHS Stripes");

        EditorGUI.indentLevel++;

        thisTarget.StripesCount = EditorHelper.Slider(@"Stripes", thisTarget.StripesCount, 0.0f, 1000.0f, 0.5f);
        thisTarget.StripesOpacity = EditorHelper.Slider(@"Opacity", "", thisTarget.StripesOpacity, 0.0f, 10.0f, 1.0f);
        thisTarget.BarsCount = EditorHelper.Slider(@"Bars", thisTarget.BarsCount, 0.0f, 1000.0f, 5.0f);

        EditorGUI.indentLevel--;

        EditorGUILayout.LabelField(@"TV Tube Dots");

        EditorGUI.indentLevel++;

        thisTarget.TVDots = EditorHelper.IntSlider(@"Style", (int)thisTarget.TVDots, 0, 4, 1);
        thisTarget.TVDotsBlend = EditorHelper.Slider(@"Stripes", thisTarget.TVDotsBlend, 0.0f, 1000.0f, 1.0f);

        EditorGUI.indentLevel--;

        EditorGUILayout.LabelField(@"Moire");

        EditorGUI.indentLevel++;

        thisTarget.MoireOpacity = EditorHelper.Slider(@"Gain", thisTarget.MoireOpacity, 0.0f, 100.0f, 1.0f);
        thisTarget.MoireScale = EditorHelper.Slider(@"Scale", thisTarget.MoireScale, 0.01f, 100.0f, 0.15f);

        EditorGUI.indentLevel--;

        EditorGUILayout.LabelField(@"CRT Distortion");

        EditorGUI.indentLevel++;

        thisTarget.CRTDistort = EditorHelper.Slider(@"Tube distortion", thisTarget.CRTDistort, 0.0f, 10.0f, 1.0f);
        thisTarget.CRTScale = EditorHelper.Slider(@"Scale", thisTarget.CRTScale, 1.0f, 10.0f, 1.06f);

        EditorGUI.indentLevel--;

        EditorGUILayout.LabelField(@"TV Lines");

        EditorGUI.indentLevel++;

        thisTarget.TVLines = EditorHelper.Slider(@"Lines", thisTarget.TVLines, 0.01f, 10.0f, 2.5f);
        thisTarget.TVLinesOpacity = EditorHelper.Slider(@"Opacity",  thisTarget.TVLinesOpacity, 0.0f, 10.0f, 1.0f);

        EditorGUI.indentLevel--;
      }
    }
  }
}
