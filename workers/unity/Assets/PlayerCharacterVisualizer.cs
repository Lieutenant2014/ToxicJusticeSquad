using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Improbable.Person;
using Improbable.Unity.Visualizer;

public class PlayerCharacterVisualizer : MonoBehaviour
{
    public List<Material> materials;
    public List<GameObject> hats;

    public GameObject BaseMesh;

    [Require] PersonStateReader personState;

    void OnEnable()
    {
        BaseMesh.GetComponent<Renderer>().material = materials[personState.PersonType];
        hats[personState.PersonType].SetActive(true);
        hats[personState.PersonType].GetComponent<Renderer>().material = materials[personState.PersonType];
    }
}
