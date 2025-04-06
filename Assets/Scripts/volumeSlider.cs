using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider; // Unity �G�f�B�^�Őݒ�
    public AudioSource audioSource; // �������w��

    void Start()
    {
        // �ۑ����ꂽ���ʂ�ǂݍ���ŃX���C�_�[�ɔ��f
        float savedVolume = PlayerPrefs.GetFloat("Volume", 1.0f);
        volumeSlider.value = savedVolume;
        audioSource.volume = savedVolume;
    }

    public void OnVolumeChanged(float value)
    {
        // �X���C�_�[�̒l���I�[�f�B�I�\�[�X�̉��ʂɓK�p
        audioSource.volume = value;

        // �ݒ��ۑ�
        PlayerPrefs.SetFloat("Volume", value);
    }
}


