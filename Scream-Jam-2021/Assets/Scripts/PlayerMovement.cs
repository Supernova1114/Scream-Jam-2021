using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rBody;
    public Animator animate;

    [SerializeField] float moveSpeed = 500;
    [SerializeField] float sprintspeed = 2f;
    [Range(1, 5)] [SerializeField] private int sprintCost = 5;
    private float sprint = 1f;

    float horizontal = 0;
    float vertical = 0;

    Vector3 controlVector;

    //Offset rotation for movement controls around y axis
    private static Quaternion controlsOffset;

    private Vector3 facingDirection;

    [SerializeField] private float gravityFactor = 1;

    //Step sound cooldown
    [SerializeField] private float stepSoundInterval = 0;
    private float stepSoundCooldown = 0;

    //Camera
    Camera mainCamera;




    // LINE BREAK--------------------------------------------------------------------




    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        rBody = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, -9.81f * gravityFactor, 0);
        controlsOffset = transform.rotation;
        
    }

    void Update()
    {
        //Testing camera shake
        CamShake();

        // Move Bob
        Move();

        // Step sounds randomizer
        StepSound();
    }



    // LINE BREAK--------------------------------------------------------------------



    private void Move()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        //Change facing direction variable
        Vector3 temp = new Vector3(horizontal, 0, vertical);        
        if (temp.magnitude > 0)
        {
            facingDirection = temp;
        }

        animate.SetInteger("Vertical", (int)vertical);
        animate.SetInteger("Horizontal", (int)horizontal);

        if (Input.GetKey(KeyCode.LeftShift)){
            if (StaminaBar.instance.CheckStamina() == 0)
            {
                sprint = 1;
            }
            else 
            {
                sprint = sprintspeed;
                StaminaBar.instance.Sprinting(sprintCost);
            }    
        }

        if (Input.GetKeyUp(KeyCode.LeftShift)){
            //reset
            sprint = 1;
        }
    }

    private void StepSound()
    {
        if (new Vector2(rBody.velocity.x, rBody.velocity.z).magnitude > 0.1f)
        {
            if (stepSoundCooldown < Time.time)
            {

                int randomNum = Random.Range(1, 7);

                switch (randomNum)
                {
                    case 1:
                        AudioManager.instance.Play("Step-1");
                        break;
                    case 2:
                        AudioManager.instance.Play("Step-2");
                        break;
                    case 3:
                        AudioManager.instance.Play("Step-3");
                        break;
                    case 4:
                        AudioManager.instance.Play("Step-4");
                        break;
                    case 5:
                        AudioManager.instance.Play("Step-5");
                        break;
                    case 6:
                        AudioManager.instance.Play("Step-6");
                        break;
                }
                stepSoundCooldown = Time.time + stepSoundInterval;
            }
        }

        if (stepSoundCooldown > Time.time)
        {
            stepSoundCooldown -= Time.deltaTime;
        }
    }

    private void CamShake()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CamController.instance.Shake();
        }
    }

    private void FixedUpdate()
    {
        //move player
        Vector3 temp = new Vector3(horizontal, 0, vertical).normalized * (moveSpeed * sprint * Time.fixedDeltaTime);
        
        rBody.velocity = controlsOffset * new Vector3(temp.x, rBody.velocity.y, temp.z);


        //print(rBody.velocity);

    }

    private void OnCollisionEnter(Collision collision)
    {
    }


    public Vector3 GetFacingDirection()
    {
        return facingDirection;
    }

    public static void SetControlsOffset(float yAngle)
    {
        controlsOffset = Quaternion.Euler(0, yAngle, 0);
    }

}
