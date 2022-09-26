using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] protected HealthBar healthBar;
    [SerializeField] protected CombatText combatTextPrefab;

    // hp
    protected float hp;
    // ten anim hien tai
    private string currentAnimName;
    public bool isDead => (hp <= 0); // return true neu hp <= 0,false neu hp >0

    private void Start()
    {
        OnInit();
    }

    public virtual void OnInit()
    {
        hp = 100;
        // khoi tao thanh mau
        healthBar.OnInit(hp,transform);
    }

    public virtual void OnDespawn()
    {

    }

    protected virtual void OnDeath()
    {
        ChangeAnim("die");
        Invoke(nameof(OnDespawn), 1.5f);
    }

    protected void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            // cai dat lai thong so trigger
            anim.ResetTrigger(animName); 
            currentAnimName = animName;
            // cai dat lai trigger theo ten anim hien tai
            anim.SetTrigger(currentAnimName);
        }
    }
    
    // tru hp khi bi tan cong
    public void OnHit(float damage)
    {
        if(!isDead)
        {
            hp -= damage;
            // identity: goc xoay = 0 (default)
            Instantiate(combatTextPrefab, transform.position + Vector3.up, Quaternion.identity).OnInit(damage);
            if (hp <= 0) //hp <= damage
            {
                hp = 0;
            }
            // cai dat lai mau
            healthBar.setNewHp(hp);
            if (hp == 0)
            {
                OnDeath();
            }
        }
    }
}
