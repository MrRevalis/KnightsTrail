using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovementVertical : MonoBehaviour
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
        endPoint = endingPoint.transform.position.y;
        startPoint = startingPoint.transform.position.y;
    }

    void FixedUpdate()
    {
        if(transform.position.y > endPoint)
        {
            moveRight = false;
        }
        else if(transform.position.y < startPoint)
        {
            moveRight = true;
        }

        if (moveRight)
        {
            transform.position = new Vector2(transform.position.x , transform.position.y + moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector2(transform.position.x , transform.position.y - moveSpeed * Time.deltaTime);
        }
    }
}
