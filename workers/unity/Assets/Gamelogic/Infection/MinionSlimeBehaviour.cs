using Improbable.Infection;
using Improbable.Life;
using Improbable.Unity.Visualizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class MinionSlimeBehaviour : MonoBehaviour
    {
    [Require]
    SlimeStateWriter SlimeState;

    [Require]
    LifeStateReader LifeState;

    void OnEnable()
    {
        InvokeRepeating("CheckForCleanup", 1.0f, 1.0f);
    }

    void OnDestroy()
    {
        CancelInvoke();
    }

    void CheckForCleanup()
    {
        if (LifeState != null && !LifeState.Alive)
        {
            if(SlimeBehaviour.GetSudCount(transform.position, 3.0f) > 0)
            {
                SlimeState.Update.TriggerCleanSlimeEvent().FinishAndSend();
            }
        }
    }
}

