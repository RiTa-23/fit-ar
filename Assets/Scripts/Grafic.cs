using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResolutionSettings : MonoBehaviour
{
    public Dropdown resolutionDropdown; // Dropdown UIをアサイン
    private List<Resolution> resolutions = new List<Resolution>();

    void Start()
    {
        // 解像度リストを取得
        Resolution[] availableResolutions = Screen.resolutions;
        resolutions.AddRange(availableResolutions);

        // Dropdownに解像度を設定
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Count; i++)
        {
            string option = $"{resolutions[i].width} x {resolutions[i].height} @ {resolutions[i].refreshRate}Hz";
            options.Add(option);

            // 現在の解像度を検出
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height &&
                resolutions[i].refreshRate == Screen.currentResolution.refreshRate)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        // 選択された解像度を適用
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);

        // 設定を保存（オプション）
        PlayerPrefs.SetInt("Resolution", resolutionIndex);
    }
}

