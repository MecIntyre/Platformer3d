using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Realisiert, wie die Tür mit einem Schalter, interaktiv geöffnet werden kann.
public class DoorSwitch : MonoBehaviour
{
    // Animator auf dem Tür-mesh, um das öffnen der Tür zu realisieren.
    public Animator doorAnimator;

    // Zeiger auf das Mesh, das die Lichter an der Schalterkonsole darstellt.
    public MeshRenderer mesh;

    // Steuert die Tür mittels der Schaltkonsole, wenn die Feuer-Taste gedrückt wird.
    private void OnTriggerStay(Collider other) 
    {
        if (Input.GetAxisRaw ("Fire1") != 0f && !doorAnimator.GetBool("isOpen"))    
        {
            openTheDoor();     
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

    private void Awaker()
    {
        SaveGameData.onSave += saveme;
    }

    private void savme(SaveGameData savegame)
    {
        savegame.doorIsOpen = doorAnimator.GetBool ("isOpen");
    }

    private void OnDestroy() 
    {
         SaveGameData.onSave -= saveme;
    }
}
