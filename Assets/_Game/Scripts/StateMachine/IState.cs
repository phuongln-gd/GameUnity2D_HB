using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    // khi bat dau vao state
    void OnEnter(Enemy enemy);
    // trang thia update lien tuc
    void OnExecute(Enemy enemy);
    //khi ket thuc state
    void OnExit(Enemy enemy);
}
