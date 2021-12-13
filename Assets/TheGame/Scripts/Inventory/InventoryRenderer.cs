using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Stellt ein einzelnes Inventarobjekt auf dem UI-Canvas dar
public class InventoryRenderer : MonoBehaviour
{
    // Vorlage des Item Renderer-GameObjects, das für jedes Inventarobjekt dupliziert wird
    public GameObject itemRendererPrototype;

    // Start is called before the first frame update
    private void Start ()
    {
        Inventory.onItemAdded += Inventory_onItemAdded;
        itemRendererPrototype.SetActive (false);
    }

    private void Inventory_onItemAdded (InventoryItem item)
    {
        GameObject newItemRenderer = Instantiate(itemRendererPrototype, itemRendererPrototype.transform.parent);
        InventoryItemRenderer iir = newItemRenderer.AddComponent<InventoryItemRenderer>();
        iir.item = item;
        newItemRenderer.SetActive (true);
        doLayout ();
    }

    private void OnDestroy() 
    {
        Inventory.onItemAdded -= Inventory_onItemAdded;   
    }

    // Ordnet die sichtbaren Inventarobjekte nebeneinander an
    public void doLayout()
    {
        float x = 20f;
        foreach (InventoryItemRenderer r in FindObjectsOfType<InventoryItemRenderer>())
        {
            if (!r.enabled) continue;
            RectTransform rt = r.GetComponent<RectTransform> ();
            rt.anchoredPosition = new Vector2(x, rt.anchoredPosition.y);
            x += rt.sizeDelta.x + 20f;
        }
    }
}
