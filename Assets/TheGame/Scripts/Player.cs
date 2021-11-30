using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Steuerung der Spielfigur
public class Player : MonoBehaviour
{
    //Laufgeschwindigkeit der Figur.
    public float speed = 0.05f;

    //Grafisches Modell, u.a. für die Drehung in Laufrichtung.
    public GameObject model;

    // Update is called once per frame
    private void Update()
    {
        transform.position += Input.GetAxis("Horizontal") * speed * transform.forward;

        if (Input.GetAxis("Horizontal") > 0f)
            model.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        else if (Input.GetAxis("Horizontal") < 0f)
            model.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    }
}
