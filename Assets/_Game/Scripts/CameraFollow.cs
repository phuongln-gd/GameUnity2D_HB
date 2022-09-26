using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // target ma camera di theo
    public Transform target;
    // vi tri tuong doi cua camera va target
    public Vector3 offset; 
    public float speed = 10;

    
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * speed);
    }
    
}
