using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Building
{
    public int buildingID;

    [Header("X Size")]
    public int width = 0;
    [Header("Z Size")]
    public int length = 0;

    public GameObject buildingMosel;

    //need to do buildng up
    public float yCorrective = 0;

    public ResourceType resourceType = ResourceType.None;

    public StorageType storageType = StorageType.None;

    public BuildingObject refOfBuilding;
    public enum ResourceType
    {
        None,
        Standart,
        Premium,
        Storage
    }

    public enum StorageType
    {
        None,
        Wood,
        Stone
    }
}
