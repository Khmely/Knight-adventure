using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float moveSpeed = 7.0f;
    public Transform RPoint, LPoint;
    public Transform startPos;

    Vector3 nextPos;

    void Start()
    {
        nextPos = startPos.position;
    }
    void Update()
    {
        if (transform.position == LPoint.position) {
            nextPos = RPoint.position;
        }
        if (transform.position == RPoint.position)
        {
            nextPos = LPoint.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPos, moveSpeed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(RPoint.position, LPoint.position);
    }
}
