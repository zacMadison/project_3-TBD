using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;
    private bool paused = false;

    private void Awake()
    {
        pauseScreen.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(pauseScreen.activeInHierarchy)
            {
                paused = false;
                // exit pause menu
                PauseGame(paused);
            }
            else
            {
                paused = true;
                // enter pause menu
                PauseGame(paused);
            }
        }
    }

    public bool isPaused()
    {
        return paused;
    }
    #region PauseMenu
    private void PauseGame(bool status)
    {
        pauseScreen.SetActive(status);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
    #endregion
}
