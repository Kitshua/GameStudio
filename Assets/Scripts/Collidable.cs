using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(BoxCollider2D))]
public class Collidable : MonoBehaviour
{
    public ContactFilter2D filter;
    private BoxCollider2D boxColl;
    private Collider2D[] hitList = new Collider2D[10];

    protected virtual void Start ()
    {
        boxColl = GetComponent<BoxCollider2D>();
	}

    protected virtual void Update()
    {
        //check collisions
        boxColl.OverlapCollider(filter, hitList);

        for (int i = 0; i < hitList.Length; i++)
        {
            if (hitList[i] == null)
                continue;

            //check hits
            //Debug.Log(hitList[i].name);

            OnCollide(hitList[i]);

            //reset
            hitList[i] = null;
        }
    }

    protected virtual void OnCollide(Collider2D coll)
    {
        //double check
        Debug.Log("OnCollide error: " + coll.name);
    }
}
