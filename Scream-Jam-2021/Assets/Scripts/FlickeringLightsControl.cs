using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLightsControl : MonoBehaviour
{
    [SerializeField] private Material lightsOn; //Material for when light is on
    [SerializeField] private Material lightsOff; //Material for when light is off
    Renderer matRenderer; //This gameobject's mesh renderer
    private bool shouldFlicker = true;
    private float delay; //Delay between on and off state
    [SerializeField] private float minimumDelay;
    [SerializeField] private float maximumDelay; 

    

    void Start()
    {
        matRenderer = GetComponent<Renderer>();
        matRenderer.enabled = true;
        matRenderer.sharedMaterial = lightsOn;

        StartCoroutine(FlickerLights());
    }



    private IEnumerator FlickerLights()
    {
        while (shouldFlicker)
        {
            LightControl(false, minimumDelay, maximumDelay);
            yield return new WaitForSeconds(delay);

            LightControl(true, minimumDelay, maximumDelay);
            yield return new WaitForSeconds(delay);
        }
    }


    //Turns on or off light gameobject and sets delay for next state
    private void LightControl(bool turnOn, float minDelay, float maxDelay)
    {
        gameObject.GetComponent<Light>().enabled = turnOn;

        if(turnOn)
        {
            matRenderer.sharedMaterial = lightsOn;
        }
        else
        {
            matRenderer.sharedMaterial = lightsOff;
        }

        delay = Random.Range(minDelay, maxDelay);
    } 
}
