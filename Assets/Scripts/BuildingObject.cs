using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingObject : MonoBehaviour
{
    public Building data;
    [Header("Resource Generation")]
    [Space(8)]

    //This build will be create this resource
    public float resource = 0;

    //Will stop generate resource on this 
    public float resourceLimit = 100;

    public float generatiomSpeed = 5;

    [Header("UI")]
    [Space(8)]
    public GameObject canvasObject;
    public Slider progressSlider;

    Coroutine buildingBehavior;

    private void Start()
    {
        if (data.resourceType == Building.ResourceType.Standart || data.resourceType == Building.ResourceType.Premium)
        {
            buildingBehavior = StartCoroutine(CreateResource());
        }
        if (data.resourceType == Building.ResourceType.Storage)
        {
            IncreaseMaxStorage();
            canvasObject.SetActive(false);
        }
    }
    private void OnMouseDown()
    {
        if (data.resourceType == Building.ResourceType.Storage)
        {
            return;
        }
        switch(data.resourceType)
        {
            case Building.ResourceType.Standart:
                ResourcesManager.Instance.AddStandartCoins((int)resource);
                break;

            case Building.ResourceType.Premium:
                ResourcesManager.Instance.AddPremiumCoins((int)resource);
                break;
        }
        EmptyResource();
    }
    private void EmptyResource()
    {
        resource = 0;
    }
    private void IncreaseMaxStorage()
    {
        switch (data.storageType)
        {
            case Building.StorageType.Wood:
                ResourcesManager.Instance.IncreaseMaxWood((int)resource);
                break;

            case Building.StorageType.Stone:
                ResourcesManager.Instance.IncreaseMaxStone((int)resource);
                break;
        }
    }
    IEnumerator CreateResource()//баг хранилище не заполняется полностью + можно выкинуть уже готовы ресурсы!!!!
    {
        //always generate resource
        while (true)
        {
            if (resource < resourceLimit)
            {
                resource += generatiomSpeed * Time.deltaTime;
            }
            else
            {
                resource = resourceLimit;
            }

            /*switch (data.resourceType)
            {//add None case?
                case Building.ResourceType.Standart:
                    break;
                case Building.ResourceType.Premium:
                    break;
                default:
                    break;
            }*/
            UpdateUI(resource, resourceLimit);
            yield return null;
        }
    }
    public void UpdateUI(float current, float maxValue)
    {
        progressSlider.value = current;
        progressSlider.maxValue = maxValue;
    }
}
