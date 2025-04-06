using UnityEngine;
using UnityEngine.UI;

public class CameraSwitch : MonoBehaviour
{
    public Button switchButton; // �{�^��
    public RawImage rawImage;   // �J�����f����\������RawImage
    private WebCamTexture webCamTexture; // WebCamTexture

    void Start()
    {
        // �{�^���ɃN���b�N�C�x���g��ǉ�
        switchButton.onClick.AddListener(OnButtonClick);

        // �J������������
        webCamTexture = new WebCamTexture();

        // RawImage�ɃJ�����f����\��
        rawImage.texture = webCamTexture;
        rawImage.material.mainTexture = webCamTexture;
    }

    void OnButtonClick()
    {
        // �J�����̉f�����J�n�܂��͒�~
        if (webCamTexture.isPlaying)
        {
            webCamTexture.Stop(); // �J������~
        }
        else
        {
            webCamTexture.Play(); // �J�����J�n
        }
    }
}

