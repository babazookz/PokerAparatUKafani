using UnityEngine;
using UnityEngine.SceneManagement;

public static class CustomSceneManager
{
    public static void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void RestartCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static AsyncOperation LoadSceneAsync(string sceneName)
    {
        return SceneManager.LoadSceneAsync(sceneName);
    }

    public static Scene CurrentScene()
    {
        return SceneManager.GetActiveScene();
    }

    public static string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
}
