using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatistics : MonoBehaviour
{
    public int level;

    public int experiance;
    public int experianceToUpgrade;

    public int healthMax;
    public int healthCurrent;
    public int healthRegen = 5;

    public int hungerMax;
    public int hungerCurrent;

    public int staminaMax;
    public int staminaCurrent;
    public int staminaRegen;

    public int strength = 100;
    public int defence; 
    public int speed;
    public int knowledge;

    //Creates a chance that a certain tree will learn a song
    public int treesPrayer;
    //Gives you the ability to choose what type of tree will grow
    //If you know the Ent song then maybe your tree will wake up and become an ent
    public int treesHerd;
    //Gives you a chance to summon a tree
    public int treesSummoner;

    //Timer
    float timer;

    private void Awake()
    {
        level = 1;
        healthMax = 100;
        healthCurrent = healthMax;
        staminaMax = 100;
        staminaCurrent = 100;
        hungerMax = 100;
        hungerCurrent = hungerMax;

        treesPrayer = 1;
    }

    private void Update()
    {
        /*timer += Time.deltaTime;

        // Health
        if(currentHealth < maxHealth)
        {
            if(timer >= regenHealth)
            {
                currentHealth = currentHealth + 1;
                timer = 0f;
            }
        }

        if(currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }*/
    }

}
