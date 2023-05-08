using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    [Header("Resources")]

    [Space(8)]

    public int maxWood;
    int wood = 0;

    public int maxStone;
    int stone = 0;

    public int maxPremiumCoins;
    int premiumCoins = 0;

    public int maxStandartCoins;
    int standartCoins = 0;

    public static ResourcesManager Instance;

    public bool debugBool = false;

    public int Wood { get => wood; set => wood = value; }
    public int Stone { get => stone; set => stone = value; }
    public int PremiumCoins { get => premiumCoins; set => premiumCoins = value; }
    public int StandartCoins { get => standartCoins; set => standartCoins = value; }

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if (debugBool)//кривая замена event
        {
            PrintCurrentResources();
            debugBool = false;
        }
    }
    #region AddResource+Storage
    public bool AddWood(int amount)
    {
        if ((wood + amount) <= maxWood)
        {
            Wood += amount;
            UIManager.Instance.UpdateWoodUI(Wood, maxWood);

            return true;
        }
        else
        {
            return false;
        }
    }    
    public void IncreaseMaxWood(int amount)
    {
        maxWood += amount;
        UIManager.Instance.UpdateWoodUI(Wood, maxWood);
    }
    public bool AddStone(int amount)
    {
        if ((stone + amount) <= maxStone)
        {
            Stone += amount;
            UIManager.Instance.UpdateStoneUI(Stone, maxStone);

            return true;
        }
        else
        {
            return false;
        }
    }
    public void IncreaseMaxStone(int amount)
    {
        maxStone += amount;
        UIManager.Instance.UpdateStoneUI(Wood, maxWood);
    }
    public bool AddPremiumCoins(int amount)
    {
        if ((premiumCoins + amount) <= maxPremiumCoins)
        {
            PremiumCoins += amount;
            UIManager.Instance.UpdatePremiumCoinsUI(PremiumCoins, maxPremiumCoins);

            return true;
        }
        else
        {
            return false;
        }
    }    
    public bool AddStandartCoins(int amount)
    {
        if ((standartCoins + amount) <= maxStandartCoins)
        {
            StandartCoins += amount;
            UIManager.Instance.UpdateStandartCoinsUI(StandartCoins, maxStandartCoins);

            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion
    private void PrintCurrentResources()
    {
        Debug.Log("wood" + Wood);
        Debug.Log("stone" + Stone);
        Debug.Log("standard" + StandartCoins);
        Debug.Log("premium" + PremiumCoins);
    }
}
