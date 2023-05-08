using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [Space(8)]

    public StandatdUIReference woodUI;
    public StandatdUIReference stoneUI;
    public StandatdUIReference standartCoinsUI;
    public StandatdUIReference premiumCoinsUI;

    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        UpdateAllUI();
    }
    #region UIUpdate
    private void UpdateAllUI()
    {
        UpdateWoodUI(ResourcesManager.Instance.Wood, ResourcesManager.Instance.maxWood);
        UpdateStoneUI(ResourcesManager.Instance.Stone, ResourcesManager.Instance.maxStone);
        UpdateStandartCoinsUI(ResourcesManager.Instance.StandartCoins, ResourcesManager.Instance.maxStandartCoins);
        UpdatePremiumCoinsUI(ResourcesManager.Instance.PremiumCoins, ResourcesManager.Instance.maxPremiumCoins);
    }
    public void UpdateWoodUI(int currentAmount, int maxAmount)
    {
        woodUI.currentUI.text = currentAmount.ToString();
        woodUI.maxUI.text = "MAX: " + maxAmount.ToString();

        woodUI.slider.maxValue = maxAmount;
        woodUI.slider.value = currentAmount;
    }
    public void UpdateStoneUI(int currentAmount, int maxAmount)
    {
        stoneUI.currentUI.text = currentAmount.ToString();
        stoneUI.maxUI.text = "MAX: " + maxAmount.ToString();

        stoneUI.slider.maxValue = maxAmount;
        stoneUI.slider.value = currentAmount;
    }
    public void UpdateStandartCoinsUI(int currentAmount, int maxAmount)
    {
        standartCoinsUI.currentUI.text = currentAmount.ToString();
        standartCoinsUI.maxUI.text = "MAX: " + maxAmount.ToString();

        standartCoinsUI.slider.maxValue = maxAmount;
        standartCoinsUI.slider.value = currentAmount;
    }
    public void UpdatePremiumCoinsUI(int currentAmount, int maxAmount)
    {
        premiumCoinsUI.currentUI.text = currentAmount.ToString();
        premiumCoinsUI.maxUI.text = "MAX: " + maxAmount.ToString();

        premiumCoinsUI.slider.maxValue = maxAmount;
        premiumCoinsUI.slider.value = currentAmount;
    }
    #endregion
}
[System.Serializable]
public class StandatdUIReference
{
    public Slider slider;
    public TMP_Text maxUI;
    public TMP_Text currentUI;
}
