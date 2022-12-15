using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Phi tieu
 */
public class Kunai : MonoBehaviour
{
    public GameObject hitVFX;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    // ham khoi tao
    public void OnInit()
    {
        rb.velocity = transform.right * 5f;
        Invoke(nameof(OnDespawn), 4f);
    }

    // ham destroy
    public void OnDespawn()
    {
        Destroy(gameObject);
    }

    // va cham voi enemy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Character>().OnHit(30f);
            // tao ra hitVFX tai vi tri va cham theo chieu cua kunai
            Instantiate(hitVFX,transform.position,transform.rotation);
            OnDespawn(); // tu huy dao
        }
    }

}
