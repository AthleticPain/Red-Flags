using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    const string MAIN_MENU_SCENE_STRING_REFERENCE = "Main Menu";

    private void Start()
    {
        if(FindObjectsOfType<LevelLoader>().Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public static void LoadNextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1);
    }

    public static void ReloadCurrentScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex);
    }

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene(MAIN_MENU_SCENE_STRING_REFERENCE);
    }
}
