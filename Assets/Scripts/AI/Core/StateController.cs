using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    [Header("State")]
    [SerializeField] private AIState currentState;
    [SerializeField] private AIState remainState;

    [Header("Shooting")]
    [SerializeField] private Transform firePoint;

    // Reference of the Path Follow
    public FollowPath Path { get; set; }

    // Player Reference
    public PlayerMotor Target { get; set; }

    public ObjectPooler Pooler { get; set; }

    public Transform FirePoint => firePoint;

    private Vector3 radiusStartPosition;
    private float detectionRadius;
    private bool playerDetected;

    private void Start()
    {
        Path = GetComponent<FollowPath>();
        Pooler = GetComponent<ObjectPooler>();
    }

    private void Update()
    {
        currentState.RunState(this);
    }

    // Update our State to a new one
    public void TransitionToState(AIState newState)
    {
        if (newState != remainState)
        {
            currentState = newState;
        }
    }

    // Create a test line to visualize the ray that we are casting
    public void DebugRay(float rayLength, Vector3 startPosition, Vector3 direction, bool playerDetected)
    {
        Debug.DrawLine(startPosition, startPosition + direction * rayLength, playerDetected ? Color.green : Color.red);
    }

    // Get the detection circle data we want to create
    public void SetRediusDetectionValues(float radius, Vector3 startPosition, bool playerDetection)
    {
        detectionRadius = radius;
        radiusStartPosition = startPosition;
        playerDetected = playerDetection;
    }

    private void OnDrawGizmos()
    {
        if (detectionRadius > 0)
        {
            Gizmos.color = playerDetected ? Color.green : Color.red;
            Gizmos.DrawWireSphere(radiusStartPosition, detectionRadius);
        }
    }
}
