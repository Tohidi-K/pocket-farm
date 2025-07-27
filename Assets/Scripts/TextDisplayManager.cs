using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public GameObject fertalizerPricePanel;
    public GameObject fertalizerMainPanel;
    public Button extendTimeButton;
    
    public int minutes;
    public int seconds;

    private float moveDistance = 70f;
    private float profitTextDuration = 1.5f;

    public float countdownTime = 300f;
    public float currentTime;
    public bool isCurrentTimeInitialized = false;

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
        isCurrentTimeInitialized = true;
        UpdateTimer();
        StartCoroutine(CountdownTimer());

        EventTrigger timeTrigger = extendTimeButton.gameObject.GetComponent<EventTrigger>();
        if (timeTrigger == null)
        {
            timeTrigger = extendTimeButton.gameObject.AddComponent<EventTrigger>();
        }

        EventTrigger fertalizerTrigger = fertalizerMainPanel.gameObject.GetComponent<EventTrigger>();
        if (fertalizerTrigger == null)
        {
            fertalizerTrigger = fertalizerMainPanel.gameObject.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry timePanelEntryEnter = new EventTrigger.Entry();
        timePanelEntryEnter.eventID = EventTriggerType.PointerEnter;
        timePanelEntryEnter.callback.AddListener((data) => { OnHoverEnterInTimePanel(); });
        timeTrigger.triggers.Add(timePanelEntryEnter);

        EventTrigger.Entry timePanelEntryExit = new EventTrigger.Entry();
        timePanelEntryExit.eventID = EventTriggerType.PointerExit;
        timePanelEntryExit.callback.AddListener((data) => { OnHoverExitOutOfTimePanel(); });
        timeTrigger.triggers.Add(timePanelEntryExit);

        EventTrigger.Entry fertalizerPanelEntryEnter = new EventTrigger.Entry();
        fertalizerPanelEntryEnter.eventID = EventTriggerType.PointerEnter;
        fertalizerPanelEntryEnter.callback.AddListener((data) => { OnHoverEnterInFertalizerPanel(); });
        fertalizerTrigger.triggers.Add(fertalizerPanelEntryEnter);

        EventTrigger.Entry fertalizerPanelEntryExit = new EventTrigger.Entry();
        fertalizerPanelEntryExit.eventID = EventTriggerType.PointerExit;
        fertalizerPanelEntryExit.callback.AddListener((data) => { OnHoverExitOutOfFertalizerPanel(); });
        fertalizerTrigger.triggers.Add(fertalizerPanelEntryExit);
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
        if (!cropsManager.isCropFertilized[cropIndex])
        {
            cropProfit[cropIndex].color = new Color(1, 0.732f, 0);
        }
        else
        {
            cropProfit[cropIndex].color = new Color(0, 1, 0.8f);
        }
        cropProfit[cropIndex].text = cropsManager.profit.ToString();
        //Color c = cropProfit[cropIndex].color;
        //c.a = 1f;
        //cropProfit[cropIndex].color = c;
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

        if (seconds > 9)
        {
            countdownText.text = $"{minutes}:{seconds}";
        }
        else
        {
            countdownText.text = $"{minutes}:0{seconds}";
        }
    }

    private IEnumerator CountdownTimer()
    {
        while (currentTime > 0)
        {
            currentTime -= 1f;
            UpdateTimer();
            if (currentTime == 0f)
            {
                SceneManager.LoadScene(0);
            }
            yield return new WaitForSeconds(1f);
        }
    }

    public void OnHoverEnterInTimePanel()
    {
        extendTimePanel.SetActive(true);
    }

    public void OnHoverExitOutOfTimePanel()
    {
        extendTimePanel.SetActive(false);
    }

    public void OnHoverEnterInFertalizerPanel()
    {
        fertalizerPricePanel.SetActive(true);
    }

    public void OnHoverExitOutOfFertalizerPanel()
    {
        fertalizerPricePanel.SetActive(false);
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
