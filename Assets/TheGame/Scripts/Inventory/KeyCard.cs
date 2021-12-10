using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Einsammelbare Schlüsselkarte
public class KeyCard : SaveableDestructable
{
    // Das Inventarobjekt, das den mitgeführten Gegenständen hinzugefügt wird
    public InventoryItem item;

    private void OnTriggerEnter(Collider other) 
    {
        SaveGameData.current.inventory.add (item);
        gameObject.SetActive (false);
    }
}
