using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerFruits
{
    public string fruitType;
    public int fruitNumber;
    public int fruitPower;
}

[System.Serializable]
public class PlayerGems
{
    public string gemType;
    public Sprite gemImage;
    public int gemNumber;
    public int gemPower;
}

[System.Serializable]
public class PlayerSwords
{
    public string swordType;
    public int swordNumber;
    public int swordEfficiency;
    public int swordCurrentEfficiency;
    public int swordSharpness;
    public int swordWeight;
    public int swordWidth;
    public int savedSouls;
    public bool holding;
    
}

public class PlayerManager : MonoBehaviour
{
    //Canvas
    CanvasTreeEnable canvasTree;
    CanvasPlayer canvasPlayer;

    //Skills in canvas skills
    public GameObject skill;
    public GameObject skillButton;

    //Player scripts
    PlayerStatistics playerStatistics;

    //List
    public List<PlayerFruits> playerFruits;
    public List<PlayerGems> playerGems;
    public List<PlayerSwords> playerSwords;

    int gemsNumbers = 0;

    //Find localisation for tree to grow (WE WILL DELETE)

    CanvasInfoText canvasInfoText;

    private void Awake()
    {
        playerStatistics = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatistics>();
        canvasTree = GameObject.FindGameObjectWithTag("CanvasTree").GetComponent<CanvasTreeEnable>();
        canvasPlayer = GameObject.FindGameObjectWithTag("CanvasPlayer").GetComponent<CanvasPlayer>();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.T))
        {
            
        }
    }

    

    void SetInfo()
    {
        canvasInfoText = GameObject.FindGameObjectWithTag("CanvasInformations").GetComponent<CanvasInfoText>();

        string dupa;
        dupa = "Dupa";

        float tim;
        tim = 5f;

        //canvasInfoText.SetText(dupa, tim);
    }


    public void AddSword(int number, int efficiency, int currentEfficiency, int sharpness, int weight, int width)
    {
        PlayerSwords newSword = new PlayerSwords();
        newSword.swordNumber = number;
        newSword.swordEfficiency = efficiency;
        newSword.swordCurrentEfficiency = currentEfficiency;
        newSword.swordSharpness = sharpness;
        newSword.swordWeight = weight;
        newSword.swordWidth = width;

        playerSwords.Add(newSword);
    }

    public void AddFruit(string type, int number, int power)
    {
        PlayerFruits newFruit = new PlayerFruits();
        newFruit.fruitType = type;
        newFruit.fruitNumber = number;
        newFruit.fruitPower = power;

        playerFruits.Add(newFruit);
    }

    public void AddGem(string type, Sprite image, int power)
    {
        PlayerGems newGem = new PlayerGems();
        newGem.gemType = type;
        newGem.gemImage = image;
        newGem.gemNumber = gemsNumbers;
        newGem.gemPower = power;

        playerGems.Add(newGem);
        gemsNumbers = gemsNumbers + 1;
    }

    public void UpdateSkillsInPlayerCancas()
    {
        GameObject[] playerSkills = GameObject.FindGameObjectsWithTag("CanvasPlayerSkill");

        foreach (GameObject playerSkill in playerSkills)
        {
            Destroy(playerSkill);
        }

        Transform skillsHolder = GameObject.FindGameObjectWithTag("CanvasPlayerSkillsHolder").transform;

        GameObject playerLevel = Instantiate(skill, new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z),
            Quaternion.identity);

        playerLevel.GetComponent<PlayerSkill>().text = "Level: " + playerStatistics.level;
        playerLevel.transform.SetParent(skillsHolder);

        GameObject playerExp = Instantiate(skill, new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z),
            Quaternion.identity);

        playerExp.GetComponent<PlayerSkill>().text = "Exp: " + playerStatistics.experiance +
            " / " + playerStatistics.experianceToUpgrade;
        playerExp.transform.SetParent(skillsHolder);

        GameObject playerHealthMax = Instantiate(skillButton, new Vector3(
           transform.position.x,
           transform.position.y,
           transform.position.z),
           Quaternion.identity);

        playerHealthMax.GetComponent<PlayerSkillButton>().text = "Health: " + playerStatistics.healthMax;
        playerHealthMax.transform.SetParent(skillsHolder);

        GameObject playerHealthReg = Instantiate(skillButton, new Vector3(
           transform.position.x,
           transform.position.y,
           transform.position.z),
           Quaternion.identity);

        playerHealthReg.GetComponent<PlayerSkillButton>().text = "Recovery: " + playerStatistics.healthRegen;
        playerHealthReg.transform.SetParent(skillsHolder);

        GameObject playerStamina = Instantiate(skillButton, new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z),
            Quaternion.identity);

        playerStamina.GetComponent<PlayerSkillButton>().text = "Stamina: " + playerStatistics.staminaMax;
        playerStamina.transform.SetParent(skillsHolder);

        GameObject playerBreath = Instantiate(skillButton, new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z),
            Quaternion.identity);

        playerBreath.GetComponent<PlayerSkillButton>().text = "Breath: " + playerStatistics.staminaRegen;
        playerBreath.transform.SetParent(skillsHolder);

        GameObject playerHunger = Instantiate(skillButton, new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z),
            Quaternion.identity);

        playerHunger.GetComponent<PlayerSkillButton>().text = "Hunger: " + playerStatistics.hungerMax;
        playerHunger.transform.SetParent(skillsHolder);

        GameObject playerStrength = Instantiate(skillButton, new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z),
            Quaternion.identity);

        playerStrength.GetComponent<PlayerSkillButton>().text = "Strength: " + playerStatistics.strength;
        playerStrength.transform.SetParent(skillsHolder);

        GameObject playerDefence = Instantiate(skillButton, new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z),
            Quaternion.identity);

        playerDefence.GetComponent<PlayerSkillButton>().text = "Defence: " + playerStatistics.defence;
        playerDefence.transform.SetParent(skillsHolder);

        GameObject playerSpeed = Instantiate(skillButton, new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z),
            Quaternion.identity);

        playerSpeed.GetComponent<PlayerSkillButton>().text = "Speed: " + playerStatistics.speed;
        playerSpeed.transform.SetParent(skillsHolder);

        GameObject playerKnowledge = Instantiate(skillButton, new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z),
            Quaternion.identity);

        playerKnowledge.GetComponent<PlayerSkillButton>().text = "Knowledge: " + playerStatistics.knowledge;
        playerKnowledge.transform.SetParent(skillsHolder);

        GameObject playerTreesPrayer = Instantiate(skillButton, new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z),
            Quaternion.identity);

        playerTreesPrayer.GetComponent<PlayerSkillButton>().text = "Prayer: " + playerStatistics.treesPrayer;
        playerTreesPrayer.transform.SetParent(skillsHolder);

        GameObject playerTreesherd = Instantiate(skillButton, new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z),
            Quaternion.identity);

        playerTreesherd.GetComponent<PlayerSkillButton>().text = "Treesherd: " + playerStatistics.treesHerd;
        playerTreesherd.transform.SetParent(skillsHolder);

        GameObject playerTreesSummoner = Instantiate(skillButton, new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z),
            Quaternion.identity);

        playerTreesSummoner.GetComponent<PlayerSkillButton>().text = "Summoner: " + playerStatistics.treesSummoner;
        playerTreesSummoner.transform.SetParent(skillsHolder);

    }

    public void UpdateSwordsInCanvas()
    {
        GameObject[] swordSlots = GameObject.FindGameObjectsWithTag("CanvasSword");

        for (int i = 0; i < playerSwords.Count; i++)
        {
            swordSlots[i].GetComponent<CanvasSword>().number = playerSwords[i].swordNumber;
            swordSlots[i].GetComponent<CanvasSword>().efficiency = playerSwords[i].swordEfficiency;
            swordSlots[i].GetComponent<CanvasSword>().currentEfficiency = playerSwords[i].swordCurrentEfficiency;
            swordSlots[i].GetComponent<CanvasSword>().sharpness = playerSwords[i].swordSharpness;
            swordSlots[i].GetComponent<CanvasSword>().weight = playerSwords[i].swordWeight;
            swordSlots[i].GetComponent<CanvasSword>().width = playerSwords[i].swordWidth;
            swordSlots[i].GetComponent<CanvasSword>().savedSouls = playerSwords[i].savedSouls;
            swordSlots[i].GetComponent<CanvasSword>().holding = playerSwords[i].holding;
            swordSlots[i].GetComponent<CanvasSword>().ShowImage();
            swordSlots[i].GetComponent<CanvasSword>().ShowSavedSouls();
            swordSlots[i].GetComponent<CanvasSword>().ShowEfficiency();
        }

        for (int i = playerSwords.Count; i < swordSlots.Length; i++)
        {
            swordSlots[i].GetComponent<CanvasSword>().number = 0;
            swordSlots[i].GetComponent<CanvasSword>().efficiency = 0;
            swordSlots[i].GetComponent<CanvasSword>().sharpness = 0;
            swordSlots[i].GetComponent<CanvasSword>().weight = 0;
            swordSlots[i].GetComponent<CanvasSword>().width = 0;
            swordSlots[i].GetComponent<CanvasSword>().savedSouls = 0;
            swordSlots[i].GetComponent<CanvasSword>().holding = false;
            swordSlots[i].GetComponent<CanvasSword>().NoImage();
            swordSlots[i].GetComponent<CanvasSword>().NoSavedSouls();
            swordSlots[i].GetComponent<CanvasSword>().NoEfficiency();
        }

        GameObject[] sharpFruits = GameObject.FindGameObjectsWithTag("CanvasSharpFruit");
        int sharpFruitNumber = -1;

        for (int i = 0; i < playerFruits.Count; i++)
        {
            if (playerFruits[i].fruitType == "Sharp Fruit")
            {
                

                sharpFruitNumber = sharpFruitNumber + 1;
                sharpFruits[sharpFruitNumber].GetComponent<CanvasSharpFruit>().fruitType = playerFruits[i].fruitType;
                sharpFruits[sharpFruitNumber].GetComponent<CanvasSharpFruit>().fruitNumber = playerFruits[i].fruitNumber;
                sharpFruits[sharpFruitNumber].GetComponent<CanvasSharpFruit>().fruitPower = playerFruits[i].fruitPower;
                sharpFruits[sharpFruitNumber].GetComponent<CanvasSharpFruit>().ShowImage();
                sharpFruits[sharpFruitNumber].GetComponent<CanvasSharpFruit>().UnselectThisSlot();
            }
        }

        for (int i = sharpFruitNumber + 1; i < sharpFruits.Length; i++)
        {
            sharpFruits[i].GetComponent<CanvasSharpFruit>().fruitType = null;
            sharpFruits[i].GetComponent<CanvasSharpFruit>().fruitNumber = 0;
            sharpFruits[i].GetComponent<CanvasSharpFruit>().fruitPower = 0;
            sharpFruits[i].GetComponent<CanvasSharpFruit>().NoImage();
        }


    }

    public void AddPointsFromFruitToSword(int fruitPower, int swordNumer)
    {
        for(int i = 0; i < playerSwords.Count; i++)
        {
            if(playerSwords[i].swordNumber == swordNumer)
            {
                playerSwords[i].swordCurrentEfficiency = playerSwords[i].swordCurrentEfficiency + fruitPower;
            }
        }
    }

        public void HoldSword(int swordNumber)
        {
            for (int i = 0; i < playerSwords.Count; i++)
            {
                playerSwords[i].holding = false;
                if (playerSwords[i].swordNumber == swordNumber)
                {
                    playerSwords[i].holding = true;
                }
            }

            UpdateSwordsInCanvas();
        }

        public void RemoveSword(int swordNumber)
        {
            for (int i = 0; i < playerSwords.Count; i++)
            {
                if (playerSwords[i].swordNumber == swordNumber)
                {
                    playerSwords.Remove(playerSwords[i]);
                }
            }

            UpdateSwordsInCanvas();
        }

        public void UpdateFruitsInPlayerCanvas()
        {
            GameObject[] fruitSlots = GameObject.FindGameObjectsWithTag("CanvasTreeFruitSlot");

            for (int i = 0; i < playerFruits.Count; i++)
            {
                fruitSlots[i].GetComponent<CanvasTreeFruit>().fruitType = playerFruits[i].fruitType;
                fruitSlots[i].GetComponent<CanvasTreeFruit>().fruitNumber = playerFruits[i].fruitNumber;
                fruitSlots[i].GetComponent<CanvasTreeFruit>().fruitPower = playerFruits[i].fruitPower;
                fruitSlots[i].GetComponent<CanvasTreeFruit>().ShowImage();
            }

            for (int i = playerFruits.Count; i < fruitSlots.Length; i++)
            {
                fruitSlots[i].GetComponent<CanvasTreeFruit>().fruitType = null;
                fruitSlots[i].GetComponent<CanvasTreeFruit>().fruitNumber = 0;
                fruitSlots[i].GetComponent<CanvasTreeFruit>().fruitPower = 0;
                fruitSlots[i].GetComponent<CanvasTreeFruit>().NoImage();
            }
        }

        public void EatFruit(string fruitType, int fruitPower, int fruitNumber)
        {
            if (fruitType == "Food Fruit")
            {
                if (playerStatistics.hungerCurrent < playerStatistics.hungerMax)
                {
                    playerStatistics.hungerCurrent =
                        playerStatistics.hungerCurrent + (fruitPower * playerStatistics.level * 5);

                    if (playerStatistics.hungerCurrent > playerStatistics.hungerMax)
                    {
                        playerStatistics.hungerCurrent = playerStatistics.hungerMax;
                    }


                }

                if (playerStatistics.hungerCurrent >= playerStatistics.hungerMax)
                {
                    Debug.Log("Player is full");
                }
            }

            if (fruitType == "Fat Fruit")
            {
                playerStatistics.hungerMax = playerStatistics.hungerMax + (fruitPower * 5);


            }

            if (fruitType == "Wisdom Fruit")
            {
                playerStatistics.experiance = playerStatistics.experiance + (fruitPower * playerStatistics.level * 10);


            }

            if (fruitType == "Sage Fruit")
            {
                playerStatistics.knowledge = playerStatistics.knowledge + fruitPower;
            }

            if (fruitType == "Sharp Fruit")
            {
                Debug.Log("We will create it later");


            }

            if (fruitType == "Strength Fruit")
            {
                playerStatistics.strength = playerStatistics.strength + fruitPower;


            }

            if (fruitType == "Health Fruit")
            {
                if (playerStatistics.healthCurrent >= playerStatistics.healthMax)
                {
                    Debug.Log("Player is healthly, there is no need to heal him");
                }

                if (playerStatistics.healthCurrent < playerStatistics.healthMax)
                {
                    playerStatistics.healthCurrent = playerStatistics.healthCurrent + fruitPower;

                    if (playerStatistics.healthCurrent > playerStatistics.healthMax)
                    {
                        playerStatistics.healthCurrent = playerStatistics.healthMax;
                    }


                }
            }

            if (fruitType == "Max Health Fruit")
            {
                playerStatistics.healthMax = playerStatistics.healthMax + (fruitPower * 5);


            }

            if (fruitType == "Recovery Fruit")
            {
                playerStatistics.healthRegen = playerStatistics.healthRegen + fruitPower;


            }

            if (fruitType == "Defence Fruit")
            {
                playerStatistics.defence = playerStatistics.defence + fruitPower;


            }

            if (fruitType == "Energy Fruit")
            {
                if (playerStatistics.staminaCurrent >= playerStatistics.staminaMax)
                {
                    Debug.Log("You're fully rested");
                }

                if (playerStatistics.staminaCurrent < playerStatistics.staminaMax)
                {
                    playerStatistics.staminaCurrent = playerStatistics.staminaCurrent + (fruitPower * playerStatistics.level * 5);

                    if (playerStatistics.staminaCurrent > playerStatistics.staminaMax)
                    {
                        playerStatistics.staminaCurrent = playerStatistics.staminaMax;
                    }


                }
            }

            if (fruitType == "Max Energy Fruit")
            {
                playerStatistics.staminaMax = playerStatistics.staminaMax + (fruitPower * 5);


            }

            if (fruitType == "Breath Fruit")
            {
                playerStatistics.staminaRegen = playerStatistics.staminaRegen + fruitPower;
            }

            if (fruitType == "Light Fruit")
            {
                playerStatistics.speed = playerStatistics.speed + fruitPower;
            }

            RemoveFruit(fruitNumber);
        }

        public void RemoveFruit(int fruitNumber)
        {
            for (int i = 0; i < playerFruits.Count; i++)
            {
                if (playerFruits[i].fruitNumber == fruitNumber)
                {
                    playerFruits.Remove(playerFruits[i]);
                }
            }
        }

        public void UpdateGemsInCanvas()
        {
            GameObject[] gemSlots = GameObject.FindGameObjectsWithTag("CanvasGem");

            for (int i = 0; i < playerGems.Count; i++)
            {
                gemSlots[i].GetComponent<CanvasGem>().gemType = playerGems[i].gemType;
                gemSlots[i].GetComponent<CanvasGem>().GemImage();
                gemSlots[i].GetComponent<CanvasGem>().gemNumber = playerGems[i].gemNumber;
                gemSlots[i].GetComponent<CanvasGem>().gemPower = playerGems[i].gemPower;
            }

            for (int i = playerGems.Count; i < gemSlots.Length; i++)
            {
                gemSlots[i].GetComponent<CanvasGem>().gemType = null;
                gemSlots[i].GetComponent<CanvasGem>().NoImage();
                gemSlots[i].GetComponent<CanvasGem>().gemNumber = -1;
                gemSlots[i].GetComponent<CanvasGem>().gemPower = 0;
            }
        }

        public void RemoveGem(int gemNumber)
        {

            for (int i = 0; i < playerGems.Count; i++)
            {

                if (playerGems[i].gemNumber == gemNumber)
                {

                    playerGems.Remove(playerGems[i]);

                }
            }

            UpdateGemsInCanvas();

        }
    }

