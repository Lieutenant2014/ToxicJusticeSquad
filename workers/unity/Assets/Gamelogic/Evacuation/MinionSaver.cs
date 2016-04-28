using Improbable;
using Improbable.Evacuation;
using Improbable.Unity.Visualizer;
using UnityEngine;

class MinionSaver : MonoBehaviour
{
    [Require]
    EvacuateMinionStateWriter EvacuateMinionState;

    void OnEnable()
    {

    }

    public void EvacuateMinion(EntityId minion)
    {
        if(EvacuateMinionState != null)
        {
            EvacuateMinionState.Update.TriggerEvacuateMinionEvent(minion).FinishAndSend();
        }
    }
}

