using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {



    public GameObject Button;

    public GameObject pauseMenuCanvas;

    // Looks for button and paused canvas.
    public void Start()
    {
        UnOnPaused();
        Button.SetActive(true);
    }

    public void OnPaused()
    {
                                   // If isPaused is true, time scale will go to zero, pause button turns to false, and pause canvas will be true.
        
            pauseMenuCanvas.SetActive(true);
            Button.SetActive(false);
            Time.timeScale = 0;
        }
    
       public void UnOnPaused()
    {
        pauseMenuCanvas.SetActive(false);   // If isPaused is false, time scale will go to one, pause button turns to true, and pause canvas will be false.
        Time.timeScale = 1;
        Button.SetActive(true);
    }
 
    
   
    public void Resume()                      // Player can click on resume button to unpause the game.
    {
        pauseMenuCanvas.SetActive(false);
        Button.SetActive(true);
    }
        public void Quit()                   // Player can clck on quit button to shut down the game application. 
    {
        Application.Quit();
    }
}


   
   

