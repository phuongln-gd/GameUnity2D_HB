using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Thanh mau
 */
public class HealthBar : MonoBehaviour
{
    [SerializeField] Image imageFill;
    [SerializeField] Vector3 offset; // vi tri tuong doi

    float hp;
    float maxHp;

    private Transform target;

    // Update is called once per frame
    void Update()
    {
        // tru mau tu tu trong 5 don vi thoi gian deltaTime
        imageFill.fillAmount = Mathf.Lerp(imageFill.fillAmount, hp/maxHp, Time.deltaTime * 5f);
        // hien thi vi tri thanh mau tren vi tri target 1 khoang offset
        transform.position = target.position + offset;
    }

    // khoi tao thanh mau
    public void OnInit(float maxHp, Transform target)
    {
        this.target = target;
        this.maxHp = maxHp;
        hp = maxHp;
        // set luong mau la 100% 
        imageFill.fillAmount = 1;
    }
    
    public void setNewHp(float hp)
    {
        this.hp = hp;
    }

}
