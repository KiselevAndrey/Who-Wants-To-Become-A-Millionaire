using UnityEngine;
using UnityEngine.SceneManagement;

public static class ManagerSceneStatic
{    public static void Exit()
    {
        Application.Quit();
    }

    public static void LoadScene(Object scene)
    {
        SceneManager.LoadScene(scene.name);
    }
}
