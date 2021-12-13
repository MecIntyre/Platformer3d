using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Speichert eine Liste der mitgeführten Gegenstände im Inventar
und übernimmt das Laden + Speichern */
[System.Serializable]
public class Inventory
    {
        /*  Signatur der Event-Listener, die aufgerufen werden sollen, 
            wenn sich etwas am Inventar verändert */
        /// <param name="item">Das veränderte Item</param>
        public delegate void ItemChangeEvent(InventoryItem item);

        // Wird aufgerufen, wenn ein Objekt ins Inventar aufgenommen wird
        public static event ItemChangeEvent onItemAdded;

        // Wird aufgerufen, wenn ein Objekt aus dem Inventar genommen wird
        public static event ItemChangeEvent onItemRemoved;

        private List<InventoryItem> items = new List<InventoryItem> ();   

        // Gibt Lesezugriff auf die aktuelle Item-Sammlung
        ///<returns>Item-Object im Inventar.</returns>
        public List<InventoryItem> getItems()
        {
            return items;
        }

        /*  Fügt ein Inventarobjekt in die Liste der mitgeführten Gegenstände ein.
            Doppelte Einträge sind möglich! */
        /// <param name="item">object, das ins Inventar aufgenommen werden soll,</param>
        public void add(InventoryItem item)
        {
            Debug.Log ("Inventar erhält " + item);
            items.Add (item);
            if (onItemAdded != null) onItemAdded (item);
        }

        // Prüft, ob sich ein Objekt derzeit im Inventar befindet
        /// <param name"item">Gesuchtes Objekt.</param>
        /// <return>True, wenn das Objekt derzeit mitgeführt wird.</returns>
        public bool contains(InventoryItem item)
        {
            return items.Contains (item);
        }

        public void remove(InventoryItem item)
        {
            Debug.Log ("Inventar verliert " + item);
            items.Remove (item);
            if (onItemRemoved != null) onItemRemoved (item);
        }
        
        // Liste der IDs/Namen der Scriptable-Objects im Inventar (*Assets)
        public List<string> IDs;
        public void save()
        {    
            IDs = new List<string>();
            foreach(InventoryItem ii in items) IDs.Add(ii.name);
        }

        public void load()
        {    
            items.Clear();
            AssetList al = new AssetList();
            foreach(string ID in IDs)
            {
                InventoryItem ii = al.findAsset(ID); // finde das .asset für die ID
                if (ii != null) items.Add(ii);
                else Debug.LogWarning("Inventar konnte kein Asset für die ID=" + ID + "Finden!");
            }
        }
    }
