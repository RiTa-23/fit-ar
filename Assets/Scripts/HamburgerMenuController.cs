using UnityEngine;
using UnityEngine.UI;

public class HamburgerMenuController : MonoBehaviour
{
    public GameObject menuPanel;   // メニューパネル
    public Button hamburgerButton; // ハンバーガーアイコンボタン
    public float slideSpeed = 5f;  // メニューのスライドスピード

    private bool isMenuOpen = false; // メニューが開いているかどうか

    void Start()
    {
        // ハンバーガーボタンにクリックリスナーを追加
        hamburgerButton.onClick.AddListener(ToggleMenu);

        // メニューを隠す
        menuPanel.transform.localPosition = new Vector3(-menuPanel.GetComponent<RectTransform>().rect.width, 0, 0);
    }

    void Update()
    {
        // メニューのスライドアニメーション
        if (isMenuOpen)
        {
            menuPanel.transform.localPosition = Vector3.Lerp(menuPanel.transform.localPosition, new Vector3(-300,0,0), Time.deltaTime * slideSpeed);
        }
        else
        {
            menuPanel.transform.localPosition = Vector3.Lerp(menuPanel.transform.localPosition, new Vector3(-800, 0, 0), Time.deltaTime * slideSpeed);
        }
    }

    // メニューを開閉する関数
    void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
    }
}

