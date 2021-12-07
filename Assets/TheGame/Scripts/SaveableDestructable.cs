using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Elternklasse für Saveables, die ihren Sichtbarkeitszustand speichern und wiederherstellen
public class SaveableDestructable : Saveable
{
    
        /// Speicher-ID dieses Objekts.
    public string ID = "";

    protected override void Start()
    {
        base.Start ();
        if (ID == "") 
            Debug.LogError ("Das speicherbare Objekt "+gameObject.name+" hat keine ID bekommen!");
    }

    protected override void saveme(SaveGameData savegame)
    {
        base.saveme (savegame);

        if (!gameObject.activeSelf && !savegame.destroyedObjects.Contains(ID)) // wenn deaktiviert und noch nicht gespeichert -> jetzt speichern
            savegame.destroyedObjects.Add (ID);

    }

    protected override void loadme(SaveGameData savegame)
    {
        base.loadme (savegame);

        if (savegame.destroyedObjects.Contains(ID)) // ich bin in der Liste der deaktivierten -> gameObject jetzt deaktivieren
            gameObject.SetActive (false);
    }

}
