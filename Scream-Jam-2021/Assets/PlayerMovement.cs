using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rBody;

    [SerializeField]
    float moveSpeed = 0;


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



    }

    private void FixedUpdate()
    {
        rBody.MovePosition(transform.position + new Vector3(horizontal * moveSpeed * Time.fixedDeltaTime, 0, vertical * moveSpeed * Time.fixedDeltaTime));
    }
}
