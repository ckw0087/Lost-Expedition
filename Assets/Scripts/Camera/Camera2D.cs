using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2D : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool horizontalFollow = true;
    [SerializeField] private bool verticalFollow = true;
    [SerializeField] private Vector3 minViewValue;
    [SerializeField] private Vector3 maxViewValue;
    [SerializeField] private float clampSmoothness = 3f;

    [Header("Horizontal")]
    [SerializeField] [Range(0, 1)] private float horizontalInfluence = 1f;
    [SerializeField] private float horizontalOffset = 0f;
    [SerializeField] private float horizontalSmoothness = 3f;   

    [Header("Vertical")]
    [SerializeField] [Range(0, 1)] private float verticalInfluence = 1f;
    [SerializeField] private float verticalOffset = 0f;
    [SerializeField] private float verticalSmoothness = 3f;

    // The target reference    
    public PlayerMotor Target { get; set; }

    // Position of the Target  
    public Vector3 TargetPosition { get; set; }

    // Reference of the Target Position known by the Camera    
    public Vector3 CameraTargetPosition { get; set; }

    private float targetHorizontalSmoothFollow;
    private float targetVerticalSmoothFollow;
    private Vector3 clampedCameraPosition;

    // Update is called once per frame
    private void Update()
    {
        MoveCamera();        
    }

    // Moves our Camera
    private void MoveCamera()
    {
        if (Target == null)
        {
            return;
        }

        // Calculate Position
        TargetPosition = GetTargetPosition(Target);
        CameraTargetPosition = new Vector3(TargetPosition.x, TargetPosition.y, 0f);

        // Follow on selected axis
        float xPos = horizontalFollow ? CameraTargetPosition.x : transform.localPosition.x;
        float yPos = verticalFollow ? CameraTargetPosition.y : transform.localPosition.y;

        // Set offset
        CameraTargetPosition += new Vector3(horizontalFollow ? horizontalOffset : 0f, verticalFollow ? verticalOffset : 0f, 0f);        

        // Set smooth value
        targetHorizontalSmoothFollow = Mathf.Lerp(targetHorizontalSmoothFollow, CameraTargetPosition.x, horizontalSmoothness * Time.deltaTime);
        targetVerticalSmoothFollow = Mathf.Lerp(targetVerticalSmoothFollow, CameraTargetPosition.y, verticalSmoothness * Time.deltaTime);

        // Get direction towards target pos
        float xDirection = targetHorizontalSmoothFollow - transform.localPosition.x;
        float yDirection = targetVerticalSmoothFollow - transform.localPosition.y;
        Vector3 deltaDirection = new Vector3(xDirection, yDirection, 0f);

        // New position
        Vector3 newCameraPosition = transform.localPosition + deltaDirection;

        // Calculate and apply clamping
        float clampedX = Mathf.Clamp(newCameraPosition.x, minViewValue.x, maxViewValue.x);
        float clampedY = Mathf.Clamp(newCameraPosition.y, minViewValue.y, maxViewValue.y);
        float clampedZ = Mathf.Clamp(newCameraPosition.z, minViewValue.z, maxViewValue.z);

        // Apply the clamped position
        Vector3 targetClampedPosition = new Vector3(clampedX, clampedY, clampedZ);

        // Apply Lerp for smooth transition
        clampedCameraPosition = Vector3.Lerp(clampedCameraPosition, targetClampedPosition, clampSmoothness * Time.deltaTime);

        // Apply the clamped and smoothed position
        transform.localPosition = new Vector3(clampedCameraPosition.x, clampedCameraPosition.y, transform.localPosition.z);
    }

    // Returns the position of out target
    private Vector3 GetTargetPosition(PlayerMotor player)
    {
        float xPos = 0f;
        float yPos = 0f;

        xPos += (player.transform.position.x + horizontalOffset) * horizontalInfluence;
        yPos += (player.transform.position.y + verticalOffset) * verticalInfluence;

        Vector3 positionTarget = new Vector3(xPos, yPos, transform.position.z);
        return positionTarget;
    }

    // Centers our camera in the target position
    private void CenterOnTarget(PlayerMotor player)
    {
        Target = player;

        Vector3 targetPos = GetTargetPosition(Target);
        targetHorizontalSmoothFollow = targetPos.x;
        targetVerticalSmoothFollow = targetPos.y;
        transform.localPosition = targetPos;
    }

    // Reset the target reference
    private void StopFollow(PlayerMotor player)
    {
        Target = null;
    }

    // Gets Target reference and center our camera
    private void StartFollowing(PlayerMotor player)
    {
        Target = player;
        CenterOnTarget(Target);
    }

    private void OnEnable()
    {
        LevelManager.OnPlayerSpawn += CenterOnTarget;
        Health.OnDeath += StopFollow;
        Health.OnRevive += StartFollowing;
    }

    private void OnDisable()
    {
        LevelManager.OnPlayerSpawn -= CenterOnTarget;
        Health.OnDeath -= StopFollow;
        Health.OnRevive -= StartFollowing;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 camPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + 2f);
        Gizmos.DrawWireSphere(camPosition, 0.5f);
    }
}
