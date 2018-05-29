using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collidable
{
    public int damage = 1;
    public float knockback = 3;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Warrior" && coll.name == "Player")
        {
            Damage dmg = new Damage
            {
                dmgDealt = damage,
                origin = transform.position,
                knockback = knockback
            };

            coll.SendMessage("TakeDamage", dmg);
        }
    }
}
