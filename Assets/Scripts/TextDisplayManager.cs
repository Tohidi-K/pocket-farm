using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TextDisplayManager : MonoBehaviour
{
    public List<TMP_Text> cropPrice;
    public List<TMP_Text> cropLevel;
    public List<TMP_Text> cropProfit;

    public TMP_Text coin;
    public TMP_Text countdownText;
    public TMP_Text extendTimePrice;
    public TMP_Text fertilizerText;
    public GameObject extendTimePanel;
    public Button extendTimeButton;

    public int minutes;
    public int seconds;

    public float moveDistance = 60f;
    public float profitTextDuration = 1.2f;
    public float countdownTime = 300f;
    public float currentTime;

    private CropsManager cropsManager;
    private CropData cropData;
    
    void Awake()
    {
        cropsManager = GameObject.Find("CropsManager").GetComponent<CropsManager>();
        cropData = GameObject.Find("CropsManager").GetComponent<CropData>();
    }

    private void Start()
    {
        currentTime = countdownTime;
        UpdateTimer();

        EventTrigger trigger = extendTimeButton.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = extendTimeButton.gameObject.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry entryEnter = new EventTrigger.Entry();
        entryEnter.eventID = EventTriggerType.PointerEnter;
        entryEnter.callback.AddListener((data) => { OnHoverEnter(); });
        trigger.triggers.Add(entryEnter);

        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((data) => { OnHoverExit(); });
        trigger.triggers.Add(entryExit);
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;
        UpdateTimer();
        if (currentTime <= 0f)
        {
            SceneManager.LoadScene(0);
        }
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
        coin.text = cropsManager.Gold.ToString();
    }

    public void UpdateLevel(int cropIndex)
    {
        cropLevel[cropIndex].text = (cropsManager.cropLevel[cropIndex] + 1).ToString();
    }

    public void DisplayProfit(int cropIndex)
    {
        cropProfit[cropIndex].text = cropsManager.profit.ToString();
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

    public void UpdateTimer()
    {
        minutes = Mathf.FloorToInt(currentTime / 60f);
        seconds = Mathf.FloorToInt(currentTime % 60f);
        countdownText.text = $"{minutes}:{seconds}";
    }

    public void OnHoverEnter()
    {
        extendTimePanel.SetActive(true);
    }

    public void OnHoverExit()
    {
        extendTimePanel.SetActive(false);
    }

    public void ChangeExtendTimeText()
    {
        extendTimePrice.text = $"{cropsManager.extendTimeCost.ToString()} coins";
    }

    public void UpdateFertilizerText()
    {
        fertilizerText.text = cropsManager.fertilizerCount.ToString();
    }
}
