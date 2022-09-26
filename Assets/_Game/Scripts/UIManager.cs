using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Quan ly UI
 * **/
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    //public static UIManager Instance
    //{
    //    get
    //    {
    //        if (instance == null)
    //        {
    //            instance = FindObjectOfType<UIManager>();
    //        }

    //        return instance;
    //    }
    //}

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] Text coinText;
    public void setCoin(int coin)
    {
        // thay doi text
        coinText.text = coin.ToString();
    }
}
