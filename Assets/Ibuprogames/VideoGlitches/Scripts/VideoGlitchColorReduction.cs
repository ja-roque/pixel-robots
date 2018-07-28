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
    /// Palette quantization and dithering.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    [AddComponentMenu("Image Effects/Video Glitches/Video Glitch Color Reduction")]
    public sealed class VideoGlitchColorReduction : VideoGlitchBase
    {
      /// <summary>
      /// Dither methods.
      /// </summary>
      public enum Ditherings
      {
        Bayer4x4,
        Bayer8x8,
        Noise,
        AnimNoise,
      }

      /// <summary>
      /// Dither methods. Default Bayer4x4.
      /// </summary>
      [Enum(typeof(Ditherings))]
      public Ditherings Dithering
      {
        get { return dithering; }
        set { dithering = value; }
      }

      /// <summary>
      /// Colors count per channel.
      /// </summary>
      [RangeInt(0, 256, 8)]
      public int PaletteSize
      {
        get { return paletteSize; }
        set { paletteSize = value < 0 ? 0 : value; }
      }

      /// <summary>
      /// Size of the pixel [1 - 25]. Default 2.
      /// </summary>
      [RangeInt(1, 25, 2)]
      public int Pixelation
      {
        get { return pixelation; }
        set { pixelation = value < 1 ? 1 : value; }
      }

      [SerializeField]
      private Ditherings dithering = Ditherings.Bayer4x4;

      [SerializeField]
      private int paletteSize = 8;

      [SerializeField]
      private int pixelation = 2;

      private Texture2D ditheringTexture;

      private const string variablePaletteSpace = @"_PaletteSpace";
      private const string variablePixelation = @"_Pixelation";
      private const string variableDitheringTexture = @"_DitheringTex";

      private const string keywordBayer4x4 = @"DITHERING_BAYER4x4";
      private const string keywordBayer8x8 = @"DITHERING_BAYER8x8";
      private const string keywordNoise = @"DITHERING_NOISE";

      /// <summary>
      /// Effect description.
      /// </summary>
      public override string ToString()
      {
        return @"Palette quantization and dithering.";
      }

      /// <summary>
      /// Load custom resources.
      /// </summary>
      protected override void LoadCustomResources()
      {
        ditheringTexture = LoadTextureFromResources(@"Textures/Bayer4x4");
      }

      /// <summary>
      /// Set the default values of the shader.
      /// </summary>
      public override void ResetDefaultValues()
      {
        dithering = Ditherings.Bayer4x4;
        paletteSize = 8;
        pixelation = 2;

        base.ResetDefaultValues();
      }

      /// <summary>
      /// Set the values to shader.
      /// </summary>
      protected override void SendValuesToShader()
      {
        switch (dithering)
        {
          case Ditherings.Bayer4x4:
            material.EnableKeyword(keywordBayer4x4);
            material.SetTexture(variableDitheringTexture, ditheringTexture);
            break;
          case Ditherings.Bayer8x8:
            material.EnableKeyword(keywordBayer8x8);
            break;
          case Ditherings.Noise:
            material.EnableKeyword(keywordNoise);
            break;
        }

        material.SetFloat(variablePixelation, pixelation * 1.0f);
        material.SetFloat(variablePaletteSpace, paletteSize > 0 ? 1.0f / (float)paletteSize : 0);
      }
    }
  }
}
