using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Scene properties")]
    public string gameSceneName;

    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
