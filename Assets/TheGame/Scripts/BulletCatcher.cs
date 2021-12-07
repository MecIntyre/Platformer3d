using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Basis-Script für alle Dinge, die mit der Pistole abgeschossen werden können
public class BulletCatcher : MonoBehaviour
{
   public virtual void onHitByBullet()
       {
            Debug.Log(gameObject.name+" wurde von einer Kugel getroffen");
       }
}
