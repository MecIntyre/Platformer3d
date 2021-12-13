using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Einsammelbare Schlüsselkarte
public class KeyCard : SaveableDestructable
{
    // Das Inventarobjekt, das den mitgeführten Gegenständen hinzugefügt wird
    public InventoryItem item;

    public GameObject aura;

    public void OnEnable()
    {
        aura.SetActive(true);
    }
    public void OnDisable()
    {
        aura.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) 
    {
        SaveGameData.current.inventory.add (item);
        gameObject.SetActive (false);
    }

    public void Update()
    {
        transform.Rotate(Vector3.up, 5f, Space.World);
    }
}
