using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : Saveable
{
    public string ID = "";

    protected override void saveme (SaveGameData savegame)
    {
        base.saveme (savegame);

        SaveGameData.BarrelData data = savegame.findBarrelDataByID (ID);
        if (data == null) 
        {
            data = new SaveGameData.BarrelData ();
            savegame.barrelData.Add (data);
        }
        data.ID = ID;
        data.position = transform.position;
    }

    private bool loadingComplete = false;

    protected override void loadme (SaveGameData savegame)
    {
        base.loadme (savegame);

        SaveGameData.BarrelData data = savegame.findBarrelDataByID (ID);
        if (data != null) // Daten gefunden
            transform.position = data.position;

        loadingComplete = true;
    }

    private Rigidbody r;
    protected override void Start()
    {
        base.Start ();
        r = GetComponent<Rigidbody> ();
        
        if (ID == "")
            Debug.LogWarning ("Das Fass "+gameObject+" braucht noch eine ID");
    }

    private void Update() 
    {
        if (loadingComplete && r.velocity.magnitude < 0.1f) // Wenn geladen + Fass quasi gestoppt
        {
            GetComponent<Danger> ().enabled = false;
            this.enabled = false;
        }
    }
}