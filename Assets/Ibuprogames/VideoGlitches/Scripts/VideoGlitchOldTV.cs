///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Ibuprogames <hello@ibuprogames.com>. All rights reserved.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR
// IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using UnityEngine;

namespace Ibuprogames
{
  namespace VideoGlitchesAsset
  {
    /// <summary>
    /// Old broken TV.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    [AddComponentMenu("Image Effects/Video Glitches/Video Glitch Old TV")]
    public sealed class VideoGlitchOldTV : VideoGlitchBase
    {
      /// <summary>
      /// Slow moving scanlines [0.0 - 1.0]. Default 0.01.
      /// </summary>
      [RangeFloat(0.0f, 1.0f, 0.01f)]
      public float SlowScan
      {
        get { return slowScan; }
        set { slowScan = Mathf.Clamp01(value); }
      }

      /// <summary>
      /// Tiny scanlines [0.0 - 2.0]. Default 0.6.
      /// </summary>
      [RangeFloat(0.0f, 2.0f, 0.6f)]
      public float ScanLine
      {
        get { return scanLine; }
        set { scanLine = Mathf.Clamp(value, 0.0f, 2.0f); }
      }

      /// <summary>
      /// Softness of the vignette [0.0 - 1.0]. Default 0.9.
      /// </summary>
      [RangeFloat(0.0f, 1.0f, 0.9f)]
      public float VignetteSoftness
      {
        get { return vignetteSoftness; }
        set { vignetteSoftness = Mathf.Clamp01(value); }
      }

      /// <summary>
      /// Scale of the vignette [0.0 - 1.0]. Default 0.14.
      /// </summary>
      [RangeFloat(0.0f, 1.0f, 0.14f)]
      public float VignetteScale
      {
        get { return vignetteScale; }
        set { vignetteScale = Mathf.Clamp01(value); }
      }

      /// <summary>
      /// Vignette tube scale [0.01 - 10.0]. Default 0.7.
      /// </summary>
      [RangeFloat(0.01f, 10.0f, 0.7f)]
      public float VignetteTubeScale
      {
        get { return vignetteTubeScale; }
        set { vignetteTubeScale = Mathf.Clamp(value, 0.01f, 10.0f); }
      }

      /// <summary>
      /// Grain opacity [0.0 - 100.0]. Default 5.
      /// </summary>
      [RangeFloat(0.0f, 100.0f, 5.0f)]
      public float GrainOpacity
      {
        get { return grainOpacity; }
        set { grainOpacity = Mathf.Clamp(value, 0.0f, 100.0f); }
      }

      /// <summary>
      /// Saturation of the grain [0.0 - 1.0]. Default 0.
      /// </summary>
      [RangeFloat(0.0f, 1.0f, 0.0f)]
      public float GrainSaturation
      {
        get { return grainSaturation; }
        set { grainSaturation = Mathf.Clamp01(value); }
      }

      /// <summary>
      /// Scanline distortion [0.0 - 10.0]. Default 0.03.
      /// </summary>
      [RangeFloat(0.0f, 10.0f, 0.03f)]
      public float ScanDistort
      {
        get { return scanDistort; }
        set { scanDistort = Mathf.Clamp(value, 0.0f, 10.0f); }
      }

      /// <summary>
      /// Distortion frecuency [0.0 - 5.0]. Default 0.85.
      /// </summary>
      [RangeFloat(0.0f, 5.0f, 0.85f)]
      public float Timer
      {
        get { return timer; }
        set { timer = Mathf.Clamp(value, 0.0f, 5.0f); }
      }

      /// <summary>
      /// Distortion speed [1.0 - 5.0]. Default 2.
      /// </summary>
      [RangeFloat(1.0f, 5.0f, 2.0f)]
      public float Speed
      {
        get { return speed; }
        set { speed = Mathf.Clamp(value, 1.0f, 5.0f); }
      }

      /// <summary>
      /// Tube distortion [0.0 - 5.0]. Default 0.03.
      /// </summary>
      [RangeFloat(0.0f, 5.0f, 0.03f)]
      public float CRTDistort
      {
        get { return crtDistort; }
        set { crtDistort = Mathf.Clamp(value, 0.0f, 5.0f); }
      }

      /// <summary>
      /// Tube distortion scale [1.0 - 10.0]. Default 1.06.
      /// </summary>
      [RangeFloat(1.0f, 10.0f, 1.06f)]
      public float CRTScale
      {
        get { return crtScale; }
        set { crtScale = Mathf.Clamp(value, 1.0f, 10.0f); }
      }

      /// <summary>
      /// VHS stripes count [0.0 - 1000.0]. Default 0.5.
      /// </summary>
      [RangeFloat(0.0f, 1000.0f, 0.5f)]
      public float StripesCount
      {
        get { return stripesCount; }
        set { stripesCount = Mathf.Clamp(value, 0.0f, 1000.0f); }
      }

      /// <summary>
      /// VHS stripes opacity [0.0 - 10.0]. Default 1.
      /// </summary>
      [RangeFloat(0.0f, 10.0f, 1.0f)]
      public float StripesOpacity
      {
        get { return stripesOpacity; }
        set { stripesOpacity = Mathf.Clamp(value, 0.0f, 10.0f); }
      }

      /// <summary>
      /// VHS bars count [0.0 - 1000.0]. Default 5.
      /// </summary>
      [RangeFloat(0.0f, 1000.0f, 5.0f)]
      public float BarsCount
      {
        get { return barsCount; }
        set { barsCount = Mathf.Clamp(value, 0.0f, 1000.0f); }
      }

      /// <summary>
      /// Moire opacity [0.0 - 100.0]. Default 1.
      /// </summary>
      [RangeFloat(0.0f, 100.0f, 1.0f)]
      public float MoireOpacity
      {
        get { return moireOpacity; }
        set { moireOpacity = Mathf.Clamp(value, 0.0f, 100.0f); }
      }

      /// <summary>
      /// Moire scale [0.01 - 100.0]. Default 0.15.
      /// </summary>
      [RangeFloat(0.01f, 100.0f, 0.15f)]
      public float MoireScale
      {
        get { return moireScale; }
        set { moireScale = Mathf.Clamp(value, 0.01f, 100.0f); }
      }

      /// <summary>
      /// TV lines [0.01 - 10.0]. Default 2.5.
      /// </summary>
      [RangeFloat(0.01f, 10.0f, 2.5f)]
      public float TVLines
      {
        get { return tvLines; }
        set { tvLines = Mathf.Clamp(value, 0.01f, 10.0f); }
      }

      /// <summary>
      /// TV lines opacity [0.0 - 10.0]. Default 1.
      /// </summary>
      [RangeFloat(0.0f, 10.0f, 1.0f)]
      public float TVLinesOpacity
      {
        get { return tvLinesOpacity; }
        set { tvLinesOpacity = Mathf.Clamp(value, 0.0f, 10.0f); }
      }

      /// <summary>
      /// TV dots style [0 - 4]. Default 1.
      /// </summary>
      [RangeFloat(0.0f, 4.0f, 1.0f)]
      public float TVDots
      {
        get { return tvDots; }
        set { tvDots = Mathf.Clamp(value, 0.0f, 4.0f); }
      }

      /// <summary>
      /// TV dots blend [0.0 - 1000.0]. Default 1.
      /// </summary>
      [RangeFloat(0.0f, 1000.0f, 1.0f)]
      public float TVDotsBlend
      {
        get { return tvDotsBlend; }
        set { tvDotsBlend = Mathf.Clamp(value, 0.0f, 1000.0f); }
      }

      [SerializeField]
      private float slowScan = 0.01f;

      [SerializeField]
      private float scanLine = 0.6f;

      [SerializeField]
      private float vignetteSoftness = 0.9f;

      [SerializeField]
      private float vignetteScale = 0.14f;

      [SerializeField]
      private float vignetteTubeScale = 0.7f;

      [SerializeField]
      private float grainOpacity = 5.0f;

      [SerializeField]
      private float grainSaturation = 0.0f;

      [SerializeField]
      private float scanDistort = 0.03f;

      [SerializeField]
      private float timer = 0.85f;

      [SerializeField]
      private float speed = 2.0f;

      [SerializeField]
      private float crtDistort = 0.03f;

      [SerializeField]
      private float crtScale = 1.06f;

      [SerializeField]
      private float stripesCount = 0.5f;

      [SerializeField]
      private float stripesOpacity = 1.0f;

      [SerializeField]
      private float barsCount = 5.0f;

      [SerializeField]
      private float moireOpacity = 1.0f;

      [SerializeField]
      private float moireScale = 0.15f;

      [SerializeField]
      private float tvLines = 2.5f;

      [SerializeField]
      private float tvLinesOpacity = 1.0f;

      [SerializeField]
      private float tvDots = 1.0f;

      [SerializeField]
      private float tvDotsBlend = 1.0f;

      private const string variableScanline = @"_Scanline";
      private const string variableSlowscan = @"_Slowscan";
      private const string variableVignetteSoftness = @"_VignetteSoftness";
      private const string variableVignetteScale = @"_VignetteScale";
      private const string variableGrainOpacity = @"_GrainOpacity";
      private const string variableSaturation = @"_SaturationTV";
      private const string variableScanDistort = @"_ScanDistort";
      private const string variableTimer = @"_Timer";
      private const string variableSpeed = @"_Speed";
      private const string variableDistort = @"_Distort";
      private const string variableScale = @"_Scale";
      private const string variableStripesCount = @"_StripesCount";
      private const string variableOpacity = @"_Opacity";
      private const string variableBarsCount = @"_BarsCount";
      private const string variableOpacityMoire = @"_OpacityMoire";
      private const string variableMoireScale = @"_MoireScale";
      private const string variableTVLines = @"_TVLines";
      private const string variableTVLinesOpacity = @"_TVLinesOpacity";
      private const string variableTVTubeVignetteScale = @"_TVTubeVignetteScale";
      private const string variableTVDots = @"_TVDots";
      private const string variableTVDotsBlend = @"_TVDotsBlend";

      /// <summary>
      /// Effect description.
      /// </summary>
      public override string ToString()
      {
        return @"Old broken TV.";
      }

      /// <summary>
      /// Set the default values of the shader.
      /// </summary>
      public override void ResetDefaultValues()
      {
        slowScan = 0.01f;
        scanLine = 0.6f;
        vignetteSoftness = 0.9f;
        vignetteScale = 0.14f;
        grainOpacity = 5.0f;
        grainSaturation = 0.0f;
        scanDistort = 0.03f;
        timer = 0.85f;
        speed = 2.0f;
        crtDistort = 1.0f;
        crtScale = 1.06f;
        stripesCount = 0.5f;
        stripesOpacity = 1.0f;
        barsCount = 5.0f;
        moireOpacity = 1.0f;
        moireScale = 0.15f;
        tvLines = 2.5f;
        tvLinesOpacity = 1.0f;
        vignetteTubeScale = 0.7f;
        tvDots = 1.0f;
        tvDotsBlend = 1.0f;

        base.ResetDefaultValues();
      }

      /// <summary>
      /// Set the values to shader.
      /// </summary>
      protected override void SendValuesToShader()
      {
        material.SetFloat(variableScanline, scanLine);
        material.SetFloat(variableSlowscan, slowScan);
        material.SetFloat(variableVignetteSoftness, vignetteSoftness);
        material.SetFloat(variableVignetteScale, vignetteScale);
        material.SetFloat(variableGrainOpacity, grainOpacity);
        material.SetFloat(variableSaturation, grainSaturation);
        material.SetFloat(variableScanDistort, scanDistort);
        material.SetFloat(variableTimer, timer);
        material.SetFloat(variableSpeed, speed);
        material.SetFloat(variableDistort, crtDistort);
        material.SetFloat(variableScale, crtScale);
        material.SetFloat(variableStripesCount, stripesCount);
        material.SetFloat(variableOpacity, stripesOpacity);
        material.SetFloat(variableBarsCount, barsCount);
        material.SetFloat(variableOpacityMoire, moireOpacity);
        material.SetFloat(variableMoireScale, moireScale);
        material.SetFloat(variableTVLines, tvLines);
        material.SetFloat(variableTVLinesOpacity, tvLinesOpacity);
        material.SetFloat(variableTVTubeVignetteScale, vignetteTubeScale);
        material.SetFloat(variableTVDots, tvDots);
        material.SetFloat(variableTVDotsBlend, tvDotsBlend);
      }
    }
  }
}
