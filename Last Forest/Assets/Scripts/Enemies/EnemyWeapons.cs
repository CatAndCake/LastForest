using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapons : MonoBehaviour
{
    EnemyBattle enemyBattle;
    
    public GameObject attackedGO;

    private void Awake()
    {
        enemyBattle = this.gameObject.GetComponentInParent<EnemyBattle>(); //GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyBattle>();
        
    }

    private void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
        
        if(other.transform.tag == ("Player"))
        {
            enemyBattle.WeaponCollision(other.gameObject);
            attackedGO = other.gameObject;
        }

        if(other.transform.tag == ("Enddar"))
        {
            enemyBattle.WeaponCollision(other.gameObject);
            attackedGO = other.gameObject;
        }

        if(other.transform.tag == ("TreeFruits") || other.transform.tag == ("TreeFighting") || other.transform.tag == ("TreeDefending"))
        {
            enemyBattle.WeaponCollision(other.gameObject);
            attackedGO = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        

        if (other.transform.tag == ("Player"))
        {
            enemyBattle.EndWeaponCollision();
            attackedGO = null;
        }

        if (other.transform.tag == ("Enddar"))
        {
            enemyBattle.EndWeaponCollision();
            attackedGO = null;
        }
        if (other.transform.tag == ("TreeFruits") || other.transform.tag == ("TreeFighting") || other.transform.tag == ("TreeDefending"))
        {
            enemyBattle.EndWeaponCollision();
            attackedGO = null;
        }
    }
}
