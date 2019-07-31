using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreesManager : MonoBehaviour
{

    //Player
    PlayerManager playerManager;
    PlayerStatistics playerStatistics;
    PlayerInteractable playerInteractable;

    //Tree
    public GameObject collidingTree;

    //Tree GameOjbects
    public GameObject treeFruitGO;
    public GameObject treeFightingGO;
    public GameObject treeDefendingGO;

    //Canvas
    CanvasTreeEnable canvasTree;

    //Skills
    public GameObject skill;
    public GameObject skillButton;

    //Button
    public GameObject fruitsButton;

    //Fruits numbers
    public int uniqueNumber = 1;

    //Fruits Images
    public Sprite fruitImage;
    public Sprite noImage;
    

    //Pray
    public float prayTime;

    Transform holder;


    private void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();
        playerStatistics = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatistics>();
        playerInteractable = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteractable>();
        canvasTree = GameObject.FindGameObjectWithTag("CanvasTree").GetComponent<CanvasTreeEnable>();
        holder = canvasTree.transform.Find("Enable").transform.Find("PanelRight").transform.Find("Skills").
                transform.Find("Mask").transform.Find("Scroll").transform.Find("Holder").transform;
    }

    private void Update()
    {
        PrayTimer();
    }

    public void NextNumberForFruit()
    {
        uniqueNumber = uniqueNumber + 1;
    }

    public void PrayTimer()
    {
        GameObject prayText = canvasTree.pray.transform.Find("Mask").transform.Find("Order").
            transform.Find("Timer").transform.Find("Text").gameObject;

        

        float prayTime = canvasTree.pray.transform.Find("Mask").transform.Find("Order").
            transform.Find("Timer").transform.Find("Slider").GetComponent<Slider>().value;

        

        prayText.GetComponent<Text>().text = prayTime.ToString("#") + " s";
    }

    public void Pray()
    {
        float prayTime = canvasTree.pray.transform.Find("Mask").transform.Find("Order").
            transform.Find("Timer").transform.Find("Slider").GetComponent<Slider>().value;

        Slider praySlider = canvasTree.pray.transform.Find("Mask").transform.Find("Order").
            transform.Find("Timer").transform.Find("Slider").GetComponent<Slider>();

        int fulfilledPray = Random.Range(1, 100);

        if(BattleManager.timerBattle - prayTime >= 0)
        {
            //Debug.Log(prayTime);
            //Debug.Log(fulfilledPray);


            if (fulfilledPray > prayTime)
            {
                CanvasTreeInfo.text = "No pray";
            }

            if (fulfilledPray <= prayTime)
            {
                Debug.Log("We pray");
                if (collidingTree.transform.tag == "TreeFruits")
                {
                    if (collidingTree.GetComponent<TreeFruits>().healthCurrent <
                        collidingTree.GetComponent<TreeFruits>().healthMax)
                    {
                        collidingTree.GetComponent<TreeFruits>().healthCurrent =
                        collidingTree.GetComponent<TreeFruits>().healthCurrent + (playerStatistics.treesPrayer * 10);
                    }

                    CanvasTreeInfo.text = "pray, fruit tree";
                    BattleManager.timerBattle = BattleManager.timerBattle - prayTime;
                }

                if (collidingTree.transform.tag == "TreeFighting")
                {
                    if (collidingTree.GetComponent<TreeFighting>().healthCurrent <
                        collidingTree.GetComponent<TreeFighting>().healthMax)
                    {
                        collidingTree.GetComponent<TreeFighting>().healthCurrent =
                        collidingTree.GetComponent<TreeFighting>().healthCurrent + (playerStatistics.treesPrayer * 10);
                    }

                    CanvasTreeInfo.text = "pray, fighting tree";
                    BattleManager.timerBattle = BattleManager.timerBattle - prayTime;
                    
                }

                if (collidingTree.transform.tag == "TreeDefending")
                {
                    if (collidingTree.GetComponent<TreeDefending>().healthCurrent <
                        collidingTree.GetComponent<TreeDefending>().healthMax)
                    {
                        collidingTree.GetComponent<TreeDefending>().healthCurrent =
                        collidingTree.GetComponent<TreeDefending>().healthCurrent + (playerStatistics.treesPrayer * 12);
                    }

                    CanvasTreeInfo.text = "pray, defending tree";
                    BattleManager.timerBattle = BattleManager.timerBattle - prayTime;
                }

                praySlider.value = 1;

            }
        }

        if (BattleManager.timerBattle - prayTime < 0)
        {
            //Write something in the info panel
            // like there is not that much
            // time before battle 
        }

        

    }

    public void UpdateTreeSkills()
    {
        Debug.Log("Dupa");

        if (collidingTree.tag == "TreeFruits")
        {
            
            //canvasTree.SwitchSkills();
            
            DeleteTreeSkills();

            if(canvasTree.fruitsOpen == false)
            {
                canvasTree.swFruits.SetActive(true);
            }

            //Spawn level info
            GameObject levelInfo = Instantiate(skill, new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z),
            Quaternion.identity);

            levelInfo.transform.SetParent(holder);
            levelInfo.GetComponent<CanvasTreeSkill>().text = "Level " + collidingTree.GetComponent<TreeFruits>().level;

            //Spawn points info

            GameObject pointsInfo = Instantiate(skill, new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z),
            Quaternion.identity);

            pointsInfo.transform.SetParent(holder);
            pointsInfo.GetComponent<CanvasTreeSkill>().text = "Points " + collidingTree.GetComponent<TreeFruits>().points;

            //Spawn Exp Info

            GameObject expInfo = Instantiate(skill, new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z),
            Quaternion.identity);

            expInfo.transform.SetParent(holder);
            expInfo.GetComponent<CanvasTreeSkill>().text = "Experience " + collidingTree.GetComponent<TreeFruits>().exp + " / " + collidingTree.GetComponent<TreeFruits>().expToUpgrade;

            //Spawn Max Health Info

            GameObject maxHealthInfo = Instantiate(skillButton, new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z),
            Quaternion.identity);

            maxHealthInfo.transform.SetParent(holder);
            maxHealthInfo.GetComponent<CanvasTreeSkillButton>().text = "Health " + collidingTree.GetComponent<TreeFruits>().healthMax;
            maxHealthInfo.GetComponent<CanvasTreeSkillButton>().tree = collidingTree.gameObject;
            maxHealthInfo.GetComponent<CanvasTreeSkillButton>().skill = ("Health Max");

            //Spawn Regen Health Info

            GameObject regenHealthInfo = Instantiate(skillButton, new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z),
            Quaternion.identity);

            regenHealthInfo.transform.SetParent(holder);
            regenHealthInfo.GetComponent<CanvasTreeSkillButton>().text = "Recovery " + collidingTree.GetComponent<TreeFruits>().healthReg;
            regenHealthInfo.GetComponent<CanvasTreeSkillButton>().tree = collidingTree.gameObject;
            regenHealthInfo.GetComponent<CanvasTreeSkillButton>().skill = "Recovery";

            //Spawn Fruits Quantity info

            GameObject fruitsQuantityInfo = Instantiate(skillButton, new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z),
            Quaternion.identity);

            fruitsQuantityInfo.transform.SetParent(holder);
            fruitsQuantityInfo.GetComponent<CanvasTreeSkillButton>().text = "Fruits " + collidingTree.GetComponent<TreeFruits>().fruits;
            fruitsQuantityInfo.GetComponent<CanvasTreeSkillButton>().tree = collidingTree.gameObject;
            fruitsQuantityInfo.GetComponent<CanvasTreeSkillButton>().skill = "Fruits";

            //Spawn Fruits Power Info

            GameObject fruitsPowerInfo = Instantiate(skillButton, new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z),
            Quaternion.identity);

            fruitsPowerInfo.transform.SetParent(holder);
            fruitsPowerInfo.GetComponent<CanvasTreeSkillButton>().text = "Fruits Power " + collidingTree.GetComponent<TreeFruits>().fruitsPower;
            fruitsPowerInfo.GetComponent<CanvasTreeSkillButton>().tree = collidingTree.gameObject;
            fruitsPowerInfo.GetComponent<CanvasTreeSkillButton>().skill = "Fruits Power";

            // Spawn Fruits God Info

            GameObject fruitsGodInfo = Instantiate(skillButton, new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z),
            Quaternion.identity);

            fruitsGodInfo.transform.SetParent(holder);
            fruitsGodInfo.GetComponent<CanvasTreeSkillButton>().text = "Wild Fruits " + collidingTree.GetComponent<TreeFruits>().fruitsWild;
            fruitsGodInfo.GetComponent<CanvasTreeSkillButton>().tree = collidingTree.gameObject;
            fruitsGodInfo.GetComponent<CanvasTreeSkillButton>().skill = "Wild Fruits";

            //Spawn Attack Possibility Info

            GameObject attackPossibilityInfo = Instantiate(skillButton, new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z),
            Quaternion.identity);

            attackPossibilityInfo.transform.SetParent(holder);
            attackPossibilityInfo.GetComponent<CanvasTreeSkillButton>().text = "Attack";
        }

        if(collidingTree.tag == "TreeFighting")
        {

            DeleteTreeSkills();
            canvasTree.swFruits.SetActive(false);
            

            //Spawn level info
            GameObject levelInfo = Instantiate(skill, new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z),
            Quaternion.identity);

            levelInfo.transform.SetParent(holder);

            levelInfo.GetComponent<CanvasTreeSkill>().text = "Level " + collidingTree.GetComponent<TreeFighting>().level;

            //Spawn points info

            GameObject pointsInfo = Instantiate(skill, new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z),
            Quaternion.identity);

            pointsInfo.transform.SetParent(holder);
            pointsInfo.GetComponent<CanvasTreeSkill>().text = "Points " + collidingTree.GetComponent<TreeFighting>().points;

            //Spawn Exp Info

            GameObject expInfo = Instantiate(skill, new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z),
            Quaternion.identity);

            expInfo.transform.SetParent(holder);
            expInfo.GetComponent<CanvasTreeSkill>().text = "Experience " + collidingTree.GetComponent<TreeFighting>().exp + " / " + collidingTree.GetComponent<TreeFighting>().expToUpgrade;

            //Spawn Max Health Info

            GameObject maxHealthInfo = Instantiate(skillButton, new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z),
            Quaternion.identity);

            maxHealthInfo.transform.SetParent(holder);
            maxHealthInfo.GetComponent<CanvasTreeSkillButton>().text = "Health " + collidingTree.GetComponent<TreeFighting>().healthMax;
            maxHealthInfo.GetComponent<CanvasTreeSkillButton>().tree = collidingTree.gameObject;
            maxHealthInfo.GetComponent<CanvasTreeSkillButton>().skill = ("Health Max");

            //Spawn Regen Health Info

            GameObject regenHealthInfo = Instantiate(skillButton, new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z),
            Quaternion.identity);

            regenHealthInfo.transform.SetParent(holder);
            regenHealthInfo.GetComponent<CanvasTreeSkillButton>().text = "Recovery " + collidingTree.GetComponent<TreeFighting>().healthReg;
            regenHealthInfo.GetComponent<CanvasTreeSkillButton>().tree = collidingTree.gameObject;
            regenHealthInfo.GetComponent<CanvasTreeSkillButton>().skill = "Recovery";

            GameObject attackInfo = Instantiate(skillButton, new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z),
            Quaternion.identity);

            attackInfo.transform.SetParent(holder);
            attackInfo.GetComponent<CanvasTreeSkillButton>().text = "Attack " + collidingTree.GetComponent<TreeFighting>().attackPower;
            attackInfo.GetComponent<CanvasTreeSkillButton>().tree = collidingTree.gameObject;
            attackInfo.GetComponent<CanvasTreeSkillButton>().skill = "Attack";

            //Spawn Atack Time Info

            GameObject attackTimeTree = Instantiate(skillButton, new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z),
            Quaternion.identity);

            attackTimeTree.transform.SetParent(holder);
            attackTimeTree.GetComponent<CanvasTreeSkillButton>().text = "Attack Quality " + collidingTree.GetComponent<TreeFighting>().attackDistance;
            attackTimeTree.GetComponent<CanvasTreeSkillButton>().tree = collidingTree.gameObject;
            attackTimeTree.GetComponent<CanvasTreeSkillButton>().skill = "Attack Quality";

            //Spawn Attack Radius Info

            GameObject attackSizeTree = Instantiate(skillButton, new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z),
            Quaternion.identity);

            attackSizeTree.transform.SetParent(holder);
            attackSizeTree.GetComponent<CanvasTreeSkillButton>().text = "Attack Distance " + collidingTree.GetComponent<TreeFighting>().attackQuality;
            attackSizeTree.GetComponent<CanvasTreeSkillButton>().tree = collidingTree.gameObject;
            attackSizeTree.GetComponent<CanvasTreeSkillButton>().skill = "Attack Distance";
        }

        if(collidingTree.tag == "TreeDefending")
        {
            
            DeleteTreeSkills();

            canvasTree.swFruits.SetActive(false);


            //Spawn level info
            GameObject levelInfo = Instantiate(skill, new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z),
            Quaternion.identity);

            levelInfo.transform.SetParent(holder);

            levelInfo.GetComponent<CanvasTreeSkill>().text = "Level " + collidingTree.GetComponent<TreeDefending>().level;

            //Spawn points info

            GameObject pointsInfo = Instantiate(skill, new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z),
            Quaternion.identity);

            pointsInfo.transform.SetParent(holder);
            pointsInfo.GetComponent<CanvasTreeSkill>().text = "Points " + collidingTree.GetComponent<TreeDefending>().points;

            //Spawn Exp Info

            GameObject expInfo = Instantiate(skill, new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z),
            Quaternion.identity);

            expInfo.transform.SetParent(holder);
            expInfo.GetComponent<CanvasTreeSkill>().text = "Experience " + collidingTree.GetComponent<TreeDefending>().exp + " / " + 
                collidingTree.GetComponent<TreeDefending>().expToUpgrade;

            //Spawn Max Health Info

            GameObject maxHealthInfo = Instantiate(skillButton, new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z),
            Quaternion.identity);

            maxHealthInfo.transform.SetParent(holder);
            maxHealthInfo.GetComponent<CanvasTreeSkillButton>().text = "Health " + collidingTree.GetComponent<TreeDefending>().healthMax;
            maxHealthInfo.GetComponent<CanvasTreeSkillButton>().tree = collidingTree.gameObject;
            maxHealthInfo.GetComponent<CanvasTreeSkillButton>().skill = ("Health Max");

            //Spawn Regen Health Info

            GameObject regenHealthInfo = Instantiate(skillButton, new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z),
            Quaternion.identity);

            regenHealthInfo.transform.SetParent(holder);
            regenHealthInfo.GetComponent<CanvasTreeSkillButton>().text = "Recovery " + collidingTree.GetComponent<TreeDefending>().healthReg;
            regenHealthInfo.GetComponent<CanvasTreeSkillButton>().tree = collidingTree.gameObject;
            regenHealthInfo.GetComponent<CanvasTreeSkillButton>().skill = "Recovery";

            GameObject hydrationInfo = Instantiate(skillButton, new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z),
            Quaternion.identity);

            regenHealthInfo.transform.SetParent(holder);
            regenHealthInfo.GetComponent<CanvasTreeSkillButton>().text = "Hydration " + collidingTree.GetComponent<TreeDefending>().hydration;
            regenHealthInfo.GetComponent<CanvasTreeSkillButton>().tree = collidingTree.gameObject;
            regenHealthInfo.GetComponent<CanvasTreeSkillButton>().skill = "Hydration";
        }
    }

    public void DeleteTreeSkills()
    {
        
        GameObject[] canvasTreeSkills = GameObject.FindGameObjectsWithTag("CanvasTreeSkill");
        foreach (GameObject canvasTreeSkill in canvasTreeSkills)
        {
            Destroy(canvasTreeSkill);
        }
    }

    public void UpdateFruits()
    {
        if(collidingTree.tag == "TreeFruits")
        {

            GameObject[] fruitSlots = GameObject.FindGameObjectsWithTag("CanvasTreeFruitSlot");

            int numberOfFruits = collidingTree.GetComponent<TreeFruits>().fruit.Count;
            
            for (int i = 0; i < numberOfFruits; i++)
            {
                fruitSlots[i].GetComponent<CanvasTreeFruit>().fruitType =
                collidingTree.GetComponent<TreeFruits>().fruit[i].fruitType;
                fruitSlots[i].GetComponent<CanvasTreeFruit>().fruitNumber =
                collidingTree.GetComponent<TreeFruits>().fruit[i].fruitNumber;
                fruitSlots[i].GetComponent<CanvasTreeFruit>().fruitPower =
                collidingTree.GetComponent<TreeFruits>().fruit[i].fruitPower;
                fruitSlots[i].GetComponent<CanvasTreeFruit>().ShowImage();
            }

            for (int i = numberOfFruits; i < fruitSlots.Length; i++)
            {
                fruitSlots[i].GetComponent<CanvasTreeFruit>().fruitType = null;
                fruitSlots[i].GetComponent<CanvasTreeFruit>().fruitNumber = 0;
                fruitSlots[i].GetComponent<CanvasTreeFruit>().fruitPower = 0;
                fruitSlots[i].GetComponent<CanvasTreeFruit>().NoImage();
            }
        }
    }

    public void AddFruitToPlayer(string type, int number, int power)
    {
        playerManager.AddFruit(type, number, power);
    }

    public void RemoveFruitFromTree(int fruitNumber)
    {
        for(int i = 0; i < collidingTree.GetComponent<TreeFruits>().fruit.Count; i++)
        {
            if(collidingTree.GetComponent<TreeFruits>().fruit[i].fruitNumber == fruitNumber)
            {
                collidingTree.GetComponent<TreeFruits>().fruit.Remove
                (collidingTree.GetComponent<TreeFruits>().fruit[i]);
            }
        }

        UpdateFruits();
    }

    public void AddPointsFromGemToTree(string gemtype,int gemNumber, int gemPower)
    {
        if (collidingTree.tag == "TreeFruits")
        {
            if (gemtype == "Health")
            {
                if (collidingTree.GetComponent<TreeFruits>().healthCurrent < collidingTree.GetComponent<TreeFruits>().healthMax)
                {
                    collidingTree.GetComponent<TreeFruits>().healthCurrent =
                        collidingTree.GetComponent<TreeFruits>().healthCurrent + ((gemPower * collidingTree.GetComponent<TreeFruits>().level) +
                        (gemPower * 10));

                    if (collidingTree.GetComponent<TreeFruits>().healthCurrent > collidingTree.GetComponent<TreeFruits>().healthMax)
                    {
                        collidingTree.GetComponent<TreeFruits>().healthCurrent = collidingTree.GetComponent<TreeFruits>().healthMax;
                    }

                    CanvasTreeInfo.text = "This tree current health has grown";
                }

                if (collidingTree.GetComponent<TreeFruits>().healthCurrent >= collidingTree.GetComponent<TreeFruits>().healthMax)
                {
                    CanvasTreeInfo.text = "This tree is healthly and gem didn't heal it";
                }
            }
            if (gemtype == "Exp")
            {
                collidingTree.GetComponent<TreeFruits>().exp =
                    collidingTree.GetComponent<TreeFruits>().exp + ((gemPower * collidingTree.GetComponent<TreeFruits>().level) +
                        (gemPower * 10));

                CanvasTreeInfo.text = "This tree experience has grown";
            }
            if (gemtype == "Fruits")
            {

                for (int i = 0; i < gemPower; i++)
                {
                    if (collidingTree.GetComponent<TreeFruits>().fruit.Count < 16)
                    {
                        collidingTree.GetComponent<TreeFruits>().AddFruit();
                    }
                }

                CanvasTreeInfo.text = "This tree's fruits has grown";
            }
            if (gemtype == "Super Health")
            {
                collidingTree.GetComponent<TreeFruits>().healthMax =
                    collidingTree.GetComponent<TreeFruits>().healthMax + (gemPower * 10);

                CanvasTreeInfo.text = "This tree max health has grown";
            }
            if (gemtype == "Recovery")
            {
                collidingTree.GetComponent<TreeFruits>().healthReg =
                    collidingTree.GetComponent<TreeFruits>().healthReg + gemPower;

                CanvasTreeInfo.text = "this tree will heal faster";
            }
            if (gemtype == "Super Fruits")
            {
                collidingTree.GetComponent<TreeFruits>().fruits =
                    collidingTree.GetComponent<TreeFruits>().fruits + gemPower;

                CanvasTreeInfo.text = "More fruits will grow on this tree!";
            }
            if (gemtype == "Fruits Power")
            {
                collidingTree.GetComponent<TreeFruits>().fruitsPower =
                    collidingTree.GetComponent<TreeFruits>().fruitsPower + gemPower;

                CanvasTreeInfo.text = "The fruits of this tree will be more powerful!";
            }
            if (gemtype == "Wild Fruits")
            {
                collidingTree.GetComponent<TreeFruits>().fruitsWild =
                    collidingTree.GetComponent<TreeFruits>().fruitsWild + gemNumber;

                CanvasTreeInfo.text = "Increased a chance of growing wild fruits on this tree!";
            }
            if (gemtype == "Attack")
            {
                CanvasTreeInfo.text = "This tree is not a fighting tree and the gem did not work";
            }
            if (gemtype == "Attack Quality")
            {
                CanvasTreeInfo.text = "This tree is not a fighting tree and the gem did not work";
            }
            if (gemtype == "Attack Distance")
            {
                CanvasTreeInfo.text = "This tree is not a fighting tree and the gem did not work";
            }
            if (gemtype == "Hydration")
            {
                CanvasTreeInfo.text = "This tree is not a defending tree and the gem did not work";
            }
            if (gemtype == "Light Branches")
            {
                CanvasTreeInfo.text = "This tree is not a defending tree and the gem did not work";
            }
            if (gemtype == "Attraction")
            {
                CanvasTreeInfo.text = "This tree is not a defending tree and the gem did not work";
            }

            if (gemtype == "Skill")
            {
                collidingTree.GetComponent<TreeFruits>().points =
                     collidingTree.GetComponent<TreeFruits>().points + gemPower;

                CanvasTreeInfo.text = "This tree can learn more skills now!";
            }
            if (gemtype == "Level")
            {
                collidingTree.GetComponent<TreeFruits>().exp =
                     collidingTree.GetComponent<TreeFruits>().expToUpgrade;

                CanvasTreeInfo.text = "Level of this tree has increased!";
            }

            RemoveGemFromPlayer(gemNumber);
        }

        if (collidingTree.tag == "TreeFighting")
        {
            if (gemtype == "Health")
            {
                if (collidingTree.GetComponent<TreeFighting>().healthCurrent < collidingTree.GetComponent<TreeFighting>().healthMax)
                {
                    collidingTree.GetComponent<TreeFighting>().healthCurrent =
                        collidingTree.GetComponent<TreeFighting>().healthCurrent + ((gemPower * collidingTree.GetComponent<TreeFighting>().level) +
                        (gemPower * 10));

                    if (collidingTree.GetComponent<TreeFighting>().healthCurrent > collidingTree.GetComponent<TreeFighting>().healthMax)
                    {
                        collidingTree.GetComponent<TreeFighting>().healthCurrent = collidingTree.GetComponent<TreeFighting>().healthMax;
                    }

                    CanvasTreeInfo.text = "This tree current health has grown";
                }

                if (collidingTree.GetComponent<TreeFighting>().healthCurrent >= collidingTree.GetComponent<TreeFighting>().healthMax)
                {
                    CanvasTreeInfo.text = "This tree is healthly and gem didn't heal it";
                }
            }
            if (gemtype == "Exp")
            {
                collidingTree.GetComponent<TreeFighting>().exp =
                    collidingTree.GetComponent<TreeFighting>().exp + ((gemPower * collidingTree.GetComponent<TreeFighting>().level) +
                        (gemPower * 10));

                CanvasTreeInfo.text = "This tree experience has grown";
            }
            if (gemtype == "Fruits")
            {
                CanvasTreeInfo.text = "This tree is not a fruit tree and the gem did not work";
            }
            if (gemtype == "Super Health")
            {
                collidingTree.GetComponent<TreeFighting>().healthMax =
                    collidingTree.GetComponent<TreeFighting>().healthMax + (gemPower * 10);

                CanvasTreeInfo.text = "This tree max health has grown";
            }
            if (gemtype == "Recovery")
            {
                collidingTree.GetComponent<TreeFighting>().healthReg =
                    collidingTree.GetComponent<TreeFighting>().healthReg + gemPower;

                CanvasTreeInfo.text = "This tree will heal faster";
            }
            if (gemtype == "Super Fruits")
            {
                CanvasTreeInfo.text = "This tree is not a fruit tree and the gem did not work";
            }
            if (gemtype == "Fruits Power")
            {
                CanvasTreeInfo.text = "This tree is not a fruit tree and the gem did not work";
            }
            if (gemtype == "Wild Fruits")
            {
                CanvasTreeInfo.text = "This tree is not a fruit tree and the gem did not work";
            }
            if (gemtype == "Attack")
            {
                collidingTree.GetComponent<TreeFighting>().attackPower =
                    collidingTree.GetComponent<TreeFighting>().attackPower + gemPower;

                CanvasTreeInfo.text = "The attack strength of this tree has increased";
            }
            if (gemtype == "Attack Quality")
            {
                collidingTree.GetComponent<TreeFighting>().attackQuality =
                    collidingTree.GetComponent<TreeFighting>().attackQuality + gemPower;

                if(collidingTree.GetComponent<TreeFighting>().treeType == "Cloud Tree")
                {
                    CanvasTreeInfo.text = "The attack recoil force of this tree has increased";
                }

                if(collidingTree.GetComponent<TreeFighting>().treeType == "Belt Tree" ||
                    collidingTree.GetComponent<TreeFighting>().treeType == "Spell Tree")
                {
                    CanvasTreeInfo.text = "The attack accuracy of this tree has increased";
                }
                
            }
            if (gemtype == "Attack Distance")
            {
                collidingTree.GetComponent<TreeFighting>().attackDistance =
                    collidingTree.GetComponent<TreeFighting>().attackDistance = gemPower;

                CanvasTreeInfo.text = "The attack distance of this tree has increased";
            }
            if (gemtype == "Hydration")
            {
                CanvasTreeInfo.text = "This tree is not a defending tree and the gem did not work";
            }
            if (gemtype == "Light Branches")
            {
                CanvasTreeInfo.text = "This tree is not a defending tree and the gem did not work";
            }
            if (gemtype == "Attraction")
            {
                CanvasTreeInfo.text = "This tree is not a defending tree and the gem did not work";
            }
            if (gemtype == "Skill")
            {
                collidingTree.GetComponent<TreeFighting>().points =
                     collidingTree.GetComponent<TreeFighting>().points + gemPower;

                CanvasTreeInfo.text = "This tree can learn more skills now!";
            }
            if (gemtype == "Level")
            {
                collidingTree.GetComponent<TreeFighting>().exp =
                     collidingTree.GetComponent<TreeFighting>().expToUpgrade;

                CanvasTreeInfo.text = "Level of this tree has increased!";
            }

            RemoveGemFromPlayer(gemNumber);
        }

        if(collidingTree.tag == "Tree Defending")
        {
            if (gemtype == "Health")
            {
                if (collidingTree.GetComponent<TreeDefending>().healthCurrent < collidingTree.GetComponent<TreeDefending>().healthMax)
                {
                    collidingTree.GetComponent<TreeDefending>().healthCurrent =
                        collidingTree.GetComponent<TreeDefending>().healthCurrent + ((gemPower * collidingTree.GetComponent<TreeDefending>().level) +
                        (gemPower * 10));

                    if (collidingTree.GetComponent<TreeDefending>().healthCurrent > collidingTree.GetComponent<TreeDefending>().healthMax)
                    {
                        collidingTree.GetComponent<TreeDefending>().healthCurrent = collidingTree.GetComponent<TreeDefending>().healthMax;
                    }

                    CanvasTreeInfo.text = "This tree current health has grown";
                }

                if (collidingTree.GetComponent<TreeDefending>().healthCurrent >= collidingTree.GetComponent<TreeDefending>().healthMax)
                {
                    CanvasTreeInfo.text = "This tree is healthly and gem didn't heal it";
                }
            }
            if (gemtype == "Exp")
            {
                collidingTree.GetComponent<TreeDefending>().exp =
                    collidingTree.GetComponent<TreeDefending>().exp + ((gemPower * collidingTree.GetComponent<TreeDefending>().level) +
                        (gemPower * 10));

                CanvasTreeInfo.text = "This tree experience has grown";
            }
            if (gemtype == "Fruits")
            {
                CanvasTreeInfo.text = "This tree is not a fruit tree and the gem did not work";
            }
            if (gemtype == "Super Health")
            {
                collidingTree.GetComponent<TreeDefending>().healthMax =
                    collidingTree.GetComponent<TreeDefending>().healthMax + (gemPower * 10);

                CanvasTreeInfo.text = "This tree max health has grown";
            }
            if (gemtype == "Recovery")
            {
                collidingTree.GetComponent<TreeDefending>().healthReg =
                    collidingTree.GetComponent<TreeDefending>().healthReg + gemPower;

                CanvasTreeInfo.text = "This tree will heal faster";
            }
            if (gemtype == "Super Fruits")
            {
                CanvasTreeInfo.text = "This tree is not a fruit tree and the gem did not work";
            }
            if (gemtype == "Fruits Power")
            {
                CanvasTreeInfo.text = "This tree is not a fruit tree and the gem did not work";
            }
            if (gemtype == "Wild Fruits")
            {
                CanvasTreeInfo.text = "This tree is not a fruit tree and the gem did not work";
            }
            if (gemtype == "Attack")
            {
                CanvasTreeInfo.text = "This tree is not a fighting tree and the gem did not work";
            }
            if (gemtype == "Attack Quality")
            {
                CanvasTreeInfo.text = "This tree is not a fighting tree and the gem did not work";
            }
            if (gemtype == "Attack Distance")
            {
                CanvasTreeInfo.text = "This tree is not a fighting tree and the gem did not work";
            }
            if (gemtype == "Hydration")
            {
                collidingTree.GetComponent<TreeDefending>().hydration =
                    collidingTree.GetComponent<TreeDefending>().hydration + gemPower;

                CanvasTreeInfo.text = "The chance of hydration appearance has increased";
            }
            if (gemtype == "Light Branches")
            {
                collidingTree.GetComponent<TreeDefending>().lightBranches =
                    collidingTree.GetComponent<TreeDefending>().lightBranches + gemPower;

                CanvasTreeInfo.text = "The chance to avoid enemy attack has increased";
            }
            if (gemtype == "Attraction")
            {
                collidingTree.GetComponent<TreeDefending>().attraction =
                    collidingTree.GetComponent<TreeDefending>().attraction + gemPower;

                CanvasTreeInfo.text = "The chance to attract the attention of the enemy has increased";
            }
            if (gemtype == "Skill")
            {
                collidingTree.GetComponent<TreeDefending>().points =
                     collidingTree.GetComponent<TreeDefending>().points + gemPower;

                CanvasTreeInfo.text = "This tree can learn more skills now!";
            }
            if (gemtype == "Level")
            {
                collidingTree.GetComponent<TreeDefending>().exp =
                     collidingTree.GetComponent<TreeDefending>().expToUpgrade;

                CanvasTreeInfo.text = "Level of this tree has increased!";
            }

            RemoveGemFromPlayer(gemNumber);
            
        }
    }

    void RemoveGemFromPlayer(int gemNumber)
    {
        playerManager.RemoveGem(gemNumber);
        
    }

    public void ChangeTreeType(GameObject treeType)
    {
        //Check if level is greater or smaller than 5
        //Check if class of the tree is the same as the one we want to have 
        //Then also check player skill
        //Don't remember to take the time for it
        Debug.Log("trees manager change tree type");

        if(BattleManager.timerBattle > 120f)
        {
            if (treeType.name == "Food")
            {
                if (collidingTree.tag == "TreeFruits")
                {
                    TreeFruits treeFruits = collidingTree.GetComponent<TreeFruits>();

                    if(treeFruits.level <= 5)
                    {
                        float changeType = Random.Range(0, 100);

                        if(changeType <= 70 - playerStatistics.treesHerd)
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                        else
                        {
                            treeFruits.treeType = "Food Tree";
                            CanvasTreeInfo.text = "Tree type has been changed";
                            canvasTree.SwitchChange();
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }

                    if(treeFruits.level > 5)
                    {
                        float changeType = Random.Range(0, 100);

                        if (changeType <= 85 - (playerStatistics.treesHerd))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                        else
                        {
                            treeFruits.treeType = "Food Tree";
                            canvasTree.SwitchChange();
                            CanvasTreeInfo.text = "Tree type has been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }
                }
                if (collidingTree.tag == "TreeFighting")
                {
                    TreeFighting treeFighting = collidingTree.GetComponent<TreeFighting>();
                    float changeType = Random.Range(0, 100);

                    if (treeFighting.level <= 5)
                    {
                        if (changeType <= 85 - (playerStatistics.treesHerd))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }

                        else
                        {
                            GameObject nTree = Instantiate(treeFruitGO);

                            nTree.transform.position = collidingTree.transform.position;
                            nTree.GetComponent<TreeFruits>().treeType = "Food Tree";

                            nTree.transform.Find("TreeMesh").transform.localScale = collidingTree.transform.Find("TreeMesh").transform.localScale;

                            GameObject treeToDestroy = collidingTree.gameObject;
                            
                            canvasTree.isEnabled = false;

                            playerInteractable.StopInteractingWithTree();

                            playerInteractable.collidingGO = nTree.transform.gameObject;
                            collidingTree = nTree.gameObject;
                            playerInteractable.alreadyCollidingWithGO = true;
                            playerInteractable.interactingWithTree = true;
                            
                            Destroy(treeToDestroy);

                            CanvasTreeInfo.text = "Tree type has been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }

                    if (treeFighting.level > 5)
                    {
                        if (changeType <= 100 - (playerStatistics.treesHerd))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }

                        else
                        {
                            GameObject nTree = Instantiate(treeFruitGO);

                            nTree.transform.position = collidingTree.transform.position;
                            nTree.GetComponent<TreeFruits>().treeType = "Food Tree";

                            nTree.transform.Find("TreeMesh").transform.localScale = collidingTree.transform.Find("TreeMesh").transform.localScale;

                            GameObject treeToDestroy = collidingTree.gameObject;

                            canvasTree.isEnabled = false;

                            playerInteractable.StopInteractingWithTree();

                            playerInteractable.collidingGO = nTree.transform.gameObject;
                            collidingTree = nTree.gameObject;
                            playerInteractable.alreadyCollidingWithGO = true;
                            playerInteractable.interactingWithTree = true;

                            Destroy(treeToDestroy);

                            CanvasTreeInfo.text = "Tree type has been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }
                }
                if (collidingTree.tag == "TreeDefending")
                {
                    TreeDefending treeDefending = collidingTree.GetComponent<TreeDefending>();
                    float changeType = Random.Range(0, 100);

                    if (treeDefending.level <= 5)
                    {
                        if (changeType <= 85 - (playerStatistics.treesHerd))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }

                        else
                        {
                            GameObject nTree = Instantiate(treeFruitGO);

                            nTree.transform.position = collidingTree.transform.position;
                            nTree.GetComponent<TreeFruits>().treeType = "Food Tree";

                            nTree.transform.Find("TreeMesh").transform.localScale = collidingTree.transform.Find("TreeMesh").transform.localScale;

                            GameObject treeToDestroy = collidingTree.gameObject;

                            canvasTree.isEnabled = false;

                            playerInteractable.StopInteractingWithTree();

                            playerInteractable.collidingGO = nTree.transform.gameObject;
                            collidingTree = nTree.gameObject;
                            playerInteractable.alreadyCollidingWithGO = true;
                            playerInteractable.interactingWithTree = true;

                            Destroy(treeToDestroy);

                            CanvasTreeInfo.text = "Tree type has been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }

                    if (treeDefending.level > 5)
                    {
                        if (changeType <= 100 - (playerStatistics.treesHerd))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }

                        else
                        {
                            GameObject nTree = Instantiate(treeFruitGO);

                            nTree.transform.position = collidingTree.transform.position;
                            nTree.GetComponent<TreeFruits>().treeType = "Food Tree";

                            nTree.transform.Find("TreeMesh").transform.localScale = collidingTree.transform.Find("TreeMesh").transform.localScale;

                            GameObject treeToDestroy = collidingTree.gameObject;

                            canvasTree.isEnabled = false;

                            playerInteractable.StopInteractingWithTree();

                            playerInteractable.collidingGO = nTree.transform.gameObject;
                            collidingTree = nTree.gameObject;
                            playerInteractable.alreadyCollidingWithGO = true;
                            playerInteractable.interactingWithTree = true;

                            Destroy(treeToDestroy);

                            CanvasTreeInfo.text = "Tree type has been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }
                }
            }

            if (treeType.name == "Life")
            {
                if (collidingTree.tag == "TreeFruits")
                {
                    TreeFruits treeFruits = collidingTree.GetComponent<TreeFruits>();

                    if (treeFruits.level <= 5)
                    {
                        float changeType = Random.Range(0, 100);

                        if (changeType <= 70 - playerStatistics.treesHerd)
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                        else
                        {
                            treeFruits.treeType = "Life Tree";
                            canvasTree.SwitchChange();
                            CanvasTreeInfo.text = "Tree type has been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }

                    if (treeFruits.level > 5)
                    {
                        float changeType = Random.Range(0, 100);

                        if (changeType <= 85 - (playerStatistics.treesHerd))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                        else
                        {
                            treeFruits.treeType = "Life Tree";
                            canvasTree.SwitchChange();
                            CanvasTreeInfo.text = "Tree type has been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }
                }
                if (collidingTree.tag == "TreeFighting")
                {
                    TreeFighting treeFighting = collidingTree.GetComponent<TreeFighting>();
                    float changeType = Random.Range(0, 100);

                    if (treeFighting.level <= 5)
                    {
                        if (changeType <= 85 - (playerStatistics.treesHerd))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }

                        else
                        {
                            GameObject nTree = Instantiate(treeFruitGO);

                            nTree.transform.position = collidingTree.transform.position;
                            nTree.GetComponent<TreeFruits>().treeType = "Life Tree";

                            nTree.transform.Find("TreeMesh").transform.localScale = collidingTree.transform.Find("TreeMesh").transform.localScale;

                            GameObject treeToDestroy = collidingTree.gameObject;

                            canvasTree.isEnabled = false;

                            playerInteractable.StopInteractingWithTree();

                            playerInteractable.collidingGO = nTree.transform.gameObject;
                            collidingTree = nTree.gameObject;
                            playerInteractable.alreadyCollidingWithGO = true;
                            playerInteractable.interactingWithTree = true;

                            Destroy(treeToDestroy);

                            CanvasTreeInfo.text = "Tree type has been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }

                    if (treeFighting.level > 5)
                    {
                        if (changeType <= 100 - (playerStatistics.treesHerd))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }

                        else
                        {
                            GameObject nTree = Instantiate(treeFruitGO);

                            nTree.transform.position = collidingTree.transform.position;
                            nTree.GetComponent<TreeFruits>().treeType = "Life Tree";

                            nTree.transform.Find("TreeMesh").transform.localScale = collidingTree.transform.Find("TreeMesh").transform.localScale;

                            GameObject treeToDestroy = collidingTree.gameObject;

                            canvasTree.isEnabled = false;

                            playerInteractable.StopInteractingWithTree();

                            playerInteractable.collidingGO = nTree.transform.gameObject;
                            collidingTree = nTree.gameObject;
                            playerInteractable.alreadyCollidingWithGO = true;
                            playerInteractable.interactingWithTree = true;

                            Destroy(treeToDestroy);

                            CanvasTreeInfo.text = "Tree type has been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }
                }
                if (collidingTree.tag == "TreeDefending")
                {
                    TreeDefending treeDefending = collidingTree.GetComponent<TreeDefending>();
                    float changeType = Random.Range(0, 100);

                    if (treeDefending.level <= 5)
                    {
                        if (changeType <= 99 - (playerStatistics.treesHerd * 0.5f))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }

                        else
                        {
                            GameObject nTree = Instantiate(treeFruitGO);

                            nTree.transform.position = collidingTree.transform.position;
                            nTree.GetComponent<TreeFruits>().treeType = "Life Tree";

                            nTree.transform.Find("TreeMesh").transform.localScale = collidingTree.transform.Find("TreeMesh").transform.localScale;

                            GameObject treeToDestroy = collidingTree.gameObject;

                            canvasTree.isEnabled = false;

                            playerInteractable.StopInteractingWithTree();

                            playerInteractable.collidingGO = nTree.transform.gameObject;
                            collidingTree = nTree.gameObject;
                            playerInteractable.alreadyCollidingWithGO = true;
                            playerInteractable.interactingWithTree = true;

                            Destroy(treeToDestroy);

                            CanvasTreeInfo.text = "Tree type has been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }

                    if (treeDefending.level > 5)
                    {
                        if (changeType <= 100 - (playerStatistics.treesHerd * 0.25f))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }

                        else
                        {
                            GameObject nTree = Instantiate(treeFruitGO);

                            nTree.transform.position = collidingTree.transform.position;
                            nTree.GetComponent<TreeFruits>().treeType = "Life Tree";

                            nTree.transform.Find("TreeMesh").transform.localScale = collidingTree.transform.Find("TreeMesh").transform.localScale;

                            GameObject treeToDestroy = collidingTree.gameObject;

                            canvasTree.isEnabled = false;

                            playerInteractable.StopInteractingWithTree();

                            playerInteractable.collidingGO = nTree.transform.gameObject;
                            collidingTree = nTree.gameObject;
                            playerInteractable.alreadyCollidingWithGO = true;
                            playerInteractable.interactingWithTree = true;

                            Destroy(treeToDestroy);

                            CanvasTreeInfo.text = "Defending Life";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }
                }
            }

            if (treeType.name == "Energy")
            {
                if (collidingTree.tag == "TreeFruits")
                {
                    TreeFruits treeFruits = collidingTree.GetComponent<TreeFruits>();

                    if (treeFruits.level <= 5)
                    {
                        float changeType = Random.Range(0, 100);

                        if (changeType <= 70 - playerStatistics.treesHerd)
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                        else
                        {
                            treeFruits.treeType = "Energy Tree";
                            canvasTree.SwitchChange();
                            CanvasTreeInfo.text = "Tree type has been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }

                    if (treeFruits.level > 5)
                    {
                        float changeType = Random.Range(0, 100);

                        if (changeType <= 85 - (playerStatistics.treesHerd))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                        else
                        {
                            treeFruits.treeType = "Energy Tree";
                            canvasTree.SwitchChange();
                            CanvasTreeInfo.text = "Tree type has been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }
                }
                if (collidingTree.tag == "TreeFighting")
                {
                    TreeFighting treeFighting = collidingTree.GetComponent<TreeFighting>();
                    float changeType = Random.Range(0, 100);

                    if (treeFighting.level <= 5)
                    {
                        if (changeType <= 85 - (playerStatistics.treesHerd))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }

                        else
                        {
                            GameObject nTree = Instantiate(treeFruitGO);

                            nTree.transform.position = collidingTree.transform.position;
                            nTree.GetComponent<TreeFruits>().treeType = "Energy Tree";

                            nTree.transform.Find("TreeMesh").transform.localScale = collidingTree.transform.Find("TreeMesh").transform.localScale;

                            GameObject treeToDestroy = collidingTree.gameObject;

                            canvasTree.isEnabled = false;

                            playerInteractable.StopInteractingWithTree();

                            playerInteractable.collidingGO = nTree.transform.gameObject;
                            collidingTree = nTree.gameObject;
                            playerInteractable.alreadyCollidingWithGO = true;
                            playerInteractable.interactingWithTree = true;

                            Destroy(treeToDestroy);

                            CanvasTreeInfo.text = "Fighting Energy";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }

                    if (treeFighting.level > 5)
                    {
                        if (changeType <= 100 - (playerStatistics.treesHerd))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }

                        else
                        {
                            GameObject nTree = Instantiate(treeFruitGO);

                            nTree.transform.position = collidingTree.transform.position;
                            nTree.GetComponent<TreeFruits>().treeType = "Energy Tree";

                            nTree.transform.Find("TreeMesh").transform.localScale = collidingTree.transform.Find("TreeMesh").transform.localScale;

                            GameObject treeToDestroy = collidingTree.gameObject;

                            canvasTree.isEnabled = false;

                            playerInteractable.StopInteractingWithTree();

                            playerInteractable.collidingGO = nTree.transform.gameObject;
                            collidingTree = nTree.gameObject;
                            playerInteractable.alreadyCollidingWithGO = true;
                            playerInteractable.interactingWithTree = true;

                            Destroy(treeToDestroy);

                            CanvasTreeInfo.text = "Fighting Energy";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }
                }
                if (collidingTree.tag == "TreeDefending")
                {
                    TreeDefending treeDefending = collidingTree.GetComponent<TreeDefending>();
                    float changeType = Random.Range(0, 100);

                    if (treeDefending.level <= 5)
                    {
                        if (changeType <= 85 - (playerStatistics.treesHerd))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }

                        else
                        {
                            GameObject nTree = Instantiate(treeFruitGO);

                            nTree.transform.position = collidingTree.transform.position;
                            nTree.GetComponent<TreeFruits>().treeType = "Energy Tree";

                            nTree.transform.Find("TreeMesh").transform.localScale = collidingTree.transform.Find("TreeMesh").transform.localScale;

                            GameObject treeToDestroy = collidingTree.gameObject;

                            canvasTree.isEnabled = false;

                            playerInteractable.StopInteractingWithTree();

                            playerInteractable.collidingGO = nTree.transform.gameObject;
                            collidingTree = nTree.gameObject;
                            playerInteractable.alreadyCollidingWithGO = true;
                            playerInteractable.interactingWithTree = true;

                            Destroy(treeToDestroy);

                            CanvasTreeInfo.text = "Defending Energy";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }

                    if (treeDefending.level > 5)
                    {
                        if (changeType <= 100 - (playerStatistics.treesHerd))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }

                        else
                        {
                            GameObject nTree = Instantiate(treeFruitGO);

                            nTree.transform.position = collidingTree.transform.position;
                            nTree.GetComponent<TreeFruits>().treeType = "Energy Tree";

                            nTree.transform.Find("TreeMesh").transform.localScale = collidingTree.transform.Find("TreeMesh").transform.localScale;

                            GameObject treeToDestroy = collidingTree.gameObject;

                            canvasTree.isEnabled = false;

                            playerInteractable.StopInteractingWithTree();

                            playerInteractable.collidingGO = nTree.transform.gameObject;
                            collidingTree = nTree.gameObject;
                            playerInteractable.alreadyCollidingWithGO = true;
                            playerInteractable.interactingWithTree = true;

                            Destroy(treeToDestroy);

                            CanvasTreeInfo.text = "Defending Energy";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }
                }
            }

            if (treeType.name == "Power")
            {
                if (collidingTree.tag == "TreeFruits")
                {
                    TreeFruits treeFruits = collidingTree.GetComponent<TreeFruits>();

                    if (treeFruits.level <= 5)
                    {
                        float changeType = Random.Range(0, 100);

                        if (changeType <= 70 - playerStatistics.treesHerd)
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                        else
                        {
                            treeFruits.treeType = "Power Tree";
                            canvasTree.SwitchChange();
                            CanvasTreeInfo.text = "Tree type has been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }

                    if (treeFruits.level > 5)
                    {
                        float changeType = Random.Range(0, 100);

                        if (changeType <= 85 - (playerStatistics.treesHerd))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                        else
                        {
                            treeFruits.treeType = "Power Tree";
                            canvasTree.SwitchChange();
                            CanvasTreeInfo.text = "Tree type has been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }
                }
                if (collidingTree.tag == "TreeFighting")
                {
                    TreeFighting treeFighting = collidingTree.GetComponent<TreeFighting>();
                    float changeType = Random.Range(0, 100);

                    if (treeFighting.level <= 5)
                    {
                        if (changeType <= 85 - (playerStatistics.treesHerd))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }

                        else
                        {
                            GameObject nTree = Instantiate(treeFruitGO);

                            nTree.transform.position = collidingTree.transform.position;
                            nTree.GetComponent<TreeFruits>().treeType = "Power Tree";

                            nTree.transform.Find("TreeMesh").transform.localScale = collidingTree.transform.Find("TreeMesh").transform.localScale;

                            GameObject treeToDestroy = collidingTree.gameObject;

                            canvasTree.isEnabled = false;

                            playerInteractable.StopInteractingWithTree();

                            playerInteractable.collidingGO = nTree.transform.gameObject;
                            collidingTree = nTree.gameObject;
                            playerInteractable.alreadyCollidingWithGO = true;
                            playerInteractable.interactingWithTree = true;

                            Destroy(treeToDestroy);

                            CanvasTreeInfo.text = "Tree type has been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }

                    if (treeFighting.level > 5)
                    {
                        if (changeType <= 100 - (playerStatistics.treesHerd))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }

                        else
                        {
                            GameObject nTree = Instantiate(treeFruitGO);

                            nTree.transform.position = collidingTree.transform.position;
                            nTree.GetComponent<TreeFruits>().treeType = "Power Tree";

                            nTree.transform.Find("TreeMesh").transform.localScale = collidingTree.transform.Find("TreeMesh").transform.localScale;

                            GameObject treeToDestroy = collidingTree.gameObject;

                            canvasTree.isEnabled = false;

                            playerInteractable.StopInteractingWithTree();

                            playerInteractable.collidingGO = nTree.transform.gameObject;
                            collidingTree = nTree.gameObject;
                            playerInteractable.alreadyCollidingWithGO = true;
                            playerInteractable.interactingWithTree = true;

                            Destroy(treeToDestroy);

                            CanvasTreeInfo.text = "Tree type has been changed Power";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }
                }
                if (collidingTree.tag == "TreeDefending")
                {
                    TreeDefending treeDefending = collidingTree.GetComponent<TreeDefending>();
                    float changeType = Random.Range(0, 100);

                    if (treeDefending.level <= 5)
                    {
                        if (changeType <= 85 - (playerStatistics.treesHerd))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }

                        else
                        {
                            GameObject nTree = Instantiate(treeFruitGO);

                            nTree.transform.position = collidingTree.transform.position;
                            nTree.GetComponent<TreeFruits>().treeType = "Power Tree";

                            nTree.transform.Find("TreeMesh").transform.localScale = collidingTree.transform.Find("TreeMesh").transform.localScale;

                            GameObject treeToDestroy = collidingTree.gameObject;

                            canvasTree.isEnabled = false;

                            playerInteractable.StopInteractingWithTree();

                            playerInteractable.collidingGO = nTree.transform.gameObject;
                            collidingTree = nTree.gameObject;
                            playerInteractable.alreadyCollidingWithGO = true;
                            playerInteractable.interactingWithTree = true;

                            Destroy(treeToDestroy);

                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }

                    if (treeDefending.level > 5)
                    {
                        if (changeType <= 100 - (playerStatistics.treesHerd))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }

                        else
                        {
                            GameObject nTree = Instantiate(treeFruitGO);

                            nTree.transform.position = collidingTree.transform.position;
                            nTree.GetComponent<TreeFruits>().treeType = "Power Tree";

                            nTree.transform.Find("TreeMesh").transform.localScale = collidingTree.transform.Find("TreeMesh").transform.localScale;

                            GameObject treeToDestroy = collidingTree.gameObject;

                            canvasTree.isEnabled = false;

                            playerInteractable.StopInteractingWithTree();

                            playerInteractable.collidingGO = nTree.transform.gameObject;
                            collidingTree = nTree.gameObject;
                            playerInteractable.alreadyCollidingWithGO = true;
                            playerInteractable.interactingWithTree = true;

                            Destroy(treeToDestroy);

                            CanvasTreeInfo.text = "Tree type has been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }
                }
            }

            if (treeType.name == "Wisdom")
            {
                if (collidingTree.tag == "TreeFruits")
                {
                    TreeFruits treeFruits = collidingTree.GetComponent<TreeFruits>();

                    if (treeFruits.level <= 5)
                    {
                        float changeType = Random.Range(0, 100);

                        if (changeType <= 70 - playerStatistics.treesHerd)
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                        else
                        {
                            treeFruits.treeType = "Wisdom Tree";
                            canvasTree.SwitchChange();
                            CanvasTreeInfo.text = "Tree type has been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }

                    if (treeFruits.level > 5)
                    {
                        float changeType = Random.Range(0, 100);

                        if (changeType <= 85 - (playerStatistics.treesHerd))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                        else
                        {
                            treeFruits.treeType = "Wisdom Tree";
                            canvasTree.SwitchChange();
                            CanvasTreeInfo.text = "Tree type has been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }
                }
                if (collidingTree.tag == "TreeFighting")
                {
                    TreeFighting treeFighting = collidingTree.GetComponent<TreeFighting>();
                    float changeType = Random.Range(0, 100);

                    if (treeFighting.level <= 5)
                    {
                        if (changeType <= 85 - (playerStatistics.treesHerd * 0.5f))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }

                        else
                        {
                            GameObject nTree = Instantiate(treeFruitGO);

                            nTree.transform.position = collidingTree.transform.position;
                            nTree.GetComponent<TreeFruits>().treeType = "Wisdom Tree";

                            nTree.transform.Find("TreeMesh").transform.localScale = collidingTree.transform.Find("TreeMesh").transform.localScale;

                            GameObject treeToDestroy = collidingTree.gameObject;

                            canvasTree.isEnabled = false;

                            playerInteractable.StopInteractingWithTree();

                            playerInteractable.collidingGO = nTree.transform.gameObject;
                            collidingTree = nTree.gameObject;
                            playerInteractable.alreadyCollidingWithGO = true;
                            playerInteractable.interactingWithTree = true;

                            Destroy(treeToDestroy);

                            
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }

                    if (treeFighting.level > 5)
                    {
                        if (changeType <= 100 - (playerStatistics.treesHerd))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }

                        else
                        {
                            GameObject nTree = Instantiate(treeFruitGO);

                            nTree.transform.position = collidingTree.transform.position;
                            nTree.GetComponent<TreeFruits>().treeType = "Wisdom Tree";

                            nTree.transform.Find("TreeMesh").transform.localScale = collidingTree.transform.Find("TreeMesh").transform.localScale;

                            GameObject treeToDestroy = collidingTree.gameObject;

                            canvasTree.isEnabled = false;

                            playerInteractable.StopInteractingWithTree();

                            playerInteractable.collidingGO = nTree.transform.gameObject;
                            collidingTree = nTree.gameObject;
                            playerInteractable.alreadyCollidingWithGO = true;
                            playerInteractable.interactingWithTree = true;

                            Destroy(treeToDestroy);

                            
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }
                }
                if (collidingTree.tag == "TreeDefending")
                {
                    TreeDefending treeDefending = collidingTree.GetComponent<TreeDefending>();
                    float changeType = Random.Range(0, 100);

                    if (treeDefending.level <= 5)
                    {
                        if (changeType <= 85 - (playerStatistics.treesHerd))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }

                        else
                        {
                            GameObject nTree = Instantiate(treeFruitGO);

                            nTree.transform.position = collidingTree.transform.position;
                            nTree.GetComponent<TreeFruits>().treeType = "Wisdom Tree";

                            nTree.transform.Find("TreeMesh").transform.localScale = collidingTree.transform.Find("TreeMesh").transform.localScale;

                            GameObject treeToDestroy = collidingTree.gameObject;

                            canvasTree.isEnabled = false;

                            playerInteractable.StopInteractingWithTree();

                            playerInteractable.collidingGO = nTree.transform.gameObject;
                            collidingTree = nTree.gameObject;
                            playerInteractable.alreadyCollidingWithGO = true;
                            playerInteractable.interactingWithTree = true;

                            Destroy(treeToDestroy);

                            
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }

                    if (treeDefending.level > 5)
                    {
                        if (changeType <= 100 - (playerStatistics.treesHerd))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }

                        else
                        {
                            GameObject nTree = Instantiate(treeFruitGO);

                            nTree.transform.position = collidingTree.transform.position;
                            nTree.GetComponent<TreeFruits>().treeType = "Wisdom Tree";

                            nTree.transform.Find("TreeMesh").transform.localScale = collidingTree.transform.Find("TreeMesh").transform.localScale;

                            GameObject treeToDestroy = collidingTree.gameObject;

                            canvasTree.isEnabled = false;

                            playerInteractable.StopInteractingWithTree();

                            playerInteractable.collidingGO = nTree.transform.gameObject;
                            collidingTree = nTree.gameObject;
                            playerInteractable.alreadyCollidingWithGO = true;
                            playerInteractable.interactingWithTree = true;

                            Destroy(treeToDestroy);
                            
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }
                }
            }

            if (treeType.name == "Belt")
            {
                if (collidingTree.tag == "TreeFruits")
                {
                    TreeFruits treeFruits = collidingTree.GetComponent<TreeFruits>();
                    float changeType = Random.Range(0, 100);

                    if (treeFruits.level <= 5)
                    {
                        if (changeType <= 85 - (playerStatistics.treesHerd))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }

                        else
                        {
                            GameObject nTree = Instantiate(treeFightingGO);

                            nTree.transform.position = collidingTree.transform.position;
                            nTree.GetComponent<TreeFighting>().treeType = "Belt Tree";

                            nTree.transform.Find("TreeMesh").transform.localScale = collidingTree.transform.Find("TreeMesh").transform.localScale;

                            GameObject treeToDestroy = collidingTree.gameObject;

                            canvasTree.isEnabled = false;

                            playerInteractable.StopInteractingWithTree();

                            playerInteractable.collidingGO = nTree.transform.gameObject;
                            collidingTree = nTree.gameObject;
                            playerInteractable.alreadyCollidingWithGO = true;
                            playerInteractable.interactingWithTree = true;

                            Destroy(treeToDestroy);
                            
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }

                    if (treeFruits.level > 5)
                    {
                        if (changeType <= 100 - (playerStatistics.treesHerd))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }

                        else
                        {
                            GameObject nTree = Instantiate(treeFightingGO);

                            nTree.transform.position = collidingTree.transform.position;
                            nTree.GetComponent<TreeFighting>().treeType = "Belt Tree";

                            nTree.transform.Find("TreeMesh").transform.localScale = collidingTree.transform.Find("TreeMesh").transform.localScale;

                            GameObject treeToDestroy = collidingTree.gameObject;

                            canvasTree.isEnabled = false;

                            playerInteractable.StopInteractingWithTree();

                            playerInteractable.collidingGO = nTree.transform.gameObject;
                            collidingTree = nTree.gameObject;
                            playerInteractable.alreadyCollidingWithGO = true;
                            playerInteractable.interactingWithTree = true;

                            Destroy(treeToDestroy);

                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }
                }
                if (collidingTree.tag == "TreeFighting")
                {
                    TreeFighting treeFighting = collidingTree.GetComponent<TreeFighting>();
                    float changeType = Random.Range(0, 100);

                    if (treeFighting.level <= 5)
                    {
                        if (changeType <= 70 - playerStatistics.treesHerd)
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                        else
                        {
                            treeFighting.treeType = "Belt Tree";
                            canvasTree.SwitchChange();
                            CanvasTreeInfo.text = "Tree type has been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }

                    if (treeFighting.level > 5)
                    {
                        
                        if (changeType <= 85 - (playerStatistics.treesHerd))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                        else
                        {
                            treeFighting.treeType = "Belt Tree";
                            canvasTree.SwitchChange();
                            CanvasTreeInfo.text = "Tree type has been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }
                }
                if (collidingTree.tag == "TreeDefending")
                {
                    TreeDefending treeDefending = collidingTree.GetComponent<TreeDefending>();
                    float changeType = Random.Range(0, 100);

                    if (treeDefending.level <= 5)
                    {
                        if (changeType <= 85 - (playerStatistics.treesHerd))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }

                        else
                        {
                            GameObject nTree = Instantiate(treeFightingGO);

                            nTree.transform.position = collidingTree.transform.position;
                            nTree.GetComponent<TreeFighting>().treeType = "Belt Tree";

                            nTree.transform.Find("TreeMesh").transform.localScale = collidingTree.transform.Find("TreeMesh").transform.localScale;

                            GameObject treeToDestroy = collidingTree.gameObject;

                            canvasTree.isEnabled = false;

                            playerInteractable.StopInteractingWithTree();

                            playerInteractable.collidingGO = nTree.transform.gameObject;
                            collidingTree = nTree.gameObject;
                            playerInteractable.alreadyCollidingWithGO = true;
                            playerInteractable.interactingWithTree = true;

                            Destroy(treeToDestroy);


                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }

                    if (treeDefending.level > 5)
                    {
                        if (changeType <= 100 - (playerStatistics.treesHerd))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }

                        else
                        {
                            GameObject nTree = Instantiate(treeFruitGO);

                            nTree.transform.position = collidingTree.transform.position;
                            nTree.GetComponent<TreeFighting>().treeType = "Belt Tree";

                            nTree.transform.Find("TreeMesh").transform.localScale = collidingTree.transform.Find("TreeMesh").transform.localScale;

                            GameObject treeToDestroy = collidingTree.gameObject;

                            canvasTree.isEnabled = false;

                            playerInteractable.StopInteractingWithTree();

                            playerInteractable.collidingGO = nTree.transform.gameObject;
                            collidingTree = nTree.gameObject;
                            playerInteractable.alreadyCollidingWithGO = true;
                            playerInteractable.interactingWithTree = true;

                            Destroy(treeToDestroy);

                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }
                }
            }

            if (treeType.name == "Cloud")
            {
                if (collidingTree.tag == "TreeFruits")
                {
                    TreeFruits treeFruits = collidingTree.GetComponent<TreeFruits>();
                    float changeType = Random.Range(0, 100);

                    if (treeFruits.level <= 5)
                    {
                        if (changeType <= 85 - (playerStatistics.treesHerd))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }

                        else
                        {
                            GameObject nTree = Instantiate(treeFightingGO);

                            nTree.transform.position = collidingTree.transform.position;
                            nTree.GetComponent<TreeFighting>().treeType = "Cloud Tree";

                            nTree.transform.Find("TreeMesh").transform.localScale = collidingTree.transform.Find("TreeMesh").transform.localScale;

                            GameObject treeToDestroy = collidingTree.gameObject;

                            canvasTree.isEnabled = false;

                            playerInteractable.StopInteractingWithTree();

                            playerInteractable.collidingGO = nTree.transform.gameObject;
                            collidingTree = nTree.gameObject;
                            playerInteractable.alreadyCollidingWithGO = true;
                            playerInteractable.interactingWithTree = true;

                            Destroy(treeToDestroy);

                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }

                    if (treeFruits.level > 5)
                    {
                        if (changeType <= 100 - (playerStatistics.treesHerd))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }

                        else
                        {
                            GameObject nTree = Instantiate(treeFightingGO);

                            nTree.transform.position = collidingTree.transform.position;
                            nTree.GetComponent<TreeFighting>().treeType = "Cloud Tree";

                            nTree.transform.Find("TreeMesh").transform.localScale = collidingTree.transform.Find("TreeMesh").transform.localScale;

                            GameObject treeToDestroy = collidingTree.gameObject;

                            canvasTree.isEnabled = false;

                            playerInteractable.StopInteractingWithTree();

                            playerInteractable.collidingGO = nTree.transform.gameObject;
                            collidingTree = nTree.gameObject;
                            playerInteractable.alreadyCollidingWithGO = true;
                            playerInteractable.interactingWithTree = true;

                            Destroy(treeToDestroy);

                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }
                }
                if (collidingTree.tag == "TreeFighting")
                {
                    TreeFighting treeFighting = collidingTree.GetComponent<TreeFighting>();
                    float changeType = Random.Range(0, 100);

                    if (treeFighting.level <= 5)
                    {
                        if (changeType <= 70 - playerStatistics.treesHerd)
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                        else
                        {
                            treeFighting.treeType = "Cloud Tree";
                            canvasTree.SwitchChange();
                            CanvasTreeInfo.text = "Tree type has been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }

                    if (treeFighting.level > 5)
                    {

                        if (changeType <= 85 - (playerStatistics.treesHerd))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                        else
                        {
                            treeFighting.treeType = "Cloud Tree";
                            canvasTree.SwitchChange();
                            CanvasTreeInfo.text = "Tree type has been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }
                }
                if (collidingTree.tag == "TreeDefending")
                {
                    TreeDefending treeDefending = collidingTree.GetComponent<TreeDefending>();
                    float changeType = Random.Range(0, 100);

                    if (treeDefending.level <= 5)
                    {
                        if (changeType <= 85 - (playerStatistics.treesHerd))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }

                        else
                        {
                            GameObject nTree = Instantiate(treeFightingGO);

                            nTree.transform.position = collidingTree.transform.position;
                            nTree.GetComponent<TreeFighting>().treeType = "Cloud Tree";

                            nTree.transform.Find("TreeMesh").transform.localScale = collidingTree.transform.Find("TreeMesh").transform.localScale;

                            GameObject treeToDestroy = collidingTree.gameObject;

                            canvasTree.isEnabled = false;

                            playerInteractable.StopInteractingWithTree();

                            playerInteractable.collidingGO = nTree.transform.gameObject;
                            collidingTree = nTree.gameObject;
                            playerInteractable.alreadyCollidingWithGO = true;
                            playerInteractable.interactingWithTree = true;

                            Destroy(treeToDestroy);


                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }

                    if (treeDefending.level > 5)
                    {
                        if (changeType <= 100 - (playerStatistics.treesHerd))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }

                        else
                        {
                            GameObject nTree = Instantiate(treeFruitGO);

                            nTree.transform.position = collidingTree.transform.position;
                            nTree.GetComponent<TreeFighting>().treeType = "Cloud Tree";

                            nTree.transform.Find("TreeMesh").transform.localScale = collidingTree.transform.Find("TreeMesh").transform.localScale;

                            GameObject treeToDestroy = collidingTree.gameObject;

                            canvasTree.isEnabled = false;

                            playerInteractable.StopInteractingWithTree();

                            playerInteractable.collidingGO = nTree.transform.gameObject;
                            collidingTree = nTree.gameObject;
                            playerInteractable.alreadyCollidingWithGO = true;
                            playerInteractable.interactingWithTree = true;

                            Destroy(treeToDestroy);

                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }
                }
            }

            if (treeType.name == "ThickBark")
            {
                if (collidingTree.tag == "TreeFruits")
                {
                    TreeFruits treeFruits = collidingTree.GetComponent<TreeFruits>();
                    float changeType = Random.Range(0, 100);

                    if (treeFruits.level <= 5)
                    {
                        if (changeType <= 85 - (playerStatistics.treesHerd))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }

                        else
                        {
                            GameObject nTree = Instantiate(treeDefendingGO);

                            nTree.transform.position = collidingTree.transform.position;
                            nTree.GetComponent<TreeDefending>().treeType = "Thick Bark Tree";

                            nTree.transform.Find("TreeMesh").transform.localScale = collidingTree.transform.Find("TreeMesh").transform.localScale;

                            GameObject treeToDestroy = collidingTree.gameObject;

                            canvasTree.isEnabled = false;

                            playerInteractable.StopInteractingWithTree();

                            playerInteractable.collidingGO = nTree.transform.gameObject;
                            collidingTree = nTree.gameObject;
                            playerInteractable.alreadyCollidingWithGO = true;
                            playerInteractable.interactingWithTree = true;

                            Destroy(treeToDestroy);

                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }

                    if (treeFruits.level > 5)
                    {
                        if (changeType <= 100 - (playerStatistics.treesHerd))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }

                        else
                        {
                            GameObject nTree = Instantiate(treeDefendingGO);

                            nTree.transform.position = collidingTree.transform.position;
                            nTree.GetComponent<TreeDefending>().treeType = "Thick Bark Tree";

                            nTree.transform.Find("TreeMesh").transform.localScale = collidingTree.transform.Find("TreeMesh").transform.localScale;

                            GameObject treeToDestroy = collidingTree.gameObject;

                            canvasTree.isEnabled = false;

                            playerInteractable.StopInteractingWithTree();

                            playerInteractable.collidingGO = nTree.transform.gameObject;
                            collidingTree = nTree.gameObject;
                            playerInteractable.alreadyCollidingWithGO = true;
                            playerInteractable.interactingWithTree = true;

                            Destroy(treeToDestroy);

                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }
                }
                if (collidingTree.tag == "TreeFighting")
                {
                    TreeFighting treeFighting = collidingTree.GetComponent<TreeFighting>();
                    float changeType = Random.Range(0, 100);

                    if (treeFighting.level <= 5)
                    {
                        if (changeType <= 85 - (playerStatistics.treesHerd))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }

                        else
                        {
                            GameObject nTree = Instantiate(treeDefendingGO);

                            nTree.transform.position = collidingTree.transform.position;
                            nTree.GetComponent<TreeDefending>().treeType = "Thick Bark Tree";

                            nTree.transform.Find("TreeMesh").transform.localScale = collidingTree.transform.Find("TreeMesh").transform.localScale;

                            GameObject treeToDestroy = collidingTree.gameObject;

                            canvasTree.isEnabled = false;

                            playerInteractable.StopInteractingWithTree();

                            playerInteractable.collidingGO = nTree.transform.gameObject;
                            collidingTree = nTree.gameObject;
                            playerInteractable.alreadyCollidingWithGO = true;
                            playerInteractable.interactingWithTree = true;

                            Destroy(treeToDestroy);


                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }

                    if (treeFighting.level > 5)
                    {
                        if (changeType <= 100 - (playerStatistics.treesHerd))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }

                        else
                        {
                            GameObject nTree = Instantiate(treeDefendingGO);

                            nTree.transform.position = collidingTree.transform.position;
                            nTree.GetComponent<TreeDefending>().treeType = "Thick Bark Tree";

                            nTree.transform.Find("TreeMesh").transform.localScale = collidingTree.transform.Find("TreeMesh").transform.localScale;

                            GameObject treeToDestroy = collidingTree.gameObject;

                            canvasTree.isEnabled = false;

                            playerInteractable.StopInteractingWithTree();

                            playerInteractable.collidingGO = nTree.transform.gameObject;
                            collidingTree = nTree.gameObject;
                            playerInteractable.alreadyCollidingWithGO = true;
                            playerInteractable.interactingWithTree = true;

                            Destroy(treeToDestroy);


                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }
                }
                if (collidingTree.tag == "TreeDefending")
                {
                    TreeDefending treeDefending = collidingTree.GetComponent<TreeDefending>();
                    float changeType = Random.Range(0, 100);

                    if (treeDefending.level <= 5)
                    {
                        if (changeType <= 70 - playerStatistics.treesHerd)
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                        else
                        {
                            treeDefending.treeType = "Thick Bark Tree";
                            canvasTree.SwitchChange();
                            CanvasTreeInfo.text = "Tree type has been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }

                    if (treeDefending.level > 5)
                    {

                        if (changeType <= 85 - (playerStatistics.treesHerd))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                        else
                        {
                            treeDefending.treeType = "Thick Bark Tree";
                            canvasTree.SwitchChange();
                            CanvasTreeInfo.text = "Tree type has been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }
                }
            }

            if (treeType.name == "Water")
            {
                if (collidingTree.tag == "TreeFruits")
                {
                    TreeFruits treeFruits = collidingTree.GetComponent<TreeFruits>();
                    float changeType = Random.Range(0, 100);

                    if (treeFruits.level <= 5)
                    {
                        if (changeType <= 85 - (playerStatistics.treesHerd))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }

                        else
                        {
                            GameObject nTree = Instantiate(treeDefendingGO);

                            nTree.transform.position = collidingTree.transform.position;
                            nTree.GetComponent<TreeDefending>().treeType = "Water Tree";

                            nTree.transform.Find("TreeMesh").transform.localScale = collidingTree.transform.Find("TreeMesh").transform.localScale;

                            GameObject treeToDestroy = collidingTree.gameObject;

                            canvasTree.isEnabled = false;

                            playerInteractable.StopInteractingWithTree();

                            playerInteractable.collidingGO = nTree.transform.gameObject;
                            collidingTree = nTree.gameObject;
                            playerInteractable.alreadyCollidingWithGO = true;
                            playerInteractable.interactingWithTree = true;

                            Destroy(treeToDestroy);

                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }

                    if (treeFruits.level > 5)
                    {
                        if (changeType <= 100 - (playerStatistics.treesHerd))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }

                        else
                        {
                            GameObject nTree = Instantiate(treeDefendingGO);

                            nTree.transform.position = collidingTree.transform.position;
                            nTree.GetComponent<TreeDefending>().treeType = "Water Tree";

                            nTree.transform.Find("TreeMesh").transform.localScale = collidingTree.transform.Find("TreeMesh").transform.localScale;

                            GameObject treeToDestroy = collidingTree.gameObject;

                            canvasTree.isEnabled = false;

                            playerInteractable.StopInteractingWithTree();

                            playerInteractable.collidingGO = nTree.transform.gameObject;
                            collidingTree = nTree.gameObject;
                            playerInteractable.alreadyCollidingWithGO = true;
                            playerInteractable.interactingWithTree = true;

                            Destroy(treeToDestroy);

                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }
                }

                if (collidingTree.tag == "TreeFighting")
                {
                    TreeFighting treeFighting = collidingTree.GetComponent<TreeFighting>();
                    float changeType = Random.Range(0, 100);

                    if (treeFighting.level <= 5)
                    {
                        if (changeType <= 85 - (playerStatistics.treesHerd))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }

                        else
                        {
                            GameObject nTree = Instantiate(treeDefendingGO);

                            nTree.transform.position = collidingTree.transform.position;
                            nTree.GetComponent<TreeDefending>().treeType = "Water Tree";

                            nTree.transform.Find("TreeMesh").transform.localScale = collidingTree.transform.Find("TreeMesh").transform.localScale;

                            GameObject treeToDestroy = collidingTree.gameObject;

                            canvasTree.isEnabled = false;

                            playerInteractable.StopInteractingWithTree();

                            playerInteractable.collidingGO = nTree.transform.gameObject;
                            collidingTree = nTree.gameObject;
                            playerInteractable.alreadyCollidingWithGO = true;
                            playerInteractable.interactingWithTree = true;

                            Destroy(treeToDestroy);


                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }

                    if (treeFighting.level > 5)
                    {
                        if (changeType <= 100 - (playerStatistics.treesHerd))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }

                        else
                        {
                            GameObject nTree = Instantiate(treeDefendingGO);

                            nTree.transform.position = collidingTree.transform.position;
                            nTree.GetComponent<TreeDefending>().treeType = "Water Tree";

                            nTree.transform.Find("TreeMesh").transform.localScale = collidingTree.transform.Find("TreeMesh").transform.localScale;

                            GameObject treeToDestroy = collidingTree.gameObject;

                            canvasTree.isEnabled = false;

                            playerInteractable.StopInteractingWithTree();

                            playerInteractable.collidingGO = nTree.transform.gameObject;
                            collidingTree = nTree.gameObject;
                            playerInteractable.alreadyCollidingWithGO = true;
                            playerInteractable.interactingWithTree = true;

                            Destroy(treeToDestroy);


                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }
                }
                if (collidingTree.tag == "TreeDefending")
                {
                    TreeDefending treeDefending = collidingTree.GetComponent<TreeDefending>();
                    float changeType = Random.Range(0, 100);

                    if (treeDefending.level <= 5)
                    {
                        if (changeType <= 70 - playerStatistics.treesHerd)
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                        else
                        {
                            treeDefending.treeType = "Water Tree";
                            canvasTree.SwitchChange();
                            CanvasTreeInfo.text = "Tree type has been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }

                    if (treeDefending.level > 5)
                    {

                        if (changeType <= 85 - (playerStatistics.treesHerd))
                        {
                            CanvasTreeInfo.text = "Tree type has not been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                        else
                        {
                            treeDefending.treeType = "Water Tree";
                            canvasTree.SwitchChange();
                            CanvasTreeInfo.text = "Tree type has been changed";
                            BattleManager.timerBattle = BattleManager.timerBattle - 120f;
                        }
                    }
                }
            }
        }
    }
    
}
