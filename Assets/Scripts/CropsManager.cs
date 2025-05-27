using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CropsManager : MonoBehaviour
{
    public int gold = 30;
    public int[] cropPrices = new int[] { 50, 100, 150, 250, 400, 600, 850, 1150, 1500, 1900};
    public int[] cropLevel = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
    public int i = 0;
    public bool isMoneyEnough;

    public List<GameObject> cropsButtons;
    public List<GameObject> upgradeButtons;
    public List<TMP_Text> upgradeText;

    private TextDisplayManager textDisplayManager;
    private CropData cropData;
    void Awake()
    {
        textDisplayManager = GameObject.Find("UIManager").GetComponent<TextDisplayManager>();
        cropData = GameObject.Find("CropsManager").GetComponent<CropData>();
    }

    public void BuyingNewCrop()
    {
        if ((gold - cropPrices[i]) > 0)
        {
            gold -= cropPrices[i];
            textDisplayManager.UpdateCoin();
            i++;
            if (i < 10)
            {
                textDisplayManager.UpdateCropPrice(cropPrices[i]);
            }
            isMoneyEnough = true;
        }
        else
        {
            isMoneyEnough = false;
        }
    }

    public void ActivateUpgradeButton(int cropIndex)
    {

        if (!cropsButtons[cropIndex].activeInHierarchy)
        {
            if (gold < cropData.upgradePrices[cropIndex, 0])
            {
                upgradeButtons[cropIndex].GetComponent<Image>().color = new Color(0.77f, 0.77f, 0.77f);
                upgradeText[cropIndex].color = new Color(096f, 0.34f, 0.34f);
            }
            else
            {
                upgradeButtons[cropIndex].GetComponent<Image>().color = new Color(0.75f, 1, 0);
                upgradeText[cropIndex].color = new Color(1, 1, 1);
            }
            upgradeText[cropIndex].text = cropData.upgradePrices[cropIndex, 0].ToString();
            upgradeButtons[cropIndex].SetActive(true);
            textDisplayManager.UpdateLevel(cropIndex);
            textDisplayManager.cropLevel[cropIndex].gameObject.SetActive(true);
        }
    }

    public void UpgradeCrop(int cropIndex)
    {
        if (gold >= cropData.upgradePrices[cropIndex, cropLevel[cropIndex]])
        {
            gold -= cropData.upgradePrices[cropIndex, cropLevel[cropIndex]];
            cropLevel[cropIndex]++;
            if (cropLevel[cropIndex] == 9)
            {
                upgradeButtons[cropIndex].SetActive(false);
            }
            textDisplayManager.UpdateCoin();
            upgradeText[cropIndex].text = cropData.upgradePrices[cropIndex, cropLevel[cropIndex]].ToString();
            textDisplayManager.UpdateLevel(cropIndex);
        }
    }

    public void EarnProfit(int cropIndex)
    {
        gold += cropData.profitNumbers[cropIndex, cropLevel[cropIndex]];
    }
}
