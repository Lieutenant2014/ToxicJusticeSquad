using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Improbable.CoreLibrary.CoordinateRemapping;
using Improbable.Spawner;
using Improbable.Unity.Visualizer;

public class SpawnMinions : MonoBehaviour
{
    [Require] private SpawnSateWriter spawnSate;
    private List<Collider> ObjectsInTrigger = new List<Collider>();
    public Transform spawnPoint;

    void OnTriggerEnter(Collider other)
    {
        if (!ObjectsInTrigger.Contains(other))
        {
            ObjectsInTrigger.Add(other);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (ObjectsInTrigger.Contains(other))
        {
            ObjectsInTrigger.Remove(other);
        }
    }

    // Use this for initialization
	void OnEnable() {
	    InvokeRepeating("AttemptToSpawnEntity", 0f, 1f);
	}

    void AttemptToSpawnEntity()
    {
        if (ObjectsInTrigger.Count == 0)
        {
            spawnSate.Update.TriggerSpawnEntityEvent(spawnPoint.position.RemapUnityVectorToGlobalCoordinates()).FinishAndSend();
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
