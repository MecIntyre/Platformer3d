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
    /// <returns>(Enumerator)</returns>
    /// <param name="toAlpha">Ziel-Transparenz zwischen 0 und 1</param>
    /// <param name="revertToSaveGame">Wenn true, wird nach dem Überblenden, der letzte Spielstand geladen.</param>
    /// <param name="delay">Wartezeit in Sekunden, vor der Überblendung.</param>
    private IEnumerator performFading(float toAlpha, bool revertToSaveGame, float delay)
    {
        if (delay > 0f) 
            yield return new WaitForSeconds (delay);

        overlay.CrossFadeAlpha (toAlpha, 1f, false);

        yield return new WaitForSeconds (1f);

        if (revertToSaveGame)
        {
        SaveGameData.current = SaveGameData.load ();
        LevelManager lm = FindObjectOfType<LevelManager> ();
        lm.loadScene (SaveGameData.current.recentScene);
        }
    }

    // Blendet die Szene ein.
    /// <param name="revertToSavegame">Wenn true, wird nach dem überblenden der letzte Speicherstand geladen</param>
    /// <param name="delay">Wartezeit in Sekunden, vor der Einblendung.</param>
    public void fadeIn(bool revertToSaveGame, float delay = 0f)
    {
        StartCoroutine(performFading (0f, revertToSaveGame, delay));
    }

    // Blendet die Szene aus.
    /// <param name="revertToSavegame">Wenn true, wird nach dem überblenden der letzte Speicherstand geladen</param>
    /// <param name="delay">Wartezeit in Sekunden, vor der Ausblendung.</param>
        public void fadeOut(bool revertToSaveGame, float delay = 0f)
    {
        StartCoroutine(performFading (1f, revertToSaveGame, delay));
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