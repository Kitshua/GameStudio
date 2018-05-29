using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCText : Collidable
{
    public string msg;

    private float cooldown = 5.0f;
    private float lastspoke;

    protected override void Start()
    {
        base.Start();
        lastspoke = -cooldown;
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (Time.time - lastspoke > cooldown)
        {
            lastspoke = Time.time;
            GameManager.instance.ShowText(msg, 25, Color.magenta, transform.position + new Vector3(0, 0.16f, 0), Vector3.zero, cooldown);
        }
    }
}
