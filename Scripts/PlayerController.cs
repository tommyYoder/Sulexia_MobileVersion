using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float tilt;
    public Boundary boundary;

    public GameObject Shield;
    public GameObject Player;
    public GameObject Player1;
    public GameObject Player2;

    public GameObject shot;
    public GameObject shot1;
    public GameObject shot2;
  

    public Transform shotSpawn;
    public Transform shotSpawn1;
    public Transform shotSpawn2;

    public float fireRate;

    public AudioSource PowerUpSound;
    public AudioSource ShieldSound;
    public AudioSource ShieldHitSound;

    public SimpleTouchPad touchPad;
    public SimpleTouchAreaButton areaButton;
    
    private float nextFire;
    private Quaternion calibrationQuaternion;

   

    void Start()
    {
        CalibrateAccelerometer();
        Player.GetComponent<MeshCollider>().enabled = true;              // Looks for mesh collider and sets this to true.
        Shield.SetActive(false);                                        // Makes shield variable false.
        Player1.SetActive(false);                                      // Makes player 1's variable false.
        Player2.SetActive(false);                                     // Makes player 2's variable false.
        shot1.SetActive(false);                                      // Makes shot 1's variable false. 
        shot2.SetActive(false);                                     // Makes shot 2's variable false.


    }

    // Allows player to fire when finger is in area button.
     void Update()                  
    {
        if (areaButton.CanFire() && Time.time > nextFire)      
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            Instantiate(shot1, shotSpawn1.position, shotSpawn.rotation);
            Instantiate(shot2, shotSpawn2.position, shotSpawn.rotation);
            GetComponent<AudioSource>().Play();
            
        }
    }

    //Used to calibrate the Iput.acceleration input
    void CalibrateAccelerometer()
    {
        Vector3 accelerationSnapshot = Input.acceleration;
        Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1.0f), accelerationSnapshot);
        calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
    }

    //Get the 'calibrated' value from the Input
    Vector3 FixAcceleration(Vector3 acceleration)
    {
        Vector3 fixedacceleration = calibrationQuaternion * acceleration;
        return fixedacceleration;
    }

    // Updates movement by the touch pad and rigidbody component. 
    void FixedUpdate()                                          
    {
        //      float moveHorizontal = Input.GetAxis ("Horizontal");
        //      float moveVertical = Input.GetAxis ("Vertical");

        //      Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

        //      Vector3 accelerationRaw = Input.acceleration;
        //      Vector3 acceleration = FixAcceleration (accelerationRaw);
        //      Vector3 movement = new Vector3 (acceleration.x, 0.0f, acceleration.y);

        Vector2 direction = touchPad.GetDirection();
        Vector3 movement = new Vector3(direction.x, 0.0f, direction.y);
        GetComponent<Rigidbody>().velocity = movement * speed;

        GetComponent<Rigidbody>().position = new Vector3
        (
            Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
        );

        GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f,GetComponent<Rigidbody>().velocity.x * -tilt);
    }
    IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PowerUp")                         // If you collide with the PowerUp tag, the game looks to see if the collider is true, turns off the player's mesh collider, turns on the shield game object before destroying the PowerUp tag game object.
        {                                                             // Sound will play for 8 seconds before the sound will stop, shiled turns to false, and player's mesh is true. Great for making a player invincible in your games.
         if (GetComponent<Collider>() != null)
            Player.GetComponent<MeshCollider>().enabled = false;
            Shield.SetActive(true);
            Destroy(other.gameObject);
            ShieldSound.Play();
            ShieldSound.SetScheduledEndTime(AudioSettings.dspTime + (8f - 0f));
            yield return new WaitForSeconds(8f);
            Player.GetComponent<MeshCollider>().enabled = true;
            Shield.SetActive(false);
        }

        if (other.gameObject.tag == "FireShot")                      // If you collide with the FireShot tag, then your fire rate turns to .1, sound will signify you collected that item before destroying the tagged game object. After 5 seconds your fire shot retuns to normal.
        {
            if (GetComponent<Collider>() != null)
            fireRate = .1f;
            PowerUpSound.Play();
            Destroy(other.gameObject);
            yield return new WaitForSeconds(5f);
            fireRate = .25f;

        }
        if (other.gameObject.tag == "Ship")                       // If you collide with the Ship tag, two additional ships will appear on your side by making them true. Tagged game object will be destroyed along with hearing sound to signify you collided with that game object. 
        {                                                        // After 5 seconds they will turn to false.
         
            Player1.SetActive(true);
            Player2.SetActive(true);
            shot1.SetActive(true);
            shot2.SetActive(true);
            PowerUpSound.Play();
            Destroy(other.gameObject);
            yield return new WaitForSeconds(5f);
            shot1.SetActive(false);
            shot2.SetActive(false);
            Player1.SetActive(false);
            Player2.SetActive(false);
        }
    } 

}
