using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider; // Unity エディタで設定
    public AudioSource audioSource; // 音源を指定

    void Start()
    {
        // 保存された音量を読み込んでスライダーに反映
        float savedVolume = PlayerPrefs.GetFloat("Volume", 1.0f);
        volumeSlider.value = savedVolume;
        audioSource.volume = savedVolume;
    }

    public void OnVolumeChanged(float value)
    {
        // スライダーの値をオーディオソースの音量に適用
        audioSource.volume = value;

        // 設定を保存
        PlayerPrefs.SetFloat("Volume", value);
    }
}


