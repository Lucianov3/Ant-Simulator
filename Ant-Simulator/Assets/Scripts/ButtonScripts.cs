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
        transform.parent.parent.GetComponent<Animator>().SetTrigger("MainMenuDissapearTrigger");
    }

    public void optionsBackButton()
    {
        transform.parent.parent.GetComponent<Animator>().SetTrigger("OptionDissapearTrigger");
    }

    public void creditsButton()
    {
        transform.parent.parent.GetComponent<Animator>().SetTrigger("CreditsTrigger");
    }

    public void creditsBackButton()
    {
        transform.parent.parent.GetComponent<Animator>().SetTrigger("CreditsDissapearTrigger");
    }

    public void startGameButton()
    {
        transform.parent.parent.GetComponent<Animator>().SetTrigger("StartGameTrigger");
    }

    public void quitGameButton()
    {
        transform.parent.parent.GetComponent<Animator>().SetTrigger("QuitGameTrigger");
    }
}