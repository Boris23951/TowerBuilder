using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObject : MonoBehaviour
{
    public Tile data;

    [Header("World Tile Data")]
    [Space(8)]

    //tile position
    public int xPos = 0;
    public int zPos = 0;

    private void OnMouseDown()
    {
        if (!data.IsOccupied)
        {
            if (GameManager.Instance.buildToPlace != null)
            {
                List<TileObject> iteratedTiles = new List<TileObject>();//!!??

                bool canPlaceBuildingHere = true;

                try
                {
                    for (int x = xPos; x < xPos + GameManager.Instance.buildToPlace.data.width; x++)
                    {
                        if (canPlaceBuildingHere)
                        {
                            for (int z = zPos; z < zPos + GameManager.Instance.buildToPlace.data.length; z++)
                            {
                                iteratedTiles.Add(GameManager.Instance.tileGrid[x, z]);

                                if (GameManager.Instance.tileGrid[x, z].data.IsOccupied)//.data.IsOccupied
                                {
                                    canPlaceBuildingHere = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                catch (System.IndexOutOfRangeException)
                {
                    Debug.Log("no tiles");
                    return;
                }

                if (canPlaceBuildingHere)
                {
                    GameManager.Instance.SpawnBuilding(GameManager.Instance.buildToPlace, iteratedTiles);
                }
                else
                {
                    Debug.Log("TileIsNotEmpty");
                }
            }
            else
            {
                Debug.Log("null");
            }
        }
    }
}
