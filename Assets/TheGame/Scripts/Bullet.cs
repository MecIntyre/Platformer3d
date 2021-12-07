using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Steuert das Verhalten, einer abgeschossenen Pistolenkugel
public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody> ().velocity = Vector3.forward * (transform.rotation.y < 0f ? 5f : -5f);
    }

    private void OnCollisionEnter(Collision collision) 
    {
        BulletCatcher bc = collision.gameObject.GetComponent<BulletCatcher> ();
        if (bc != null)
            bc.onHitByBullet ();
        Destroy (gameObject); // Löschen der Kugel, bei Treffer
    }
}