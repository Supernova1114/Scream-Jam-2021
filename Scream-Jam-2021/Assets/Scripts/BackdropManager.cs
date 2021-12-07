using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackdropManager : MonoBehaviour
{
    //Empty containing a sprite
    //3 is a good size
    public GameObject[] backdrops;

    //resets a backdrop once it passes this point
    public Transform backdropReset;

    public float moveSpeed;
    public float spawnOffset;

    private int previousBackdrop;

    // Start is called before the first frame update
    void Start()
    {
        previousBackdrop = backdrops.Length - 1;
    }

    
    void Update()
    {
        for (int i = 0; i < backdrops.Length; i++)
        {
            if (backdrops[i].transform.localPosition.x <= backdropReset.localPosition.x)
            {
                backdrops[i].transform.localPosition = new Vector3(backdrops[previousBackdrop].transform.localPosition.x + spawnOffset, 0, 0);

                previousBackdrop = i;
            }
            backdrops[i].transform.localPosition += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        }
    }
}