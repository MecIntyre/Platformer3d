using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sammelbares Munitionspaket, das Gun.ammo erhöht
public class AmmoPack : SaveableDestructable
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
}
