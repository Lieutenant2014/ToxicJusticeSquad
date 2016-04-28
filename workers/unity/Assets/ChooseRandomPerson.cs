using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Improbable.Infection;
using Improbable.Person;
using Improbable.Unity.Visualizer;

public class ChooseRandomPerson : MonoBehaviour
{
    [Require] private PersonStateReader personState;
    [Require]
    InfectionStateReader InfectionState;


    public List<Material> materials;
    public List<GameObject> hats;

    public GameObject BaseMesh;

    void OnEnable()
    {
        var baseRenderer = BaseMesh.GetComponent<Renderer>();
        baseRenderer.material = materials[personState.PersonType];
        hats[personState.PersonType].SetActive(true);
        var renderer = hats[personState.PersonType].GetComponent<Renderer>();
        if (renderer)
        {
            renderer.material = materials[personState.PersonType];
        }


        GetComponent<Infection>().isInfected = InfectionState.Infected;
        if (BaseMesh != null)
        {
            if (InfectionState.Infected)
            {
                baseRenderer.material.color = Color.green;
            }
            else
            {
                baseRenderer.material.color = Color.white;
            }
        }
    }
}
