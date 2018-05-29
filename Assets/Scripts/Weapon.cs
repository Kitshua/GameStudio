using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    public int[] dmgList = { 1, 2, 3, 4, 5, 6, 7 };
    public float[] knockbackList = { 2.0f, 2.2f, 2.5f, 3f, 3.2f, 3.6f, 4f };

    [HideInInspector]
    public int weaponLvl = 0;
    public SpriteRenderer spriteRenderer;

    //attack
    private Animator anim;
    private float cooldown = 0.5f;
    private float lastSwing;

    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    //on collision
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Warrior")
        {
            if (coll.name == "Player")
                return;

            Damage dmg = new Damage
            {
                dmgDealt = dmgList[weaponLvl],
                origin = transform.position,
                knockback= knockbackList[weaponLvl]
            };

            coll.SendMessage("ReceiveDamage", dmg);
        }
    }

    public void UpgradeWeapon()
    {
        weaponLvl++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLvl];
    }

    public void SetWeaponLvl(int lvl)
    {
        weaponLvl = lvl;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLvl];
    }

    //SWING
    private void Swing()
    {
        anim.SetTrigger("Swing");
    }
}
