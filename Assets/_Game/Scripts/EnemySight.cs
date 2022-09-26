using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 Tam nhin cua enemy
 */
public class EnemySight : MonoBehaviour
{
    public Enemy enemy;

    // Player va cham voi voi EnemySight 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            enemy.SetTarget(collision.GetComponent<Character>());
        }
    }

    // Player roi khoi vung va cham voi EnemySight
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            enemy.SetTarget(null);
        }
    }
}
