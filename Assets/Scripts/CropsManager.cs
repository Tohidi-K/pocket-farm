using UnityEngine;

public class CropsManager : MonoBehaviour
{
    public int gold = 30;
    public int[] cropPrices = new int[] { 50, 100, 150, 250, 400, 600, 850, 1150, 1500, 1900};
    public int i = 0;
    public bool isMoneyEnough;

    private TextDisplayManager textDisplayManager;
    void Awake()
    {
        textDisplayManager = GameObject.Find("UIManager").GetComponent<TextDisplayManager>();
    }

    public void BuyingNewCrop()
    {
        if ((gold - cropPrices[i]) > 0)
        {
            gold -= cropPrices[i];
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
}
