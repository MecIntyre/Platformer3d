using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToScene : MonoBehaviour
{
    
    // Name der Szene, die geladen wird, wenn die Figur den Trigger auslöst.
    public string scene = "";

    private void OnTriggerEnter(Collider other) 
    {
        LevelManager lm = FindObjectOfType<LevelManager> ();
        lm.loadScene (scene);
    }

    private void OnDrawGizmos() 
    {
        Utils.DrawBoxCollider (this);
    }
}
