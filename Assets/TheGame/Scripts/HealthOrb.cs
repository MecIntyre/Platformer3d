using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sammelbare heilungskugel, das Leben erhöht
public class HealthOrb : SaveableDestructable
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
}
