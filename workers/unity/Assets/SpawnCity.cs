using UnityEngine;

public class SpawnCity : MonoBehaviour {

    public GameObject BuildingTile;
    public GameObject RoadSection;
    public GameObject RoadCorner;
    public GameObject SeaWall;
    public GameObject RoadTriWay;
    public GameObject RoadFourWay;

    public float RoadWidth;
    public float BuildingTileSize;

    public int GridSize;

    public void Start()
    {
        Debug.Log("Activate");

        for (var i = transform.childCount - 1; i >= 0; i--)
        {
            // objectA is not the attached GameObject, so you can do all your checks with it.
            var child = transform.GetChild(i);
            GameObject.DestroyImmediate(child.gameObject);
        }

        for (int x = -GridSize; x < GridSize; x++)
        {
            for (int z = -GridSize; z < GridSize; z++)
            {
                Vector3 buildingPos = new Vector3(x * (BuildingTileSize + RoadWidth), 0, z * (BuildingTileSize + RoadWidth));
                var buildingGameObject = Instantiate(BuildingTile, buildingPos, Quaternion.identity) as GameObject;
                buildingGameObject.transform.parent = transform;

                Vector3 northRoadPos = new Vector3(x * (BuildingTileSize + RoadWidth), 0, z * (BuildingTileSize + RoadWidth) + RoadWidth * 1.5f);
                var northRoadGameObject = Instantiate(RoadSection, northRoadPos, Quaternion.identity) as GameObject;
                northRoadGameObject.transform.parent = transform;

                if (z == GridSize - 1)
                {
                    var northRoadSeaWallGameObject = Instantiate(SeaWall, northRoadPos, Quaternion.Euler(0, 180, 0)) as GameObject;
                    northRoadSeaWallGameObject.transform.parent = transform;
                }

                Vector3 eastRoadPos = new Vector3(x * (BuildingTileSize + RoadWidth) + RoadWidth * 1.5f, 0, z * (BuildingTileSize + RoadWidth));
                var eastRoadGameObject = Instantiate(RoadSection, eastRoadPos, Quaternion.Euler(0, 90, 0)) as GameObject;
                eastRoadGameObject.transform.parent = transform;

                if (x == GridSize - 1)
                {
                    var eastRoadSeaWallGameObject = Instantiate(SeaWall, eastRoadPos, Quaternion.Euler(0, -90, 0)) as GameObject;
                    eastRoadSeaWallGameObject.transform.parent = transform;
                }

                if (z != GridSize - 1 || x != GridSize - 1)
                {
                    if (x == GridSize - 1)
                    {
                        Vector3 triwayPos = new Vector3(x * (BuildingTileSize + RoadWidth) + RoadWidth * 1.5f, 0,
                            z * (BuildingTileSize + RoadWidth) + RoadWidth * 1.5f);
                        var triwayGameObject =
                            Instantiate(RoadTriWay, triwayPos, Quaternion.Euler(0, -90, 0)) as GameObject;
                        triwayGameObject.transform.parent = transform;
                    } else if (z == GridSize - 1)
                    {
                        Vector3 triwayPos = new Vector3(x * (BuildingTileSize + RoadWidth) + RoadWidth * 1.5f, 0,
                            z * (BuildingTileSize + RoadWidth) + RoadWidth * 1.5f);
                        var triwayGameObject =
                            Instantiate(RoadTriWay, triwayPos, Quaternion.Euler(0, 180, 0)) as GameObject;
                        triwayGameObject.transform.parent = transform;
                    }
                    else
                    {
                        Vector3 fourwayPos = new Vector3(x*(BuildingTileSize + RoadWidth) + RoadWidth*1.5f, 0,
                            z*(BuildingTileSize + RoadWidth) + RoadWidth*1.5f);
                        var fourwayPosGameObject =
                            Instantiate(RoadFourWay, fourwayPos, Quaternion.identity) as GameObject;
                        fourwayPosGameObject.transform.parent = transform;
                    }
                }

                if (x == -GridSize)
                {
                    Vector3 westRoadPos = new Vector3(x * (BuildingTileSize + RoadWidth) - RoadWidth * 1.5f, 0, z * (BuildingTileSize + RoadWidth));
                    var westRoadGameObject = Instantiate(RoadSection, westRoadPos, Quaternion.Euler(0, 90, 0)) as GameObject;
                    westRoadGameObject.transform.parent = transform;

                    var westRoadSeaWallGameObject = Instantiate(SeaWall, westRoadPos, Quaternion.Euler(0, 90, 0)) as GameObject;
                    westRoadSeaWallGameObject.transform.parent = transform;

                    if (z != GridSize - 1)
                    {
                        Vector3 triwayPos = new Vector3(x*(BuildingTileSize + RoadWidth) - RoadWidth*1.5f, 0,
                            z*(BuildingTileSize + RoadWidth) + RoadWidth*1.5f);
                        var triwayGameObject =
                            Instantiate(RoadTriWay, triwayPos, Quaternion.Euler(0, 90, 0)) as GameObject;
                        triwayGameObject.transform.parent = transform;
                    }
                }

                if (z == -GridSize)
                {
                    Vector3 southRoadPos = new Vector3(x * (BuildingTileSize + RoadWidth), 0, z * (BuildingTileSize + RoadWidth) - RoadWidth * 1.5f);
                    var southRoadGameObject = Instantiate(RoadSection, southRoadPos, Quaternion.identity) as GameObject;
                    southRoadGameObject.transform.parent = transform;

                    var southRoadSeaWallGameObject = Instantiate(SeaWall, southRoadPos, Quaternion.identity) as GameObject;
                    southRoadSeaWallGameObject.transform.parent = transform;

                    if (x != GridSize - 1)
                    {
                        Vector3 triwayPos = new Vector3(x * (BuildingTileSize + RoadWidth) + RoadWidth * 1.5f, 0,
                            z * (BuildingTileSize + RoadWidth) - RoadWidth * 1.5f);
                        var triwayGameObject =
                            Instantiate(RoadTriWay, triwayPos, Quaternion.Euler(0, 0, 0)) as GameObject;
                        triwayGameObject.transform.parent = transform;
                    }
                }
            }
        }

        Vector3 northWestPos = new Vector3(-GridSize * (BuildingTileSize + RoadWidth) - RoadWidth * 1.5f, 0,
                            -GridSize * (BuildingTileSize + RoadWidth) - RoadWidth * 1.5f);
        var northWestGameObject =
            Instantiate(RoadCorner, northWestPos, Quaternion.Euler(0, 90, 0)) as GameObject;
        northWestGameObject.transform.parent = transform;

        Vector3 northEastPos = new Vector3((GridSize - 1) * (BuildingTileSize + RoadWidth) + RoadWidth * 1.5f, 0,
                            -GridSize * (BuildingTileSize + RoadWidth) - RoadWidth * 1.5f);
        var northEastGameObject =
            Instantiate(RoadCorner, northEastPos, Quaternion.Euler(0, 0, 0)) as GameObject;
        northEastGameObject.transform.parent = transform;



        Vector3 southWestPos = new Vector3(-GridSize * (BuildingTileSize + RoadWidth) - RoadWidth * 1.5f, 0,
                            (GridSize) * (BuildingTileSize + RoadWidth) - RoadWidth * 1.5f);
        var southWestGameObject =
            Instantiate(RoadCorner, southWestPos, Quaternion.Euler(0, 180, 0)) as GameObject;
        southWestGameObject.transform.parent = transform;

        Vector3 southEastPos = new Vector3((GridSize - 1) * (BuildingTileSize + RoadWidth) + RoadWidth * 1.5f, 0,
                            (GridSize) * (BuildingTileSize + RoadWidth) - RoadWidth * 1.5f);
        var southEastGameObject =
            Instantiate(RoadCorner, southEastPos, Quaternion.Euler(0, -90, 0)) as GameObject;
        southEastGameObject.transform.parent = transform;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
/*
[CustomEditor(typeof(SpawnCity))]
public class ObjectBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SpawnCity myScript = (SpawnCity)target;
        if (GUILayout.Button("Build Object"))
        {
            myScript.Activate();
        }
    }
}*/