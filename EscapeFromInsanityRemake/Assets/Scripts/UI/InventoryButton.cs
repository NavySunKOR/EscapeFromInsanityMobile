using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour, IPointerDownHandler
{
    public int index;
    public Text amount;

    public void SetAmountText(int value)
    {
        amount.text = value.ToString();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }
}
