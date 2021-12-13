using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Repräsentiert die Daten eines einsammelbaren Objekts
[CreateAssetMenu(menuName="Inventar-Asset", fileName="Neues Inventarobjekt.asset", order=300)]
public class InventoryItem : ScriptableObject 
{
    // Das Bild, das für dieses Objekt angezeigt wird
    public Sprite uiImage;
}

