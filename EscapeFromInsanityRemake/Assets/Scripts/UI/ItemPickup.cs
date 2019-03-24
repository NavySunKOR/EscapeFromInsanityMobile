using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemPickup : MonoBehaviour,IPointerDownHandler
{

    public void OnPointerDown(PointerEventData eventData)
    {
        Pickable pck = transform.GetComponentInParent<Pickable>();
        if (pck != null)
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerOverrideController>().AddItem(pck);
    }
}
