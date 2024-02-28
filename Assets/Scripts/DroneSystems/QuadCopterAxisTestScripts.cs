using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadCopterAxisTestScripts : MonoBehaviour
{
    public float altitudeMin = 50f;
    public float altitudeMax = 100f;
    public float speed = 5f;
    public GameObject targetPlane;

    private List<Vector3> waypoints = new List<Vector3>();
    private int currentWaypointIndex = 0;

    private void Start()
    {
        float altitude = Random.Range(altitudeMin, altitudeMax);
        Vector3 targetPosition = new Vector3(transform.position.x, altitude, transform.position.z);
        transform.position = targetPosition;

        CalculateShortestPath();
        StartCoroutine(FlyAround());
    }

    private void CalculateShortestPath()
    {
        Vector3 planePosition = targetPlane.transform.position;
        float halfSize = targetPlane.transform.localScale.x / 2;

        List<Vector3> queue = new List<Vector3>();
        HashSet<Vector3> visited = new HashSet<Vector3>();

        queue.Add(planePosition);
        visited.Add(planePosition);

        while (queue.Count > 0)
        {
            Vector3 currentPos = queue[0];
            queue.RemoveAt(0);
            waypoints.Add(currentPos);

            if (Mathf.Abs(currentPos.x - planePosition.x) < halfSize)
            {
                if (!visited.Contains(new Vector3(currentPos.x, currentPos.y, planePosition.z)))
                {
                    queue.Add(new Vector3(currentPos.x, currentPos.y, planePosition.z));
                    visited.Add(new Vector3(currentPos.x, currentPos.y, planePosition.z));
                }
            }

            if (Mathf.Abs(currentPos.z - planePosition.z) < halfSize)
            {
                if (!visited.Contains(new Vector3(planePosition.x, currentPos.y, currentPos.z)))
                {
                    queue.Add(new Vector3(planePosition.x, currentPos.y, currentPos.z));
                    visited.Add(new Vector3(planePosition.x, currentPos.y, currentPos.z));
                }
            }
        }
    }

    private IEnumerator FlyAround()
    {
        while (true)
        {
            Vector3 targetPosition = waypoints[currentWaypointIndex];
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                yield return null;
            }
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
        }
    }
}
