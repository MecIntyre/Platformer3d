using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Auslöser für automatischen Speicherpunkt
public class SaveGameTrigger : MonoBehaviour
{
    // Speicher ID für den Trigger, womit ein mehrmaliges Triggern verhindert wird.
    public string ID = "";

    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log("Jetzt speichern");  
        SaveGameData savegame = SaveGameData.current;

        Player p = other.gameObject.GetComponent<Player> ();
        if (p == null) // Kein Spieler
        {
            // Kollision mit anderem Objekt als Spieler -> Ignorieren.
            return;
        }
        else if (p.health <= 0f) // Spieler schon tot
        {
            Debug.Log ("Der Spieler hat keine Gesundheitspunkte mehr. Überspringe das Speichern");
        }
        else if (savegame.lastTriggerID == ID) //Speicherpunkt schon gespeichert.
        {
            Debug.Log ("Dieser Speicherpunkt hat bereits zuletzt gespeichert. Überspringe das Speichern");
        }
        else // Speichern möglich
        {
            savegame.lastTriggerID = ID;
            savegame.save ();  
        }
    }

    private void OnDrawGizmos() 
    {
        Utils.DrawBoxCollider (this, Color.magenta);
    }
}
