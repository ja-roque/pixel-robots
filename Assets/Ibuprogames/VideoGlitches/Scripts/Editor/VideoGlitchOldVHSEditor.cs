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
    /// Video Glitch Old VHS Editor.
    /// </summary>
    [CustomEditor(typeof(VideoGlitchOldVHS))]
    public class VideoGlitchOldVHSEditor : VideoGlitchEditorBase
    {
      /// <summary>
      /// Inspector.
      /// </summary>
      protected override void Inspector()
      {
        VideoGlitchOldVHS thisTarget = (VideoGlitchOldVHS)target;

        EditorGUILayout.LabelField(@"Distortions");

        EditorGUI.indentLevel++;

        thisTarget.Noise = EditorHelper.Slider(@"Noise", @"Noise [0.0 - 1.0].", thisTarget.Noise, 0.0f, 1.0f, 0.25f);

        thisTarget.Waving = EditorHelper.Slider(@"Waving", @"Wave strength [0.0 - 1.0].", thisTarget.Waving, 0.0f, 1.0f, 0.5f);

        thisTarget.SwitchingNoise = EditorHelper.Slider(@"Switching noise", @"Head switching noise [0.0 - 1.0].", thisTarget.SwitchingNoise, 0.0f, 1.0f, 0.5f);

        EditorGUI.indentLevel--;

        EditorGUILayout.LabelField(@"Stripes");

        EditorGUI.indentLevel++;

        thisTarget.StripeStrength = EditorHelper.Slider(@"Strength", @"Stripes strength [0.0 - 1.0].", thisTarget.StripeStrength, 0.0f, 1.0f, 1.0f);
        thisTarget.StripeCount = Mathf.Floor(EditorHelper.Slider(@"Count", @"Stripes count [0 - 32].", thisTarget.StripeCount, 0.0f, 32.0f, 2.0f));
        thisTarget.StripeVelocity = EditorHelper.Slider(@"Velocity", @"Stripes velocity [-10.0 - 10.0].", thisTarget.StripeVelocity, -10.0f, 10.0f, 1.2f);
        thisTarget.StripeNoise = EditorHelper.Slider(@"Noise", @"Stripes noise [0.0 - 1.0].", thisTarget.StripeNoise, 0.0f, 1.0f, 0.5f);

        EditorGUI.indentLevel--;

        EditorGUILayout.LabelField(@"AC Beat");

        EditorGUI.indentLevel++;

        thisTarget.ACBeatWidth = EditorHelper.Slider(@"Width", @"Electrical ground loop interference width [0.0 - 1.0].", thisTarget.ACBeatWidth, 0.0f, 1.0f, 0.6f);
        thisTarget.ACBeatVelocity = EditorHelper.Slider(@"Velocity", @"Electrical ground loop interference velocity [-10.0 - 10.0].", thisTarget.ACBeatVelocity, -10.0f, 10.0f, 0.2f);

        EditorGUI.indentLevel--;

        thisTarget.BloomPasses = Mathf.Floor(EditorHelper.Slider(@"Bloom passes", @"Bloom passes [0 - 10].", thisTarget.BloomPasses, 0.0f, 10.0f, 5.0f));
      }
    }
  }
}
