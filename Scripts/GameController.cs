using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;


    public Vector3 spawnValues;
    public int hazardCount;
   
    
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    //public Text restartText;
    public Text gameOverText;
    public Text liveText;
    public Text waveCounterText;

    public GameObject PlayAgainBTN;

    private float Level;
    public int playerHealth;

    public GameObject BG;
    public GameObject BG1;
    public GameObject BG2;
    public GameObject BG3;
    public GameObject BG4;

    public GameObject bossPrefab;
    public GameObject bossPrefab1;
    public GameObject bossPrefab2;
    public GameObject bossPrefab3;

    public AudioSource SpaceBGSound;
    public AudioSource SpaceBGSound1;
    public AudioSource SpaceBGSound2;
    public AudioSource SpaceBGSound3;
    public AudioSource SpaceBGSound4;

    public AudioSource HitSound;

    private int score;
    public bool gameOver;
    //private bool restart;
    public int WaveCounter = 1;
    public int HazardCountIncrease = 1;
    public float SpawnSpeedIncrease = 0.5f;

    public PowerUpScript powerUpScript;

    void Start()
    {
        playerHealth = 3;                                  // Player lives is set to 3

        BG.SetActive(true);                               // Background is set to true.
        BG1.SetActive(false);                            // Background is set to false.
        BG2.SetActive(false);                           // Background is set to false.
        BG3.SetActive(false);                          // Background is set to false.
        BG4.SetActive(false);                         // Background is set to false.
        PlayAgainBTN.SetActive(false);               // Button is set to false.
        bossPrefab.SetActive(false);                 // BossPrefab is set to false.
        bossPrefab1.SetActive(false);               // BossPrefab is set to false.
        bossPrefab2.SetActive(false);              // BossPrefab is set to false.
        bossPrefab3.SetActive(false);             // BossPrefab is set to false.

        gameOver = false;                        // Game over is set to false.
        //restart = false;                        // Restart is set to false.
        //restartText.text = "";                 // Reset text is set to null.
        gameOverText.text = "";               // Game Over text is set to null.
        liveText.text = "Live:3";            // Lives text set to 3.
        waveCounterText.text = "Wave:1";    // Wave counter text is set to 1.

         
       score = 0;                          // Score is set to 0.
        
        UpdateScore();                    // Will update score text when enemy is destroyed. 

        StartCoroutine(SpawnWaves());
    }
    public void SubLive()                // If player is hit sub ives will decrease by one and sound will play. This gets updated to the lives text.
    {
        playerHealth--;
        HitSound.Play();
        UpdateLives();
        if(playerHealth < 1)            // If your lives is less than one, then hit sound stops.
        {
            HitSound.Stop();
        }
        if (playerHealth <= 0)         // If your lives is less than or equal to 0, then game over is set to true.
        {
            gameOver = true;
        }
    }

    void UpdateLives()               // Updated lives to the UI text element. 
    {
        liveText.text = "Lives: " + playerHealth;
    }

    //void Update()
    //{
    //    if (restart)               // If restart is true, player can press R to re-load the game level.
    //    {
    //        if (Input.GetKeyDown(KeyCode.R))
    //        {
    //            Application.LoadLevel(Application.loadedLevel);
    //        }
    //    }
    //}


    IEnumerator SpawnWaves()                           
    {
        yield return new WaitForSeconds(startWait);
        while (true)                                          // If true, hazard count instantiates by a randon range. This random range is determined by the vector 3 positions. After each wave passes thru the GM waits before respawning more hazards.
        {                                                    // Wave counter is increased to allow more hazards to appear on the screen. The hazrd's speed is also increased to add difficulty as the player progresses in the game. Wave counter UI text is updated after each wave passes thru. 
            hazardCount += (WaveCounter * HazardCountIncrease);

            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait - (WaveCounter * SpawnSpeedIncrease));
                waveCounterText.text = "Wave:" + WaveCounter;

            }
            yield return new WaitForSeconds(waveWait);
                 WaveCounter++;


     // If game over is true, then restart game is set to true, play again button appears, powerUpScript's counter is set to 0, and spawn wave is false. 
                 if (gameOver)               
                {
                PlayAgainBTN.SetActive(true);
                //restartText.text = "Press 'R' for Restart";
                //restart = true;
                powerUpScript.powerUpCount = 0;
                    break;

                }
                if(WaveCounter == 9)       // If waveCounter equals 9, then the boss is instantiated by its transform position. 
            {
                bossPrefab.SetActive(true);
                Instantiate(bossPrefab, transform.position, Quaternion.identity);
                
            }
            if (WaveCounter == 10)       // If waveCounter equals 10, then BG turns false and new BG turns true. Sound will stop to allow new sound to play.
            {
                BG.SetActive(false);
                BG1.SetActive(true);
                SpaceBGSound.Stop();
                SpaceBGSound1.Play();
            }
            if(WaveCounter == 19)          // If waveCounter equals 19, then the boss is instantiated by its transform position. 
            {
                bossPrefab1.SetActive(true);
                Instantiate(bossPrefab1, transform.position, Quaternion.identity);
            }
            if (WaveCounter == 20)       // If waveCounter equals 20, then BG turns false and new BG turns true. Sound will stop to allow new sound to play.
            {
                BG1.SetActive(false);
                BG2.SetActive(true);
                SpaceBGSound1.Stop();
                SpaceBGSound2.Play();
            }
            if(WaveCounter == 29)        // If waveCounter equals 29, then the boss is instantiated by its transform position. 
            {
                bossPrefab2.SetActive(true);
                Instantiate(bossPrefab2, transform.position, Quaternion.identity);
            }
            if (WaveCounter == 30)       // If waveCounter equals 30, then BG turns false and new BG turns true. Sound will stop to allow new sound to play.
            {
                BG2.SetActive(false);
                BG3.SetActive(true);
                SpaceBGSound2.Stop();
                SpaceBGSound3.Play();
            }
            if(WaveCounter == 39)         // If waveCounter equals 39, then the boss is instantiated by its transform position. 
            {
                bossPrefab3.SetActive(true);
                Instantiate(bossPrefab3, transform.position, Quaternion.identity);
            }
            if (WaveCounter == 40)       // If waveCounter equals 40, then BG turns false and new BG turns true. Sound will stop to allow new sound to play.
            {
                BG3.SetActive(false);
                BG4.SetActive(true);
                SpaceBGSound3.Stop();
                SpaceBGSound4.Play();
            }
        }
    }

    public void AddScore(int newScoreValue)  // Updates score value when enemy is destroyed. 
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()                      // Updates the score UI text.
    {
        scoreText.text = "Score: " + score;
    }


public void GameOver()                     // Game Over text appears when true.
    {
        gameOverText.text = "Game Over!";
        gameOver = true;

    }

    // Reloads level when Button is pressed.
    public void RestartGame()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}

