using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevProfi.Utils;
using System.IO;

[System.Serializable]
public class SaveGameData
{
    public Vector3 playerPosition = Vector3.zero;

    public bool doorIsOpen = false;

    public delegate void SaveHandler(SaveGameData saveGame);

    public static event SaveHandler onSave;
    public static event SaveHandler onLoad;


    private static string getFilename()
    {
        return Application.persistentDataPath + Path.DirectorySeparatorChar + "savegame.xml";
    }

    // Speichert einen Spielstand
    public void save()
    {
        Debug.Log ("Speichere Spielstand " +getFilename());

        Player p = Component.FindObjectOfType<Player> ();
        playerPosition = p.transform.position;

        if (onSave!=null) onSave(this);

        string xml = XML.Save(this);
        File.WriteAllText(getFilename (), xml);

        Debug.Log (xml);
    }

    // Lädt einen Spielstand
    public static SaveGameData load()
    {
        Debug.Log ("Lade Spielstand " + getFilename ());
        SaveGameData save = XML.Load<SaveGameData> (File.ReadAllText(getFilename()));

        Player p = Component.FindObjectOfType<Player> ();
        p.transform.position = save.playerPosition;

        if(onLoad!=null) onLoad(save);

        return save;
    }

}
