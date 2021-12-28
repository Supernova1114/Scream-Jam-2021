using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTowardsTarget : MonoBehaviour
{
    public GameObject target;

    private Vector3 targetVector;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        targetVector = target.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(targetVector, transform.up);
    }
}
