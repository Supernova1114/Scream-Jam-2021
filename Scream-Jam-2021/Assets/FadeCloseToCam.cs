using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeCloseToCam : MonoBehaviour
{
    [SerializeField]
    private float fadeDistance;
    [SerializeField]
    private float offset;

    [SerializeField]
    private List<SpriteRenderer> sprites;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (SpriteRenderer sprite in sprites)
        {
            float alphaPercent = Mathf.Clamp( ((sprite.transform.position - transform.position).magnitude / fadeDistance) + offset, 0.0f, 1.0f);
            
            print(alphaPercent);

            sprite.color = new Color(255, 255, 255, alphaPercent);
        }
    }
}
