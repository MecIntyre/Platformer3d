using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Speichert eine Liste der mitgeführten Gegenstände im Inventar
und übernimmt das Laden + Speichern */
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
    }
