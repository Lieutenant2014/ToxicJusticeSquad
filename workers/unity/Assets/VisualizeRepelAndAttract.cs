using UnityEngine;
using System.Collections;
using DG.Tweening;
using Improbable.Player.Controls;
using Improbable.Unity.Visualizer;

public class VisualizeRepelAndAttract : MonoBehaviour {

    [Require]
    private PlayerControlsStateReader PlayerControls;

    public GameObject Sphere;

    public float Range = 40.0f;

    void OnEnable()
    {
        PlayerControls.AttractMinions += OnAttractMinions;
        PlayerControls.RepelMinions += OnRepelMinions;
        Sphere.SetActive(false);
    }

    private void OnRepelMinions(RepelMinions obj)
    {
        Sphere.transform.localScale = new Vector3(0f, 0f, 0f);
        Sphere.SetActive(true);
        Sphere.transform.DOScale(new UnityEngine.Vector3(Range, Range, Range), 0.5f);
        Invoke("DisableSphere", 0.5f);
    }

    private void OnAttractMinions(AttractMinions obj)
    {
        Sphere.transform.localScale = new Vector3(Range, Range, Range);
        Sphere.SetActive(true);
        Sphere.transform.DOScale(new UnityEngine.Vector3(0f, 0f, 0f), 0.5f);
        Invoke("DisableSphere", 0.5f);
    }

    void DisableSphere()
    {
        Sphere.SetActive(false);
    }

}
