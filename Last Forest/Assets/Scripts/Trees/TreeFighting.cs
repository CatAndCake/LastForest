using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeFighting : MonoBehaviour
{
    //Trees Manager
    

    //This Tree
    GameObject treeMesh;
    float treeSize = 1;

    //Tree Type
    public string treeType;

    //Stats
    public int level;

    public int points;

    public int exp;
    public int expToUpgrade;

    public int healthCurrent;
    public int healthMax;
    public int healthReg;
    public int healthRegMax;

    public int attackPower;
    public int attackPowerMax;

    public int attackQuality;
    public int attackQualityMax;

    public int attackDistance;
    public int attackDistanceMax;

    float timerGrow;

    private void Awake()
    {
        treeMesh = this.gameObject.transform.Find("TreeMesh").gameObject;

        TreeType();

        level = 1;
        points = 1;
        exp = 0;
        expToUpgrade = 20;
        healthMax = Random.Range(7, 38);
        healthCurrent = healthMax;
        healthReg = Random.Range(1, 4);
        healthRegMax = Random.Range(2, 9);
        attackPower = 1;
        attackPowerMax = Random.Range(2, 9);
        attackQuality = 1;
        attackQualityMax = Random.Range(2, 9);
        attackDistance = 1;
        attackDistance = Random.Range(2, 9);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        Grow();
        LevelUp();
        LevelDown();
    }

    void TreeType()
    {
        int randomTree = Random.Range(0, 100);
        if(randomTree >= 0 && randomTree <= 95)
        {
            int randomTree2 = Random.Range(0, 3);

            if(randomTree2 == 0 || randomTree2 == 1)
            {
                treeType = "Belt Tree";
            }
            
            if(randomTree2 == 2)
            {
                treeType = "Cloud Tree";
            }
        }
        if(randomTree > 95 && randomTree <= 100)
        {
            treeType = "Spell Tree";
        }
    }

    void LevelUp()
    {
        if (exp >= expToUpgrade)
        {
            level = level + 1;
            healthMax = healthMax + 10;
            points = points + 2;
            exp = exp - expToUpgrade;
            expToUpgrade = expToUpgrade * 2;
        }
    }

    void LevelDown()
    {
        if (level > 1)
        {
            if (healthCurrent < 0)
            {
                level = level - 1;
                exp = 0;
                healthCurrent = healthMax;
            }
        }
    }

    void Grow()
    {
        if (level <= 3)
        {
            if (treeSize <= level)
            {
                if (timerGrow > 0.1)
                {
                    treeSize = treeSize + 0.001f;
                    timerGrow = 0f;
                }
            }
        }

        if (level > 3 && level <= 7)
        {
            if (treeSize <= level * 2f)
            {
                if (timerGrow > 0.1)
                {
                    treeSize = treeSize + 0.003f;
                    timerGrow = 0f;
                }
            }
        }

        if (level > 7)
        {
            if (treeSize <= level * 4f)
            {
                if (timerGrow > 0.1)
                {
                    treeSize = treeSize + 0.005f;
                    timerGrow = 0f;
                }
            }
        }

        treeMesh.transform.localScale = new Vector3(treeSize, treeSize, treeSize);
    }

}
