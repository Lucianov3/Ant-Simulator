using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScripts : MonoBehaviour
{
    public void SceneLoad(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void switchCameras()
    {
        GameManager.SwitchCamera();
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