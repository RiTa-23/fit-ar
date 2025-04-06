using UnityEngine;
using UnityEngine.UI;

public class CameraSwitch : MonoBehaviour
{
    public Button switchButton; // ボタン
    public RawImage rawImage;   // カメラ映像を表示するRawImage
    private WebCamTexture webCamTexture; // WebCamTexture

    void Start()
    {
        // ボタンにクリックイベントを追加
        switchButton.onClick.AddListener(OnButtonClick);

        // カメラを初期化
        webCamTexture = new WebCamTexture();

        // RawImageにカメラ映像を表示
        rawImage.texture = webCamTexture;
        rawImage.material.mainTexture = webCamTexture;
    }

    void OnButtonClick()
    {
        // カメラの映像を開始または停止
        if (webCamTexture.isPlaying)
        {
            webCamTexture.Stop(); // カメラ停止
        }
        else
        {
            webCamTexture.Play(); // カメラ開始
        }
    }
}

