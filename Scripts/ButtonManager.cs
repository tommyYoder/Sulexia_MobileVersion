using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public AudioSource ClickSound;
    public AudioSource MainSound;

    public void NewGameBtn(string newGameLevel)               // If player hit new game button, then the game will load level designed in the inspector, click sound will play, and main audio will stop.
    {
        SceneManager.LoadScene(newGameLevel);
        ClickSound.Play();
        MainSound.Stop();
    }
    public void ExitGameBtn()                                // If player hit quit game button game, then the game application will close, click sound will play, and main audio will stop.
    {
        ClickSound.Play();
        MainSound.Stop();
        Application.Quit();
      
    }
}
