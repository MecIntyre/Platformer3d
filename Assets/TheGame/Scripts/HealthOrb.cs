using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sammelbare heilungskugel, das Leben erhöht
public class HealthOrb : Saveable
{
    public void OnTriggerEnter(Collider other) 
    {   
        Player p = other.gameObject.GetComponent<Player> ();
        if (p != null) // Kollision mit dem Spiele
        {
            p.health += 0.25f;
            gameObject.SetActive (false);
        }
    }

    /// Speicher-ID dieses Objekts.
    public string ID = "";

    protected override void Start()
    {
        base.Start();
        if (ID == "") 
            Debug.LogError ("Die Healthorb "+gameObject.name+" hat keine ID bekommen!");
    }

    protected override void saveme(SaveGameData savegame)
    {
        base.saveme(savegame);

        if (!gameObject.activeSelf && !savegame.disabledHealthOrbs.Contains(ID)) // wenn deaktiviert und noch nicht gespeichert -> jetzt speichern
            savegame.disabledHealthOrbs.Add (ID);

    }

    protected override void loadme(SaveGameData savegame)
    {
        base.loadme(savegame);
        if (savegame.disabledHealthOrbs.Contains(ID)) // ich bin in der Liste der deaktivierten -> gameObject jetzt deaktivieren
        gameObject.SetActive (false);
    }
}
