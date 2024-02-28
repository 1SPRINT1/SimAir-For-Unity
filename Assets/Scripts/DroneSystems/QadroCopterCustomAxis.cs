using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QadroCopterCustomAxis : MonoBehaviour
{
    public float altitudeMin = 50f;
    public float altitudeMax = 100f;
    public float speed = 5f;
    public GameObject targetPlane;

    private Vector3[] waypoints;
    private int currentWaypointIndex = 0;

    private void Start()
    {
        float altitude = Random.Range(altitudeMin, altitudeMax);
        Vector3 targetPosition = new Vector3(transform.position.x, altitude, transform.position.z);
        transform.position = targetPosition;

        CalculateWaypoints();
        StartCoroutine(FlyAround());
    }

    private void CalculateWaypoints()
    {
        waypoints = new Vector3[4];
        float planeSize = targetPlane.transform.localScale.x;
        waypoints[0] = targetPlane.transform.position + Vector3.forward * planeSize / 2;
        waypoints[1] = targetPlane.transform.position + Vector3.right * planeSize + Vector3.forward * planeSize / 2;
        waypoints[2] = targetPlane.transform.position + Vector3.right * planeSize + Vector3.back * planeSize / 2;
        waypoints[3] = targetPlane.transform.position + Vector3.back * planeSize / 2;
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
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    } 
}
