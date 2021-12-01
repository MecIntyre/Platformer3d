using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
   private void Awake() 
   {
       SaveGameData.load();
   }

}
