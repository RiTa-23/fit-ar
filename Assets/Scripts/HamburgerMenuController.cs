using UnityEngine;
using UnityEngine.UI;

public class HamburgerMenuController : MonoBehaviour
{
    public GameObject menuPanel;   // ���j���[�p�l��
    public Button hamburgerButton; // �n���o�[�K�[�A�C�R���{�^��
    public float slideSpeed = 5f;  // ���j���[�̃X���C�h�X�s�[�h

    private bool isMenuOpen = false; // ���j���[���J���Ă��邩�ǂ���

    void Start()
    {
        // �n���o�[�K�[�{�^���ɃN���b�N���X�i�[��ǉ�
        hamburgerButton.onClick.AddListener(ToggleMenu);

        // ���j���[���B��
        menuPanel.transform.localPosition = new Vector3(-menuPanel.GetComponent<RectTransform>().rect.width, 0, 0);
    }

    void Update()
    {
        // ���j���[�̃X���C�h�A�j���[�V����
        if (isMenuOpen)
        {
            menuPanel.transform.localPosition = Vector3.Lerp(menuPanel.transform.localPosition, new Vector3(-300,0,0), Time.deltaTime * slideSpeed);
        }
        else
        {
            menuPanel.transform.localPosition = Vector3.Lerp(menuPanel.transform.localPosition, new Vector3(-800, 0, 0), Time.deltaTime * slideSpeed);
        }
    }

    // ���j���[���J����֐�
    void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
    }
}

