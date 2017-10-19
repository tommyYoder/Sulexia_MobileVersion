using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpScript : MonoBehaviour
{

    public GameObject[] powerUps;
    public Vector3 powerUpValues;
    public int powerUpCount;
    public float powerUpSpawnWait;
    public float powerUpStartWait;
    public float powerUpWaveWait;
    public bool gameOver;


  
    public MeshCollider invincible;

    void Start()
    {
       
        StartCoroutine(PowerUpWaves());                             // Will begin PowerUpWaves when game is started. 
        invincible = GetComponent<MeshCollider>();                 // Looks for mesh collider attached to player game object. 
    }

    void Update()
    {

    }



    IEnumerator PowerUpWaves() 
    {
        yield return new WaitForSeconds(powerUpStartWait);
        while (true)                                          // If this is true, then each powerup in your list array will instantiate forward with a random position in the game space. They will be clamped in the x, y, and Z directions.
        {                                                    // After spawn wait is greater then start wait and wave wait, then a new powerup will be instantiated in its transfrom positon and rotation. 
            for (int i = 0; i < powerUpCount; i++)
            {
                GameObject powerUp = powerUps[Random.Range(0, powerUps.Length)];
                Vector3 powerUpPosition = new Vector3(Random.Range(-powerUpValues.x, powerUpValues.x), powerUpValues.y, powerUpValues.z);
                Quaternion powerUpRotation = Quaternion.identity;
                Instantiate(powerUp, powerUpPosition, powerUpRotation);
                yield return new WaitForSeconds(powerUpSpawnWait);
            }
            yield return new WaitForSeconds(powerUpWaveWait);
           
        }
    }
       
}
