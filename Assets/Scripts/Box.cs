using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Warrior
{
    protected override void Death()
    {
        //base.Death();
        Destroy(gameObject);
    }
}
