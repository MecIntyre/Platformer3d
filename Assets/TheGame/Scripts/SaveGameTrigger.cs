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

        if (savegame.lastTriggerID != ID)
        {
            savegame.lastTriggerID = ID;
            savegame.save ();  
        } else
            Debug.Log ("Dieser Speicherpunkt hat bereits zuletzt gespeichert. Überspringe das Speichern");
    }

    private void OnDrawGizmos() 
    {
        if (UnityEditor.Selection.activeGameObject != this.gameObject)
        {
            Gizmos.color = Color.magenta;
            Matrix4x4 oldMatrix = Gizmos.matrix;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(GetComponent<BoxCollider>().center, GetComponent<BoxCollider>().size);
            Gizmos.matrix = oldMatrix;
        }
    }
}
