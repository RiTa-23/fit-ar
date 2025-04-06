using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResolutionSettings : MonoBehaviour
{
    public Dropdown resolutionDropdown; // Dropdown UI���A�T�C��
    private List<Resolution> resolutions = new List<Resolution>();

    void Start()
    {
        // �𑜓x���X�g���擾
        Resolution[] availableResolutions = Screen.resolutions;
        resolutions.AddRange(availableResolutions);

        // Dropdown�ɉ𑜓x��ݒ�
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Count; i++)
        {
            string option = $"{resolutions[i].width} x {resolutions[i].height} @ {resolutions[i].refreshRate}Hz";
            options.Add(option);

            // ���݂̉𑜓x�����o
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
        // �I�����ꂽ�𑜓x��K�p
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);

        // �ݒ��ۑ��i�I�v�V�����j
        PlayerPrefs.SetInt("Resolution", resolutionIndex);
    }
}

