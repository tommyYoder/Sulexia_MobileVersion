using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour {

    public float lifetime;
    
    void Start()
    {
        Destroy(gameObject, lifetime);                // Will destroy game object after set variable if true for lifetime. 
    } 
}