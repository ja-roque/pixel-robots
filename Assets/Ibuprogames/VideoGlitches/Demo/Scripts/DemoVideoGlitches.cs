///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Ibuprogames <hello@ibuprogames.com>. All rights reserved.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR
// IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Collections.Generic;
using System.Reflection;

using UnityEngine;

using Ibuprogames.VideoGlitchesAsset;

/// <summary>
/// UI for the demo.
/// </summary>
public class DemoVideoGlitches : MonoBehaviour
{
  [SerializeField]
  private bool guiShow = true;

  [SerializeField]
  private bool showEffectName = false;

  [SerializeField]
  private float slideEffectTime = 0.0f;

  [SerializeField]
  private AudioClip musicClip = null;

  private enum ScreenModes
  {
    Fullscreen,

    Layers,
  }

  private float effectTime = 0.0f;

  private ScreenModes screenMode = ScreenModes.Fullscreen;

  private List<VideoGlitchBase> videoGlitches = new List<VideoGlitchBase>();

  private int guiSelection = 0;
  private bool showCustomProperties = false;

  private bool menuOpen = false;

  private const float guiMargen = 10.0f;
  private const float guiWidth = 475.0f;
  private const string guiTab = @"   ";

  private Vector2 scrollPosition = Vector2.zero;

  private float updateInterval = 0.5f;
  private float accum = 0.0f;
  private int frames = 0;
  private float timeleft;
  private float fps = 0.0f;

  private GUIStyle effectNameStyle;
  private GUIStyle menuStyle;
  private GUIStyle boxStyle;

  private void OnEnable()
  {
    timeleft = updateInterval;

    Camera selectedCamera = null;
    Camera[] cameras = GameObject.FindObjectsOfType<Camera>();

    for (int i = 0; i < cameras.Length; ++i)
    {
      if (cameras[i].enabled == true)
      {
        selectedCamera = cameras[i];

        break;
      }
    }

    if (selectedCamera != null)
    {
      VideoGlitchBase[] effects = selectedCamera.gameObject.GetComponents<VideoGlitchBase>();
      if (effects.Length > 0)
      {
        for (int i = 0; i < effects.Length; ++i)
        {
          if (effects[i].IsSupported() == true)
            videoGlitches.Add(effects[i]);
          else
            effects[i].enabled = false;
        }
      }
      else
      {
        System.Type[] types = Assembly.GetAssembly(typeof(VideoGlitchBase)).GetTypes();
        for (int i = 0; i < types.Length; ++i)
        {
          if (types[i].IsClass == true && types[i].IsAbstract == false && types[i].IsSubclassOf(typeof(VideoGlitchBase)) == true)
          {
            VideoGlitchBase vintageEffect = selectedCamera.gameObject.AddComponent(types[i]) as VideoGlitchBase;
            if (vintageEffect.IsSupported() == true)
              videoGlitches.Add(vintageEffect);
            else
              Destroy(vintageEffect);
          }
        }
      }

      Debug.Log(string.Format("{0} Video Glitches.", videoGlitches.Count));

      for (int i = 0; i < videoGlitches.Count; ++i)
      {
        videoGlitches[i].enabled = (i == guiSelection);
        videoGlitches[i].EffectMode = screenMode == ScreenModes.Fullscreen ? EffectModes.Screen : EffectModes.Layer;
        videoGlitches[i].Layer = 1 << LayerMask.NameToLayer(@"DynamicObjects");
      }

      if (musicClip != null)
      {
        AudioSource audioSource = this.gameObject.AddComponent<AudioSource>();
        audioSource.clip = musicClip;
        audioSource.volume = 0.25f;
        audioSource.loop = (slideEffectTime > 0.0f);
        audioSource.PlayDelayed(0.0f);
      }
    }
    else
      Debug.LogWarning("No camera found.");
  }

  private void Update()
  {
    timeleft -= Time.deltaTime;
    accum += Time.timeScale / Time.deltaTime;
    frames++;

    if (timeleft <= 0.0f)
    {
      fps = accum / frames;
      timeleft = updateInterval;
      accum = 0.0f;
      frames = 0;
    }

    if (slideEffectTime > 0.0f && videoGlitches.Count > 0)
    {
      effectTime += Time.deltaTime;
      if (effectTime >= slideEffectTime)
      {
        videoGlitches[guiSelection].enabled = false;

        guiSelection = (guiSelection < (videoGlitches.Count - 1) ? guiSelection + 1 : 0);

        EnableEffect(videoGlitches[guiSelection], true);

        effectTime = 0.0f;
      }
    }

    if (Input.GetKeyUp(KeyCode.Tab) == true)
      guiShow = !guiShow;

    if (Input.GetKeyUp(KeyCode.KeypadPlus) == true ||
        Input.GetKeyUp(KeyCode.KeypadMinus) == true ||
        Input.GetKeyUp(KeyCode.PageUp) == true ||
        Input.GetKeyUp(KeyCode.PageDown) == true)
    {
      int effectSelected = 0;

      slideEffectTime = 0.0f;

      for (int i = 0; i < videoGlitches.Count; ++i)
      {
        if (videoGlitches[i].enabled == true)
        {
          videoGlitches[i].enabled = false;

          effectSelected = i;

          break;
        }
      }

      if (Input.GetKeyUp(KeyCode.KeypadPlus) == true || Input.GetKeyUp(KeyCode.PageDown) == true)
      {
        guiSelection = (effectSelected < videoGlitches.Count - 1 ? effectSelected + 1 : 0);

        EnableEffect(videoGlitches[guiSelection], true);
      }

      if (Input.GetKeyUp(KeyCode.KeypadMinus) == true || Input.GetKeyUp(KeyCode.PageUp) == true)
      {
        guiSelection = (effectSelected > 0 ? effectSelected - 1 : videoGlitches.Count - 1);

        EnableEffect(videoGlitches[guiSelection], true);
      }
    }
  }

  private void OnGUI()
  {
    if (videoGlitches.Count == 0)
      return;

    if (effectNameStyle == null)
    {
      effectNameStyle = new GUIStyle(GUI.skin.textArea);
      effectNameStyle.alignment = TextAnchor.MiddleCenter;
      effectNameStyle.fontSize = 22;
    }

    if (boxStyle == null)
    {
      boxStyle = new GUIStyle(GUI.skin.box);
      boxStyle.normal.background = MakeTex(2, 2, new Color(0.5f, 0.5f, 0.5f, 0.5f));
      boxStyle.focused.textColor = Color.red;
    }

    if (menuStyle == null)
    {
      menuStyle = new GUIStyle(GUI.skin.textArea);
      menuStyle.alignment = TextAnchor.MiddleCenter;
      menuStyle.fontSize = 22;
    }

    if (showEffectName == true && guiShow == false)
    {
      string effectName = EffectName(videoGlitches[guiSelection].GetType().ToString());

      GUILayout.BeginArea(new Rect(Screen.width * 0.5f - 150.0f, 20.0f, 300.0f, 30.0f), effectName.ToUpper(), effectNameStyle);
      GUILayout.EndArea();
    }

    if (guiShow == false)
      return;

    GUILayout.BeginHorizontal(boxStyle, GUILayout.Width(Screen.width));
    {
      GUILayout.Space(guiMargen);

      if (GUILayout.Button("MENU", menuStyle, GUILayout.Width(80.0f)) == true)
        menuOpen = !menuOpen;

      GUILayout.FlexibleSpace();

      if (GUILayout.Button("<<<", menuStyle) == true)
      {
        showCustomProperties = false;

        slideEffectTime = 0.0f;

        if (guiSelection > 0)
          guiSelection--;
        else
          guiSelection = videoGlitches.Count - 1;

        Event.current.Use();
      }

      GUI.contentColor = Color.white;

      string effectName = EffectName(videoGlitches[guiSelection].GetType().ToString());

      GUILayout.Label(effectName.ToUpper(), menuStyle, GUILayout.Width(325.0f));

      if (GUILayout.Button(">>>", menuStyle) == true)
      {
        showCustomProperties = false;

        slideEffectTime = 0.0f;

        if (guiSelection < videoGlitches.Count - 1)
          guiSelection++;
        else
          guiSelection = 0;
      }

      GUILayout.FlexibleSpace();

        if (musicClip != null && GUILayout.Button(@"MUTE", menuStyle) == true)
          AudioListener.volume = 1.0f - AudioListener.volume;

      if (fps < 24.0f)
        GUI.contentColor = Color.yellow;
      else if (fps < 15.0f)
        GUI.contentColor = Color.red;
      else
        GUI.contentColor = Color.green;

      GUILayout.Label(fps.ToString("000"), menuStyle, GUILayout.Width(50.0f));

      GUI.contentColor = Color.white;

      GUILayout.Space(guiMargen);
    }
    GUILayout.EndHorizontal();

    // Update
    for (int i = 0; i < videoGlitches.Count; ++i)
    {
      VideoGlitchBase imageEffect = videoGlitches[i];

      if (guiSelection == i && imageEffect.enabled == false)
      {
        showCustomProperties = false;

        EnableEffect(imageEffect, true);
      }

      if (imageEffect.enabled == true && guiSelection != i)
        imageEffect.enabled = false;
    }

    if (menuOpen == true)
    {
      GUILayout.BeginVertical(boxStyle, GUILayout.Height(Screen.height), GUILayout.Width(guiWidth));
      {
        GUILayout.Space(guiMargen);

        int newScreenMode = GUILayout.Toolbar((int)screenMode, new string[] { @"Full screen", @"Layers" });
        if (newScreenMode != (int)screenMode)
        {
          screenMode = (ScreenModes)newScreenMode;

          videoGlitches[guiSelection].EffectMode = screenMode == ScreenModes.Fullscreen ? EffectModes.Screen : EffectModes.Layer;
        }

        GUILayout.Space(guiMargen);

        // Video Glitches.
        if (videoGlitches.Count > 0)
        {
          scrollPosition = GUILayout.BeginScrollView(scrollPosition, "box");
          {
            int effectChanged = -1;

            // Draw.
            for (int i = 0; i < videoGlitches.Count; ++i)
            {
              VideoGlitchBase imageEffect = videoGlitches[i];

              GUILayout.BeginVertical(imageEffect.enabled == true ? @"box" : string.Empty);
              {
                GUILayout.BeginHorizontal();
                {
                  bool enableChanged = GUILayout.Toggle(imageEffect.enabled, guiTab + EffectName(imageEffect.GetType().ToString()));
                  if (enableChanged != imageEffect.enabled)
                  {
                    showCustomProperties = false;

                    slideEffectTime = 0.0f;

                    effectChanged = i;
                  }

                  if (CustomAttributesCount(imageEffect) > 0)
                  {
                    GUILayout.FlexibleSpace();

                    if (imageEffect.enabled == true && GUILayout.Button(showCustomProperties == true ? @"-" : @"+") == true)
                    {
                      slideEffectTime = 0.0f;

                      showCustomProperties = !showCustomProperties;
                    }
                  }
                }
                GUILayout.EndHorizontal();

                if (imageEffect.enabled == true && showCustomProperties == true)
                  DrawCustomAttributes(imageEffect);
              }
              GUILayout.EndVertical();

              GUILayout.Space(guiMargen * 0.5f);
            }

            // Update
            for (int i = 0; i < videoGlitches.Count; ++i)
            {
              VideoGlitchBase imageEffect = videoGlitches[i];

              if (effectChanged == i)
              {
                EnableEffect(imageEffect, !imageEffect.enabled);

                if (imageEffect.enabled == true)
                  guiSelection = i;
              }

              if (imageEffect.enabled == true && guiSelection != i)
                imageEffect.enabled = false;
            }
          }
          GUILayout.EndScrollView();
        }
        else
          GUILayout.Label("No 'Video Glitches' found.");

        GUILayout.FlexibleSpace();

        GUILayout.BeginVertical("box");
        {
          GUILayout.Label("TAB - Hide/Show gui.");
          GUILayout.Label("PageUp/Down - Change effects.");
        }
        GUILayout.EndVertical();

        GUILayout.Space(guiMargen);

        if (GUILayout.Button(@"Open Web") == true)
          Application.OpenURL(@"http://www.ibuprogames.com/2015/07/02/video-glitches/‎");

        GUILayout.Space(guiMargen);
      }
      GUILayout.EndVertical();
    }
  }

  private int CustomAttributesCount(VideoGlitchBase imageEffect)
  {
    int count = 0;

    PropertyInfo[] properties = imageEffect.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly);
    for (int i = 0; i < properties.Length; ++i)
    {
      count += properties[i].GetCustomAttributes(typeof(RangeIntAttribute), false).Length;
      count += properties[i].GetCustomAttributes(typeof(RangeFloatAttribute), false).Length;
      count += properties[i].GetCustomAttributes(typeof(RangeVector2Attribute), false).Length;
    }

    return count;
  }

  private void DrawCustomAttributes(VideoGlitchBase imageEffect)
  {
    PropertyInfo[] properties = imageEffect.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly);
    if (properties.Length > 0)
    {
      for (int i = 0; i < properties.Length; ++i)
      {
        object[] rangeAtts = properties[i].GetCustomAttributes(typeof(EnumAttribute), false);
        if (rangeAtts.Length > 0)
        {
          EnumAttribute attb = rangeAtts[0] as EnumAttribute;
          GUILayout.BeginHorizontal();
          {
            GUILayout.Label(@" " + properties[i].GetGetMethod().Name.Replace(@"get_", string.Empty), GUILayout.Width(125));

            int value = (int)properties[i].GetValue(imageEffect, null);
            for (int j = 0; j < attb.enumNames.Count; ++j)
            {
              if (GUILayout.Button(attb.enumNames[j]) == true)
              {
                value = j;

                break;
              }
            }

            properties[i].SetValue(imageEffect, value, null);
          }
          GUILayout.EndHorizontal();
        }

        rangeAtts = properties[i].GetCustomAttributes(typeof(RangeIntAttribute), false);
        if (rangeAtts.Length > 0)
        {
          RangeIntAttribute attb = rangeAtts[0] as RangeIntAttribute;
          GUILayout.BeginHorizontal();
          {
            GUILayout.Label(@" " + properties[i].GetGetMethod().Name.Replace(@"get_", string.Empty), GUILayout.Width(125));

            int value = (int)GUILayout.HorizontalSlider((int)properties[i].GetValue(imageEffect, null), attb.min, attb.max, GUILayout.ExpandWidth(true));
            properties[i].SetValue(imageEffect, value, null);
          }
          GUILayout.EndHorizontal();
        }

        rangeAtts = properties[i].GetCustomAttributes(typeof(RangeFloatAttribute), false);
        if (rangeAtts.Length > 0)
        {
          RangeFloatAttribute attb = rangeAtts[0] as RangeFloatAttribute;
          GUILayout.BeginHorizontal();
          {
            GUILayout.Label(@" " + properties[i].GetGetMethod().Name.Replace(@"get_", string.Empty), GUILayout.Width(125));

            float value = GUILayout.HorizontalSlider((float)properties[i].GetValue(imageEffect, null), attb.min, attb.max, GUILayout.ExpandWidth(true));
            properties[i].SetValue(imageEffect, value, null);
          }
          GUILayout.EndHorizontal();
        }

        rangeAtts = properties[i].GetCustomAttributes(typeof(RangeVector2Attribute), false);
        if (rangeAtts.Length > 0)
        {
          RangeVector2Attribute attb = rangeAtts[0] as RangeVector2Attribute;

          Vector2 value = (Vector2)properties[i].GetValue(imageEffect, null);

          GUILayout.BeginHorizontal();
          {
            GUILayout.Label(@" " + properties[i].GetGetMethod().Name.Replace(@"get_", string.Empty), GUILayout.Width(125));

            value.x = GUILayout.HorizontalSlider(value.x, attb.min.x, attb.max.x, GUILayout.ExpandWidth(true));
          }
          GUILayout.EndHorizontal();

          GUILayout.BeginHorizontal();
          {
            GUILayout.Label(string.Empty, GUILayout.Width(125));

            value.y = GUILayout.HorizontalSlider(value.y, attb.min.y, attb.max.y, GUILayout.ExpandWidth(true));
          }
          GUILayout.EndHorizontal();

          properties[i].SetValue(imageEffect, value, null);
        }

        rangeAtts = properties[i].GetCustomAttributes(typeof(RangeVector3Attribute), false);
        if (rangeAtts.Length > 0)
        {
          RangeVector3Attribute attb = rangeAtts[0] as RangeVector3Attribute;
          GUILayout.BeginHorizontal();
          {
            GUILayout.Label(@" " + properties[i].GetGetMethod().Name.Replace(@"get_", string.Empty), GUILayout.Width(125));

            Vector3 value = (Vector3)properties[i].GetValue(imageEffect, null);

            value.x = GUILayout.HorizontalSlider(value.x, attb.min.x, attb.max.x, GUILayout.ExpandWidth(true));
            value.y = GUILayout.HorizontalSlider(value.y, attb.min.y, attb.max.y, GUILayout.ExpandWidth(true));
            value.z = GUILayout.HorizontalSlider(value.z, attb.min.z, attb.max.z, GUILayout.ExpandWidth(true));

            properties[i].SetValue(imageEffect, value, null);
          }
          GUILayout.EndHorizontal();
        }
      }

      if (GUILayout.Button(@"Reset") == true)
        imageEffect.ResetDefaultValues();
    }
  }

  private void EnableEffect(VideoGlitchBase imageEffect, bool enable)
  {
    imageEffect.enabled = enable;
    imageEffect.EffectMode = screenMode == ScreenModes.Fullscreen ? EffectModes.Screen : EffectModes.Layer;
  }

  private Texture2D MakeTex(int width, int height, Color col)
  {
    Color[] pix = new Color[width * height];
    for (int i = 0; i < pix.Length; ++i)
      pix[i] = col;

    Texture2D result = new Texture2D(width, height);
    result.SetPixels(pix);
    result.Apply();

    return result;
  }

  private string EffectName(string name)
  {
    name = name.Replace(@"Ibuprogames.VideoGlitchesAsset.VideoGlitch", string.Empty);
    name = System.Text.RegularExpressions.Regex.Replace(name, @"((?<=\p{Ll})\p{Lu})|((?!\A)\p{Lu}(?>\p{Ll}))", " $0");

    return name;
  }
}
