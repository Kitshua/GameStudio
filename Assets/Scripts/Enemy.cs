using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    public int expValue = 1;

    public float aggroRange = 1;        
    public float anchorDist = 7;  
    
    [HideInInspector]
    public bool chase;
    private bool collidePlayer;
    private Transform playerTransform;
    private Vector3 startPos;

    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];

    private void FixedUpdate()
    {
        //aggro or no
        if (Vector3.Distance(playerTransform.position, startPos) < aggroRange)
        {
            if (Vector3.Distance(playerTransform.position, startPos) < aggroRange)
                chase = true;

            if (chase)
            {
                if (!collidePlayer)
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                }
            }
            else
            {
                UpdateMotor(startPos - transform.position);
            }
        }
        else
        {
            UpdateMotor(startPos - transform.position);
            chase = false;
        }

        collidePlayer = false;
        boxColl.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            if (hits[i].tag == "Warrior" && hits[i].name == "Player")
            {
                collidePlayer = true;
            }

            //reset
            hits[i] = null;
        }
    } //end fixed update

    protected override void Start()
    {
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        startPos = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.instance.GainXp(expValue);
        GameManager.instance.ShowText("+" + expValue + " xp", 30, Color.green, transform.position, Vector3.up * 40, 1.0f);
    }
}
