using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField]
    UnityEngine.UI.Image canvasImage;
    [SerializeField]
    private Transform teleportDestination;
    [SerializeField]
    private Collider newCameraBounds;

    [SerializeField][Range(1, 100)]
    private int fadeSmooth;
    [SerializeField]
    private float fadeInterval;
    [SerializeField]
    private float waitBetweenFades;

    private static bool isTeleporting = false;

    [SerializeField]
    private bool isLocked = false;
    [SerializeField]
    private int lockAmount = 0;


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") && isTeleporting == false)
        {
            if (isLocked == false)
            {
                isTeleporting = true;

                StartCoroutine("BeginTeleport", collision);
            }
            
        }
    }

    IEnumerator BeginTeleport(Collider collision)
    {
        //play door open sound
        AudioManager.instance.Play("Door-Open");

        //freeze movement
        collision.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        //fade screen to black
        for (int i = 0; i <= 100; i += fadeSmooth)
        {
            yield return new WaitForSeconds(fadeInterval);
            canvasImage.color = new Color(0, 0, 0, i / 100.0f);
            //print(i);

        }


        //teleport player to specified location and update camera bounds
        collision.transform.position = teleportDestination.position;
        CamController.instance.SetBounds(newCameraBounds);
        CamController.instance.SetPosition(teleportDestination.position);

        //let player fall onto floor
        collision.GetComponent<Rigidbody>().constraints = 
            RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

        yield return new WaitForSeconds(waitBetweenFades);

        AudioManager.instance.Play("Door-Close");

        //fade in screen
        for (int i = 100; i >= 0; i -= fadeSmooth)
        {
            yield return new WaitForSeconds(fadeInterval);
            canvasImage.color = new Color(0, 0, 0, i / 100.0f);
            //print(i);
        }

        //unfreeze movement
        collision.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

        isTeleporting = false;
    }

    public void DecreaseLock()
    {
        lockAmount--;
        if (lockAmount <= 0)
        {
            isLocked = false;
        }
    }


}
