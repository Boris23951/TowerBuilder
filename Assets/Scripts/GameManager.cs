using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("BackgroundGeneration")]
    public int bachgroundWight;
    public int backgroundLenght;
    public GameObject backgroundPrefab;
    public Transform backgroundTilesHolder;

    [Header("Builder")]

    [Space(8)]

    public GameObject tilePrefab;

    public int levelWight;
    public int levelLenght;
    public Transform tilesHolder;
    public float tileSize = 1;
    public float tileEndHeight = 1;

    [Space(8)]

    //this is the grid stores all of the information.
    public TileObject[,] tileGrid = new TileObject[0, 0];

    [Header("Resources")]

    [Space(8)]
    public GameObject woodPrefab;
    public GameObject rockPrefab;
    public Transform resourcesHolder;

    [Range(0, 1)]
    public float obstacaleChance = 0.3f;

    public int xBounds = 4;
    public int zBounds = 4;

    [Space(8)]

    public BuildingObject buildToPlace;

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        CreateBackground();
        CreateLevel();
    }
    #region CreateBackground
    public void CreateBackground()//!!!
    {
        {
            List<TileObject> visualGrid = new List<TileObject>();
            for (int x = -30; x < backgroundLenght; x++)
            {
                for (int z = -30; z < backgroundLenght; z++)
                {
                    TileObject spawnedTile = SpawnBackgroundTile(x * tileSize, z * tileSize);//spawn a tile
                                                                                   //Set the TileObject world space data.
                    spawnedTile.xPos = x;
                    spawnedTile.zPos = z;
                }
            }
        }
    }
    TileObject SpawnBackgroundTile(float xPos, float zPos)
    {
        GameObject tmpTile = Instantiate(backgroundPrefab);
        tmpTile.transform.position = new Vector3(xPos, 0, zPos);
        tmpTile.transform.SetParent(backgroundTilesHolder);

        tmpTile.name = "Background_Tile" + xPos + "-" + zPos;

        return tmpTile.GetComponent<TileObject>();
    }
    #endregion
    #region CreateLvl
    public void CreateLevel()
    {
        List<TileObject> visualGrid = new List<TileObject>();
        for(int x = 0; x < levelLenght; x++)
        {
            for(int z= 0; z < levelLenght; z++)
            {
                TileObject spawnedTile =  SpawnTile(x * tileSize, z * tileSize);//spawn a tile
                //Set the TileObject world space data.
                spawnedTile.xPos = x;
                spawnedTile.zPos = z;

                if (x < xBounds || z < zBounds || z >= (levelLenght - zBounds) || x >= (levelWight - xBounds))
                {
                    spawnedTile.data.StarterTileValue(false);
                }
                if (spawnedTile.data.CanSpawnObstacle)
                {
                    bool spawnObstace = Random.value <= obstacaleChance;
                    if (spawnObstace)
                    {
                        spawnedTile.data.SetOccupied(Tile.ObstacleType.Resource, buildToPlace);//, buildToPlace
                        SpawnObstacle(spawnedTile.transform.position.x, spawnedTile.transform.position.z);
                    }
                }
                visualGrid.Add(spawnedTile);//вот здесь можно исключить спаун на grid!!!!
            }
        }

        CreateGrid(visualGrid);
    }
    TileObject SpawnTile(float xPos , float zPos)
    {
        GameObject tmpTile = Instantiate(tilePrefab);
        tmpTile.transform.position = new Vector3(xPos, 0, zPos);
        tmpTile.transform.SetParent(tilesHolder);

        tmpTile.name = "Tile" + xPos + "-" + zPos;

        return tmpTile.GetComponent<TileObject>();
    }
    #endregion
    public void SpawnObstacle(float xPos, float zPos)
    {
        bool isWood = Random.value <= 0.5f;
        GameObject spawnObstacle = null;

        if (isWood)
        {
            spawnObstacle = Instantiate(woodPrefab);
            spawnObstacle.name = "Wood" + xPos + "-" + zPos;
        }
        else
        {
            spawnObstacle = Instantiate(rockPrefab);
            spawnObstacle.name = "Stone" + xPos + "-" + zPos;
        }

        spawnObstacle.transform.position = new Vector3(xPos, tileEndHeight, zPos);
        spawnObstacle.transform.SetParent(resourcesHolder);
    }
    //Create tile grid to add buildings.
    public void CreateGrid(List<TileObject> refVisualGrid)
    {
        //Set the size of our tile grid.
        tileGrid = new TileObject[levelWight, levelLenght];//can change size for build

        for(int x = 0; x < levelWight; x++)
        {
            for(int z = 0; z< levelLenght; z++)
            {
                tileGrid[x, z] = refVisualGrid.Find(v => v.xPos == x && v.zPos == z);
            }
        }
    }
    public void SpawnBuilding(BuildingObject building, List<TileObject> tiles)
    {
        GameObject spawnedBuilding = Instantiate(building.gameObject);
        float sumX = 0;
        float sumZ = 0;

        for(int i = 0; i < tiles.Count; i++)
        {
            sumX += tiles[i].xPos;
            sumZ += tiles[i].zPos;

            tiles[i].data.SetOccupied(Tile.ObstacleType.Building, spawnedBuilding.GetComponent<BuildingObject>());
        }

        Vector3 position = new Vector3((sumX/tiles.Count), tileEndHeight + building.data.yCorrective, (sumZ/tiles.Count));

        spawnedBuilding.transform.position = position;
    }
    public void SelectBuilding(int id)
    {
        for(int i = 0; i < BuildingsDatabase.Instance.buildingsDatabase.Count; i++)
        {
            if (BuildingsDatabase.Instance.buildingsDatabase[i].buildingID == id)
            {
                buildToPlace = BuildingsDatabase.Instance.buildingsDatabase[i].refOfBuilding;
            }
        }
    }
}
