using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(BoxCollider2D))]
public class Player : Mover
{
    //private BoxCollider2D boxColl;
    //private Vector3 delta;
    //private RaycastHit2D hit;
    public SpriteRenderer spriteRenderer;
    private bool isAlive = true;

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (isAlive)
            UpdateMotor(new Vector3(x, y, 0));
    }

    protected override void TakeDamage(Damage dmg)
    {
        if (!isAlive)
            return;

        base.TakeDamage(dmg);
        GameManager.instance.OnHitpointChange();
    }

    protected override void Death()
    {
        isAlive = false;
        GameManager.instance.deathMenuAnim.SetTrigger("Show");
    }

    public void SwapSprite(int skinId)
    {
        spriteRenderer.sprite = GameManager.instance.playerSprites[skinId];
    }

    public void OnLvlUp()
    {
        maxHp++;
        hp = maxHp;
    }

    public void SetLvl(int lvl)
    {
        for (int i = 0; i < lvl; i++)
            OnLvlUp();
    }

    public void Heal(int healingAmount)
    {
        if (hp == maxHp)
            return;

        hp += healingAmount;
        if (hp > maxHp)
            hp = maxHp;
        GameManager.instance.ShowText("+" + healingAmount.ToString() + "hp", 25, Color.green, transform.position, Vector3.up * 30, 1.0f);
        GameManager.instance.OnHitpointChange();
    }

    public void Respawn()
    {
        Heal(maxHp);
        isAlive = true;
        lastImmune = Time.time;
        pushDir = Vector3.zero;
    }

}
