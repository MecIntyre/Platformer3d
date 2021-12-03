using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Implementiert eine Gefahrenquelle, die den Spieler verletzt.
public class Danger : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (!enabled) // Wenn das Script inaktiv ist, nicht auf Kollision reagieren
            return;

        Player p = collision.gameObject.GetComponent<Player> ();
        if (p != null) // Kollisionspartner ist der Spieler
        {
            p.looseHealth ();
        }
    }
}