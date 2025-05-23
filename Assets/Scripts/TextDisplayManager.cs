using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class TextDisplayManager : MonoBehaviour
{
    public List<TMP_Text> cropPrice;
    public TMP_Text coin;
    private CropsManager cropsManager;
    void Awake()
    {
        cropsManager = GameObject.Find("CropsManager").GetComponent<CropsManager>();
    }

    public void UpdateCropPrice(int price)
    {
        foreach (TMP_Text text in cropPrice)
        {
            text.text = price.ToString();
        }
    }

    public void UpdateCoin()
    {
        coin.text = cropsManager.gold.ToString();
    }
}
