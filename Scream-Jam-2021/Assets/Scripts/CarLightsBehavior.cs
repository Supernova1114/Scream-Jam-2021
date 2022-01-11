using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLightsBehavior : MonoBehaviour
{
    public Light[] carLights;
    public Renderer[] carLightsMat;
    public Material lightOn;
    public Material lightOff;
    public AudioSource leverSwitchAudio;

    //-------TEMPORARY, JUST TESTING DO NOT REMOVE-------//
    public AudioSource ghostlyGiggles;
    public GameObject lightFlickers;
    //---------------------------------------------------//

    void Start()
    {
        carLights = GetComponentsInChildren<Light>();
        carLightsMat = GetComponentsInChildren<Renderer>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            LightsManagement(false);
            leverSwitchAudio.Play();

            //-------TEMPORARY, JUST TESTING DO NOT REMOVE-------//
            ghostlyGiggles.PlayDelayed(2);
            lightFlickers.GetComponent<FlickeringLightsControl>().enabled = true;
            //---------------------------------------------------//
        }
    }

    void LightsManagement(bool flag)
    {
        Material temp = flag?
                        (temp = lightOn) : 
                        (temp = lightOff);

        foreach(Renderer matBox in carLightsMat)
        {
            matBox.material = temp;
        }

        foreach(Light lightBox in carLights)
        {
            lightBox.enabled = flag;
        }
    }

}
