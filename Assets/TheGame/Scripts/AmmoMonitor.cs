using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoMonitor : MonoBehaviour
{
    // Zeiger auf das UI-text-Object
    public Text uiText;

    // Zeiger auf die aktuelle Waffe des Spielers
    private Gun gun;

    // Update is called once per frame
    void Update()
    {
        if (gun = null)
        {
            Player p = FindObjectOfType<Player> ();
            gun = p.GetComponentInChildren<Gun> ();
        }
        else
            uiText.text = gun.ammo.ToString();
    }
}
