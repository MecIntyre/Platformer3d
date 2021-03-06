using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

// Steuerung der Spielfigur
public class Player : Saveable
{
    // Laufgeschwindigkeit der Figur.
    public float speed = 0.05f;

    // Die Kraft, mit der nach oben gesprungen wird.
    public float jumpPush = 1f;

    // Verstärkung der Gravitation, damit die Figur schneller fällt.
    public float extraGravity = 20f;

    // Grafisches Modell, u.a. für die Drehung in Laufrichtung.
    public GameObject model;

    // Der Winkel zu dem sich die Figur um die eigene Achse (=Y) drehen soll
    private float towardsY = 0f;

    // Zeiger auf die Physik-Komponente der Spielfigur.
    private Rigidbody rigid;

    // Zeiger auf die Animations-Komponente
    private Animator anim;

    /* Ist die Figur gerade auf dem Boden?
       Wenn false, fällt oder springt sie. */
    private bool onGround = false;

    // Soundeffekt für's Springen
    public AudioSource soundJump;

    // Soundeffekt für's Springen
    public AudioSource soundDeath;

    protected override void Start()
    {
        rigid = GetComponent<Rigidbody> ();
        anim = GetComponentInChildren<Animator>();

        base.Start ();
        setRagdollMode (false);
    }

    // Aktiviert oder deaktiviert die Ragdoll-Simulation
    /// <param name="isDead">Wenn true, dann ist die Ragdoll aktiv, sonst der interaktive Spielmodus</param>
    private void setRagdollMode(bool isDead)
    {

        foreach (Collider c in GetComponentsInChildren<Collider>())
        {
            if (c.gameObject.name.StartsWith("mixamorig:")) //nur wenn dies ein (Ragdoll)-bone ist
                c.enabled = isDead;
        }

        foreach (Rigidbody r in GetComponentsInChildren<Rigidbody>())
        {
            if (r.gameObject.name.StartsWith("mixamorig:"))
                r.isKinematic = !isDead;
        }

        GetComponent<Rigidbody> ().isKinematic = isDead;
        GetComponent<Collider> ().enabled = !isDead;
        GetComponentInChildren<Animator> ().enabled = !isDead;

        if (isDead)
        {
            ScreenFader sf = FindObjectOfType<ScreenFader> ();
            sf.fadeOut (true, 1f);

            CinemachineVirtualCamera cvc = FindObjectOfType<CinemachineVirtualCamera> ();
            if (cvc != null)
            {
                cvc.Follow = null;
                cvc.LookAt = null;
            }

            soundDeath.Play ();
            enabled = false;
        }
    }

    private float _health = 1f;
    // Aktueller Gesundheitszustand in Prozent, von 0 - 1.
    public float health
    {
        get{ return _health;}
        set{ _health = Mathf.Clamp01(value); }
    }

    // Tod der Spielfigur
    public void looseHealth()
    {
        health -= 0.5f;

        if (health <= 0f)
            setRagdollMode (true);
    }

    // Update is called once per frame
    private void Update()
    {   
        if (Time.timeScale == 0f) return; // Wenn pausiert, update abbrechen.

        if (transform.position.y < -2.34f) //Wenn Spieler runtergefallen...sterben
        {
            looseHealth ();
            return;
        }

        float h = Input.GetAxis("Horizontal"); // Eingabesignal fürs Laufen
        anim.SetFloat("forward", Mathf.Abs(h));

        // Vorwärtsbewegung
        transform.position += h * speed * transform.forward;

        // Drehung
        if (h > 0f) // nach rechts gehen
            towardsY = 0f;
        else if (h < 0f) // nach links gehen 
            towardsY = -180f;

        model.transform.rotation = Quaternion.Lerp(model.transform.rotation,Quaternion.Euler(0f, towardsY, 0f), Time.deltaTime * 10f);

        // Springen
        RaycastHit hitInfo; 
        onGround = Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, 0.25f, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore);
        
        if (onGround && Vector3.Angle (Vector3.up, hitInfo.normal) > 10) //rutschen an Schrägen
        {
            rigid.AddForce(hitInfo.normal);
        }
        
        anim.SetBool ("grounded", onGround);
        if (Input.GetAxis("Jump") > 0f && onGround)
        {
            Vector3 power = rigid.velocity; 
            power.y = jumpPush;
            rigid.velocity = power;

            if (!soundJump.isPlaying)
                soundJump.Play ();
        }
        rigid.AddForce(new Vector3 (0f, extraGravity, 0f));

        // Schießen
        if (Input.GetAxisRaw("Fire2") > 0f)
        {
            GetComponentInChildren<Gun> ().shoot ();
        }
    }
    // Farbige Zeichnung des Hilfsstrahl für Entfernungsmessung beim Springen (Raycast)
    private void OnDrawGizmos() 
    {
        if (onGround) Gizmos.color = Color.magenta;
        else Gizmos.color = Color.green;

        Vector3 rayStartPosition = transform.position + (Vector3.up * 0.1f);
        Gizmos.DrawLine(rayStartPosition, rayStartPosition + (Vector3.down * 0.12f));
        
    }
        /* Das Ziel, das die Kamera verfolgt.
           Normalerweise ist das der Hüftknochen */
        public GameObject cameraTarget;

        protected override void Awake()
    {
        base.Awake ();

        CinemachineVirtualCamera cvc = FindObjectOfType<CinemachineVirtualCamera> ();
        if (cvc != null)
        {
            cvc.Follow = transform;
            cvc.LookAt = cameraTarget.transform;
        }
    }

    // Speicherpunkt für Spielerposition
    protected override void saveme(SaveGameData savegame)
    {
        base.saveme (savegame);

        savegame.playerPosition = transform.position;
        savegame.recentScene = gameObject.scene.name;
        savegame.playerHealth = health;
    }

    /* Nur wenn die geladene Szene die ist, in der zuletzt die Position gespeichert wurde,
       wird die gespeicherte Spielerposition wieder hergestellt. */
    protected override void loadme(SaveGameData savegame)
    {
        base.loadme (savegame);

        if (savegame.recentScene == gameObject.scene.name)
            transform.position = savegame.playerPosition;
        health = savegame.playerHealth;
    }

}
