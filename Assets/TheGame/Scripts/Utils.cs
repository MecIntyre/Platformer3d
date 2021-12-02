using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    /* Zeichnet den Box-Collider. 
       <param name="mb">Monobehaviour, das einen Boxcollider als Geschwisterkomponente hat. </param> */
   public static void DrawBoxCollider(MonoBehaviour mb) 
   {
        if (UnityEditor.Selection.activeGameObject != mb.gameObject)
        {
            BoxCollider bc = mb.GetComponent<BoxCollider>();
            if (bc == null)
                return;
                
            Gizmos.color = Color.magenta;
            Matrix4x4 oldMatrix = Gizmos.matrix;
            Gizmos.matrix = mb.transform.localToWorldMatrix;
            Gizmos.DrawWireCube(bc.center, bc.size);
            Gizmos.matrix = oldMatrix;
        }   
   }
}
