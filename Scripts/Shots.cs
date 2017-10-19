using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shots : MonoBehaviour {

    private AudioSource audioSource;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();      // Looks for Audio Source attached to game object.
    }
    void OnTriggerEnter(Collider other)                // If game object collides with another game objects with the tag player, then audio source will play.
    {
        if(other.gameObject.tag == "Player")
        {
            GetComponent<AudioSource>().Play();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
