using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetList
{
    // Findet das Inventory-Item-Asset mit der gegebenen ID
    ///<param name="ID">Idenfikator/Name des gesuchten Assets.</param>
    ///<returns>Zeiger auf das Asset or null, wenn nicht auffindbar.</returns>
    public InventoryItem findAsset(string ID)
    {
        InventoryItem[] cache = Resources.LoadAll<InventoryItem>("Items");
        foreach(InventoryItem ii in cache)
            if (ii.name == ID) return ii;
        
        return null;
    }
}
