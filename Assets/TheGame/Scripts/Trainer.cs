using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Verhalten für die Robo-Kugel
public class Trainer : BulletCatcher
{
    public override void onHitByBullet()
    {
        base.onHitByBullet();
        Debug.Log ("Trainer zerstört");
        Destroy (gameObject);
        
    }
}
