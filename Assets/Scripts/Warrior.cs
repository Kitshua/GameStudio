using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour
{
    public int hp = 15;
    public int maxHp = 15;
    public float pushRecoverSpeed = 0.2f;

    protected float immuneTime = 1.0f;
    protected float lastImmune;

    protected Vector3 pushDir;

    protected virtual void TakeDamage(Damage dmg)
    {
        if (Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            hp -= dmg.dmgDealt;
            pushDir = (transform.position - dmg.origin).normalized * dmg.knockback;

            GameManager.instance.ShowText(dmg.dmgDealt.ToString(), 25, Color.red, transform.position, Vector3.zero, 0.5f);

            if (hp <= 0)
            {
                hp = 0;
                Death();
            }
        }
    }

    protected virtual void Death()
    {

    }
}
