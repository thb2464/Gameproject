using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit(); //Quits the game (only works on build)

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //Exist play mode (will only be excuted in the editor)
#endif
    }
}
