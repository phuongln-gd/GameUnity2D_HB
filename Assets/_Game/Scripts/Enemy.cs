using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Enemy ke thua tu Character 
 */
public class Enemy : Character
{
    [SerializeField] private float attackRange = 1;
    [SerializeField] private float moveSpeed = 2;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private GameObject attackArea;

    private IState currentState;
    private bool isRight = true;
    private Character target;
    public Character Target => target;

    void Update()
    {
        if (currentState != null && !isDead)
        {
            currentState.OnExecute(this);
        }
    }

    // khoi tao enemy
    public override void OnInit()
    {
        base.OnInit();
        ChangeState(new IdleState());
        DeactiveAttack();
    }

    // destroy enemy
    public override void OnDespawn()
    {
        Destroy(healthBar.gameObject);
        Destroy(gameObject);
    }

    //enmy die
    protected override void OnDeath()
    {
        currentState = null;
        ChangeAnim("die");
        Invoke(nameof(OnDespawn),1f);
    }

    //thay doi trang thai enemy
    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    // thay doi cho enemy di dung huong
    public void ChangeDirection(bool isRight)
    {
        this.isRight = isRight;
        transform.rotation = isRight ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up * 180);
    }

    // di chuyen
    public void Moving()
    {
        ChangeAnim("run");
        rb.velocity = transform.right * moveSpeed;
    }

    // dung im
    public void StopMoving()
    {
        ChangeAnim("idle");
        rb.velocity = Vector2.zero;
    }
    
    // tan cong
    public void Attack()
    {
        if (isTargetInRange())
        {
            ChangeAnim("attack");
            ActiveAttack();
            Invoke(nameof(DeactiveAttack), 0.5f);
        }
    }

    // kiem tra target trong tam danh 
    public bool isTargetInRange()
    {
        if (target != null && Vector2.Distance(transform.position, target.transform.position) <= attackRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // va cham mem voi 2 diem dau cuoi trong khoang di chuyen
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyWall")
        {
            ChangeDirection(!isRight);
        }
    }

    private void ActiveAttack()
    {
        attackArea.SetActive(true);
    }

    private void DeactiveAttack()
    {
        attackArea.SetActive(false);
    }
    
    // thiet lap target
    internal void SetTarget(Character character)
    {
        this.target = character;
        if (isTargetInRange())
        {
            ChangeState(new AttackState());
        }
        else if (Target != null)
        {
            ChangeState(new PatronState());
        }
        else
        {
            ChangeState(new IdleState());
        }
    }
}
