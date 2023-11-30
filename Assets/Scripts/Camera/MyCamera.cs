using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float smoothing;
    [SerializeField] private Vector3 offset;

    private void LateUpdate()
    {
        // Determine the target position based on the player's position and the offset
        Vector3 targetPosition = player.position + offset;

        // Smoothly interpolate between the camera's current position and the target position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothing);

        // Update the camera's position
        transform.position = smoothedPosition;
    }
}
