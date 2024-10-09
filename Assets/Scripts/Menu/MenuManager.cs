using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] string GAME_SCENE = "Game";
    Coroutine coroutine = null;

    // Start is called before the first frame update
    void Start()
    {
        if (coroutine == null) 
        {
            //coroutine = StartCoroutine(LoadYourAsyncScene(GAME_SCENE));
        }
    }

    public void StartGame()
    {
        Debug.Log("Lancement jeux");
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(GAME_SCENE);

        // attendre la fin du chargement en asynchrone
        /*while (!asyncLoad.isDone)
        {
            yield return null;
        }*/
    }

    IEnumerable LoadYourAsyncScene(string _name)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_name);

        // attendre la fin du chargement en asynchrone
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void QuitGame()
    {
        Debug.Log("Fermeture game");
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // Update is called once per frame
    void Update()
    {
    }
}
