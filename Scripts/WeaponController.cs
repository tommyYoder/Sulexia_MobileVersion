using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {


    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public float delay;
  
    private AudioSource audioSource;
   

    void Start()
    {
        audioSource = GetComponent<AudioSource>();                 // Looks for audio source attached to game object.
       
        InvokeRepeating("Fire", delay, fireRate);                 // Will invoke a repeating fire sequence by the enemy when delay is greater then fire rate. 
    }

    void Fire()                                                   // When Fire is true, each shot will be instantiated by the shotspawn's position and rotation. Sound will play when fire is true. 
    {
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        GetComponent<AudioSource>().Play();
    }
   
}


