using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Steuerung der Spielfigur
public class Player : MonoBehaviour
{
    // Laufgeschwindigkeit der Figur.
    public float speed = 0.05f;

    // Grafisches Modell, u.a. für die Drehung in Laufrichtung.
    public GameObject model;

    // Der Winkel zu dem sich die Figur um die eigene Achse (=Y) drehen soll
    private float towardsY = 0f;

    // Update is called once per frame
    private void Update()
    {   
        float h = Input.GetAxis("Horizontal");

        transform.position += h * speed * transform.forward;

        if (h > 0f) // nach rechts gehen
            towardsY = 0f;
        else if (h < 0f) // nach links gehen 
            towardsY = -180f;

        model.transform.rotation = Quaternion.Lerp(model.transform.rotation,Quaternion.Euler(0f, towardsY, 0f), Time.deltaTime * 10f);
    }
}
