using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SimpleTouchAreaButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    private bool touched;
    private int pointerID;
    private bool canFire;


    // Notifies that no touches have been made.
    void Awake()
    {
        touched = false;
    }


    // Is notified that touches have been made and player can fire.
    public void OnPointerDown(PointerEventData data)
    {
        if (!touched)
        {
            touched = true;
            pointerID = data.pointerId;
            canFire = true;
        }
    }

    // If no fingers on the screen, then ship stops firing and touches are set to 0.
    public void OnPointerUp(PointerEventData data)
    {
        if (data.pointerId == pointerID)
        {
            canFire = false;
            touched = false;
        }
    }

    // Allows player to shoot if canFire is true.
    public bool CanFire()
    {
        return canFire;
    }

}