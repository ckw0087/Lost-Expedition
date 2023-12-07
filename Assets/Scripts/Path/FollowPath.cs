using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float minDistanceToPoint = 0.1f;

    public List<Vector3> points = new List<Vector3>();  // Get the Point list

    private bool playing;
    private bool moved;
    private int currentPoint = 0;
    private Vector3 currentPosition;

    // Start is called before the first frame update
    private void Start()
    {
        playing = true;

        currentPosition = transform.position;
        transform.position = currentPosition + points[0];
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // Set first position
        if (!moved)
        {
            transform.position = currentPosition + points[0];
            currentPoint++;
            moved = true;
        }

        // Move to next point
        transform.position = Vector3.MoveTowards(transform.position, currentPosition + points[currentPoint], Time.deltaTime * moveSpeed);

        // Evaluate move to next point
        float distanceToNextPoint = Vector3.Distance(currentPosition + points[currentPoint], transform.position);
        if (distanceToNextPoint < minDistanceToPoint)
        {
            currentPoint++;
        }

        // If we are on the last point, reset our position to the first one
        if (currentPoint == points.Count)
        {
            currentPoint = 0;
        }
    }

    private void OnDrawGizmos()
    {
        if (transform.hasChanged && !playing)
        {
            currentPosition = transform.position;
        }

        if (points != null)
        {
            for (int i = 0; i < points.Count; i++)
            {
                if (i < points.Count)
                {
                    // Draw points
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireSphere(currentPosition + points[i], 0.4f);

                    // Draw lines
                    Gizmos.color = Color.black;
                    if (i < points.Count - 1)
                    {
                        Gizmos.DrawLine(currentPosition + points[i], currentPosition + points[i + 1]);
                    }

                    // Draw line from last point to first point
                    if (i == points.Count - 1)
                    {
                        Gizmos.DrawLine(currentPosition + points[i], currentPosition + points[0]);
                    }
                }
            }
        }
    }
}
