using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : Warrior
{
    private Vector3 originSize;

    protected BoxCollider2D boxColl;
    protected Vector3 delta;
    protected RaycastHit2D hit;

    public float ySpeed = 0.75f;
    public float xSpeed = 1.0f;

    protected virtual void Start()
    {
        originSize = transform.localScale;
        boxColl = GetComponent<BoxCollider2D>();
    }
    protected virtual void UpdateMotor(Vector3 input)
    {
        //reset
        delta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);

        if (delta.x > 0)
            transform.localScale = originSize;
        else if (delta.x < 0)
            transform.localScale = new Vector3(originSize.x * -1, originSize.y, originSize.z);

        delta += pushDir;

        pushDir = Vector3.Lerp(pushDir, Vector3.zero, pushRecoverSpeed);

        hit = Physics2D.BoxCast(transform.position, boxColl.size, 0, new Vector2(0, delta.y), Mathf.Abs(delta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            transform.Translate(0, delta.y * Time.deltaTime, 0);
        }

        hit = Physics2D.BoxCast(transform.position, boxColl.size, 0, new Vector2(delta.x, 0), Mathf.Abs(delta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            transform.Translate(delta.x * Time.deltaTime, 0, 0);
        }
    }
}
