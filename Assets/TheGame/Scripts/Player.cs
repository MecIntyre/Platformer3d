using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Steuerung der Spielfigur

public class Player : MonoBehaviour
{
    public float speed = 0.05f;

    private void Update()
    {
        transform.position += Input.GetAxis("Horizontal") * speed * transform.forward;
    }
}
