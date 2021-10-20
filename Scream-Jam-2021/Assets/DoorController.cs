using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField]
    UnityEngine.UI.Image canvasImage;
    [SerializeField]
    private Transform teleportDestination;

    private static bool isTeleporting = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") && isTeleporting == false)
        {
            isTeleporting = true;

            StartCoroutine("BeginTeleport", collision);
        }
    }

    IEnumerator BeginTeleport(Collider collision)
    {
        //FIXME loops do not go to 1 and 0
        //freeze movement
        collision.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        //fade screen to black
        for (float i=0; i <= 1; i += 0.05f)
        {
            yield return new WaitForSeconds(0.05f);
            canvasImage.color = new Color(0, 0, 0, i);
        }

        //teleport player to specified location
        collision.transform.position = teleportDestination.position;

        //let player fall onto floor
        collision.GetComponent<Rigidbody>().constraints = 
            RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;


        //fade in screen
        for (float i = 1.0f; i >= 0; i -= 0.05f)
        {
            yield return new WaitForSeconds(0.05f);
            canvasImage.color = new Color(0, 0, 0, i);
            print(i);
        }

        //unfreeze movement
        collision.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

        isTeleporting = false;
    }


}
