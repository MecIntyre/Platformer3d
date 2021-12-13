using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Stellt ein einzelnes Inventarobjekt auf dem UI-Canvas dar
public class InventoryRenderer : MonoBehaviour
{
    // Vorlage des Item Renderer-GameObjects, das für jedes Inventarobjekt dupliziert wird
    public GameObject itemRendererPrototype;

    // Start is called before the first frame update
    private void Start ()
    {
        SceneManager.sceneLoaded += onSceneLoaded;
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
        SceneManager.sceneLoaded -= onSceneLoaded;
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

    private void onSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Renderer komplett neu aufbauen, um aktuellen Zustand des aktiven Savegames zu zeigen
        foreach(InventoryItemRenderer iir in FindObjectsOfType<InventoryItemRenderer>())
            Destroy(iir.gameObject);
        foreach(InventoryItem ii in SaveGameData.current.inventory.getItems())
            Inventory_onItemAdded(ii);
    }    
}
