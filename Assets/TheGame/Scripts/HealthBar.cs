using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Zeigt die Spieler-Gesundheit in Form eines Fortschrittbalkens
public class HealthBar : MonoBehaviour
{

    // Zeiger auf die skalierte Grafik
    public Image progressbar;

    // Zeiger auf die aktuelle Spieler-Komponente
    private Player player;

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            player = FindObjectOfType<Player> ();
        else 
            progressbar.fillAmount = player.health;
    }
}
