using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Platform di chuyen **/
public class MovingPlatform : MonoBehaviour
{
    [SerializeField]private Transform aPoint, bPoint;
    [SerializeField]private float speed;

    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        /*
         * Khoi tao vi tri ban dau va vi tri ket thuc 
         */
        transform.position = aPoint.position;
        target = bPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        // di chuyen tu vi tri hien tai den target
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);

        // kiem tra khoang cach den aPoint
        // neu cham aPoint thay doi target la diem bPoint
        if(Vector2.Distance(transform.position,aPoint.position) < 0.1f)
        {
            target = bPoint.position;
        }
        // kiem tra khoang cach den bPoint
        // neu cham aPoint thay doi target la diem aPoint
        else if (Vector2.Distance(transform.position, bPoint.position) < 0.1f)
        {
            target = aPoint.position;
        }
    }

    // on collision enter: Neu Player dang va cham voi MovingPlatform
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // thiet lap gameobject Player la con cua MovingPlatform
            collision.transform.SetParent(transform);
        }
    }

    // on collision exit: Neu Player khong va cham voi MovingPlatform
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null);
        }
    }
}
