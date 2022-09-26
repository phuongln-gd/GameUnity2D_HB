using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Hieu ung luong mau hien thi khi bi tan cong
 * **/
public class CombatText : MonoBehaviour
{
    [SerializeField] Text hpText;
    public void OnInit(float damage)
    {
        // cai dat text hien thi so dame
        hpText.text = damage.ToString();
        Invoke(nameof(OnDespawn), 1f);
    }

    public void OnDespawn()
    {
        Destroy(gameObject);
    }
}
