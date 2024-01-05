using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private float parallaxEffectIntensity;
    [SerializeField] private GameObject cam;

    private float length, xPos, yPos;

    // Start is called before the first frame update
    void Start()
    {
        xPos = transform.position.x;
        yPos = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float relativeDistance = cam.transform.position.x * (1 - parallaxEffectIntensity);
        float xDistance = cam.transform.position.x * parallaxEffectIntensity;
        float yDistance = cam.transform.position.y;

        transform.position = new Vector3(xPos + xDistance, yPos + yDistance, transform.position.z);

        if (relativeDistance > xPos + length)
        {
            xPos += length;
        }
        else if (relativeDistance < xPos - length)
        {
            xPos -= length;
        }            
    }
}
