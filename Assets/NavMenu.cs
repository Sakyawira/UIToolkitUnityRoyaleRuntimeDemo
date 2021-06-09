using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public enum Scenes
{
    MENU = 0,
    WORKOUT = 1,
    CREATE = 2,
    EXPLORE = 3,
    PROFILE = 4,
} 

public class NavMenu : MonoBehaviour
{
    private float secondsToLoadNextScene = 0.5f;
    private static int lastScene;
    private int mainScene = 1;
    private int currentScene;

    public static Stack<int> sceneStack = new Stack<int>();


    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    public void OnEnable()
    {
        GetComponent<UIDocument>().rootVisualElement.Q<Button>("Menu").RegisterCallback<ClickEvent>(ev => LoadNextScene((int)Scenes.MENU));
    }

    private void Update()
    {
        BackButtonPressed();
    }

    public void LoadNextScene(int numberOfSceneToLoad)
    {
        StartCoroutine(LoadScene(numberOfSceneToLoad));
    }

    private IEnumerator LoadScene(int numberOfScene)
    {
        SetLastScene(currentScene);

        yield return new WaitForSeconds(secondsToLoadNextScene);
        LoadNewScene(numberOfScene);
    }

    public void BackButtonPressed()
    {
        if (Input.GetKey(KeyCode.Escape) && currentScene > mainScene)
        {
            if (lastScene == 0)
            {
                Debug.Log("Last scene was Splash Screen so load Main Scene instead.");
                SceneManager.LoadScene(mainScene);
            }
            else
            {
                LoadLastScene();
            }
        }
    }

    public void LoadNewScene(int sceneToLoad)
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        sceneStack.Push(currentScene);
        SceneManager.LoadScene(sceneToLoad);
    }

    public void LoadLastScene()
    {
        lastScene = sceneStack.Pop();
        SceneManager.LoadScene(lastScene);
    }

    public static void SetLastScene(int makeCurrentSceneTheLastScene)
    {
        lastScene = makeCurrentSceneTheLastScene;
    }

    public static int GetLastScene()
    {
        return lastScene;
    }

}
