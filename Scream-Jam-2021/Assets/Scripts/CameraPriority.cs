using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPriority : MonoBehaviour
{
    
    //Player---------------------------------------------
    private Rigidbody playerRB;
    private GameObject playerSprite;

    //Camera---------------------------------------------
    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera cmVCam; //Cinemachine vcam for this transition object
    private Cinemachine.CinemachineBrain cmBrain;

    private GetVcamList vcamList;

    //Offset Helper--------------------------------------

    //Distance player moves towards or away from center of camera priority trigger
    //  to make it less likely to accidentally exit trigger
    [SerializeField]
    private float moveTowardsCenter;

    //Logic----------------------------------------------
    [SerializeField]
    private bool exitToPreviousCam = false; //On trigger exit, transition to previous vcam
    [SerializeField]
    private bool specifyPlayerRotation = false;

    //Player Rotation------------------------------------
    [SerializeField]
    private float newPlayerRotation; //New rotation for player sprite
    private float previousPlayerRotation; //Original rotation for player sprite

    //---------------------------------------------------
    
    void Start()
    {
        vcamList = GameObject.Find("VCam List").GetComponent<GetVcamList>();

        cmBrain = FindObjectOfType<Cinemachine.CinemachineBrain>();
        playerRB = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
        playerSprite = GameObject.Find("PlayerSprite");  
    }

    
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            cmVCam.Priority = 2;

            vcamList.ResetPriority(cmVCam);

            TransitionCamera(true);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        
        if (other.CompareTag("Player"))
        {
            if (exitToPreviousCam)
            {
                cmVCam.Priority = 0;
                TransitionCamera(false);
            }
        }
         
           
    }



    //Freezes player until the active camera transition finishes
    IEnumerator WaitForCamera()
    {
        playerRB.constraints = RigidbodyConstraints.FreezeAll;

        yield return null;
    
        // float blendDuration = cmBrain.ActiveBlend.Duration;
        
        // yield return new WaitForSeconds(blendDuration);

        while (cmBrain.IsBlending)
        {
            yield return null;
        }
        
        playerRB.constraints = RigidbodyConstraints.FreezeRotation;
    }


    //priority - set the new priority of this virtual camera
    //controlsOffset - set the new vector for wasd;
    //isEntering - if player is entering the trigger or not
    private void TransitionCamera(bool isEntering)
    {
        StartCoroutine("WaitForCamera");

        Vector3 towardPlayer = (playerRB.position - transform.position);
        Vector3 xzPlane = new Vector3(towardPlayer.x, 0, towardPlayer.z);

        Quaternion temp = playerSprite.transform.rotation;

        if (isEntering)
        {
            //Makes it harder for player to accidentally exit trigger after camera transition
            playerRB.transform.position += -xzPlane.normalized * moveTowardsCenter;

            previousPlayerRotation = temp.eulerAngles.y;

            if (!specifyPlayerRotation)
            {
                Vector3 back = -cmVCam.transform.forward;
                Vector3 xzPlane2 = new Vector3(back.x, 0, back.z);

                newPlayerRotation = (Mathf.Rad2Deg * Mathf.Atan2(xzPlane2.x, xzPlane2.z)) + 180;
            }

            playerSprite.transform.rotation = Quaternion.Euler(temp.x, newPlayerRotation, temp.z);

            PlayerMovement.SetControlsOffset(newPlayerRotation);
        }
        else
        {
            //Makes it harder for player to accidentally enter trigger after camera transition
            playerRB.transform.position += xzPlane.normalized * moveTowardsCenter;

            playerSprite.transform.rotation = Quaternion.Euler(temp.x, previousPlayerRotation, temp.z);
            
            PlayerMovement.SetControlsOffset(previousPlayerRotation);
        }

    }

}
