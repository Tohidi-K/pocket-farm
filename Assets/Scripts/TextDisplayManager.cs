using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class TextDisplayManager : MonoBehaviour
{
    public List<TMP_Text> cropPrice;

    public void UpdateCropPrice(int price)
    {
        foreach (TMP_Text text in cropPrice)
        {
            text.text = price.ToString();
        }
    }
}
