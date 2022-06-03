using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPasser : MonoBehaviour
{
    private Enemy enemy;
    private breakOnTrigger breakable;
    private bool isEnemy;
    private bool isBreakable;

    void Start()
    {
        if(GetComponent<Enemy>() != null)
        {
            enemy = GetComponent<Enemy>();
            isEnemy = true;
        }
        if(GetComponent<breakOnTrigger>() != null)
        {
            breakable = GetComponent<breakOnTrigger>();
            isBreakable = true;
        }
    }

    public void PassHit(int hitType)
    {
        if (hitType == 0)
        {
            if(isEnemy)
            {
                enemy.PassHit(1);
            }
            if(isBreakable)
            {
                breakable.BreakObject();
            }
        }
    }



}
