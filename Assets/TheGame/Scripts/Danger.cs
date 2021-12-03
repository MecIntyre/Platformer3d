using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Implementiert eine Gefahrenquelle, die den Spieler verletzt.
public class Danger : MonoBehaviour
{
  
    private void OnCollisionEnter(Collision collision) 
    {
        Player p = collision.gameObject.GetComponent<Player> ();
        if (p != null) // Kollisionspartner ist der Spieler
        {
            p.looseHealth();
        }
    }

}
