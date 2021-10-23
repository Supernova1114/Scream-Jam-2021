using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    private bool isColliding = false;

    Collider currentCollision = null;

    //does not take into account if colliding with multiples switches at once so space them
    //apart as needed
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Switch"))
        {
            isColliding = true;
            currentCollision = other;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Switch"))
        {
            isColliding = false;
            currentCollision = null;
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isColliding && currentCollision != null)
            {
                currentCollision.gameObject.SendMessage("Flip");
            }
        }
    }

}
