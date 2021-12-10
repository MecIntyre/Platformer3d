using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Realisiert, wie die Tür mit einem Schalter, interaktiv geöffnet werden kann.
public class DoorSwitch : Saveable
{
    // Animator auf dem Tür-mesh, um das öffnen der Tür zu realisieren.
    public Animator doorAnimator;

    // Zeiger auf das Mesh, das die Lichter an der Schalterkonsole darstellt.
    public MeshRenderer mesh;

    // Schlüssel, der im Inventar vorhanden sein muss, damit der Schalter funktioniert
    public InventoryItem key;

    // Steuert die Tür mittels der Schaltkonsole, wenn die Feuer-Taste gedrückt wird.
    private void OnTriggerStay(Collider other) 
    {
        if (Input.GetAxisRaw ("Fire1") != 0f && !doorAnimator.GetBool("isOpen"))    
        {
            if (SaveGameData.current.inventory.contains (key))
                openTheDoor();   
            else
                Debug.Log ("Schlüssel fehlt");  
        }
    }

    // Öffnet die Tür, inklusive Synchronisation von Schalter und Türobjekt.
    private void openTheDoor()
    {
        doorAnimator.SetBool("isOpen", true);

        Material[] mats = mesh.materials;
        Material m2 = mats[2]; //ausgeschaltetes Material
        mats[2] = mats[1];
        mats[1] = m2;
        mesh.materials = mats;
    }

    protected override void saveme(SaveGameData savegame)
    {
        base.saveme(savegame);
        savegame.doorIsOpen = doorAnimator.GetBool ("isOpen");
    }

    protected override void loadme(SaveGameData savegame)
    {
        base.loadme (savegame);
        Debug.Log ("Doorswitch loadme");
        if (savegame.doorIsOpen)
            openTheDoor ();
    }

        private void OnDrawGizmos() 
    {
        Utils.DrawBoxCollider (this, Color.green);
    }
}
