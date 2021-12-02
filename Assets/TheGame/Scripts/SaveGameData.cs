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

    public bool doorIsOpen = false;

    /* Die ID des zuletzt ausgelösten Save-Triggers. 
       <seealso cref="SaveGameTrigger.ID"/> */
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

    /* Liefert den Namen der Datei, in die der Spielstand geschrieben wird
       <returns>Name der Spielstandatei</returns> */
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
