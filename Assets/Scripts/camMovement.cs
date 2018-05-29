using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMovement : MonoBehaviour
{
    public Transform focus;
    public float xBound = 0.15f;
    public float yBound = 0.05f;

    private void Start()
    {
        focus = GameObject.Find("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        //check x axis
        float xDelta = focus.position.x - transform.position.x;
        if (xDelta > xBound || xDelta < -xBound)
        {
            if (transform.position.x < focus.position.x)
            {
                delta.x = xDelta - xBound;
            }
            else
            {
                delta.x = xDelta + xBound;
            }
        }

        //check y axis
        float yDelta = focus.position.y - transform.position.y;
        if (yDelta > yBound || yDelta < -yBound)
        {
            if (transform.position.y < focus.position.y)
            {
                delta.y = yDelta - yBound;
            }
            else
            {
                delta.y = yDelta + yBound;
            }
        }

        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}
