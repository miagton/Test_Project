using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
 [SerializeField]  GameObject pauseMenu =null;
    // Start is called before the first frame update
    void Awake()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
    }

    // Update is called once per frame
    public void StartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void PauseMenu()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }
    public void ContinueGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Reset()
    {
        GameObject ball = FindObjectOfType<BallController>().gameObject;
        Destroy(ball);
    }
}
