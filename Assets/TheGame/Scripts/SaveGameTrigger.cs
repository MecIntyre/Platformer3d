using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Auslöser für automatischen Speicherpunkt
public class SaveGameTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log("Jetzt speichern");    
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.magenta;
        Matrix4x4 oldMatrix = Gizmos.matrix;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(GetComponent<BoxCollider>().center, GetComponent<BoxCollider>().size);
        Gizmos.matrix = oldMatrix;
    }
}
