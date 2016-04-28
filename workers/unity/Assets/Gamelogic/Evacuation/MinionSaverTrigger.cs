
using UnityEngine;

class MinionSaverTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.IsEntityObject())
        {
            if (transform.parent.GetComponent<MinionSaver>() != null)
            {
                if (c.gameObject.name.Contains("Minion") && 
                    c.gameObject.GetComponent<Infection>() != null && 
                    !c.gameObject.GetComponent<Infection>().isInfected)
                {
                    Debug.Log("Trying to save " + c.gameObject.name);
                    transform.parent.GetComponent<MinionSaver>().EvacuateMinion(c.gameObject.EntityId());
                }
            }    
        }
    }
}

