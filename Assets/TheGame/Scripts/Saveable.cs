using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Elternklasse für alle Verhalten, die Speichern und Laden implementieren wollen.
public class Saveable : MonoBehaviour
{
protected virtual void Awake()
    {
        SaveGameData.onSave += saveme;
        SaveGameData.onLoad += loadme;
    }

    protected virtual void Start() 
    {
        loadme (SaveGameData.current);
    }

    protected virtual void saveme(SaveGameData savegame)
    {
        Debug.Log ("Saveable will jetzt speichern.");
    }

    protected virtual void loadme(SaveGameData savegame)
    {
    }

    protected virtual void OnDestroy() 
    {
         SaveGameData.onSave -= loadme;
         SaveGameData.onSave -= saveme;
    }
}
