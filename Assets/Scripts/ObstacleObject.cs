using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleObject : MonoBehaviour
{
    public ObstacleType obstacleType;
    public int resourceAmount = 10;//add 10 resource
    private void OnMouseDown()
    {
        bool usedResource = false;

        switch (obstacleType)
        {
            case ObstacleType.Wood:
                usedResource = ResourcesManager.Instance.AddWood(resourceAmount);
                break;

            case ObstacleType.Rock:
                usedResource = ResourcesManager.Instance.AddStone(resourceAmount);
                break;
        }
        if (usedResource)
        {
            //refTile.data.CleanTile();
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("InventoryIsFull");
        }
    }
    public void SetTileReference(TileObject obj)
    {
        //refTile = obj;
    }
    public enum ObstacleType
    {
        Wood,
        Rock
    }
}
