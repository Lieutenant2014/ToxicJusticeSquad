using UnityEngine;
using Improbable.Unity.Visualizer;
using Improbable.Infection;

public class SlimeVisualizer : MonoBehaviour {

    public Transform slime;

    [Require]
    SlimeStateReader SlimeState;
	
    void OnEnable()
    {
        SlimeState.SlimeSizeUpdated += SlimeState_SlimeSizeUpdated;
    }

    private void SlimeState_SlimeSizeUpdated(float obj)
    {
        slime.localScale = new Vector3(obj, obj / 10.0f, obj);
    }
}
