using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Implementiert das verhalten der Pistole
public class Gun : MonoBehaviour
{
    // Lichtquelle für den Schuss
    private Light fireLight;

    private Animator playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        fireLight = GetComponentInChildren<Light> ();
        fireLight.enabled = false;
        playerAnim = GetComponentInParent<Animator> ();
    }

    // Ist der vorherige Schuss schon fertig?
    private bool shotDone =true;

    //Feuert einen Schuss aus der Pistole ab
    public void shoot()
    {
        if (shotDone)
            StartCoroutine(doShoot ());
    }

    // Regelt die Schuss-Ausführung
    private IEnumerator doShoot()
    {
        shotDone = false;

        playerAnim.SetTrigger("gunShot");
        fireLight.enabled = true;
        yield return new WaitForSeconds (0.1f);
        fireLight.enabled = false;

        shotDone = true;
    }
}
