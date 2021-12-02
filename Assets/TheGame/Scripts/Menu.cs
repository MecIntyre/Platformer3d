using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Canvas> ().enabled = false;
    }

    /* Wahr, wenn die Taste bereits als gedrückt erkannt wurde.
       Nötig, um Mehrfachauswertungen der Menütaste zu verhindern.  */
    private bool keyWasPressed = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw ("Menu") > 0f)
        {
            if (!keyWasPressed)
            GetComponent<Canvas> ().enabled = !GetComponent<Canvas>().enabled;

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

        GetComponent<Canvas> ().enabled = false;
    }

    // Beendet das Spiel
    public void OnButtonQuitPressed()
    {
        Debug.Log ("Spiel beenden...");
        Application.Quit ();
    }
}
