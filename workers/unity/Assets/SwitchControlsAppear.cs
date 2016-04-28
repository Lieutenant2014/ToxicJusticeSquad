using UnityEngine;
using System.Collections;
using Improbable.Player.Controls;
using Improbable.Unity.Visualizer;

public class SwitchControlsAppear : MonoBehaviour
{
    [Require] private PlayerControlsStateWriter player;

    public GameObject ControlsUI;

    void Update () {
        if (Input.GetButtonDown("Back"))
        {
            ControlsUI.SetActive(!ControlsUI.activeSelf);
        }
	}
}
