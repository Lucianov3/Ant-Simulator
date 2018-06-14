using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScripts : MonoBehaviour
{
    public void SceneLoad(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void switchToUnderWorldCamera()
    {
        GameManager.SwitchToUnderWorldCamera();
    }
    public void switchToOverWorldCamera()
    {
        GameManager.SwitchToOverWorldCamera();
    }

    public void ExitScene()
    {
        Debug.Log("Cya");
        Application.Quit();
    }

    public void optionsButton()
    {
    }

    public void creditsButton()
    {
    }
}