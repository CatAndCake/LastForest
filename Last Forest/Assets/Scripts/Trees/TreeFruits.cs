using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Fruit
{
    public string fruitType;
    public int fruitNumber;
    public int fruitPower;
}

public class TreeFruits : MonoBehaviour
{

    //Trees Manager
    TreesManager treesManager;

    //Canvas Tree
    CanvasTreeEnable canvasTree;

    //This Tree
    GameObject treeMesh;
    float treeSize = 1;

    public List <Fruit> fruit;

    //Tree Type
    bool setTreeType;
    public string treeName;
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



    public int fruits;
    public int fruitsMax;

    public int fruitsPower;
    public int fruitsPowerMax;

    public int fruitsWild;
    public int fruitsWildMax;

    public int currentFruits;
    float timerFruits= 95;

    float timerGrow;

    private void Awake()
    {
        treesManager = GameObject.FindGameObjectWithTag("TreesManager").GetComponent<TreesManager>();
        canvasTree = GameObject.FindGameObjectWithTag("CanvasTree").GetComponent<CanvasTreeEnable>();
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
        fruits = 0;
        fruitsMax = Random.Range(2, 9);
        fruitsPower = 0;
        fruitsPowerMax = Random.Range(2, 9);
        fruitsWild = 0;
        fruitsWildMax = Random.Range(2, 9);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        timerFruits += Time.deltaTime;
        LevelUp();
        LevelDown();
        Grow();
        Fruitage();
    }

    void TreeType()
    {
        int randomTree = Random.Range(0, 101);

        if(randomTree <= 75)
        {
            int randomTree2 = Random.Range(0, 3);
            
            if(randomTree2 == 0)
            {
                treeType = "Food Tree";
                Debug.Log("Fruit Tree");
            }
            if(randomTree2 == 1)
            {
                treeType = "Life Tree";
                Debug.Log("Health Tree");
            }
            if(randomTree2 == 2)
            {
                treeType = "Energy Tree";
                Debug.Log("Energy Tree");
            }
            if(randomTree2 < 0 || randomTree2 > 2)
            {
                Debug.Log("Error");
            }
        }

        if(randomTree > 75 && randomTree <= 98)
        {
            int randomTree2 = Random.Range(0, 2);

            if(randomTree2 == 0)
            {
                treeType = "Power Tree";
                Debug.Log("Power Tree");
            }
            if(randomTree2 == 1)
            {
                treeType = "Wisdom Tree";
                Debug.Log("Wisdom Tree");
            }
            if (randomTree2 < 0 || randomTree2 > 1)
            {
                Debug.Log("Error");
            }
        }

        if(randomTree > 98 && randomTree <= 100)
        {
            treeType = "Dream Tree";
            Debug.Log("Dream Tree");
        }

        if(randomTree < 0 || randomTree > 100)
        {
            Debug.Log("Error");
        }
    }

    void LevelUp()
    {
        if (exp >= expToUpgrade)
        {
            level = level + 1;
            healthMax = healthMax + (level * 5);
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

    void Fruitage()
    {
        if (timerFruits >= 7f)
        {
            currentFruits = currentFruits + 1;
            AddFruit();
            timerFruits = 0f;
        }
    }

    public void AddFruit()
    {
        if(fruit.Count < 16)
        {
            if (treeType == "Food Tree")
            {
                Fruit newFruit = new Fruit();
                int newFruitType = Random.Range(0, 101);

                if (newFruitType >= 0 && newFruitType < 98)
                {
                    newFruit.fruitType = "Food Fruit";
                }

                if (newFruitType >= 98 && newFruitType <= 100)
                {
                    newFruit.fruitType = "Fat Fruit";
                }

                newFruit.fruitNumber = treesManager.uniqueNumber;
                treesManager.NextNumberForFruit();
                newFruit.fruitPower = Random.Range(0, 4) + fruitsPower;
                fruit.Add(newFruit);
            }

            if (treeType == "Life Tree")
            {
                Fruit newFruit = new Fruit();
                int newFruitType = Random.Range(0, 101);

                if (newFruitType >= 0 && newFruitType < 98)
                {
                    newFruit.fruitType = "Health Fruit";
                }

                if (newFruitType >= 98 && newFruitType <= 100)
                {
                    int newStrongFruit = Random.Range(0, 3);

                    if (newStrongFruit == 0)
                    {
                        newFruit.fruitType = "Max Health Fruit";
                    }

                    if (newStrongFruit == 1)
                    {
                        newFruit.fruitType = "Recovery Fruit";
                    }

                    if (newStrongFruit == 2)
                    {
                        newFruit.fruitType = "Defence Fruit";
                    }
                }

                newFruit.fruitNumber = treesManager.uniqueNumber;
                treesManager.NextNumberForFruit();
                newFruit.fruitPower = Random.Range(0, 4) + fruitsPower;
                fruit.Add(newFruit);
            }

            if (treeType == "Energy Tree")
            {
                Fruit newFruit = new Fruit();
                int newFruitType = Random.Range(0, 101);

                if (newFruitType >= 0 && newFruitType < 98)
                {
                    newFruit.fruitType = "Energy Fruit";
                }

                if (newFruitType >= 98 && newFruitType <= 100)
                {
                    int newStrongFruit = Random.Range(0, 3);

                    if (newStrongFruit == 0)
                    {
                        newFruit.fruitType = "Max Energy Fruit";
                    }

                    if (newStrongFruit == 1)
                    {
                        newFruit.fruitType = "Breath Fruit";
                    }

                    if (newStrongFruit == 2)
                    {
                        newFruit.fruitType = "Light Fruit";
                    }
                }
                newFruit.fruitNumber = treesManager.uniqueNumber;
                treesManager.NextNumberForFruit();
                newFruit.fruitPower = Random.Range(0, 4) + fruitsPower;
                fruit.Add(newFruit);
            }

            if (treeType == "Power Tree")
            {
                Fruit newFruit = new Fruit();
                int newFruitType = Random.Range(0, 101);

                if (newFruitType >= 0 && newFruitType < 98)
                {
                    newFruit.fruitType = "Sharp Fruit";
                }

                if (newFruitType >= 98 && newFruitType <= 100)
                {
                    newFruit.fruitType = "Strength Fruit";
                }

                newFruit.fruitNumber = treesManager.uniqueNumber;
                treesManager.NextNumberForFruit();
                newFruit.fruitPower = Random.Range(0, 4) + fruitsPower;
                fruit.Add(newFruit);
            }

            if (treeType == "Wisdom Tree")
            {
                Fruit newFruit = new Fruit();
                int newFruitType = Random.Range(0, 101);

                if (newFruitType >= 0 && newFruitType <= 99)
                {
                    newFruit.fruitType = "Wisdom Fruit";
                }

                if (newFruitType == 100)
                {
                    newFruit.fruitType = "Sage Fruit";
                }

                newFruit.fruitNumber = treesManager.uniqueNumber;
                treesManager.NextNumberForFruit();
                newFruit.fruitPower = Random.Range(0, 4) + fruitsPower;
                fruit.Add(newFruit);
            }

            if (treeType == "Dream Tree")
            {
                Fruit newFruit = new Fruit();
                int fruitType = Random.Range(0, 101);

                if (fruitType >= 0 /*&& fruitType <= 96*/)
                {
                    int newFruitType = Random.Range(0, 8);

                    if (newFruitType == 0)
                    {
                        newFruit.fruitType = "Fat Fruit";
                    }
                    if (newFruitType == 1)
                    {
                        newFruit.fruitType = "Max Health Fruit";
                    }
                    if (newFruitType == 2)
                    {
                        newFruit.fruitType = "Recovery Fruit";
                    }
                    if (newFruitType == 3)
                    {
                        newFruit.fruitType = "Defence Fruit";
                    }
                    if (newFruitType == 4)
                    {
                        newFruit.fruitType = "Max Energy Fruit";
                    }
                    if (newFruitType == 5)
                    {
                        newFruit.fruitType = "Breath Fruit";
                    }
                    if (newFruitType == 6)
                    {
                        newFruit.fruitType = "Light Fruit";
                    }
                    if (newFruitType == 7)
                    {
                        newFruit.fruitType = "Strength Fruit";
                    }
                }

                if (fruitType > 96 && fruitType <= 100)
                {
                    newFruit.fruitType = "Sage Fruit";
                }

                newFruit.fruitNumber = treesManager.uniqueNumber;
                treesManager.NextNumberForFruit();
                newFruit.fruitPower = Random.Range(0, 4) + fruitsPower;
                fruit.Add(newFruit);
            }
            

            if (canvasTree.isEnabled == true)
            {
                if (treesManager.collidingTree == this.gameObject)
                {
                    if(canvasTree.fruitsOpen == true)
                    {
                        treesManager.UpdateFruits();
                        canvasTree.UnselectAllSlots();
                        CanvasTreeInfo.text = "New Fruit!";
                    }
                    Debug.Log("Updating Canvas");
                    //Write something in tree info
                }
            }

            
        }
    }

}
