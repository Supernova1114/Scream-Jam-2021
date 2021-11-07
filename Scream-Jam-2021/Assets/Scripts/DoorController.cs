using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
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

    [SerializeField]
    private bool isSceneChange = false;
    [SerializeField]
    private string nextSceneName;


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
        CamController.instance.StartCoroutine("FadeToBlack", true);
        yield return new WaitUntil(() => CamController.instance.GetIsFading() == false);

        if (isSceneChange == false)
        {
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
            CamController.instance.StartCoroutine("FadeToBlack", false);
            yield return new WaitUntil(() => CamController.instance.GetIsFading() == false);

            //unfreeze movement
            collision.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

            isTeleporting = false;
        }
        else
        {
            SceneManager.LoadScene(sceneName: nextSceneName);
        }
        
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
