using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    // Zwischengespeicherter Zeiger auf den menü-Canvas für schnelleren Zugriff.
    private Canvas canvas;

    void Start()
    {   canvas = GetComponent<Canvas> ();
        canvas.enabled = false;
    }

    /* Wahr, wenn die Taste bereits als gedrückt erkannt wurde.
       Nötig, um Mehrfachauswertungen der Menütaste zu verhindern.  */
    private bool keyWasPressed = false;

    void Update() {
        if (Input.GetAxisRaw ("Menu") > 0f)
        {
            if (!keyWasPressed)
            {
                canvas.enabled = canvas.enabled;
                
                Time.timeScale = canvas.enabled ? 0f : 1f ;      /* Alternative zu:     if (canvas.enabled)
                                                                                            Time.timeScale = 0f;
                                                                                        else
                                                                                            Time.timeScale = 1f; */
                                                                
            }
                
            keyWasPressed = true;
        } else
            keyWasPressed = false;
    }

    // Startet ein neues Spiel, bei Klick auf den Neu-Button.
    public void OnButtonNewPressed()
    {
        SaveGameData.current = new SaveGameData ();
        LevelManager lm = FindObjectOfType<LevelManager> ();
        lm.loadScene ("Scene1");

        canvas.enabled = false;
        Time.timeScale = 1f;
    }

    // Beendet das Spiel
    public void OnButtonQuitPressed()
    {
        Debug.Log ("Spiel beenden...");
        Application.Quit ();
    }
}
