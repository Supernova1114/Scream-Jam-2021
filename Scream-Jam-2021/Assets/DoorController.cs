using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField]
    UnityEngine.UI.Image canvasImage;
    [SerializeField]
    private Transform teleportDestination;

    [SerializeField][Range(1, 100)]
    private int fadeSmooth;
    [SerializeField]
    private float fadeInterval;
    [SerializeField]
    private float waitBetweenFades;

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
        //freeze movement
        collision.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        //fade screen to black
        for (int i = 0; i <= 100; i += fadeSmooth)
        {
            yield return new WaitForSeconds(fadeInterval);
            canvasImage.color = new Color(0, 0, 0, i / 100.0f);
            //print(i);

        }

        //teleport player to specified location
        collision.transform.position = teleportDestination.position;

        //let player fall onto floor
        collision.GetComponent<Rigidbody>().constraints = 
            RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

        yield return new WaitForSeconds(waitBetweenFades);

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


}
