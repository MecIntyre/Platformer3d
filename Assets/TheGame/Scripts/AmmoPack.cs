using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sammelbares Munitionspaket, das Gun.ammo erhöht
public class AmmoPack : Saveable
{
    private void OnTriggerEnter(Collider other) 
    {
        Player p = other.gameObject.GetComponent<Player> ();
        if (p != null) // Kollision mit dem Spieler
        {
            p.GetComponentInChildren<Gun> ().ammo += 5;
            gameObject.SetActive (false);
        }
    }

    // Speicher-ID dieses Objekts
    public string ID = "";
    protected override void Start()
    {
        base.Start();
        if (ID == "")
            Debug.LogError ("Das AmmoPack "+gameObject.name+" hat keine ID bekommen");
    }

    protected override void saveme(SaveGameData savegame)
    {
        base.saveme(savegame);

        if (!gameObject.activeSelf && !savegame.collectedAmmos.Contains(ID)) // wenn deaktiviert und noch nicht gespeichert -> jetzt speichern
            savegame.collectedAmmos.Add (ID);

    }

    protected override void loadme(SaveGameData savegame)
    {
        base.loadme(savegame);
        if (savegame.collectedAmmos.Contains(ID)) // ich bin in der Liste der deaktivierten -> gameObject jetzt deaktivieren
        gameObject.SetActive (false);
    }
}
