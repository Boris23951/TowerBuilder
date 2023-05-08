using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tile
{
    public BuildingObject buildingRef;
    public ObstacleType obstacleType;

    public bool occupied;//??

    private bool isStarterTile = true;
    public enum ObstacleType
    {
        None,
        Resource,
        Building
    }
    #region Metods
    public void SetOccupied(ObstacleType t, BuildingObject b)
    {
        obstacleType = t;
        occupied = true;

        buildingRef = b;
    }
    public void CleanTile()
    {
        obstacleType = ObstacleType.None;
    }
    public void StarterTileValue(bool value)
    {
        isStarterTile = value;
    }
    #endregion
    #region Booleans
    public bool IsOccupied
    {
        get
        {
            return obstacleType != ObstacleType.None;
        }
    }
    public bool CanSpawnObstacle
    {
        get
        {
            return !isStarterTile;
        }
    }
    #endregion
}
