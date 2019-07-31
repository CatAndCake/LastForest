using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattle : MonoBehaviour
{
    //Enemy Statistics
    public int level;
    public int healthLevel;
    public float health;

    public int strengthLevel;
    public int strength;

    public int defenceLevel;
    public float defence;

    public int speedLevel;
    public float speed;

    public float initialHealth;
    public float healthBarValue = 100;

    //Enemy Scripts
    EnemyMovement movement;
    public float inFront;
    EnemyManager enemyManager;
    EnemyWeapons weapons;

    //Enemy Health Bar
    GameObject healthBar;
    float healthBarSize;
    
    //Player
    GameObject player;

    //Enddar
    GameObject enddar;

    //Trees
    GameObject[] trees;

    //Animations
    Animator anim;

    //Bools
    public bool weaponCollision;

    //Vectors
    Vector3 forwardVector;

    //GameObjects
    public GameObject attackedGO;


    private void Awake()
    {
        enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
        weapons = this.gameObject.GetComponent<EnemyWeapons>();
        player = GameObject.FindGameObjectWithTag("Player");
        enddar = GameObject.FindGameObjectWithTag("Enddar");
        trees = GameObject.FindGameObjectsWithTag("Tree");
        anim = this.gameObject.GetComponent<Animator>();
        movement = this.gameObject.GetComponent<EnemyMovement>();

        level = 1;

        healthLevel = Random.Range(1, 6);
        health = healthLevel * 10;
        initialHealth = health;

        strengthLevel = Random.Range(1, 6);
        strength = strengthLevel + (level * 10);

        defenceLevel = Random.Range(1, 6);
        defence = defenceLevel + (level * 10);

        speedLevel = Random.Range(1, 6);
        speed = speedLevel + (level * 10);

        healthBar = this.gameObject.transform.Find("HealthBar").gameObject.transform.Find("Bar").gameObject;
    }

    private void Update()
    {
        forwardVector = this.gameObject.transform.TransformDirection(Vector3.forward);

        SetHealthBarSize();
        Death();
    }

    void SetHealthBarSize()
    {
        if (health > 0)
        {
            healthBarSize = healthBarValue / 100f;
        }
        if (health <= 0)
        {
            healthBarSize = 0f;
        }

        healthBar.transform.localScale = new Vector3(healthBarSize, 1, 1);
    }

    public void GetHit(int playerStrength)
    {
        healthBarValue = healthBarValue - ((playerStrength * 100) / initialHealth);
        health = health - playerStrength;
    }


    public void WeaponCollision(GameObject attacked)
    {
        weaponCollision = true;
        attackedGO = attacked;
    }

    public void EndWeaponCollision()
    {
        weaponCollision = false;
        attackedGO = null;
    }

    void Hit(int i = 0)
    {
        if (i == 1)
        {
            if (weaponCollision)
            {
                AttackEffect();
            }
        }
    }

    void AttackEffect()
    {
        if (attackedGO != null)
        {
            if (attackedGO == player.gameObject)
            {
                player.GetComponent<Rigidbody>().AddForce(forwardVector * 100);
                player.GetComponent<PlayerStatistics>().healthCurrent =
                    player.GetComponent<PlayerStatistics>().healthCurrent - strength;
            }

            if (attackedGO == enddar.gameObject)
            {
                enddar.GetComponent<EnddarStatistics>().healthCurrent
                    = enddar.GetComponent<EnddarStatistics>().healthCurrent - strength;
            }

            if (attackedGO.transform.tag == "TreeFruits")
            {
                //Debug.Log("Attacked Fruit tree");
                Debug.Log(this.gameObject.name);
                //attackedGO.GetComponent<TreeFruits>().healthCurrent =
                //attackedGO.GetComponent<TreeFruits>().healthCurrent - strength;
            }

            if (attackedGO.transform.tag == "TreeFighting")
            {
                Debug.Log("Attacked Fighting Tree");
                //attackedGO.GetComponent<TreeFighting>().healthCurrent =
                //attackedGO.GetComponent<TreeFighting>().healthCurrent - strength;
            }

            if (attackedGO.transform.tag == "TreeDefending")
            {
                Debug.Log("Attacked Defeding Tree");
                //attackedGO.GetComponent<TreeDefending>().healthCurrent =
                //attackedGO.GetComponent<TreeDefending>().healthCurrent - strength;
            }
            
        }

    }

    void Death()
    {
        if (health <= 0)
        {
            int experience = ((healthLevel + strengthLevel + defenceLevel + speedLevel) + (level * 10));

            enemyManager.AfterDeathEvent(transform.position.x, transform.position.y, transform.position.z, experience);
            Destroy(this.gameObject);
        }
    }

    void FootL()
    {

    }

    void FootR()
    {

    }
}
