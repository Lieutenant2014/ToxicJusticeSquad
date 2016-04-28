using Improbable.Infection;
using Improbable.Unity.Visualizer;
using UnityEngine;

class InfectionVisualizer : MonoBehaviour
{
    public SkinnedMeshRenderer mesh;

    [Require]
    InfectionStateReader InfectionState;

    void OnEnable()
    {
        InfectionState.InfectedUpdated += InfectionState_InfectedUpdated;
    }

    private void InfectionState_InfectedUpdated(bool newIsInfected)
    {
        GetComponent<Infection>().isInfected = newIsInfected;
        if(mesh != null)
        {
            if (newIsInfected)
            {
                mesh.material.color = Color.green;
            }
            else
            {
                mesh.material.color = Color.white;
            }
        } 
    }
}

