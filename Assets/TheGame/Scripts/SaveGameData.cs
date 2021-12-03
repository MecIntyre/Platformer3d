using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevProfi.Utils;
using System.IO;

[System.Serializable]
public class SaveGameData
{
    // Das Aktuelle Savegame.
    public static SaveGameData current = new SaveGameData();

    public Vector3 playerPosition = Vector3.zero;
    public float playerHealth = 1f;

    // Liste der IDs aller Health-Orbs, die bereits eingesammelt wurden.
    public List<string> disabledHealthOrbs = new List<string> ();

    [System.Serializable]
    public class BarrelData
    {
        public string ID="";
        public Vector3 position=Vector3.zero;
    }

    // Position der Fässer
    public List<BarrelData> barrelData = new List<BarrelData> ();

    // Sucht die gespeicherten Daten für das Fass mit der gegebenen ID.
    /// <param name="ID">ID des gesuchten Fasses.</param>
    /// <returns>Datensatz für das Fass mit der gegebenen ID oder NULL, wenn nicht vorhanden</return>
    public BarrelData findBarrelDataByID (string ID)
    {
        foreach(BarrelData bd in barrelData)
            if (bd.ID == ID)
                return bd;
        return null;
    }

    public bool doorIsOpen = false;

    /// <summary>Die ID des zuletzt ausgelösten Save-Triggers. </summary>
    /// <seealso cref="SaveGameTrigger.ID"/>
    public string lastTriggerID="";

    // Name der Szene, in der sich die Spielfigur momentan befindet.
    public string recentScene = "";

    /* Methoden, die sich in ein Save-Event eintragen wollen, 
       müssen von dieser Form sein. */
    public delegate void SaveHandler(SaveGameData saveGame);

    /* Methoden, die sich hier Eintragen, werden aufgerufen, 
       wenn sich Szenenobjekte ihren Zustand in den Speicherstand eintragen sollen */
    public static event SaveHandler onSave;

    /* Methoden, die sich hier Eintragen, werden aufgerufen, 
       wenn ein Spielstand aus einer Savegame-Datei geladen wurde. 
       Die Methoden sollten das Wiederherstellen des Objektzustands
       aus dem Spielstand implementieren. */
    public static event SaveHandler onLoad;

    /// <summary>Liefert den Namen der Datei, in die der Spielstand geschrieben wird</summary>
    /// <returns>Name der Spielstandatei</returns>
    private static string getFilename()
    {
        return Application.persistentDataPath + Path.DirectorySeparatorChar + "savegame.xml";
    }

    // Speichert einen Spielstand
    public void save()
    {
        Debug.Log ("Speichere Spielstand " +getFilename());

        if (onSave!=null) onSave(this);

        string xml = XML.Save(this);
        File.WriteAllText(getFilename (), xml);

        Debug.Log (xml);
    }

    // Lädt einen Spielstand
    public static SaveGameData load()
    {   
        if (!File.Exists (getFilename ()))
            return new SaveGameData();

        Debug.Log ("Lade Spielstand " + getFilename ());
        SaveGameData save = XML.Load<SaveGameData> (File.ReadAllText(getFilename()));

        if(onLoad!=null) onLoad(save);

        return save;
    }

}
