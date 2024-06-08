using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class BattleUnitHealth : MonoBehaviour
{
    public int health;
    public int maxhealth;
    public static event Action Hit;
    public static event Action<GameObject> PlayHitAnim;

    public void takeDamage(int damage, bool successful, bool parried)
    {
        if (damage < 0)
            damage = 0;
        health -= damage;
        print(health + "/" + maxhealth);
        Hit();
        if (health <= 0)
        {
            Die();
        }
        else if(successful && !parried)
        {
            print("hit");
            PlayHitAnim(this.gameObject);
        }
    }
    public void Heal(int healAmmount)
    {
        health += healAmmount;
        print(health + "/" + maxhealth);
        
        if (health > maxhealth)
        {
            health = maxhealth;
        }
        Hit();
    }
    protected void CallHit()
    {
        Hit();
    }

    virtual protected void Die()
    {
        print("dead");
        gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = -1;
    }
}
