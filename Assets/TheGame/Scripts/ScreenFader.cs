using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenFader : MonoBehaviour
{   
    //Zeiger auf das Canvas
    public Image overlay;

    // CoRoutine zum Ein- und Ausblenden der Szene.
    private IEnumerator performFading(float toAlpha, bool revertToSaveGame)
    {
        overlay.CrossFadeAlpha (toAlpha, 1f, false);

        yield return new WaitForSeconds (1f);

        if (revertToSaveGame)
        {
        SaveGameData.current = SaveGameData.load ();
        LevelManager lm = FindObjectOfType<LevelManager> ();
        lm.loadScene (SaveGameData.current.recentScene);
        }
    }

    /// Blendet die Szene ein.
    /// <param name="revertToSavegame">Wenn true, wird nach dem überblenden der letzte Speicherstand geladen</param>
    public void fadeIn(bool revertToSaveGame)
    {
        StartCoroutine(performFading (0f, revertToSaveGame));
    }

    /// Blendet die Szene aus.
    /// <param name="revertToSavegame">Wenn true, wird nach dem überblenden der letzte Speicherstand geladen</param>
        public void fadeOut(bool revertToSaveGame)
    {
        StartCoroutine(performFading (1f, revertToSaveGame));
    }

    private void Awake() 
    {
        overlay.gameObject.SetActive (true);
        SceneManager.sceneLoaded += WhenLevelWasLoaded;
    }   

    private void OnDestroy() 
    {
        SceneManager.sceneLoaded -= WhenLevelWasLoaded;   
    }

    private void WhenLevelWasLoaded(Scene scene, LoadSceneMode mode) 
    {
        fadeIn (false);
    }

}