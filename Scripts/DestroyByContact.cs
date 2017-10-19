using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
  
    public GameObject playerExplosion;

    

    public int scoreValue;
    private GameController gameController;

    void Start()
    { 
    GameObject gameControllerObject = GameObject.FindWithTag("GameController");      // Looks for Game Controller script and if true the game will begind. If false, game will notify you that it can't find the script.
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log ("Cannot find 'GameController' script");
        }
    }



    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))      // If enemy collider with the boundary tag while they are being spawned in the game, then they will avoid each other until the enemy exits the boundary's triggered collider. 
        {
            return;
        }


        if (explosion != null)                                                    // Will instansite explosion if set to true.
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }
        if (other.tag == "Bolt")                                                // Bolt tag will destroy the enemy game objects.
        {
            Destroy(other.gameObject);
        }
        if(other.tag == "EnemyBolt")                                         // EnemyBot tag will destroy the player game object. 
        {         
         
            Destroy(gameObject);
        }

       

            if (other.tag == "Player")                                   // If player tag is hit, lives will decrease, will look to see if game over is true, instantiate the player's explosion, and will destroy the game object when lives = o.
        {
            gameController.SubLive();
            if (gameController.gameOver == true)
            {
                Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                gameController.GameOver();
                Destroy(other.gameObject);
            }
        }

        gameController.AddScore(scoreValue);                         // If enemy is destroyed, then the score value will increase before destryoing that game object. 
        
        Destroy(gameObject);
    }
}
