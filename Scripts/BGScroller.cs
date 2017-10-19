using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour {

    public float scrollSpeed;
    public float tileSizeZ;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;                                     // Looks for backgrounds starting position by its tranform position.
    }

    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);  // Wil scroll the background in the Z access by the scroll speed, tileSize, and vector 3 forward position. Great for games to make the player feel like they are moving thru your game level.
        transform.position = startPosition + Vector3.forward * newPosition;
    }
}
