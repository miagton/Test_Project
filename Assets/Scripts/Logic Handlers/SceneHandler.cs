using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    //pause menu GameObject to control 
    [SerializeField]  GameObject pauseMenu =null;
  
    
    void Awake()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);//disabling Pause menu
        }
    }

   
    public void StartGame()//Used in Main menu
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void QuitGame()//quits application
    {
        Application.Quit();
    }
    public void PauseMenu()
    {
        // disabling Ball controller incase of input working while in menu
        GameObject ball = FindObjectOfType<BallController>().gameObject;
        ball.GetComponent<BallController>().enabled = false;
        //entering PauseMenu
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }
    public void ContinueGame()
    {
       //returning to game
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
       //Restoring control over the ball
        GameObject ball = FindObjectOfType<BallController>().gameObject;
        ball.GetComponent<BallController>().enabled = true;
    }
    public void LoadMainMenu()// Loading MainMenu
    {
        SceneManager.LoadScene(0);
    }
    
    //TODO fix rare moments of ball drowing in endless bounce
    public void Reset()//Functionality made for Debug and helping player if endless bounce cycle occure
    {
        
        GameObject ball = FindObjectOfType<BallController>().gameObject;
        Destroy(ball);
       
        GameObject handler = FindObjectOfType<GameHandler>().gameObject;
        handler.GetComponent<GameHandler>().RespawnTargets();
    }
}
