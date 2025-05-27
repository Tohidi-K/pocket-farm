
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CropsColorChanger : MonoBehaviour
{
    public List<Image> cauliflowerImages;
    public List<Image> cornImages;
    public List<Image> carrotImages;
    public List<Image> garlicImages;
    public List<Image> watermelonImages;
    public List<Image> eggplantImages;
    public List<Image> tomatoImages;
    public List<Image> radishImages;
    public List<Image> pepperImages;
    public List<Image> pumpkinImages;

    public GameObject cauliflowerButton;
    public GameObject cornButton;
    public GameObject carrotButton;
    public GameObject garlicButton;
    public GameObject watermelonButton;
    public GameObject eggplantButton;
    public GameObject tomatoButton;
    public GameObject radishButton;
    public GameObject pepperButton;
    public GameObject pumpkinButton;

    private CropsManager cropsManager;
    void Awake()
    {
        cropsManager = GameObject.Find("CropsManager").GetComponent<CropsManager>();
    }

    public void ChangeCauliflowerColor()
    {
        cropsManager.BuyingNewCrop();
        if (cropsManager.isMoneyEnough)
        {
            foreach (Image img in cauliflowerImages)
            {
                img.color = new Color(1, 1, 1);
            }
            cauliflowerButton.SetActive(false);
        }
    }

    public void ChangeCornColor()
    {
        cropsManager.BuyingNewCrop();
        if (cropsManager.isMoneyEnough)
        {
            foreach (Image img in cornImages)
            {
                img.color = new Color(1, 1, 1);
            }
            cornButton.SetActive(false);
        }
    }

    public void ChangeCarrotColor()
    {
        cropsManager.BuyingNewCrop();
        if (cropsManager.isMoneyEnough)
        {
            foreach (Image img in carrotImages)
            {
                img.color = new Color(1, 1, 1);
            }
            carrotButton.SetActive(false);
        }
    }

    public void ChangeGarlicColor()
    {
        cropsManager.BuyingNewCrop();
        if (cropsManager.isMoneyEnough)
        {
            foreach (Image img in garlicImages)
            {
                img.color = new Color(1, 1, 1);
            }
            garlicButton.SetActive(false);
        }
    }

    public void ChangeWatermelonColor()
    {
        cropsManager.BuyingNewCrop();
        if (cropsManager.isMoneyEnough)
        {
            foreach (Image img in watermelonImages)
            {
                img.color = new Color(1, 1, 1);
            }
            watermelonButton.SetActive(false);
        }
    }

    public void ChangeEggplantColor()
    {
        cropsManager.BuyingNewCrop();
        if (cropsManager.isMoneyEnough)
        {
            foreach (Image img in eggplantImages)
            {
                img.color = new Color(1, 1, 1);
            }
            eggplantButton.SetActive(false);
        }
    }

    public void ChangeTomatoColor()
    {
        cropsManager.BuyingNewCrop();
        if (cropsManager.isMoneyEnough)
        {
            foreach (Image img in tomatoImages)
            {
                img.color = new Color(1, 1, 1);
            }
            tomatoButton.SetActive(false);
        }
    }

    public void ChangeRadishColor()
    {
        cropsManager.BuyingNewCrop();
        if (cropsManager.isMoneyEnough)
        {
            foreach (Image img in radishImages)
            {
                img.color = new Color(1, 1, 1);
            }
            radishButton.SetActive(false);
        }
    }

    public void ChangePepperColor()
    {
        cropsManager.BuyingNewCrop();
        if (cropsManager.isMoneyEnough)
        {
            foreach (Image img in pepperImages)
            {
                img.color = new Color(1, 1, 1);
            }
            pepperButton.SetActive(false);
        }
    }

    public void ChangePumpkinColor()
    {
        cropsManager.BuyingNewCrop();
        if (cropsManager.isMoneyEnough)
        {
            foreach (Image img in pumpkinImages)
            {
                img.color = new Color(1, 1, 1);
            }
            pumpkinButton.SetActive(false);
        }
    }
}
