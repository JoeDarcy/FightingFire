using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleMovement : MonoBehaviour
{
    //position to move to
    [Header("Point to move to:")]
    [SerializeField] Transform[] movementPoints = new Transform[0];

    //time to reach this point
    [Header("Time to reach this point:")]
    [SerializeField] float[] pointTravelTime = new float[0];

    //time to wait at point
    [Header("Time to wait at this point:")]
    [SerializeField] float[] pointWaitTime = new float[0];


    private float currentTravelTime = 0.0f;
    private float currentWaitTime = 0.0f;

    private int currentPointIndex = 0;
    private int nextPointIndex = 1;

    // Update is called once per frame
    void Update()
    {

        if (nextPointIndex == currentPointIndex) nextPointIndex += 1;
        if (nextPointIndex == movementPoints.Length) nextPointIndex = 0;
        if (currentPointIndex == movementPoints.Length) currentPointIndex = 0;

        if (currentWaitTime >= pointWaitTime[currentPointIndex])
        {
            currentTravelTime += Time.deltaTime;

            Vector3 startPos = movementPoints[currentPointIndex].position;
            Vector3 endPos = movementPoints[nextPointIndex].position;
            float timeRatio = currentTravelTime / pointTravelTime[nextPointIndex];

            Vector3 newPosition = Vector3.Lerp(startPos, endPos, timeRatio);

            transform.position = newPosition;

            if (transform.position == endPos)
            {
                currentWaitTime = 0;
                currentPointIndex++;
                currentTravelTime = 0;
            }
        }
        else
        {
            currentWaitTime += Time.deltaTime;
        }
    }
}
