using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CanvasTreeEnable : MonoBehaviour
{
    //Is Enable?
    public bool isEnabled = false;
    GameObject enable;

    //Player
    PlayerManager playerManager;

    //Trees Manager
    TreesManager treesManager;

    //Images
    public Sprite fruitImage;
    public Sprite gemImage;
    public Sprite noImage;


    //Tree Modifications
    public GameObject pray;
    

    //Selected Slot
    public GameObject selectedSlot;

    //Pray
    GameObject swPray;
    public GameObject prayButton;
    public bool skillsOpen;

    //Skills
    public GameObject skills;
    GameObject swSkills;

    //Gems
    public GameObject gems;
    GameObject swGems;
    public GameObject gemUse;
    public GameObject gemDrop;

    //Fruits
    public GameObject fruits;
    public GameObject swFruits;
    public GameObject fruitTake;
    public GameObject fruitRemove;
    public bool fruitsOpen;

    //Change
    GameObject swChange;
    GameObject change;
    GameObject fruitTrees;
    GameObject fightingTrees;
    GameObject defendingTrees;
    GameObject changeClassText;

    //Interacting With Buttons
    PointerEventData eventDataCurrentPosition;

    //Colliding Tree
    public int treePoints;

    bool canAddPoint;

    private void Awake()
    {
        eventDataCurrentPosition = new PointerEventData(EventSystem.current);

        playerManager = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();
        treesManager = GameObject.FindGameObjectWithTag("TreesManager").GetComponent<TreesManager>();
        enable = this.gameObject.transform.Find("Enable").gameObject;

        
        swPray = enable.transform.Find("PanelMiddle").transform.Find("SwitchButtons").transform.Find("SwitchPray").gameObject;
        swSkills = enable.transform.Find("PanelMiddle").transform.Find("SwitchButtons").transform.Find("SwitchSkills").gameObject;
        
        
        //Pray
        pray = enable.transform.Find("PanelRight").transform.Find("Pray").gameObject;
        skills = enable.transform.Find("PanelRight").transform.Find("Skills").gameObject;
        
        

        prayButton = enable.transform.Find("PanelRight").transform.Find("Pray").
            transform.Find("Mask").transform.Find("Order").transform.Find("Button").gameObject;

        //Fruits
        swFruits = enable.transform.Find("PanelMiddle").transform.Find("SwitchButtons").transform.Find("SwitchFruits").gameObject;
        fruits = enable.transform.Find("PanelRight").transform.Find("TreeFruits").gameObject;
        fruitTake = enable.transform.Find("PanelRight").transform.Find("TreeFruits").
            transform.Find("Buttons").transform.Find("Take").gameObject;
        fruitRemove = enable.transform.Find("PanelRight").transform.Find("TreeFruits").
            transform.Find("Buttons").transform.Find("Remove").gameObject;

        //Gems
        swGems = enable.transform.Find("PanelMiddle").transform.Find("SwitchButtons").transform.Find("SwitchGems").gameObject;
        gems = enable.transform.Find("PanelRight").transform.Find("TreeGems").gameObject;
        gemUse = enable.transform.Find("PanelRight").transform.Find("TreeGems").
            transform.Find("Buttons").transform.Find("Use").gameObject;
        gemDrop = enable.transform.Find("PanelRight").transform.Find("TreeGems").
            transform.Find("Buttons").transform.Find("Drop").gameObject;

        //Change
        swChange = enable.transform.Find("PanelMiddle").transform.Find("SwitchButtons").transform.Find("SwitchChange").gameObject;
        change = enable.transform.Find("PanelRight").transform.Find("ChangeTree").gameObject;

        fruitTrees = enable.transform.Find("PanelRight").transform.Find("ChangeTree").transform.Find("Mask").
            transform.Find("Order").transform.Find("TreeTypes").transform.Find("Types").transform.Find("FruitTrees").gameObject;
        fightingTrees = enable.transform.Find("PanelRight").transform.Find("ChangeTree").transform.Find("Mask").
            transform.Find("Order").transform.Find("TreeTypes").transform.Find("Types").transform.Find("FightingTrees").gameObject;
        defendingTrees = enable.transform.Find("PanelRight").transform.Find("ChangeTree").transform.Find("Mask").
            transform.Find("Order").transform.Find("TreeTypes").transform.Find("Types").transform.Find("DefendingTrees").gameObject;
        changeClassText = enable.transform.Find("PanelRight").transform.Find("ChangeTree").transform.Find("Mask").
            transform.Find("Order").transform.Find("TreeTypes").transform.Find("Types").transform.Find("ChooseClassText").gameObject;

    }

    private void Start()
    {
        
        
    }
    private void Update()
    {
        Enabled();
        CanvasButtons();
    }

    public void Enabled()
    {
        if (isEnabled == true)
        {
            enable.SetActive(true);
            ControlManager.canvasTree = true;
        }

        if (isEnabled == false)
        {
            ControlManager.canvasTree = false;
            enable.SetActive(false);
        }
    }

    void CanvasButtons()
    {
        
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Ended)
            {
                PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
                eventDataCurrentPosition.position = new Vector2(touch.position.x, touch.position.y);
                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

                foreach (RaycastResult result in results)
                {
                    //Pray

                    if (result.gameObject == swPray)
                    {
                        SwitchPray();
                    }

                    if (result.gameObject == prayButton)
                    {
                        Pray();
                    }

                    //Skills

                    if (result.gameObject == swSkills)
                    {
                        SwitchSkills();
                    }

                    if (result.gameObject.transform.tag == ("CanvasTreeSkillButton"))
                    {
                        if (treesManager.collidingTree.transform.tag == "TreeFruits")
                        {
                            if (treesManager.collidingTree.GetComponent<TreeFruits>().points <= 0)
                            {
                                CanvasTreeInfo.text = "This Tree doesn't have any points to grow";
                                
                            }
                            if (treesManager.collidingTree.GetComponent<TreeFruits>().points > 0)
                            {
                                result.gameObject.transform.GetComponentInParent<CanvasTreeSkillButton>().AddPoint();
                                
                            }

                        }
                        if (treesManager.collidingTree.transform.tag == "TreeFighting")
                        {
                            if (treesManager.collidingTree.GetComponent<TreeFighting>().points <= 0)
                            {
                                CanvasTreeInfo.text = "This Tree doesn't have any points to grow";
                                
                            }
                            if (treesManager.collidingTree.GetComponent<TreeFighting>().points > 0)
                            {
                                result.gameObject.transform.GetComponentInParent<CanvasTreeSkillButton>().AddPoint();
                                
                            }

                        }
                        if (treesManager.collidingTree.transform.tag == "TreeDefending")
                        {
                            if (treesManager.collidingTree.GetComponent<TreeDefending>().points <= 0)
                            {
                                CanvasTreeInfo.text = "This Tree doesn't have any points to grow";
                                
                            }
                            if (treesManager.collidingTree.GetComponent<TreeDefending>().points > 0)
                            {
                                result.gameObject.transform.GetComponentInParent<CanvasTreeSkillButton>().AddPoint();
                                
                            }
                        }

                    }

                    //Fruits
                    if (result.gameObject == swFruits)
                    {
                        SwitchFruits();
                    }

                    if (result.gameObject.transform.tag == ("CanvasTreeFruitSlot"))
                    {
                        if (result.gameObject.GetComponent<CanvasTreeFruit>().fruitType != null)
                        {
                            SelectFruit(result.gameObject);
                            
                        }
                    }

                    if (result.gameObject == fruitTake)
                    {
                        if (selectedSlot != null)
                        {
                            Debug.Log("Take");
                            TakeFruit();
                            
                        }
                    }

                    if (result.gameObject == fruitRemove)
                    {
                        if (selectedSlot != null)
                        {
                            DropFruit();
                            Debug.Log("Remove");
                            
                        }
                    }

                    //Gems

                    if (result.gameObject == swGems)
                    {
                        SwitchGems();
                    }

                    if (result.gameObject.transform.tag == ("CanvasGem"))
                    {
                        SelectGem(result.gameObject);
                        
                    }

                    if (result.gameObject == gemUse)
                    {
                        if (selectedSlot != null)
                        {
                            UseGem();
                        }
                    }

                    if(result.gameObject == gemDrop)
                    {
                        if (selectedSlot != null)
                        {
                            DropGem();
                        }
                    }

                    //Change

                    if (result.gameObject == swChange)
                    {
                        SwitchChange();
                    }

                    if (result.gameObject.name == "TreeFruits")
                    {
                        FruitTreesList();
                    }

                    if(result.gameObject.name == "TreeFighting")
                    {
                        FightingTreesList();
                    }

                    if(result.gameObject.name == "TreeDefending")
                    {
                        DefendingTreesList();
                    }

                    if (result.gameObject.tag == "CanvasTreeType")
                    {
                        SelectTreeType(result.gameObject);
                    }

                    if(result.gameObject.name == "Change")
                    {
                        if(selectedSlot == null)
                        {
                            CanvasTreeInfo.text = "Choose tree type";
                        }
                        if(selectedSlot != null)
                        {
                            ChangeTreeType();
                        }
                        
                    }
                }
            }
        }
    }

    void SwitchPray()
    {
        selectedSlot = null;
        UnselectAllSlots();

        if (skillsOpen == true)
        {
            treesManager.DeleteTreeSkills();
            skills.SetActive(false);
        }

        gems.SetActive(false);

        if (fruits != null)
        {
            fruits.SetActive(false);
            fruitsOpen = false;
        }
        
        change.SetActive(false);
        pray.SetActive(true);
    }

    public void SwitchSkills()
    {
        
        selectedSlot = null;
        UnselectAllSlots();

        
        
        pray.SetActive(false);
        gems.SetActive(false);
        if (fruits != null)
        {
            fruitsOpen = false;
            fruits.SetActive(false);
        }
        
        
        
        change.SetActive(false);
        skillsOpen = true;
        skills.SetActive(true);
        treesManager.UpdateTreeSkills();
    }

    void SwitchFruits()
    {
        selectedSlot = null;
        UnselectAllSlots();

        if (skillsOpen == true)
        {
            treesManager.DeleteTreeSkills();
            skills.SetActive(false);
        }

        
        pray.SetActive(false);
        gems.SetActive(false);

        
        change.SetActive(false);
        fruitsOpen = true;
        fruits.SetActive(true);
        treesManager.UpdateFruits();
    }

    void SwitchGems()
    {
        selectedSlot = null;
        UnselectAllSlots();

        pray.SetActive(false);
        
        if (skillsOpen == true)
        {
            treesManager.DeleteTreeSkills();
            skills.SetActive(false);
        }
        
        
        if(fruits != null)
        {
            fruits.SetActive(false);
            fruitsOpen = false;
        }
        change.SetActive(false);
        gems.SetActive(true);

        playerManager.UpdateGemsInCanvas();
    }

    public void SwitchChange()
    {
        selectedSlot = null;
        UnselectAllSlots();

        if (skillsOpen == true)
        {
            treesManager.DeleteTreeSkills();
            skills.SetActive(false);
        }

        
        if (fruits != null)
        {
            fruits.SetActive(false);
            fruitsOpen = false;
        }
        gems.SetActive(false);
        change.SetActive(true);

        fruitTrees.SetActive(false);
        defendingTrees.SetActive(false);
        fightingTrees.SetActive(false);
        changeClassText.SetActive(true);
    }


    public void UnselectAllSlots()
    {
        selectedSlot = null;

        GameObject[] fruitSlots = GameObject.FindGameObjectsWithTag("CanvasTreeFruitSlot");

        foreach (GameObject fruitSlot in fruitSlots)
        {
            if(fruitSlot != null)
            {
                fruitSlot.GetComponent<CanvasTreeFruit>().UnselectThisSlot();
            }
        }

        GameObject[] gemsSlots = GameObject.FindGameObjectsWithTag("CanvasGem");
        foreach (GameObject gemSlot in gemsSlots)
        {
            if (gemSlot != null)
            {
                gemSlot.GetComponent<CanvasGem>().UnselectThisSlot();
            }
        }
    }

    void Pray()
    {
        treesManager.Pray();
    }

    //FRUITS
    void SelectFruit(GameObject fruitSlot)
    {
        selectedSlot = null;

        
        GameObject[] fruitSlots = GameObject.FindGameObjectsWithTag("CanvasTreeFruitSlot");
        foreach (GameObject fruitSLOT in fruitSlots)
        {
            fruitSLOT.GetComponent<CanvasTreeFruit>().UnselectThisSlot();
        }

        selectedSlot = fruitSlot;
        selectedSlot.GetComponent<CanvasTreeFruit>().SelectThisSlot();

        if (selectedSlot.GetComponent<CanvasTreeFruit>().fruitType == "Food Fruit")
        {
            CanvasTreeInfo.text = "Food Fruit \n +" + 
                selectedSlot.GetComponent<CanvasTreeFruit>().fruitPower + " Hunger Reduce";
        }
        if (selectedSlot.GetComponent<CanvasTreeFruit>().fruitType == "Fat Fruit")
        {
            CanvasTreeInfo.text = "Fat Fruit \n +" + 
                selectedSlot.GetComponent<CanvasTreeFruit>().fruitPower + " Hunger Resistance";
        }
        if (selectedSlot.GetComponent<CanvasTreeFruit>().fruitType == "Health Fruit")
        {
            CanvasTreeInfo.text = "Health Fruit \n +" +
                selectedSlot.GetComponent<CanvasTreeFruit>().fruitPower + " Current Health";
        }
        if (selectedSlot.GetComponent<CanvasTreeFruit>().fruitType == "Max Health Fruit")
        {
            CanvasTreeInfo.text = "Max Health Fruit \n +" +
                selectedSlot.GetComponent<CanvasTreeFruit>().fruitPower + " Max Health";
        }
        if (selectedSlot.GetComponent<CanvasTreeFruit>().fruitType == "Recovery Fruit")
        {
            CanvasTreeInfo.text = "Recovery Fruit \n +" +
                selectedSlot.GetComponent<CanvasTreeFruit>().fruitPower + " Health Recovery";

        }
        if (selectedSlot.GetComponent<CanvasTreeFruit>().fruitType == "Defence Fruit")
        {
            CanvasTreeInfo.text = "Defence Fruit \n +" +
                selectedSlot.GetComponent<CanvasTreeFruit>().fruitPower + " Defence";
        }
        if (selectedSlot.GetComponent<CanvasTreeFruit>().fruitType == "Energy Fruit")
        {
            CanvasTreeInfo.text = "Energy Fruit \n +" +
                selectedSlot.GetComponent<CanvasTreeFruit>().fruitPower + " Current Stamina";
        }
        if (selectedSlot.GetComponent<CanvasTreeFruit>().fruitType == "Max Energy Fruit")
        {
            CanvasTreeInfo.text = "Max Energy Fruit \n +" +
                selectedSlot.GetComponent<CanvasTreeFruit>().fruitPower + " Max Stamina";
        }
        if (selectedSlot.GetComponent<CanvasTreeFruit>().fruitType == "Breath Fruit")
        {
            CanvasTreeInfo.text = "Breath Fruit \n +" +
                selectedSlot.GetComponent<CanvasTreeFruit>().fruitPower + " Stamina Recovery";
        }
        if (selectedSlot.GetComponent<CanvasTreeFruit>().fruitType == "Light Fruit")
        {
            CanvasTreeInfo.text = "Light Fruit \n +" +
                selectedSlot.GetComponent<CanvasTreeFruit>().fruitPower + " Speed";
        }
        if (selectedSlot.GetComponent<CanvasTreeFruit>().fruitType == "Sharp Fruit")
        {
            CanvasTreeInfo.text = "Sharp Fruit \n +" +
                selectedSlot.GetComponent<CanvasTreeFruit>().fruitPower + " Weapons Sharpened";
        }
        if (selectedSlot.GetComponent<CanvasTreeFruit>().fruitType == "Strength Fruit")
        {
            CanvasTreeInfo.text = "Strength Fruit \n +" +
                selectedSlot.GetComponent<CanvasTreeFruit>().fruitPower + " Strength";
        }
        if (selectedSlot.GetComponent<CanvasTreeFruit>().fruitType == "Wisdom Fruit")
        {
            CanvasTreeInfo.text = "Wisdom Fruit \n +" +
                selectedSlot.GetComponent<CanvasTreeFruit>().fruitPower + " Experience";
        }
        if (selectedSlot.GetComponent<CanvasTreeFruit>().fruitType == "Sage Fruit")
        {
            CanvasTreeInfo.text = "Sage Fruit \n +" +
                selectedSlot.GetComponent<CanvasTreeFruit>().fruitPower + " Wisdom";
        }
    }

    void TakeFruit()
    {
        GameObject[] fruitSlots = GameObject.FindGameObjectsWithTag("CanvasTreeFruitSlot");
        foreach (GameObject fruitSLOT in fruitSlots)
        {
            fruitSLOT.GetComponent<CanvasTreeFruit>().UnselectThisSlot();
        }

        treesManager.AddFruitToPlayer(selectedSlot.GetComponent<CanvasTreeFruit>().fruitType,
            selectedSlot.GetComponent<CanvasTreeFruit>().fruitNumber,
            selectedSlot.GetComponent<CanvasTreeFruit>().fruitPower);

        treesManager.RemoveFruitFromTree(selectedSlot.GetComponent<CanvasTreeFruit>().fruitNumber);
        

        selectedSlot = null;

        
    }

    void DropFruit()
    {
        GameObject[] fruitSlots = GameObject.FindGameObjectsWithTag("CanvasTreeFruitSlot");
        foreach (GameObject fruitSLOT in fruitSlots)
        {
            fruitSLOT.GetComponent<CanvasTreeFruit>().UnselectThisSlot();
        }

        treesManager.RemoveFruitFromTree(selectedSlot.GetComponent<CanvasTreeFruit>().fruitNumber);
        selectedSlot = null;
    }

    //GEMS
    void SelectGem(GameObject gemSlot)
    {
        selectedSlot = null;

        GameObject[] gemsSlots = GameObject.FindGameObjectsWithTag("CanvasGem");
        foreach (GameObject gemSLOT in gemsSlots)
        {
            if (gemSLOT != null)
            {
                gemSLOT.GetComponent<CanvasGem>().UnselectThisSlot();
            }
        }

        selectedSlot = gemSlot;
        selectedSlot.GetComponent<CanvasGem>().SelectThisSlot();

        if (selectedSlot.GetComponent<CanvasGem>().gemType == "Health")
        {
            CanvasTreeInfo.text = "Health Gem \n" + "+ " + selectedSlot.GetComponent<CanvasGem>().gemPower + " Current Health";
        }
        if (selectedSlot.GetComponent<CanvasGem>().gemType == "Exp")
        {
            CanvasTreeInfo.text = "Experience Gem \n" + "+ " + selectedSlot.GetComponent<CanvasGem>().gemPower + " Experience";
        }
        if (selectedSlot.GetComponent<CanvasGem>().gemType == "Fruits")
        {
            CanvasTreeInfo.text = "Fruits Gem \n" + "+ " + selectedSlot.GetComponent<CanvasGem>().gemPower + " Fruits";
        }
        if (selectedSlot.GetComponent<CanvasGem>().gemType == "Super Health")
        {
            CanvasTreeInfo.text = "Super Health Gem \n" + "+ " + selectedSlot.GetComponent<CanvasGem>().gemPower + " Max Health";
        }
        if (selectedSlot.GetComponent<CanvasGem>().gemType == "Recovery")
        {
            CanvasTreeInfo.text = "Recovery Gem \n" + "+ " + selectedSlot.GetComponent<CanvasGem>().gemPower + " Recovery";
        }
        if (selectedSlot.GetComponent<CanvasGem>().gemType == "Super Fruits")
        {
            CanvasTreeInfo.text = "Super Fruits Gem \n" + "+ " + selectedSlot.GetComponent<CanvasGem>().gemPower + " Fruits Growth";
        }
        if (selectedSlot.GetComponent<CanvasGem>().gemType == "Fruits Power")
        {
            CanvasTreeInfo.text = "Fruits Power Gem \n" + "+ " + selectedSlot.GetComponent<CanvasGem>().gemPower + " Fruits Power";
        }
        if (selectedSlot.GetComponent<CanvasGem>().gemType == "Wild Fruits")
        {
            CanvasTreeInfo.text = "Wild Fruits Gem \n" + "+ " + selectedSlot.GetComponent<CanvasGem>().gemPower + " Wild Fruits";
        }
        if (selectedSlot.GetComponent<CanvasGem>().gemType == "Attack")
        {
            CanvasTreeInfo.text = "Power Gem \n" + "+ " + selectedSlot.GetComponent<CanvasGem>().gemPower + " Attack Power";
        }
        if (selectedSlot.GetComponent<CanvasGem>().gemType == "Attack Quality")
        {
            CanvasTreeInfo.text = "Attack Quality Gem \n" + "+ " + selectedSlot.GetComponent<CanvasGem>().gemPower + " Attack Quality";
        }
        if (selectedSlot.GetComponent<CanvasGem>().gemType == "Attack Distance")
        {
            CanvasTreeInfo.text = "Attack Distance \n" + "+ " + selectedSlot.GetComponent<CanvasGem>().gemPower + " Attack Distance";
        }
        if (selectedSlot.GetComponent<CanvasGem>().gemType == "Hydration")
        {
            CanvasTreeInfo.text = "Hydration Gem \n" + "+ " + selectedSlot.GetComponent<CanvasGem>().gemPower + " Hydration";
        }
        if (selectedSlot.GetComponent<CanvasGem>().gemType == "Light Branches")
        {
            CanvasTreeInfo.text = "Light Branches Gem \n" + "+ " + selectedSlot.GetComponent<CanvasGem>().gemPower + " Light Branches";
        }
        if (selectedSlot.GetComponent<CanvasGem>().gemType == "Attraction")
        {
            CanvasTreeInfo.text = "Attraction Gem \n" + "+ " + selectedSlot.GetComponent<CanvasGem>().gemPower + " Attraction";
        }
        if (selectedSlot.GetComponent<CanvasGem>().gemType == "Skill")
        {
            CanvasTreeInfo.text = "Skill Gem \n" + "+ " + selectedSlot.GetComponent<CanvasGem>().gemPower + " To Skills";
        }
        if (selectedSlot.GetComponent<CanvasGem>().gemType == "Level")
        {
            CanvasTreeInfo.text = "Level Gem \n" + "+ " + selectedSlot.GetComponent<CanvasGem>().gemPower + " Level";
        }
    }

    void UseGem()
    {

        GameObject[] gemsSlots = GameObject.FindGameObjectsWithTag("CanvasGem");
        foreach (GameObject gemSLOT in gemsSlots)
        {
            if (gemSLOT != null)
            {
                gemSLOT.GetComponent<CanvasGem>().UnselectThisSlot();
            }
        }

        treesManager.AddPointsFromGemToTree(selectedSlot.GetComponent<CanvasGem>().gemType,
           selectedSlot.GetComponent<CanvasGem>().gemNumber,
           selectedSlot.GetComponent<CanvasGem>().gemPower);

        //We will unselect slot in gems update

        playerManager.UpdateGemsInCanvas();
        
        
        selectedSlot = null;
    }

    //CHANGE TREE TYPE
    //In trees lists we should colour selected slot
    void DropGem()
    {
        GameObject[] gemsSlots = GameObject.FindGameObjectsWithTag("CanvasGem");
        foreach (GameObject gemSLOT in gemsSlots)
        {
            if (gemSLOT != null)
            {
                gemSLOT.GetComponent<CanvasGem>().UnselectThisSlot();
            }
        }

        playerManager.RemoveGem(selectedSlot.GetComponent<CanvasGem>().gemNumber);

        CanvasTreeInfo.text = "Removed Gem";
    }

    void FruitTreesList()
    {
        defendingTrees.SetActive(false);
        fightingTrees.SetActive(false);
        changeClassText.SetActive(false);
        fruitTrees.SetActive(true);

        GameObject[] treeTypes = GameObject.FindGameObjectsWithTag("CanvasTreeType");
        foreach (GameObject tType in treeTypes)
        {
            tType.transform.Find("Panel").gameObject.GetComponent<Image>().color = new Color32(190, 190, 190, 255);
        }

        if (treesManager.collidingTree.tag == "TreeFruits")
        {
            if(treesManager.collidingTree.GetComponent<TreeFruits>().treeType == "Food Tree")
            {
                defendingTrees.SetActive(false);
                fightingTrees.SetActive(false);
                fruitTrees.SetActive(true);

                fruitTrees.transform.Find("Food").gameObject.SetActive(false);
                fruitTrees.transform.Find("Life").gameObject.SetActive(true);
                fruitTrees.transform.Find("Energy").gameObject.SetActive(true);
                fruitTrees.transform.Find("Power").gameObject.SetActive(true);
                fruitTrees.transform.Find("Wisdom").gameObject.SetActive(true);
                
            }
            if (treesManager.collidingTree.GetComponent<TreeFruits>().treeType == "Life Tree")
            {
                defendingTrees.SetActive(false);
                fightingTrees.SetActive(false);
                fruitTrees.SetActive(true);

                fruitTrees.transform.Find("Food").gameObject.SetActive(true);
                fruitTrees.transform.Find("Life").gameObject.SetActive(false);
                fruitTrees.transform.Find("Energy").gameObject.SetActive(true);
                fruitTrees.transform.Find("Power").gameObject.SetActive(true);
                fruitTrees.transform.Find("Wisdom").gameObject.SetActive(true);
            }
            if (treesManager.collidingTree.GetComponent<TreeFruits>().treeType == "Energy Tree")
            {
                defendingTrees.SetActive(false);
                fightingTrees.SetActive(false);
                fruitTrees.SetActive(true);

                fruitTrees.transform.Find("Food").gameObject.SetActive(true);
                fruitTrees.transform.Find("Life").gameObject.SetActive(true);
                fruitTrees.transform.Find("Energy").gameObject.SetActive(false);
                fruitTrees.transform.Find("Power").gameObject.SetActive(true);
                fruitTrees.transform.Find("Wisdom").gameObject.SetActive(true);
            }
            if (treesManager.collidingTree.GetComponent<TreeFruits>().treeType == "Power Tree")
            {
                defendingTrees.SetActive(false);
                fightingTrees.SetActive(false);
                fruitTrees.SetActive(true);

                fruitTrees.transform.Find("Food").gameObject.SetActive(true);
                fruitTrees.transform.Find("Life").gameObject.SetActive(true);
                fruitTrees.transform.Find("Energy").gameObject.SetActive(true);
                fruitTrees.transform.Find("Power").gameObject.SetActive(false);
                fruitTrees.transform.Find("Wisdom").gameObject.SetActive(true);
            }
            if (treesManager.collidingTree.GetComponent<TreeFruits>().treeType == "Wisdom Tree")
            {
                defendingTrees.SetActive(false);
                fightingTrees.SetActive(false);
                fruitTrees.SetActive(true);

                fruitTrees.transform.Find("Food").gameObject.SetActive(true);
                fruitTrees.transform.Find("Life").gameObject.SetActive(true);
                fruitTrees.transform.Find("Energy").gameObject.SetActive(true);
                fruitTrees.transform.Find("Power").gameObject.SetActive(true);
                fruitTrees.transform.Find("Wisdom").gameObject.SetActive(false);
            }
            if (treesManager.collidingTree.GetComponent<TreeFruits>().treeType == "Dream Tree")
            {
                defendingTrees.SetActive(false);
                fightingTrees.SetActive(false);
                fruitTrees.SetActive(true);

                fruitTrees.transform.Find("Food").gameObject.SetActive(true);
                fruitTrees.transform.Find("Life").gameObject.SetActive(true);
                fruitTrees.transform.Find("Energy").gameObject.SetActive(true);
                fruitTrees.transform.Find("Power").gameObject.SetActive(true);
                fruitTrees.transform.Find("Wisdom").gameObject.SetActive(true);
            }
        }
        if (treesManager.collidingTree.tag == "TreeFighting")
        {
            defendingTrees.SetActive(false);
            fightingTrees.SetActive(false);
            fruitTrees.SetActive(true);

            fruitTrees.transform.Find("Food").gameObject.SetActive(true);
            fruitTrees.transform.Find("Life").gameObject.SetActive(true);
            fruitTrees.transform.Find("Energy").gameObject.SetActive(true);
            fruitTrees.transform.Find("Power").gameObject.SetActive(true);
            fruitTrees.transform.Find("Wisdom").gameObject.SetActive(true);
        }
        if (treesManager.collidingTree.tag == "TreeDefending")
        {
            defendingTrees.SetActive(false);
            fightingTrees.SetActive(false);
            fruitTrees.SetActive(true);

            fruitTrees.transform.Find("Food").gameObject.SetActive(true);
            fruitTrees.transform.Find("Life").gameObject.SetActive(true);
            fruitTrees.transform.Find("Energy").gameObject.SetActive(true);
            fruitTrees.transform.Find("Power").gameObject.SetActive(true);
            fruitTrees.transform.Find("Wisdom").gameObject.SetActive(true);
        }


        CanvasTreeInfo.text = "Write here sth";
    }

    void FightingTreesList()
    {
        fruitTrees.SetActive(false);
        defendingTrees.SetActive(false);
        changeClassText.SetActive(false);
        fightingTrees.SetActive(true);

        GameObject[] treeTypes = GameObject.FindGameObjectsWithTag("CanvasTreeType");
        foreach (GameObject tType in treeTypes)
        {
            tType.transform.Find("Panel").gameObject.GetComponent<Image>().color = new Color32(190, 190, 190, 255);
        }

        if (treesManager.collidingTree.tag == "TreeFruits")
        {
            fightingTrees.transform.Find("Belt").gameObject.SetActive(true);
            fightingTrees.transform.Find("Cloud").gameObject.SetActive(true);
        }
        if (treesManager.collidingTree.tag == "TreeFighting")
        {
            if(treesManager.collidingTree.GetComponent<TreeFighting>().treeType == "Belt Tree")
            {
                fightingTrees.transform.Find("Belt").gameObject.SetActive(false);
                fightingTrees.transform.Find("Cloud").gameObject.SetActive(true);
            }
            if (treesManager.collidingTree.GetComponent<TreeFighting>().treeType == "Cloud Tree")
            {
                fightingTrees.transform.Find("Belt").gameObject.SetActive(true);
                fightingTrees.transform.Find("Cloud").gameObject.SetActive(false);
            }
            if(treesManager.collidingTree.GetComponent<TreeFighting>().treeType == "Spell Tree")
            {
                fightingTrees.transform.Find("Belt").gameObject.SetActive(true);
                fightingTrees.transform.Find("Cloud").gameObject.SetActive(true);
            }
        }
        if (treesManager.collidingTree.tag == "TreeDefending")
        {
            fightingTrees.transform.Find("Belt").gameObject.SetActive(true);
            fightingTrees.transform.Find("Cloud").gameObject.SetActive(true);
        }

        CanvasTreeInfo.text = "Write here sth";
    }

    void DefendingTreesList()
    {
        fruitTrees.SetActive(false);
        fightingTrees.SetActive(false);
        changeClassText.SetActive(false);
        defendingTrees.SetActive(true);

        GameObject[] treeTypes = GameObject.FindGameObjectsWithTag("CanvasTreeType");
        foreach (GameObject tType in treeTypes)
        {
            tType.transform.Find("Panel").gameObject.GetComponent<Image>().color = new Color32(190, 190, 190, 255);
        }

        if (treesManager.collidingTree.tag == "TreeFruits")
        {
            defendingTrees.transform.Find("ThickBark").gameObject.SetActive(true);
            defendingTrees.transform.Find("Water").gameObject.SetActive(true);
        }
        if (treesManager.collidingTree.tag == "TreeFighting")
        {
            defendingTrees.transform.Find("ThickBark").gameObject.SetActive(true);
            defendingTrees.transform.Find("Water").gameObject.SetActive(true);
        }
        if (treesManager.collidingTree.tag == "TreeDefending")
        {
            if(treesManager.collidingTree.GetComponent<TreeDefending>().treeType == "Thick Bark Tree")
            {
                defendingTrees.transform.Find("ThickBark").gameObject.SetActive(false);
                defendingTrees.transform.Find("Water").gameObject.SetActive(true);
            }

            if (treesManager.collidingTree.GetComponent<TreeDefending>().treeType == "Water Tree")
            {
                defendingTrees.transform.Find("ThickBark").gameObject.SetActive(true);
                defendingTrees.transform.Find("Water").gameObject.SetActive(false);
            }

            if (treesManager.collidingTree.GetComponent<TreeDefending>().treeType == "Weed Tree")
            {
                defendingTrees.transform.Find("ThickBark").gameObject.SetActive(true);
                defendingTrees.transform.Find("Water").gameObject.SetActive(true);
            }
        }

        CanvasTreeInfo.text = "Write here sth";
    }

    void SelectTreeType(GameObject treeType)
    {
        selectedSlot = null;

        GameObject[] treeTypes = GameObject.FindGameObjectsWithTag("CanvasTreeType");

        foreach(GameObject tType in treeTypes)
        {
            tType.transform.Find("Panel").gameObject.GetComponent<Image>().color = new Color32(190, 190, 190, 255);
        }
        
        selectedSlot = treeType;

        treeType.transform.Find("Panel").gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        
    }

    void ChangeTreeType()
    {
        treesManager.ChangeTreeType(selectedSlot.gameObject);
        selectedSlot = null;
    }
}
