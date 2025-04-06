using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // シーンを切り替えるメソッド
    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

