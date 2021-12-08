using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Implementiert das verhalten der Pistole
public class Gun : Saveable
{
    // Lichtquelle für den Schuss
    private Light fireLight;

    // Verweis auf den Animator
    private Animator playerAnim;

    // Sound für den Pistolenschuss
    public AudioSource soundShot;

    // Sound für den leeren Pistolenschuss
    public AudioSource soundEmpty;

    // Sound für das nachladen der Waffe / aufheben Ammopack
    public AudioSource soundLoad;

    // Anzahl der Patronen in der Waffe
    private int _ammo = 3;
    public int ammo 
    { 
        get{ return _ammo; }
        set{ 
            if (!soundLoad.isPlaying && value > _ammo) 
            {
                soundLoad.Play(); 
            }
            _ammo = value;
        }
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        fireLight = GetComponentInChildren<Light> ();
        fireLight.enabled = false;
        playerAnim = GetComponentInParent<Animator> ();

        bulletPrototype.SetActive (false);

        base.Start();
    }

    // Ist der vorherige Schuss schon fertig?
    private bool shotDone = true;

    //Feuert einen Schuss aus der Pistole ab
    public void shoot()
    {
        if (shotDone)
        {   
            if (ammo > 0)
                StartCoroutine(doShoot ());
            else
                if (!soundEmpty.isPlaying) soundEmpty.Play();
        }
    }

    // Original Kugel, die dupliziert in die Szene geschossen wird
    public GameObject bulletPrototype;

    // Regelt die Schuss-Ausführung
    private IEnumerator doShoot()
    {
        shotDone = false;

        soundShot.Play ();
        GameObject bullet = Instantiate (bulletPrototype,bulletPrototype.transform.parent);
        bullet.transform.parent = null;
        bullet.SetActive (true);
        ammo -= 1; // Patrone verbrauchen

        playerAnim.SetTrigger("gunShot");
        fireLight.enabled = true;
        yield return new WaitForSeconds (0.1f);
        fireLight.enabled = false;

        while(playerAnim.GetCurrentAnimatorStateInfo(1).IsName ("gunShot"))
            yield return new WaitForEndOfFrame ();

        shotDone = true;
    }

    protected override void saveme (SaveGameData savegame)
    {
        base.saveme (savegame);
        savegame.playerAmmo = ammo;
    }

    protected override void loadme (SaveGameData savegame)
    {
        base.loadme (savegame);
        ammo = savegame.playerAmmo;
    }
}
