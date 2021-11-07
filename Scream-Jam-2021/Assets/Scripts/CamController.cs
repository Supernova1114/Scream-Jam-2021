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


    //Fade-to-black controller-----------------------------
    [SerializeField]
    UnityEngine.UI.Image canvasImage;

    [SerializeField]
    [Range(1, 100)]
    private int fadeSmooth;

    [SerializeField]
    private float fadeInterval;

    [SerializeField]
    private bool fadeInOnSceneChange = true;

    private bool isFading = false;
    //-----------------------------------------------------


    private void Start()
    {
        if (fadeInOnSceneChange == true)
        {
            canvasImage.color = new Color(0, 0, 0, 1);
            StartCoroutine("FadeToBlack", false);

        }

    }


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

    public IEnumerator FadeToBlack(bool b)
    {
        if (isFading == false)
        {
            isFading = true;

            if (b == true)
            {
                //fade screen to black
                for (int i = 0; i <= 100; i += fadeSmooth)
                {
                    yield return new WaitForSeconds(fadeInterval);
                    canvasImage.color = new Color(0, 0, 0, i / 100.0f);
                }
            }
            else
            {
                //fade in screen
                for (int i = 100; i >= 0; i -= fadeSmooth)
                {
                    yield return new WaitForSeconds(fadeInterval);
                    canvasImage.color = new Color(0, 0, 0, i / 100.0f);
                }
            }
        }
        else
        {
            Debug.LogWarning("Wait until fade is completed to call again!");
        }

        isFading = false;
    }


    public bool GetIsFading()
    {
        return isFading;
    }


}


