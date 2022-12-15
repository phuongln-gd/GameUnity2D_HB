using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce = 350;

    [SerializeField] private Kunai kunaiPrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private GameObject attackArea;

    private bool isGrounded = true;
    private bool isJumping = false;
    private bool isAttack = false;

    private float horizontal;

    private int coin = 0;

    private Vector3 savePoint;

    private void Awake()
    {
        // luu 1 gia tri voi key ="coin", gia tri mac dinh la 0
        coin = PlayerPrefs.GetInt("coin", 0); 
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = CheckGrounded();

        //horizontal = Input.GetAxis("Horizontal"); 
        //vertical = Input.GetAxis("vertical");

        if (isDead)
        {
            return;
        }

        // Check Grounded
        if (isGrounded)
        {
            //CheckAttack 
            if (isAttack)
            {
                rb.velocity = Vector2.zero;
                return;
            }

            // CheckJump
            if (isJumping)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            //change anim run
            if (Mathf.Abs(horizontal) > 0.1f)
            {
                ChangeAnim("run");
            }

            //Attack
            if (Input.GetKeyDown(KeyCode.C))
            {
                Attack();
            }

            //Throw
            if (Input.GetKeyDown(KeyCode.V))
            {
                Throw();
            }
                
        }

        //check jumpingout
        if (CheckFalling() && isJumping)
        {
            Fall();
            isJumping = false;
        }

        // check falling
        if (CheckFalling())
        {
            Fall();
        }

        //Moving 
        if (Mathf.Abs(horizontal)> 0.1f)
        {
            rb.velocity = new Vector2(horizontal * speed,rb.velocity.y);
            // quay Player
            transform.rotation = Quaternion.Euler(new Vector3(0, horizontal> 0 ? 0 : 180, 0));
        }

        //idle
        else if (isGrounded)
        {
            ChangeAnim("idle");
            rb.velocity = Vector2.zero;
        }

    }

    // ham khoi tao
    public override void OnInit()
    {
        base.OnInit();
        ChangeAnim("idle");
        isAttack = false;
        transform.position = savePoint;
        DeactiveAttack();
        SavePoint();
        // khoi tao UI text coin
        UIManager.instance.setCoin(coin);
    }

    //ham ket thuc
    public override void OnDespawn()
    {
        base.OnDespawn();
    }

    // khi player chet
    public override void OnDeath()
    {
        base.OnDeath();
        Invoke(nameof(OnInit),1f);
    }

    // kiem tra player duoi dat 
    private bool CheckGrounded()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 1.1f,Color.red);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);
        //if (hit.collider != null)
        //    return true;
        //else
        //    return false;
        return hit.collider != null;
    }

    public void Attack()
    {
        if (isGrounded && !isAttack)
        {
            ChangeAnim("attack");
            isAttack = true;
            Invoke(nameof(ResetAttack), 0.5f);
            ActiveAttack();
            Invoke(nameof(DeactiveAttack), 0.5f);
        }
    }

    public void Throw()
    {
        if (isGrounded && !isAttack)
        {
            ChangeAnim("throw");
            isAttack = true;
            Invoke(nameof(ResetAttack), 0.5f);
            Instantiate(kunaiPrefab, throwPoint.position, throwPoint.rotation);
        }
    }
    public void Jump()
    {
        if (isGrounded && !isJumping)
        {
            isJumping = true;
            ChangeAnim("jump");
            rb.AddForce(Vector2.up * jumpForce);
        }
    }

    private void Fall()
    {
        if (CheckFalling())
        {
            ChangeAnim("fall");
        }
    }

    // kiem tra player roi 
    private bool CheckFalling()
    {
        return !isGrounded && rb.velocity.y < 0 ;
    }
    
    //cai dat lai tan cong
    public void ResetAttack()
    {
        ChangeAnim("idle");
        isAttack = false;
    }

    // on trigger: va cham mem
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // kiem tra dieu kien va cham voi coin
        if (collision.tag == "Coin")
        {
            coin += 1;
            // luu so coin hien tai vao PlayerPrefs
            PlayerPrefs.SetInt("coin", coin);
            // hien thi lai so coin tren UI
            UIManager.instance.setCoin(coin);
            // Pha huy object va cham voi player
            Destroy(collision.gameObject);
        }

        // kiem tra dieu kien va cham voi deathzone
        if (collision.tag == "DeathZone")
        {
           OnDeath();
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

    public void SetMove(float horizontal)
    {
        this.horizontal = horizontal;

    }
    internal void SavePoint()
    {
        savePoint = transform.position;
    }
}