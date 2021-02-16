using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public GameObject startingPoint;
    public GameObject endingPoint;

    public Vector3 velocity;
    public int platformWidth;

    bool moveRight = true;

    double endPoint;
    double startPoint;

    float moveSpeed = 2f;

    void Start()
    {
        endPoint = endingPoint.transform.position.x;
        startPoint = startingPoint.transform.position.x;
    }

    void FixedUpdate()
    {
        if(transform.position.x > endPoint)
        {
            moveRight = false;
        }
        else if(transform.position.x + platformWidth < startPoint)
        {
            moveRight = true;
        }

        if (moveRight)
        {
            transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
        }
    }
}
