using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScripts : MonoBehaviour
{
    public void SceneLoad(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SwitchToUnderWorldCamera()
    {
        GameManager.SwitchToUnderWorldCamera();
    }

    public void SwitchToOverWorldCamera()
    {
        GameManager.SwitchToOverWorldCamera();
    }

    public void ExitScene()
    {
        Debug.Log("Cya");
        Application.Quit();
    }

    public void OptionsButton()
    {
        transform.parent.parent.GetComponent<Animator>().SetTrigger("MainMenuDissapearTrigger");
    }

    public void OptionsBackButton()
    {
        transform.parent.parent.GetComponent<Animator>().SetTrigger("OptionDissapearTrigger");
    }

    public void CreditsButton()
    {
        transform.parent.parent.GetComponent<Animator>().SetTrigger("CreditsTrigger");
    }

    public void CreditsBackButton()
    {
        transform.parent.parent.GetComponent<Animator>().SetTrigger("CreditsDissapearTrigger");
    }

    public void StartGameButton()
    {
        transform.parent.parent.GetComponent<Animator>().SetTrigger("StartGameTrigger");
    }

    public void QuitGameButton()
    {
        transform.parent.parent.GetComponent<Animator>().SetTrigger("QuitGameTrigger");
    }

    public void CloseAntStatWindowButton()
    {
        GetComponent<Animator>().SetTrigger("CloseWindowTrigger");
    }

    public void OpenInGameMenu()
    {
        transform.parent.parent.GetComponent<Animator>().SetTrigger("OpenInGameMenuTrigger");
    }

    public void CloseInGameMenu()
    {
        transform.parent.parent.GetComponent<Animator>().SetTrigger("CloseInGameMenuTrigger");
    }

    public void CloseButton()
    {
        Destroy(transform.parent.gameObject);
        StatScript.statScreenIsActive = false;
    }
}