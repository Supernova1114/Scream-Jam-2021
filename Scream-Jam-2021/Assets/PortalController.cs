using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    
    public GameObject oppositeCamera;
    public GameObject player;

    public Transform parallaxPoint;

    //public float parallaxFactor;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 temp = -(player.transform.position - parallaxPoint.position).normalized;
        Vector3 temp2 = new Vector3(temp.x, 0, temp.z);
        oppositeCamera.transform.rotation = Quaternion.LookRotation(temp2, Vector3.up);
    }
}
