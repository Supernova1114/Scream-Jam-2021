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

    [SerializeField]
    private float gravityFactor = 1;

    //Step sound cooldown
    [SerializeField]
    private float stepSoundInterval = 0;
    private float stepSoundCooldown = 0;

    private bool smoothRotateFlag = true;
    [SerializeField]
    private float smoothRotateInterval = 0.05f;

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, -9.81f * gravityFactor, 0);
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (smoothRotateFlag)
            {
                smoothRotateFlag = false;
                StartCoroutine("SmoothRotate", 90);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (smoothRotateFlag)
                {
                    smoothRotateFlag = false;
                    StartCoroutine("SmoothRotate", -90);
                }
            }
        }

        //testing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CamController.instance.Shake();
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        animate.SetInteger("Vertical", (int)vertical);
        animate.SetInteger("Horizontal", (int)horizontal);

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

        rBody.velocity = transform.rotation * new Vector3(horizontal * moveSpeed * Time.deltaTime, rBody.velocity.y, vertical * moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if on "Environment" layer
        if (collision.gameObject.layer == 3)
        {
            //play bumping into wall sound
            AudioManager.instance.Play("Bump");
        }
    }

    IEnumerator SmoothRotate(int addRotation)
    {
        
        //FIXME fuckl aisjldlasjldkasjldkjlasjldjalsjdlkasmldmalckjiefnkdnlckn  help
        
        
        for (int i = 0; i < Mathf.Abs(addRotation) / 2.0f; i++)
        {
            Vector3 rotation = transform.rotation.eulerAngles;

            transform.rotation = Quaternion.Euler(rotation.x, rotation.y + (2f * Mathf.Clamp(addRotation, -1, 1)), rotation.z);
            yield return new WaitForSeconds(smoothRotateInterval);
        }

        smoothRotateFlag = true;
    }


}
