using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    //GameObjects
    public GameObject enemy;

    
    //bools
    bool battle;
    public bool showBattle;
    bool setEnemiesNumber = true;

    //floats
    public static float timerBattle;
    public float showTimer;
    float timerBetweenEnemies;
    public float timeBetweenEnemies = 0;

    
    public int enemiesNumber;
    public int aliveEnemies;
    public int round = 1;

    //transforms
    public Transform[] attackLocations;

    // New More Clear

    
    private void Awake()
    {
        timerBattle = 300f;
    }

    

    private void Update()
    {
        

        timerBattle -= Time.deltaTime;
        showTimer = timerBattle;
        showBattle = battle;


        
        timerBetweenEnemies += Time.deltaTime;
        aliveEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

        //Can I start new Battle?
        if(timerBattle <= 0)
        {
            battle = true;
            ControlManager.battle = true;
            PlayerInteractable.canvasEnable = false;
        }

        if(battle == true)
        {
            //Set the number of enemies
            if(setEnemiesNumber == true)
            {
                enemiesNumber = Random.Range(2, 13);
                setEnemiesNumber = false;
            }

            //All enemies Are dead?
            if (aliveEnemies == 0 && enemiesNumber == 0)
            {
                battle = false;
                timerBattle = 300f;
                ControlManager.battle = false;
                setEnemiesNumber = true;
                round = round + 1;
            }

            //Spawn Enemy
            if (timeBetweenEnemies <= timerBetweenEnemies)
            {
                if(enemiesNumber > 0)
                {
                    SpawnEnemy();
                    enemiesNumber = enemiesNumber - 1;
                    timerBetweenEnemies = 0;
                    timeBetweenEnemies = Random.Range(2, 23);
                }
            }
            
        }
    }

    void SpawnEnemy()
    {
        int i = Random.Range(0, attackLocations.Length);
        
        Instantiate(enemy, new Vector3(
        attackLocations[i].position.x,
        attackLocations[i].position.y,
        attackLocations[i].position.z),
        Quaternion.identity);
    }


}
