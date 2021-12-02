using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private void Awake() 
    {
        SaveGameData.current = SaveGameData.load();
    }

    public void loadScene(string name)
    {
        Debug.Log ("Lade jetzt Szene:" + name);
        SceneManager.LoadScene(name, LoadSceneMode.Additive);
    }
        
    private void Update() 
    {
        if (Input.GetKeyUp (KeyCode.Alpha1))
            SaveGameData.current = SaveGameData.load();
    }

}
