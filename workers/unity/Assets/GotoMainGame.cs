using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GotoMainGame : MonoBehaviour
{
    public Text StartText;
    public GameObject GameEntry;
    public GameObject MainMenuAssets;
    public GameObject MainMenuUI;
    public GameObject Tutorial;
    public GameObject TutorialButton;
    public GameObject MainMenuCamera;

    public void ShowTutorial()
    {
        MainMenuAssets.SetActive(false);
        MainMenuUI.SetActive(false);
        Tutorial.SetActive(true);
        GetComponent<EventSystem>().SetSelectedGameObject(TutorialButton);
    }

    public void GameStart()
    {
        StartText.text = "Connecting";
        GameEntry.SetActive(true);
        Invoke("DisableTutorial", 2f);
    }

    public void DisableTutorial()
    {
        MainMenuCamera.SetActive(false);
        Tutorial.SetActive(false);
    }
}
