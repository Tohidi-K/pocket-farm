using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class TextDisplayManager : MonoBehaviour
{
    public List<TMP_Text> cropPrice;
    public List<TMP_Text> cropLevel;
    public List<TMP_Text> cropProfit;
    public TMP_Text coin;

    public float moveDistance = 60f;
    public float profitTextDuration = 1.2f;

    private CropsManager cropsManager;
    private CropData cropData;

    void Awake()
    {
        cropsManager = GameObject.Find("CropsManager").GetComponent<CropsManager>();
        cropData = GameObject.Find("CropsManager").GetComponent<CropData>();
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

    public void UpdateLevel(int cropIndex)
    {
        cropLevel[cropIndex].text = (cropsManager.cropLevel[cropIndex] + 1).ToString();
    }

    public void DisplayProfit(int cropIndex)
    {
        cropProfit[cropIndex].text = cropData.profitNumbers[cropIndex, cropsManager.cropLevel[cropIndex]].ToString();
        Color c = cropProfit[cropIndex].color;
        c.a = 1f;
        cropProfit[cropIndex].color = c;
        StartCoroutine(FadeAndMoveText(cropIndex));
    }

    private IEnumerator FadeAndMoveText(int cropIndex)
    {
        Vector3 startPos = new Vector2(0, 0);
        Vector3 endPos = startPos + Vector3.up * moveDistance;

        Color startColor = cropProfit[cropIndex].color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        float elapsed = 0f;

        while (elapsed < profitTextDuration)
        {
            float t = elapsed / profitTextDuration;

            cropProfit[cropIndex].rectTransform.anchoredPosition = Vector3.Lerp(startPos, endPos, t);
            cropProfit[cropIndex].color = Color.Lerp(startColor, endColor, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        cropProfit[cropIndex].rectTransform.anchoredPosition = new Vector2(0, 0);
    }
}
