using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    public float speed;

    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;    // Looks for Rigidbody attached to game object to move them by the forward transform by the speed variable. 
    }
}