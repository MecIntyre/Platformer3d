using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Aktiviert ein Objeckt, wenn ein Trigger ausgelöst wird
public class TriggerGameObject : MonoBehaviour
{
    // Das Objekt, das durch den Trigger aktiviert wird
    public GameObject target;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.GetComponent<Player> () == null)
            return; //Kollision mit etwas anderem als den Spieler -> ignorieren

        target.GetComponent<Rigidbody> ().isKinematic = false;
    }
}
