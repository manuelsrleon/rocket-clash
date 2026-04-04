using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private TMP_Dropdown refreshRateDropdown;
    [SerializeField] private TMP_Dropdown fullscreenModeDropdown;
    [SerializeField] private Toggle vsyncToggle;

    [Header("Settings")]
    [SerializeField] private bool applyOnAwake = true;

    private Resolution[] availableResolutions;
    private List<int> availableRefreshRates;
    private const string SettingsKey = "GameSettings";

    private void Awake()
    {
        InitializeResolutions();
        InitializeRefreshRates();
        InitializeFullscreenModes();

        if (applyOnAwake)
        {
            LoadSettings();
        }
    }

    private void Start()
    {
        SetupUIListeners();
    }

    private void InitializeResolutions()
    {
        if (resolutionDropdown == null) return;

        availableResolutions = Screen.resolutions
            .GroupBy(r => new { r.width, r.height })
            .Select(g => g.First())
            .ToArray();

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < availableResolutions.Length; i++)
        {
            string option = $"{availableResolutions[i].width} x {availableResolutions[i].height}";
            options.Add(option);

            if (availableResolutions[i].width == Screen.currentResolution.width &&
                availableResolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    private void InitializeRefreshRates()
    {
        if (refreshRateDropdown == null) return;

        availableRefreshRates = Screen.resolutions
            .Select(r => (int)r.refreshRateRatio.value)
            .Distinct()
            .OrderBy(r => r)
            .ToList();

        refreshRateDropdown.ClearOptions();

        List<string> options = availableRefreshRates.Select(r => $"{r} Hz").ToList();
        refreshRateDropdown.AddOptions(options);

        int currentRefreshRate = (int)Screen.currentResolution.refreshRateRatio.value;
        int currentIndex = availableRefreshRates.IndexOf(currentRefreshRate);
        refreshRateDropdown.value = currentIndex >= 0 ? currentIndex : 0;
        refreshRateDropdown.RefreshShownValue();
    }

    private void InitializeFullscreenModes()
    {
        if (fullscreenModeDropdown == null) return;

        fullscreenModeDropdown.ClearOptions();
        List<string> options = new List<string> { "Fullscreen", "Windowed", "Borderless" };
        fullscreenModeDropdown.AddOptions(options);

        fullscreenModeDropdown.value = Screen.fullScreenMode switch
        {
            FullScreenMode.ExclusiveFullScreen => 0,
            FullScreenMode.Windowed => 1,
            FullScreenMode.FullScreenWindow => 2,
            _ => 0
        };
        fullscreenModeDropdown.RefreshShownValue();
    }

    private void SetupUIListeners()
    {
        if (resolutionDropdown != null)
            resolutionDropdown.onValueChanged.AddListener(delegate { OnSettingsChanged(); });

        if (refreshRateDropdown != null)
            refreshRateDropdown.onValueChanged.AddListener(delegate { OnSettingsChanged(); });

        if (fullscreenModeDropdown != null)
            fullscreenModeDropdown.onValueChanged.AddListener(delegate { OnSettingsChanged(); });

        if (vsyncToggle != null)
            vsyncToggle.onValueChanged.AddListener(delegate { OnSettingsChanged(); });
    }

    private void OnSettingsChanged()
    {
        ApplySettings();
        SaveSettings();
    }

    public void ApplySettings()
    {
        if (resolutionDropdown != null && resolutionDropdown.value < availableResolutions.Length)
        {
            Resolution resolution = availableResolutions[resolutionDropdown.value];
            int refreshRate = 0;

            if (refreshRateDropdown != null && refreshRateDropdown.value < availableRefreshRates.Count)
            {
                refreshRate = availableRefreshRates[refreshRateDropdown.value];
            }

            FullScreenMode fullscreenMode = FullScreenMode.ExclusiveFullScreen;
            if (fullscreenModeDropdown != null)
            {
                fullscreenMode = fullscreenModeDropdown.value switch
                {
                    0 => FullScreenMode.ExclusiveFullScreen,
                    1 => FullScreenMode.Windowed,
                    2 => FullScreenMode.FullScreenWindow,
                    _ => FullScreenMode.ExclusiveFullScreen
                };
            }

            if (refreshRate > 0)
            {
                Screen.SetResolution(resolution.width, resolution.height, fullscreenMode, refreshRate);
            }
            else
            {
                Screen.SetResolution(resolution.width, resolution.height, fullscreenMode);
            }
        }

        if (vsyncToggle != null)
        {
            QualitySettings.vSyncCount = vsyncToggle.isOn ? 1 : 0;
        }
    }

    public void SaveSettings()
    {
        GameSettings settings = new GameSettings
        {
            resolutionIndex = resolutionDropdown != null ? resolutionDropdown.value : 0,
            refreshRateIndex = refreshRateDropdown != null ? refreshRateDropdown.value : 0,
            fullscreenModeIndex = fullscreenModeDropdown != null ? fullscreenModeDropdown.value : 0,
            vsyncEnabled = vsyncToggle != null && vsyncToggle.isOn
        };

        string json = JsonUtility.ToJson(settings);
        PlayerPrefs.SetString(SettingsKey, json);
        PlayerPrefs.Save();
    }

    public void LoadSettings()
    {
        if (!PlayerPrefs.HasKey(SettingsKey))
        {
            ApplySettings();
            return;
        }

        try
        {
            string json = PlayerPrefs.GetString(SettingsKey);
            GameSettings settings = JsonUtility.FromJson<GameSettings>(json);

            if (resolutionDropdown != null && settings.resolutionIndex < availableResolutions.Length)
            {
                resolutionDropdown.value = settings.resolutionIndex;
            }

            if (refreshRateDropdown != null && settings.refreshRateIndex < availableRefreshRates.Count)
            {
                refreshRateDropdown.value = settings.refreshRateIndex;
            }

            if (fullscreenModeDropdown != null)
            {
                fullscreenModeDropdown.value = settings.fullscreenModeIndex;
            }

            if (vsyncToggle != null)
            {
                vsyncToggle.isOn = settings.vsyncEnabled;
            }

            ApplySettings();
        }
        catch (System.Exception e)
        {
            Debug.LogWarning($"Error loading settings: {e.Message}. Resetting to defaults.");
            PlayerPrefs.DeleteKey(SettingsKey);
            PlayerPrefs.Save();
            ApplySettings();
        }
    }

    public void ResetToDefaults()
    {
        PlayerPrefs.DeleteKey(SettingsKey);

        if (resolutionDropdown != null)
            resolutionDropdown.value = availableResolutions.Length - 1;

        if (refreshRateDropdown != null)
            refreshRateDropdown.value = availableRefreshRates.Count - 1;

        if (fullscreenModeDropdown != null)
            fullscreenModeDropdown.value = 0;

        if (vsyncToggle != null)
            vsyncToggle.isOn = true;

        ApplySettings();
    }

    private void OnDestroy()
    {
        if (resolutionDropdown != null)
            resolutionDropdown.onValueChanged.RemoveAllListeners();

        if (refreshRateDropdown != null)
            refreshRateDropdown.onValueChanged.RemoveAllListeners();

        if (fullscreenModeDropdown != null)
            fullscreenModeDropdown.onValueChanged.RemoveAllListeners();

        if (vsyncToggle != null)
            vsyncToggle.onValueChanged.RemoveAllListeners();
    }
}

[System.Serializable]
public class GameSettings
{
    public int resolutionIndex;
    public int refreshRateIndex;
    public int fullscreenModeIndex;
    public bool vsyncEnabled;
}
