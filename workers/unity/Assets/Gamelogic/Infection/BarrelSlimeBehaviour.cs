using Improbable.CoreLibrary.CoordinateRemapping;
using Improbable.Infection;
using Improbable.Life;
using Improbable.Unity.Visualizer;
using UnityEngine;

class BarrelSlimeBehaviour : MonoBehaviour
{
    [Require]
    SlimeStateWriter SlimeState;

    public bool initiallyLeaked = false;
    public Vector3 lastPosition;

    void OnEnable()
    {
        initiallyLeaked = false;
        lastPosition = transform.position;
        InvokeRepeating("CheckForCleanup", 0.0f, 1.0f);
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    void CheckForCleanup()
    {
        if(Vector3.Distance(transform.position, lastPosition) < 0.1f && !initiallyLeaked)
        {
            initiallyLeaked = true;
            var slimePosition = new Vector3(transform.position.x, -1.0f, transform.position.z);
            SlimeState.Update.TriggerSpreadSlimeEvent(slimePosition.RemapUnityVectorToGlobalCoordinates()).FinishAndSend();
        }

        if (SlimeBehaviour.GetSudCount(transform.position, 3.0f) > 0)
        {
            SlimeState.Update.TriggerCleanSlimeEvent().FinishAndSend();
        }
    }
}

