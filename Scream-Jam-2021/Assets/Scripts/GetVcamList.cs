using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetVcamList : MonoBehaviour
{
    private Cinemachine.CinemachineVirtualCamera [] temp;
    public List <Cinemachine.CinemachineVirtualCamera> vcamList;
    

    void Start()
    {
        temp = FindObjectsOfType<Cinemachine.CinemachineVirtualCamera>();

        for (int i = 0; i < temp.Length; i++)
        {
            if (!temp[i].CompareTag("Main_CM_vcam"))
            {
                vcamList.Add(temp[i]);
            }
        }
    }

    public void ResetPriority(Cinemachine.CinemachineVirtualCamera except)
    {
        foreach (Cinemachine.CinemachineVirtualCamera vcam in vcamList)
        {
            if (vcam != except)
            {
                vcam.Priority = 0;
            }
        }
    }

}
