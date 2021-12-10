using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Verwaltet die Darstellung der einzelnen InventoryItemRenderer
public class InventoryItemRenderer : MonoBehaviour
{
    private InventoryItem _item;
    // Zeiger  auf das Item, das dieser Renderer darstellt
    public InventoryItem item
    {
        get{ return _item;}
        set
        { 
            _item = value;

            GetComponent<Image> ().sprite = _item.uiImage;
        }
    }
    private void Start() 
    {
        Inventory.onItemRemoved += onItemRemoved;
    }

    private void onItemRemoved(InventoryItem item)
    {
        if(item == _item)
        OnDestroy(gameObject);
    }

    private void OnDestroy() 
    {
        Inventory.onItemRemoved -= onItemRemoved;
    }
}
