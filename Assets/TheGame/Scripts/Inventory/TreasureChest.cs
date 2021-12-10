using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : SaveableDestructable
{
    // Schlüssel, der sich im Inventar befinden muss, um die Schatzkiste zu öffnen
    public InventoryItem key;

    // Objekt, das sich in der Kiste befindet
    public InventoryItem treasure;

    private bool keyWasPressed = false;

    private void OnTriggerStay(Collider collider)
    {
        if (Input.GetAxisRaw("Fire1") != 0) //Aktionstaste gedrückt
        {
            if (keyWasPressed) return;
            keyWasPressed = true;
            if (SaveGameData.current.inventory.contains (key))
            {
                SaveGameData.current.inventory.add (treasure);
                SaveGameData.current.inventory.remove (key);
                gameObject.SetActive (false);
            }
            else // Wenn der Schlüssel nicht im Inventar ist
            {
                Debug.Log ("Schlüsselobjekt fehlt");
            }
        }
        else
            keyWasPressed = false;
    }
}
