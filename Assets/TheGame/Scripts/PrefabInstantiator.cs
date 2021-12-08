using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Erzeugt eine Pefab-Instanz unf kopiert Position und Rotation
public class PrefabInstantiator : MonoBehaviour
{
   // Das Prefab
   public GameObject prototype;

   private void Awake() 
   {
        Instantiate (prototype, transform.position, transform.rotation, transform.parent);
        Destroy (gameObject);
   }
}
