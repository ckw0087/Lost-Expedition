using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private float parallaxEffectIntensity;
    [SerializeField] private GameObject cam;

    private float length, startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float relativeDistance = cam.transform.position.x * (1 - parallaxEffectIntensity);
        float distance = cam.transform.position.x * parallaxEffectIntensity;
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if (relativeDistance > startPos + length)
        {
            startPos += length;
        }
        else if (relativeDistance < startPos - length)
        {
            startPos -= length;
        }            
    }
}
