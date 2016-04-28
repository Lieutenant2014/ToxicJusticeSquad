using UnityEngine;
using System.Collections;
using Improbable.CoreLibrary.CoordinateRemapping;
using Improbable.Player.Controls;
using Improbable.Unity.Common.Core.Math;
using Improbable.Unity.Visualizer;

public class SpawnSudWhenFiring : MonoBehaviour
{
    public Transform SudPoint;

    [Require]
    protected PlayerControlsStateWriter PlayerControls;

    void OnEnable()
    {
        InvokeRepeating("ShootSuds", 0, 0.3f);
    }

    void ShootSuds()
    {
        if (FiringGun())
        {
            PlayerControls.Update.TriggerSpawnSud(SudPoint.position.RemapUnityVectorToGlobalCoordinates()).FinishAndSend();
        }
    }

    private static bool FiringGun()
    {
        var rightStickHoriz = Input.GetAxis("BuildHoriz");
        var rightStickVert = Input.GetAxis("BuildVert");
        return rightStickHoriz != 0 || rightStickVert != 0;
    }
}
