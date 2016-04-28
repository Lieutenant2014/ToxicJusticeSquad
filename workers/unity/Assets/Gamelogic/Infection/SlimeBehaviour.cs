using Improbable.Infection;
using Improbable.Unity.Visualizer;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Improbable.CoreLibrary.CoordinateRemapping;

class SlimeBehaviour : MonoBehaviour
{
    [Require]
    SlimeStateWriter SlimeState;
    
    void OnEnable()
    {
        InvokeRepeating("SpreadSlime", 0.0f, 0.3f);
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    void SpreadSlime()
    {
        var currentSize = SlimeState.SlimeSize;
        GetComponent<Infection>().size = currentSize;
        var sudCount = GetSudCount(transform.position, SlimeState.SlimeSize + 4.0f);
        if (sudCount > 0)
        {
            var newSize = currentSize - 0.3f * sudCount;
            SlimeState.Update.SlimeSize(newSize).FinishAndSend();
            if(newSize <= 0.3f)
            {
                SlimeState.Update.TriggerCleanSlimeEvent().FinishAndSend();
            }
        }
        else if(currentSize < 8)
        {
            SlimeState.Update.SlimeSize(currentSize + 0.1f).FinishAndSend();
        }
        else
        {
            var randomPos = Random.insideUnitCircle.normalized * 8.0f;
            var proposedPosition = new Vector3(randomPos.x + transform.position.x, -1.0f, randomPos.y + transform.position.z);
            if (GetInfections(proposedPosition, 6.0f).Count() < 1)
            {
                RaycastHit raycastHit;
                if(Physics.Raycast(proposedPosition + Vector3.up * 100.0f, Vector3.down, out raycastHit))
                {
                    var name = raycastHit.collider.gameObject.name;
                    if(name.Contains("Road") || name.Contains("Four"))
                    {
                        SlimeState.Update.TriggerSpreadSlimeEvent(proposedPosition.RemapUnityVectorToGlobalCoordinates()).FinishAndSend();
                    }
                   
                }   
            }
        }
    }

    public static Collider[] GetInfections(Vector3 position, float range)
    {
        var colliders = Physics.OverlapSphere(position, range);
        var steerables =
            colliders.Where(someCollider => someCollider.gameObject.GetComponent<Infection>() != null);
        return steerables.ToArray();
    }

    public static int GetSudCount(Vector3 position, float range)
    {
        var colliders = Physics.OverlapSphere(position, range);
        return colliders.Where(someCollider => someCollider.gameObject.GetComponent<Sud>() != null).Count();
    }
}