using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CropTimer : MonoBehaviour
{
    public List<Image> cropsTimer;
    public List<GameObject> cropsButton;
    //public System.Action onCycleComplete;

    //radish, garlic, cauliflower, tomatom, pepper, corn, eggplant, carrot, pumpkin, watermelon
    public float[] cycleDuration = { 1f, 1.2f, 1.4f, 1.7f, 2f, 2.4f, 2.7f, 3f, 3.5f, 5f };

    private float[] timers = {0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f};
    private float fillAmount;
    private CropsManager cropsManager;
    private TextDisplayManager textDisplayManager;

    void Awake()
    {
        cropsManager = GameObject.Find("CropsManager").GetComponent<CropsManager>();
        textDisplayManager = GameObject.Find("UIManager").GetComponent<TextDisplayManager>();
    }

    private void Update()
    {
        for (int i = 0; i < 10; i++)
        {
            if (cropsTimer[i].gameObject.activeInHierarchy)
            {
                timers[i] += Time.deltaTime;
                fillAmount = timers[i] / cycleDuration[i];
                cropsTimer[i].fillAmount = fillAmount;

                if (timers[i] >= cycleDuration[i])
                {
                    if (!textDisplayManager.cropProfit[i].gameObject.activeInHierarchy)
                    {
                        textDisplayManager.cropProfit[i].gameObject.SetActive(true);
                    }
                    cropsManager.EarnProfit(i);
                    textDisplayManager.UpdateCoin();
                    timers[i] = 0f;
                    cropsTimer[i].fillAmount = 0f;
                    textDisplayManager.DisplayProfit(i);
                }
            }
        }
    }

    public void ActivateRadishTimer()
    {
        if (!cropsButton[0].activeInHierarchy)
        {
            cropsTimer[0].gameObject.SetActive(true);
        }
    }

    public void ActivateGarlicTimer()
    {
        if (!cropsButton[1].activeInHierarchy)
        {
            cropsTimer[1].gameObject.SetActive(true);
        }
    }

    public void ActivateCaulifowerTimer()
    {
        if (!cropsButton[2].activeInHierarchy)
        {
            cropsTimer[2].gameObject.SetActive(true);
        }
    }

    public void ActivateTomatoTimer()
    {
        if (!cropsButton[3].activeInHierarchy)
        {
            cropsTimer[3].gameObject.SetActive(true);
        }
    }

    public void ActivatepepperTimer()
    {
        if (!cropsButton[4].activeInHierarchy)
        {
            cropsTimer[4].gameObject.SetActive(true);
        }
    }

    public void ActivateCornTimer()
    {
        if (!cropsButton[5].activeInHierarchy)
        {
            cropsTimer[5].gameObject.SetActive(true);
        }
    }

    public void ActivateEggplantTimer()
    {
        if (!cropsButton[6].activeInHierarchy)
        {
            cropsTimer[6].gameObject.SetActive(true);
        }
    }

    public void ActivateCarrotTimer()
    {
        if (!cropsButton[7].activeInHierarchy)
        {
            cropsTimer[7].gameObject.SetActive(true);
        }
    }

    public void ActivatePumpkinTimer()
    {
        if (!cropsButton[8].activeInHierarchy)
        {
            cropsTimer[8].gameObject.SetActive(true);
        }
    }

    public void ActivateWatermelonTimer()
    {
        if (!cropsButton[9].activeInHierarchy)
        {
            cropsTimer[9].gameObject.SetActive(true);
        }
    }
}
