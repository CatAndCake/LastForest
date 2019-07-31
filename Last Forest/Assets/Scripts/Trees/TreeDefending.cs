﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeDefending : MonoBehaviour
{
    //Trees Manager
    TreesManager treesManager;

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

    public int hydration;
    public int hydrationMax;

    public int lightBranches;
    public int lightBranchesMax;

    public int attraction;
    public int attractionMax;

    bool changeTreeType = true;

    float timerGrow;

    public GameObject delete;

    private void Awake()
    {
        treesManager = GameObject.FindGameObjectWithTag("TreesManager").GetComponent<TreesManager>();
        treeMesh = this.gameObject.transform.Find("TreeMesh").gameObject;

        TreeType();

        level = 1;
        points = 1;
        exp = 0;
        expToUpgrade = 20;
        healthMax = Random.Range(31, 151);
        healthCurrent = healthMax;
        healthReg = Random.Range(2, 7);
    }

    private void Start()
    {

    }

    private void Update()
    {
        timerGrow += Time.deltaTime;

        Grow();
        LevelUp();
        LevelDown();
        
    }

    void TreeType()
    {
        int randomTree = Random.Range(0, 100);
        if (randomTree >= 0 && randomTree <= 95)
        {
            treeType = "Thick Bark Tree";
        }

        // recovery tree

        if (randomTree > 95 && randomTree <= 100)
        {
            treeType = "Weed Tree";
        }
    }

    void LevelUp()
    {
        if (exp >= expToUpgrade)
        {
            if(level <= 5)
            {
                level = level + 1;
                healthMax = healthMax + 10;
                points = points + 2;
                exp = exp - expToUpgrade;
                expToUpgrade = expToUpgrade * 2;
            }

            if (level > 5)
            {
                if (treeType == "Thick Bark Tree")
                {
                    level = level + 1;
                    healthMax = healthMax + 30;
                    points = points + 2;
                    exp = exp - expToUpgrade;
                    expToUpgrade = expToUpgrade * 2;
                }

                if(treeType == "Weed Tree")
                {
                    level = level + 1;
                    healthMax = healthMax + 10;
                    points = points + 2;
                    exp = exp - expToUpgrade;
                    expToUpgrade = expToUpgrade * 2;
                }
            }
        }

        if(level == 6)
        {
            changeTreeType = false;
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
