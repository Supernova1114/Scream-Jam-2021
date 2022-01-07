using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLightsControl : MonoBehaviour
{
    [SerializeField] private Material lightsOn;
    [SerializeField] private Material lightsOff;
    Renderer matRenderer;
    private bool flickering = false;
    private float delay;
    [SerializeField] private float rangeMin;
    [SerializeField] private float rangeMax;

    

    void Start()
    {
        matRenderer = GetComponent<Renderer>();
        matRenderer.enabled = true;
        matRenderer.sharedMaterial = lightsOn;
    }



    void Update()
    {
        if(flickering == false)
        {
            StartCoroutine(FlickerLights());
        }
    }



    IEnumerator FlickerLights()
    {
        flickering = true;

        LightControl(false, rangeMin, rangeMax);
        yield return new WaitForSeconds(delay);

        LightControl(true, rangeMin, rangeMax);
        yield return new WaitForSeconds(delay);

        flickering = false;
    }



    private void LightControl(bool flag, float min, float max)
    {
        this.gameObject.GetComponent<Light>().enabled = flag;

        if(flag)
        {
            matRenderer.sharedMaterial = lightsOn;
        }
        else
        {
            matRenderer.sharedMaterial = lightsOff;
        }

        delay = Random.Range(min, max);
    } 
}
