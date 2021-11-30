using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Steuerung der Spielfigur
public class Player : MonoBehaviour
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

    // Zeiger auf die Physik-Komponente
    private Rigidbody rigid;

    /* Ist die Figur gerade auf dem Boden?
       Wenn false, fällt oder springt sie. */
    private bool onGround = false;

    private void Start()
    {
        rigid = GetComponent<Rigidbody> ();
    }

    // Update is called once per frame
    private void Update()
    {   
        float h = Input.GetAxis("Horizontal"); // Eingabesignal fürs Laufen

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
        onGround = Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, 0.12f, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore);
        if (Input.GetAxis("Jump") > 0f && onGround)
        {
            Vector3 power = rigid.velocity; 
            power.y = jumpPush;
            rigid.velocity = power;
        }
        rigid.AddForce(new Vector3 (0f, extraGravity, 0f));
    }
}
