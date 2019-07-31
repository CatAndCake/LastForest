using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    Animator anim;

    EnemySpeed enemySpeed;
    EnemyPathfindin pathfinding;
    EnemyTarget target;
    

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemySpeed = GetComponent<EnemySpeed>();
        pathfinding = GetComponent<EnemyPathfindin>();
        target = GetComponent<EnemyTarget>();
        anim.SetFloat("Movement", 0);
    }

    private void Update()
    {
        anim.SetFloat("Move", enemySpeed.speed / 2);
        AttackAnimation();
    }

    void AttackAnimation()
    {
        //if our target reached attack position
        //if our target is in good angle to the target
        //if time beetween attacks is goood
        if (pathfinding.reachedAttackPosition == true)
        {
            if (target.distance <= 1.6)
            {
                anim.SetTrigger("Attack");
            }
        }
    }
}
