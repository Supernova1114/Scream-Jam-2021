using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBehavior : MonoBehaviour
{
    [SerializeField]
    DoorController doorController;

    private bool isFlipped = false;

    public void Flip()
    {
        if (isFlipped == false)
        {
            isFlipped = true;
            doorController.DecreaseLock();

        }
    }

}
