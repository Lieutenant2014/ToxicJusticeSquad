using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Improbable.Corelib.Entity;
using Improbable.CoreLibrary.CoordinateRemapping;
using Improbable.Global;
using Improbable.Player.Controls;
using Improbable.Unity.Visualizer;

public class DisplayPlayerDirections : MonoBehaviour
{
    public Prefab ArrowToInstatiate;

    public GameObject Arrow1;
    public GameObject Arrow2;
    public GameObject Arrow3;

    [Require] protected PlayerControlsStateWriter PlayerControls;

    private List<MapEntity> allPlayers = new List<MapEntity>();

    void OnEnable()
    {
        Arrow1.SetActive(true);
        Arrow2.SetActive(true);
        Arrow3.SetActive(true);
    }

    void Update()
    {
        Debug.Log("Playercount " + allPlayers.Count);
        if (allPlayers.Count == 0)
        {
            Arrow1.SetActive(false);
            Arrow2.SetActive(false);
            Arrow3.SetActive(false);
        }
        else if (allPlayers.Count == 1)
        {
            Arrow1.SetActive(true);
            Arrow2.SetActive(false);                                        
            Arrow3.SetActive(false);
            Arrow1.transform.rotation = Quaternion.RotateTowards(Arrow1.transform.rotation, Quaternion.LookRotation(allPlayers[0].Coordinates.RemapGlobalToUnityVector() - transform.position), 3);
        } else if (allPlayers.Count == 2)
        {
            Arrow1.SetActive(true);
            Arrow2.SetActive(true);
            Arrow3.SetActive(false);
            Arrow1.transform.rotation = Quaternion.RotateTowards(Arrow1.transform.rotation, Quaternion.LookRotation(allPlayers[0].Coordinates.RemapGlobalToUnityVector() - transform.position), 3);
            Arrow2.transform.rotation = Quaternion.RotateTowards(Arrow2.transform.rotation, Quaternion.LookRotation(allPlayers[1].Coordinates.RemapGlobalToUnityVector() - transform.position), 3);
        }
        else
        {
            Arrow1.SetActive(true);
            Arrow2.SetActive(true);
            Arrow3.SetActive(true);
            Arrow1.transform.rotation = Quaternion.RotateTowards(Arrow1.transform.rotation, Quaternion.LookRotation(allPlayers[0].Coordinates.RemapGlobalToUnityVector() - transform.position), 3);
            Arrow2.transform.rotation = Quaternion.RotateTowards(Arrow2.transform.rotation, Quaternion.LookRotation(allPlayers[1].Coordinates.RemapGlobalToUnityVector() - transform.position), 3);
            Arrow3.transform.rotation = Quaternion.RotateTowards(Arrow3.transform.rotation, Quaternion.LookRotation(allPlayers[2].Coordinates.RemapGlobalToUnityVector() - transform.position), 3);
        }
    }

    public void UpdatePlayerLocations(List<MapEntity> playersPos)
    {
        RenderArrows(playersPos);
    }

    private void RenderArrows(List<MapEntity> players)
    {
        FindClosestPlayer(players);
    }

    private void FindClosestPlayer(List<MapEntity> players)
    {
        float minDistanceSoFar = 0;
        List<MapEntity> filteredList = players.Where(x => x.Id != gameObject.EntityId()).ToList();
        filteredList.Sort(new CompareMapEntity(transform.position));
        allPlayers = filteredList;
    }

    public class CompareMapEntity : IComparer<MapEntity>
    {
        private Vector3 currentPos = Vector3.zero;

        public CompareMapEntity(Vector3 pos)
        {
            currentPos = pos;
        }

        public int Compare(MapEntity x, MapEntity y)
        {
            var xDiff = Mathf.Abs(Vector3.Distance(x.Coordinates.RemapGlobalToUnityVector(), currentPos));
            var yDiff = Mathf.Abs(Vector3.Distance(y.Coordinates.RemapGlobalToUnityVector(), currentPos));
            return (int) Mathf.Round(xDiff - yDiff);

        }
    }
}
