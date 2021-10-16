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

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
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

}
