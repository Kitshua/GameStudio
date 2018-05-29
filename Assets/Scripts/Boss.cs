using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    //rotation speed
    public float[] fireballSpeed = { 2.5f, -2.5f };  
    //distance it can go
    public float dist = 0.25f;  
    //array
    public Transform[] fireballs;

    private void Update()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            fireballs[i].position = transform.position + new Vector3(-Mathf.Cos(Time.time * fireballSpeed[i]) * dist, Mathf.Sin(Time.time * fireballSpeed[i]) * dist, 0);
        }
    }
}
