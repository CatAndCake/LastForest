using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnddarStatistics : MonoBehaviour
{
    //Game Objects
    GameObject[] enemies;


    //Max Statistics
    public float level;

    public float roots;
    public float experience;
    public float expToUpgrade;

    //Health
    public float healthMax;
    public float healthCurrent;
    public float healthRegeneration;

    //Magic 

    //Timer
    float timer;

    private void Awake()
    {
        level = 1;
        roots= 7;
        experience = 0;
        healthMax = 100f;
        healthCurrent = healthMax;
        healthRegeneration = 7f;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        HealthRegeneration();
        //AddExperienve();
        //IncreaseLevel();
        FallingLeaf();
    }

    void HealthRegeneration()
    {
        if(healthCurrent < healthMax)
        {
            if(timer >= healthRegeneration)
            {
                healthCurrent = healthCurrent + 1;
                timer = 0f;
            }
        }
    }

    public void AddExperienve()
    {

    }

    void IncreaseLevel()
    {

    }

    void FallingLeaf()
    {
        if(level == 1)
        {
            if (roots >= 0)
            {
                if (healthCurrent <= 0)
                {
                    foreach (GameObject enemy in enemies)
                    {
                        enemy.GetComponent<EnemyBattle>().health = 0f;
                        roots = roots - 1;
                        healthCurrent = healthMax;
                    }
                }
            }
        }

        if(level > 1)
        {
            if (healthCurrent <= 0)
            {
                foreach (GameObject enemy in enemies)
                {
                    enemy.GetComponent<EnemyBattle>().health = 0f;
                    level = level - 1;
                    healthCurrent = healthMax;
                }
            }
        }
        
    }
}
