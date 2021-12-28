using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rBody;
    public Animator animate;

    [SerializeField]
    float moveSpeed = 500;


    float horizontal = 0;
    float vertical = 0;

    private Vector3 facingDirection;

    [SerializeField]
    private float gravityFactor = 1;

    //Step sound cooldown
    [SerializeField]
    private float stepSoundInterval = 0;
    private float stepSoundCooldown = 0;

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, -9.81f * gravityFactor, 0);
    }

    void Update()
    {

        //testing camera shake
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CamController.instance.Shake();
        }


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


        //Step sounds randomizer
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

    private void FixedUpdate()
    {
        //move player
        Vector3 temp = new Vector3(horizontal, 0, vertical).normalized * moveSpeed * Time.fixedDeltaTime;
        rBody.velocity = transform.rotation * new Vector3(temp.x, rBody.velocity.y, temp.z);
        //print(rBody.velocity);

    }

    private void OnCollisionEnter(Collision collision)
    {
    }


    public Vector3 GetFacingDirection()
    {
        return facingDirection;
    }


}
