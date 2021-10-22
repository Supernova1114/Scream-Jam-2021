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

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, -9.81f * gravityFactor, 0);
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        animate.SetInteger("Vertical", (int)vertical);
        animate.SetInteger("Horizontal", (int)horizontal);
    }

    private void FixedUpdate()
    {
        rBody.velocity = new Vector3(horizontal * moveSpeed * Time.deltaTime, rBody.velocity.y, vertical * moveSpeed * Time.deltaTime);
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

}
