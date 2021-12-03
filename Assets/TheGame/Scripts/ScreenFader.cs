using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{   
    //Zeiger auf das Canvas
    public Image overlay;

    void Start()
    {
        fadeIn();
    }

    // Funktion zum Ein- und Ausblenden der Szene.
    private void performFading(float toAlpha)
    {
        overlay.CrossFadeAlpha (toAlpha, 1f, false);
    }

    // Blendet die Szene ein.
    public void fadeIn()
    {
        performingFading (0f);
    }

    // Blendet die Szene aus.
        public void fadeOut()
    {
        performingFading (1f);
    }
}