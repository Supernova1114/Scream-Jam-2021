using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CamController : MonoBehaviour
{

    public static CamController instance;

    [SerializeField]
    private GameObject virtualCameraObj;
    [SerializeField]
    private Collider defaultCameraBounds;
    [SerializeField]
    private Cinemachine.CinemachineImpulseSource impulseSource;

    private Cinemachine.CinemachineConfiner cinemachineConfiner;
    private Cinemachine.CinemachineVirtualCamera cinemachineVirtualCamera;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        cinemachineConfiner = virtualCameraObj.GetComponent<Cinemachine.CinemachineConfiner>();
        cinemachineConfiner.m_BoundingVolume = defaultCameraBounds;

        cinemachineVirtualCamera = virtualCameraObj.GetComponent<Cinemachine.CinemachineVirtualCamera>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBounds(Collider CameraBounds)
    {
        cinemachineConfiner.m_BoundingVolume = CameraBounds;
    }

    public void SetPosition(Vector3 position)
    {
        cinemachineVirtualCamera.ForceCameraPosition(position, cinemachineVirtualCamera.transform.rotation);
    }
    public void Shake()
    {
        impulseSource.GenerateImpulse();
    }
}


