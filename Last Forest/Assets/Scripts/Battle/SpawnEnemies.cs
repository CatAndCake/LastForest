using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject Enemy;
    float timer = 15;

    
    void Update()
    {
        timer += Time.deltaTime;


        if(timer >= 20)
        {
            //SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        Instantiate(Enemy, new Vector3(
        transform.position.x,
        transform.position.y - 1,
        transform.position.z),
        Quaternion.identity);
    }
}
