using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {

    void OnTriggerExit(Collider other)                   // If game objects exits the boundary's collider trigger component they will be destroyed. 
    {
        Destroy(other.gameObject);  

    }
}
