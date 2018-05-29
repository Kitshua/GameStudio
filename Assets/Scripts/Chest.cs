using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collect
{
    public Sprite emptyChest;
    public int gold = 5;

    /*
    protected override void OnCollide(Collider2D coll)
    {
        //Parent call
        //base.OnCollide(coll);

        Debug.Log("Get Gold");
    }
    */

    protected override void OnCollect()
    {
        if(!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.gold += gold;
            GameManager.instance.ShowText("+" + gold + " Gold!", 25, Color.yellow, transform.position, Vector3.up * 25, 1.5f);
            //Debug.Log("Gold + " + gold);
        }
    }
}
