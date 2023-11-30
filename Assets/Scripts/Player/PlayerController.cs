using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float gravity = -20f;

    [Header("Collisions")]
    [SerializeField] private LayerMask collideWith;
    [SerializeField] private int verticalRayAmount = 4;
    [SerializeField] private int horizontalRayAmount = 4;

    //Properties
    public bool FacingRight { get; set; }
    public float Gravity => gravity;

    //Internal
    private BoxCollider2D boxCollider2D;
    private PlayerConditions conditions;

    private Vector2 boundsTopLeft;
    private Vector2 boundsTopRight;
    private Vector2 boundsBottomLeft;
    private Vector2 boundsBottomRight;
    private float boundsWidth;
    private float boundsHeight;

    private Vector2 force;
    private Vector2 movePosition;
    private float currentGravity;
    private float skin = 0.05f;

    private float internalFaceDirection = 1f;
    private float faceDirection;

    // Start is called before the first frame update
    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();

        conditions = new PlayerConditions();
        conditions.Reset();
    }

    // Update is called once per frame
    private void Update()
    {
        ApplyGravity();
        StartMovement();

        SetupRayOrigin();
        GetFaceDirection();

        if (FacingRight)
        {
            CollideHorizontal(1);
        }
        else
        {
            CollideHorizontal(-1);
        }

        CollideBelow();

        Debug.DrawRay(boundsTopLeft, Vector2.left, Color.blue);
        Debug.DrawRay(boundsTopRight, Vector2.right, Color.blue);
        Debug.DrawRay(boundsBottomLeft, Vector2.left, Color.blue);
        Debug.DrawRay(boundsBottomRight, Vector2.right, Color.blue);

        transform.Translate(movePosition, Space.Self);

        SetupRayOrigin();
        CalculateMovement();
    }

    private void ApplyGravity()
    {
        currentGravity = gravity;

        force.y += currentGravity * Time.deltaTime;
    }

    private void CalculateMovement()
    {
        if (Time.deltaTime > 0)
            force = movePosition / Time.deltaTime;
    }

    private void StartMovement()
    {
        movePosition = force * Time.deltaTime;
        conditions.Reset();
    }

    public void SetHorizontalForce(float xForce)
    {
        force.x = xForce;
    }

    public void SetVerticalForce(float yForce)
    {
        force.y = yForce;
    }

    private void SetupRayOrigin()
    {
        Bounds playerBounds = boxCollider2D.bounds;

        boundsTopLeft = new Vector2(playerBounds.min.x, playerBounds.max.y);
        boundsTopRight = new Vector2(playerBounds.max.x, playerBounds.max.y);
        boundsBottomLeft = new Vector2(playerBounds.min.x, playerBounds.min.y);
        boundsBottomRight = new Vector2(playerBounds.max.x, playerBounds.min.y);

        boundsWidth = Vector2.Distance(boundsTopLeft, boundsTopRight);
        boundsHeight = Vector2.Distance(boundsTopLeft, boundsBottomLeft);
    }

    private void CollideBelow()
    {
        if (movePosition.y < -0.0001f)
        {
            conditions.IsFalling = true;
        }
        else
        {
            conditions.IsFalling = false;
        }

        if (!conditions.IsFalling)
        {
            conditions.IsCollidingBelow = false;
            return;
        }

        //Calculate ray length
        float rayLength = boundsHeight / 2f + skin;
        if (movePosition.y < 0)
            rayLength += Mathf.Abs(movePosition.y);

        //Calculate ray origin
        Vector2 leftOrigin = (boundsBottomLeft + boundsTopLeft) / 2f;
        Vector2 rightOrigin = (boundsBottomRight + boundsTopRight) / 2f;
        leftOrigin += (Vector2)(transform.up * skin) + (Vector2)(transform.right * movePosition.x);
        rightOrigin += (Vector2)(transform.up * skin) + (Vector2)(transform.right * movePosition.x);

        //Perform raycast
        for (int i = 0; i < verticalRayAmount; i++)
        {
            Vector2 rayOrigin = Vector2.Lerp(leftOrigin, rightOrigin, (float) i / (float) (verticalRayAmount - 1));
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, -transform.up, rayLength, collideWith);
            Debug.DrawRay(rayOrigin, -transform.up * rayLength, Color.blue);

            if (hit)
            {
                if (force.y > 0)
                {
                    movePosition.y = force.y * Time.deltaTime;
                    conditions.IsCollidingBelow = false;
                }
                else
                {
                    movePosition.y = -hit.distance + boundsHeight / 2f + skin;
                }

                conditions.IsCollidingBelow = true;
                conditions.IsFalling = false;

                if (Mathf.Abs(movePosition.y) < 0.0001f)
                {
                    movePosition.y = 0f;
                }
            }
        }
    }

    private void CollideHorizontal(int direction)
    {
        Vector2 rayHorizontalBottom = (boundsBottomLeft + boundsBottomRight) / 2f;
        Vector2 rayHorizontalTop = (boundsTopLeft + boundsTopRight) / 2f;
        rayHorizontalBottom += (Vector2)transform.up * skin;
        rayHorizontalTop -= (Vector2)transform.up * skin;

        float rayLength = Mathf.Abs(force.x * Time.deltaTime) + boundsWidth / 2f + skin * 2f;

        for (int i = 0; i < horizontalRayAmount; i++)
        {
            Vector2 rayOrigin = Vector2.Lerp(rayHorizontalBottom, rayHorizontalTop, (float)i / (horizontalRayAmount - 1));
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, direction * transform.right, rayLength, collideWith);
            Debug.DrawRay(rayOrigin, transform.right * rayLength * direction, Color.cyan);

            if (hit)
            {
                if (direction >= 0)
                {
                    movePosition.x = hit.distance - boundsWidth / 2f - skin * 2f;
                    conditions.IsCollidingRight = true;
                }
                else
                {
                    movePosition.x = -hit.distance + boundsWidth / 2f + skin * 2f;
                    conditions.IsCollidingLeft = true;
                }

                force.x = 0f;
            }
        }
    }

    private void GetFaceDirection()
    {
        faceDirection = internalFaceDirection;
        FacingRight = faceDirection == 1;  // if FacingRight is TRUE

        if (force.x > 0.0001f)
        {
            faceDirection = 1f;
            FacingRight = true;
        }
        else if (force.x < -0.0001f)
        {
            faceDirection = -1f;
            FacingRight = false;
        }

        internalFaceDirection = faceDirection;
    }
}
