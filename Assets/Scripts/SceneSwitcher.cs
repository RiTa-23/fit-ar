using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // �V�[����؂�ւ��郁�\�b�h
    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

