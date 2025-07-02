using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class CropsManager : MonoBehaviour
{
    public int[] cropPrices = new int[] { 50, 100, 150, 250, 400, 600, 850, 1150, 1500, 1900};
    public int[] cropLevel = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
    public int i = 0;
    public int fertilizerCount = 0;
    public int extendTimeCost = 1000;
    public bool isMoneyEnough;
    public bool[] isCropFertilized;
    public int profit;

    private int _gold = 150;

    public int Gold
    {
        get => _gold;
        set
        {
            if (_gold != value)
            {
                _gold = value;
                ChangeUpgradeButtonColor();
                ScoreManager.Instance.SetGold(value);
            }
        }
    }

    public List<GameObject> cropsButtons;
    public List<GameObject> upgradeButtons;
    public List<TMP_Text> upgradeText;

    private TextDisplayManager textDisplayManager;
    private CropData cropData;
    private CropTimer cropTimer;

    void Awake()
    {
        textDisplayManager = GameObject.Find("UIManager").GetComponent<TextDisplayManager>();
        cropData = GameObject.Find("CropsManager").GetComponent<CropData>();
        cropTimer = GameObject.Find("CropTimer").GetComponent<CropTimer>();

        if (isCropFertilized == null || isCropFertilized.Length != 10)
        {
            isCropFertilized = new bool[10];
        }   
    }

    public void BuyingNewCrop()
    {
        if ((Gold - cropPrices[i]) > 0)
        {
            Gold -= cropPrices[i];
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
            if (Gold < cropData.upgradePrices[cropIndex, 0])
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
        if (Gold >= cropData.upgradePrices[cropIndex, cropLevel[cropIndex]])
        {
            Gold -= cropData.upgradePrices[cropIndex, cropLevel[cropIndex]];
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
        if (!isCropFertilized[cropIndex])
        {
            profit = cropData.profitNumbers[cropIndex, cropLevel[cropIndex]];
        }
        else
        {
            profit = cropData.profitNumbers[cropIndex, cropLevel[cropIndex]] * 2;
        }
        Gold += profit;
    }

    public void ChangeUpgradeButtonColor()
    {
        for (int i = 0; i < 10; i++)
        {
            if (Gold < cropData.upgradePrices[i, cropLevel[i]])
            {
                upgradeButtons[i].GetComponent<Image>().color = new Color(0.77f, 0.77f, 0.77f);
                upgradeText[i].color = new Color(096f, 0.34f, 0.34f);
            }
            else
            {
                upgradeButtons[i].GetComponent<Image>().color = new Color(0.75f, 1, 0);
                upgradeText[i].color = new Color(1, 1, 1);
            }
        }
    }

    public void OnExtendTimeButtonClicked()
    {
        if (Gold >= extendTimeCost)
        {
            Gold -= extendTimeCost;
            extendTimeCost += 1000;
            textDisplayManager.currentTime += 60;
            textDisplayManager.ChangeExtendTimeText();
        }
    }

    public void BuyFertilizer()
    {
        if (Gold >= 120)
        {
            Gold -= 120;
            fertilizerCount++;
            textDisplayManager.UpdateFertilizerText();
        }
    }

    public void OnFertilizationButtonClicked(int cropIndex)
    {
        if (fertilizerCount > 0)
        {
            fertilizerCount--;
            textDisplayManager.UpdateFertilizerText();
            isCropFertilized[cropIndex] = true;
            cropTimer.fertilizerCounter[cropIndex] = 5;
        }
    }
}
