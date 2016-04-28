using UnityEngine;
using System.Collections;

public class ShowControls : MonoBehaviour
{

    public GameObject ControlsMenu;
    public GameObject MainMenu;

    public void GotoMainGame()
    {
        Debug.Log("Goto main scene!");
    }

    public void EnableControlsMenu()
    {
        ControlsMenu.SetActive(true);
        MainMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Back"))
        {
            MainMenu.SetActive(true);
            MainMenu.SetActive(false);
        }
    }

}
