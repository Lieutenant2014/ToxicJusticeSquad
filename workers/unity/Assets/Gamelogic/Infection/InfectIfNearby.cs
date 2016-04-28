
using Assets.Gamelogic.Navigation;
using Improbable.Infection;
using Improbable.Unity.Visualizer;
using UnityEngine;

class InfectIfNearby : MonoBehaviour
{
    [Require]
    InfectionStateWriter InfectionState;

    void OnEnable()
    {
        InvokeRepeating("DoInfectIfNearby", 1.0f, 1.0f);
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    void DoInfectIfNearby()
    {
        if(!InfectionState.Infected && Random.value > 0.5 && GetInfectionCount(transform.position, 1.0f) > 0)
        {
            InfectionState.Update.Infected(true).FinishAndSend();
        }
        if (!InfectionState.Infected && GetSlimeCount(transform.position, 5.0f) > 0)
        {
            InfectionState.Update.Infected(true).FinishAndSend();
        }
        if (InfectionState.Infected && SlimeBehaviour.GetSudCount(transform.position, 4.0f) > 0)
        {
            InfectionState.Update.Infected(false).FinishAndSend();
        }
    }

    public static int GetInfectionCount(Vector3 position, float range)
    {
        var colliders = Physics.OverlapSphere(position, range);
        int count = 0;
        foreach(var collider in colliders)
        {
            if(collider.gameObject.GetComponent<Infection>() != null && collider.gameObject.GetComponent<Infection>().isInfected)
            {
                count += 1;
            }
        }
        return count;
    }
    public static int GetSlimeCount(Vector3 position, float range)
    {
        var colliders = Physics.OverlapSphere(position, range);
        int count = 0;
        foreach (var collider in colliders)
        {
            if (collider.gameObject.name.Contains("Slime"))
            {
                count += 1;
            }
        }
        return count;
    }
}
