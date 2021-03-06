using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Verhalten für die Robo-Kugel
public class Trainer : BulletCatcher
{
    // Prefab, das die Explosion realisiert
    public GameObject explosionPrototype;
    
    // Soundplayer für den Explosionseffekt
    public GameObject explosionAudioPrototype;

    public override void onHitByBullet()
    {
        base.onHitByBullet();
        Debug.Log ("Trainer zerstört");

        Instantiate (explosionPrototype, transform.position, transform.rotation);
        Instantiate (explosionAudioPrototype, transform.position, transform.rotation);

        gameObject.SetActive (false);
    }
}
