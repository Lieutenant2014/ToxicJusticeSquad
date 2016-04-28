using UnityEngine;
using System.Collections;
using Improbable.CoreLibrary.CoordinateRemapping;
using Improbable.Player.Controls;
using Improbable.Unity.Common.Core.Math;
using Improbable.Unity.Visualizer;
using UnityEngine.UI;

public class PlaceObjectBehaviour : MonoBehaviour
{
    [Require] public PlacementStateWriter PlacementState;
    public GameObject PlacementSphere;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (IsPlacing())
	    {
            PlacementSphere.SetActive(true);
            PlacementSphere.transform.localPosition = new Vector3(Input.GetAxis("BuildHoriz") * 5, -1, Input.GetAxis("BuildVert") * 5);
	        if (Input.GetButtonDown("Build"))
	        {
	            PlacementState.Update.TriggerPlaceObjectEvent(PlacementSphere.transform.position.RemapUnityVectorToGlobalCoordinates(), 1).FinishAndSend();
	        }
	    }
	    else
	    {
	        PlacementSphere.SetActive(false);
	    }
	}

    private static bool IsPlacing()
    {
        return false;
    }
}
