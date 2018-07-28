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
    /// Image effect base.
    /// </summary>
    [HelpURL("http://www.ibuprogames.com/2015/07/03/video-glitches/")]
    public abstract class VideoGlitchBase : MonoBehaviour
    {
      /// <summary>
      /// Strength of the effect [0, 1]. Default 1.
      /// </summary>
      public float Strength
      {
        get { return strength; }
        set { strength = Mathf.Clamp01(value); }
      }

      /// <summary>
      /// Screen or Layer mode. Default Screen.
      /// </summary>
      public EffectModes EffectMode
      {
        get { return effectMode; }
        set
        {
          if (value != effectMode)
          {
            effectMode = value;

            if (effectMode == EffectModes.Screen)
            {
              this.GetComponent<Camera>().depthTextureMode = DepthTextureMode.None;

              DestroyDepthCamera();
            }
            else
            {
              this.GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;

              CreateDepthCamera();
            }
          }
        }
      }

      /// <summary>
      /// The layer to which the effect affects. Used in EffectMode.Layer. Default 'Everything'.
      /// </summary>
      public LayerMask Layer
      {
        get { return layer; }
        set
        {
          layer = value;

          if (renderDepth != null)
            renderDepth.layer = layer;
        }
      }

      /// <summary>
      /// Effect strength curve. Used in EffectMode.Distance.
      /// </summary>
      public AnimationCurve DistanceCurve
      {
        get { return distanceCurve; }
        set
        {
          distanceCurve = value;

          UpdateDistanceCurveTexture();
        }
      }

      /// <summary>
      /// Enable color controls (Brightness, Contrast, Gamma, Hue and Saturation).
      /// </summary>
      public bool EnableColorControls
      {
        get { return enableColorControls; }
        set { enableColorControls = value; }
      }

      /// <summary>
      /// Brightness [-1.0, 1.0]. Default 0.
      /// </summary>
      public float Brightness
      {
        get { return brightness; }
        set { brightness = Mathf.Clamp(value, -1.0f, 1.0f); }
      }

      /// <summary>
      /// Contrast [-1.0, 1.0]. Default 0.
      /// </summary>
      public float Contrast
      {
        get { return contrast; }
        set { contrast = Mathf.Clamp(value, -1.0f, 1.0f); }
      }

      /// <summary>
      /// Gamma [0.1, 10.0]. Default 1.
      /// </summary>
      public float Gamma
      {
        get { return gamma; }
        set { gamma = Mathf.Clamp(value, 0.1f, 10.0f); }
      }

      /// <summary>
      /// The color wheel [0.0, 1.0]. Default 0.
      /// </summary>
      public float Hue
      {
        get { return hue; }
        set { hue = Mathf.Clamp01(value); }
      }

      /// <summary>
      /// Intensity of a colors [0.0, 2.0]. Default 1.
      /// </summary>
      public float Saturation
      {
        get { return saturation; }
        set { saturation = Mathf.Clamp(value, 0.0f, 2.0f); }
      }

      /// <summary>
      /// Accuracy of depth texture [0.0, 0.01]. Used in EffectMode.Layer. Default 0.004.
      /// </summary>
      public float DepthThreshold
      {
        get { return depthThreshold; }
        set { depthThreshold = Mathf.Clamp01(value); }
      }

      /// <summary>
      /// Use scaled time.
      /// </summary>
      public bool UseScaledTime
      {
        get { return useScaledTime; }
        set { useScaledTime = value; }
      }

      /// <summary>
      /// Custom time scale.
      /// </summary>
      public float CustomTimeScale
      {
        get { return timeScale; }
        set { timeScale = value >= 0.0f ? value : 0.0f; }
      }

      [SerializeField]
      private float strength = 1.0f;

      [SerializeField]
      private EffectModes effectMode = EffectModes.Screen;

      [SerializeField]
      private bool enableColorControls = false;

      [SerializeField]
      private float brightness = 0.0f;

      [SerializeField]
      private float contrast = 0.0f;

      [SerializeField]
      private float gamma = 1.0f;

      [SerializeField]
      private float hue = 0.0f;

      [SerializeField]
      private float saturation = 1.0f;

      [SerializeField]
      private LayerMask layer = -1;

      [SerializeField]
      private AnimationCurve distanceCurve = new AnimationCurve(new Keyframe(1.0f, 0.0f, 0.0f, 0.0f), new Keyframe(0.0f, 1.0f, 0.0f, 0.0f));

      [SerializeField]
      private RenderDepth renderDepth;

      [SerializeField]
      private float depthThreshold = 0.004f;

      [SerializeField]
      private bool useScaledTime = true;

      [SerializeField]
      private float timeScale = 1.0f;

      private float customTime = 0.0f;

      private Shader shader;

      protected Material material;

      private Texture2D distanceTexture;

      private const string variableStrength = @"_Strength";
      private const string variableCustomTime = @"_CustomTime";
      private const string variableBrightness = @"_Brightness";
      private const string variableContrast = @"_Contrast";
      private const string variableGamma = @"_Gamma";
      private const string variableHue = @"_Hue";
      private const string variableSaturation = @"_Saturation";
      private const string variableRenderToTexture = @"_RTT";
      private const string variableDepthThreshold = @"_DepthThreshold";
      private const string variableDistanceTexture = @"_DistanceTex";

      private const string keywordModeScreen = @"MODE_SCREEN";
      private const string keywordModeLayer = @"MODE_LAYER";
      private const string keywordModeDistance = @"MODE_DISTANCE";

      private const string keywordColorControls = @"COLOR_CONTROLS";

      /// <summary>
      /// Effect supported by the current hardware?
      /// </summary>
      public bool IsSupported()
      {
        bool supported = false;

        if (SystemInfo.supportsImageEffects == true)
        {
          string shaderPath = ShaderPath();

          Shader test = Resources.Load<Shader>(shaderPath);
          if (test != null)
          {
            supported = test.isSupported == true && CheckHardwareRequirements() == true;

            Resources.UnloadAsset(test);
          }
        }

        if (supported == true && (effectMode == EffectModes.Layer || effectMode == EffectModes.Distance))
          supported = SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth);

        return supported;
      }

      /// <summary>
      /// Reset to default values.
      /// </summary>
      public virtual void ResetDefaultValues()
      {
        strength = 1.0f;

        brightness = 0.0f;
        contrast = 0.0f;
        gamma = 1.0f;
        hue = 0.0f;
        saturation = 1.0f;

        depthThreshold = 0.004f;
        useScaledTime = true;
        timeScale = 1.0f;
      }

      /// <summary>
      /// Effect description.
      /// </summary>
      public override string ToString()
      {
        return @"No description.";
      }      
      
      protected virtual string ShaderPath()
      {
        return string.Format("Shaders/{0}", this.GetType().Name);
      }

      private Material Material
      {
        get
        {
          if (material == null && shader != null)
          {
            string materialName = this.GetType().Name;

            material = new Material(shader);
            if (material != null)
            {
              material.name = materialName;
              material.hideFlags = HideFlags.HideAndDontSave;
            }
            else
            {
              Debug.LogErrorFormat("[Ibuprogames.VideoGlitches] '{0}' material null. Please contact with 'hello@ibuprogames.com' and send the log file.", materialName);

              this.enabled = false;
            }
          }

          return material;
        }
      }

      private void CreateDepthCamera()
      {
        if (renderDepth == null)
        {
          GameObject go = new GameObject(@"VideoGlitchesDepthCamera", typeof(Camera))
          {
            hideFlags = HideFlags.HideAndDontSave,
          };

          go.transform.parent = this.transform;
          go.transform.localPosition = Vector3.zero;
          go.transform.localRotation = Quaternion.identity;
          go.transform.localScale = Vector3.one;

          renderDepth = go.AddComponent<RenderDepth>();
          renderDepth.layer = layer;
        }
      }

      private void DestroyDepthCamera()
      {
        if (renderDepth != null)
        {
          GameObject obj = renderDepth.gameObject;
          renderDepth = null;

          DestroyImmediate(obj);
        }
      }

      /// <summary>
      /// Set the values to shader.
      /// </summary>
      protected abstract void SendValuesToShader();

      /// <summary>
      /// Load custom resources.
      /// </summary>
      protected virtual void LoadCustomResources() { }

      /// <summary>
      /// Load texture from "Resources/Textures". Internal use.
      /// </summary>
      protected Texture2D LoadTextureFromResources(string texturePathFromResources)
      {
        Texture2D texture = Resources.Load<Texture2D>(texturePathFromResources);
        if (texture == null)
        {
          Debug.LogErrorFormat("[Ibuprogames.VideoGlitches] Texture '{0}' not found in 'Ibuprogames/VideoGlitches/Resources/Textures' folder. Please contact with 'hello@ibuprogames.com' and send the log file.", texturePathFromResources);

          this.enabled = false;
        }

        return texture;
      }

      /// <summary>
      /// Custom hardware requirements.
      /// </summary>
      protected virtual bool CheckHardwareRequirements()
      {
        if ((effectMode == EffectModes.Layer || effectMode == EffectModes.Distance) && SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth) == false)
        {
          Debug.LogWarning(@"[Ibuprogames.Vintage] Depth textures aren't supported on this device.");

          return false;
        }

        return true;
      }

      private void UpdateDistanceCurveTexture()
      {
        if (distanceTexture == null)
        {
          distanceTexture = new Texture2D(1024, 2)
          {
            filterMode = FilterMode.Bilinear,
            wrapMode = TextureWrapMode.Clamp,
            anisoLevel = 1
          };
        }

        float step = 1.0f / (float)distanceTexture.width;
        for (int i = 0; i < distanceTexture.width; ++i)
        {
          Color color = Color.white * Mathf.Clamp01(distanceCurve.Evaluate((float)i * step));

          distanceTexture.SetPixel(i, 0, color);
          distanceTexture.SetPixel(i, 1, color);
        }

        distanceTexture.Apply();
      }

      /// <summary>
      /// Called on the frame when a script is enabled just before any of the Update methods is called the first time.
      /// </summary>
      private void Start()
      {
        if (SystemInfo.supportsImageEffects == true)
        {
          string shaderPath = ShaderPath();

          shader = Resources.Load<Shader>(shaderPath);
          if (shader != null)
          {
            if (shader.isSupported == true && CheckHardwareRequirements() == true)
              LoadCustomResources();
            else
            {
              Debug.LogErrorFormat("[Ibuprogames.VideoGlitches] '{0}' shader not supported. Please contact with 'hello@ibuprogames.com' and send the log file.", shaderPath);

              this.enabled = false;
            }
          }
          else
          {
            Debug.LogErrorFormat("[Ibuprogames.VideoGlitches] Shader 'Ibuprogames/VideoGlitches/Resources/{0}.shader' not found. '{1}' disabled.", shaderPath, this.GetType().Name);

            this.enabled = false;
          }
        }
        else
        {
          Debug.LogErrorFormat("[Ibuprogames.VideoGlitches] Hardware not support Image Effects. '{0}' disabled.", this.GetType().Name);

          this.enabled = false;
        }
      }

      /// <summary>
      /// Called when the object becomes enabled and active.
      /// </summary>
      private void OnEnable()
      {
        if (effectMode != EffectModes.Screen && renderDepth == null)
          CreateDepthCamera();

        Camera effectCamera = this.GetComponent<Camera>();
        effectCamera.depthTextureMode = (effectMode == EffectModes.Screen ? DepthTextureMode.None : DepthTextureMode.Depth);
      }

      /// <summary>
      /// When the MonoBehaviour will be destroyed.
      /// </summary>
      protected virtual void OnDestroy()
      {
        if (material != null)
#if UNITY_EDITOR
          DestroyImmediate(material);
#else
				  Destroy(material);
#endif
      }

      private void Update()
      {
        customTime += (useScaledTime == true ? Time.deltaTime : Time.unscaledDeltaTime) * timeScale;
      }

      /// <summary>
      /// Called after all rendering is complete to render image.
      /// </summary>
      private void OnRenderImage(RenderTexture source, RenderTexture destination)
      {
        if (Material != null)
        {
          material.shaderKeywords = null;

          material.SetFloat(variableStrength, strength);
          material.SetVector(variableCustomTime, new Vector4(customTime / 20.0f, customTime, customTime * 2.0f, customTime * 3.0f));

          switch (effectMode)
          {
            case EffectModes.Screen:
              material.EnableKeyword(keywordModeScreen);
              break;
            case EffectModes.Layer:
              material.EnableKeyword(keywordModeLayer);

              if (renderDepth != null)
                material.SetTexture(variableRenderToTexture, renderDepth.renderTexture);

              material.SetFloat(variableDepthThreshold, depthThreshold);
              break;
            case EffectModes.Distance:
              material.EnableKeyword(keywordModeDistance);

              if (distanceTexture == null)
                UpdateDistanceCurveTexture();

              material.SetTexture(variableDistanceTexture, distanceTexture);
              break;
          }

          if (enableColorControls == true && strength > 0.0f)
          {
            material.EnableKeyword(keywordColorControls);

            material.SetFloat(variableBrightness, brightness);
            material.SetFloat(variableContrast, contrast);
            material.SetFloat(variableGamma, 1.0f / gamma);
            material.SetFloat(variableHue, hue);
            material.SetFloat(variableSaturation, saturation);
          }

          SendValuesToShader();

          Graphics.Blit(source, destination, material);
        }
        else
          Graphics.Blit(source, destination);
      }
    }
  }
}
