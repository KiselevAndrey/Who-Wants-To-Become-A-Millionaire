using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScene : MonoBehaviour
{
    public void Exit()
    {
        Application.Quit();
    }

    public void LoadScene(Object scene)
    {
        SceneManager.LoadScene(scene.name);
    }
}
