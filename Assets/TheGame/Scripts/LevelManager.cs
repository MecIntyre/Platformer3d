using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Lädt zu beginn automatisch den aktuellen Spielstand.
    private void Awake() 
    {
        SaveGameData.current = SaveGameData.load();
    }

    // Lädt zu beginn automatisch die aktuelle Szene.
    private void start()
    {
        loadScene (SaveGameData.current.recentScene);
    }

    // Laden der neuen Szene und entladen der alten.
    public void loadScene(string name)
    {
        if (name == "")
            return; // Ungültiger Aufruf bei fehlendem Szenennamen

        for (int i = SceneManager.sceneCount -1; i > 0; i--)
        {
            SceneManager.UnloadSceneAsync (SceneManager.GetSceneAt(i).name);
        }

        Debug.Log ("Lade jetzt Szene: " + name);
        SceneManager.LoadScene(name, LoadSceneMode.Additive);
    }
    
    #if UNITY_EDITOR
    // Spielstand schnell laden und speichern durch drücken von Taste 1 und 2
    private void Update() 
    {
        if (Input.GetKeyUp (KeyCode.Alpha1))
            SaveGameData.current = SaveGameData.load();
        else if (Input.GetKeyUp (KeyCode.Alpha2))
            SaveGameData.current.save ();
    }  
    #endif
}
