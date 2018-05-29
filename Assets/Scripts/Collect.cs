using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : Collidable
{
    protected bool collected;

    protected override void OnCollide(Collider2D coll)
    {
        //parent call
        //base.OnCollide(coll);

        if(coll.name == "Player")
        {
            OnCollect();
        }
    }

    protected virtual void OnCollect()
    {
        collected = true;
    }
}
