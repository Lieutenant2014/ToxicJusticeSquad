using UnityEngine;
using System.Collections;
using Improbable.Player.Controls;
using Improbable.Unity.Visualizer;

public class PlayerHealDirection : MonoBehaviour
{
    [Require]
    protected PlayerControlsStateReader PlayerControls;

    public Transform SpineTransform;
    public Transform CameraRoot;
    public GameObject VFX;

    // Update is called once per frame
    void LateUpdate()
    {
        var rightStickHoriz = (float) PlayerControls.FireDirection.X;
        var rightStickVert = (float) PlayerControls.FireDirection.Z;
        if (FiringGun(rightStickHoriz, rightStickVert))
        {
            VFX.SetActive(true);
        }
        else
        {
            VFX.SetActive(false);
            return;
        }

        Vector3 rotationAxis = CameraRoot.rotation * new Vector3(-rightStickHoriz, 0 , rightStickVert);
        var angleAxis = Quaternion.LookRotation(rotationAxis);
        var quaternion = Quaternion.Euler(Vector3.forward*-90);
        SpineTransform.rotation = angleAxis * quaternion;
    }

    private static bool FiringGun(double rightStickHoriz, double rightStickVert)
    {
        return rightStickHoriz != 0 || rightStickVert != 0;
    }
}
