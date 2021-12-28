using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private PlayerMovement PlayerMovement;

    [SerializeField]
    private float smoothDamp = 0.05f;
    
    //Y rotation
    private Quaternion targetRotation;




    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement = GetComponentInParent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 temp = PlayerMovement.GetFacingDirection();

        //print(temp);

        targetRotation = Quaternion.LookRotation(temp, transform.up);
        //float targetRotation = (Mathf.Rad2Deg * Mathf.Atan2(temp.y, temp.x)) - 90;

        print(transform.rotation.eulerAngles.y + " | " + targetRotation);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothDamp);


        //Quaternion.Euler(transform.rotation.x, Mathf.Lerp(transform.rotation.eulerAngles.y, targetRotation, smoothDamp), transform.rotation.y );

        
    }
}
