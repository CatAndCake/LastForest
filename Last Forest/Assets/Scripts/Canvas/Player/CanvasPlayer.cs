using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CanvasPlayer : MonoBehaviour
{
    //Images for this canvas
    public Sprite swordImage;
    public Sprite noImage;

    //Player Manager
    PlayerManager playerManager;

    //Enable canvas
    GameObject enable;
    bool isEnable;
    GameObject developmentButton;

    // Skills 
    GameObject skills;
    GameObject swSkills;

    // Swords
    GameObject swords;
    GameObject swSwords;
    public GameObject sharpFruits;

    //Fruits
    GameObject fruits;
    GameObject swFruits;
    GameObject fruitEat;
    GameObject fruitDrop;

    // Gems
    GameObject gems;
    GameObject swGems;
    GameObject gemDrop;


    //Interacting With Buttons
    PointerEventData eventDataCurrentPosition;

    //SelectedSlot
    GameObject selectedSlot;
    GameObject selectedSharpFruit;

    //timer
    float timer;

    private void Awake()
    {
        enable = this.gameObject.transform.Find("Enable").gameObject;
        playerManager = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();
        developmentButton = GameObject.FindGameObjectWithTag("CanvasTouch").transform.Find("PanelNoBattle").transform.Find("PlayerDevelopment").gameObject;
        eventDataCurrentPosition = new PointerEventData(EventSystem.current);

        // Skills
        skills = this.gameObject.transform.Find("Enable").transform.Find("PanelRight").transform.Find("PlayerSkills").gameObject;
        swSkills = this.gameObject.transform.Find("Enable").transform.Find("PanelMiddle").transform.Find("SwitchButtons").
            transform.Find("SwitchSkills").gameObject;

        // Swords
        swords = this.gameObject.transform.Find("Enable").transform.Find("PanelRight").transform.Find("PlayerSwords").gameObject;
        swSwords = this.gameObject.transform.Find("Enable").transform.Find("PanelMiddle").transform.Find("SwitchButtons").
            transform.Find("SwitchSwords").gameObject;
        sharpFruits = this.gameObject.transform.Find("Enable").transform.Find("PanelRight").
            transform.Find("PlayerSwords").transform.Find("SharpFruits").gameObject;
        // Fruits
        fruits = this.gameObject.transform.Find("Enable").transform.Find("PanelRight").transform.Find("PlayerFruits").gameObject;
        swFruits = this.gameObject.transform.Find("Enable").transform.Find("PanelMiddle").transform.Find("SwitchButtons").
            transform.Find("SwitchFruits").gameObject;
        fruitEat = this.gameObject.transform.Find("Enable").transform.Find("PanelRight").transform.Find("PlayerFruits").
            transform.Find("Buttons").transform.Find("Eat").gameObject;
        fruitDrop = this.gameObject.transform.Find("Enable").transform.Find("PanelRight").transform.Find("PlayerFruits").
            transform.Find("Buttons").transform.Find("Drop").gameObject;

        // Gems
        gems = this.gameObject.transform.Find("Enable").transform.Find("PanelRight").transform.Find("PlayerGems").gameObject;
        swGems = this.gameObject.transform.Find("Enable").transform.Find("PanelMiddle").transform.Find("SwitchButtons").
            transform.Find("SwitchGems").gameObject;
        gemDrop = this.gameObject.transform.Find("Enable").transform.Find("PanelRight").transform.Find("PlayerGems").
            transform.Find("Buttons").transform.Find("Drop").gameObject;
    }

    public void EnableDisableCanvas()
    {
        if(ControlManager.canvasTree == false)
        {
            isEnable = !isEnable;

            if (isEnable == true)
            {
                ControlManager.canvasPlayer = true;
                enable.SetActive(true);
                SwitchSkills();
            }

            if (isEnable == false)
            {
                ControlManager.canvasPlayer = false;
                enable.SetActive(false);
            }
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        InteractingWithCanvas();
    }

    void InteractingWithCanvas()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
                eventDataCurrentPosition.position = new Vector2(touch.position.x, touch.position.y);
                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

                foreach (RaycastResult result in results)
                {
                    //Enable button
                    if(result.gameObject == developmentButton)
                    {
                        EnableDisableCanvas();
                    }

                    //Skills
                    if (result.gameObject == swSkills)
                    {
                        SwitchSkills();
                    }

                    if (result.gameObject.transform.tag == ("CanvasPlayerSwitchWeapons"))
                    {
                        //SwitchWeapons();
                        Debug.Log("We will create it later");
                    }

                    // Swords
                    if(result.gameObject == swSwords)
                    {
                        SwitchSwords();
                    }

                    if(result.gameObject.tag == "CanvasSword")
                    {
                        if(result.gameObject.GetComponent<CanvasSword>().efficiency >= 80)
                        {
                            SelectSword(result.gameObject);
                        }
                    }

                    if (result.gameObject.tag == "CanvasSharpFruit")
                    {
                        if (selectedSlot == null)
                        {
                            CanvasPlayerInfo.text = "First, Select a sword";
                        }
                        if (selectedSlot != null)
                        {
                            if (selectedSlot.GetComponent<CanvasSword>().efficiency >= 80)
                            {
                                if (result.gameObject.GetComponent<CanvasSharpFruit>().fruitType == "Sharp Fruit")
                                {
                                    SelectSharpFruit(result.gameObject);
                                }
                            }
                        }
                    }

                    if (result.gameObject.name == "Hold")
                    {
                        if (selectedSlot != null)
                        {
                            if(selectedSlot.GetComponent<CanvasSword>().efficiency >= 80)
                            {
                                HoldSword();
                            }
                        }
                    }

                    if(result.gameObject.name == "Polish")
                    {
                        if(selectedSlot == null)
                        {
                            CanvasPlayerInfo.text = "Select which sword You want to polish then select which sharp fruit you want to use for it";
                        }
                        if(selectedSlot != null)
                        {
                            if(selectedSharpFruit == null)
                            {
                                CanvasPlayerInfo.text = "Select which sharp fruit You want to use for polishing sword";
                            }
                            if(selectedSharpFruit != null)
                            {
                                PolishSword();
                            }
                        }
                    }

                    if(result.gameObject.name == "DropSword")
                    {
                        if(selectedSlot == null)
                        {
                            CanvasPlayerInfo.text = "Select a sword";
                        }
                        if(selectedSlot != null)
                        {
                            if(selectedSlot.GetComponent<CanvasSword>().efficiency >= 80)
                            {
                                DropSword();
                            }
                        }
                    }
                    
                    // Fruits

                    if (result.gameObject == swFruits)
                    {
                        SwitchFruits();
                    }

                    if (result.gameObject.transform.tag == ("CanvasTreeFruitSlot"))
                    {
                        SelectFruit(result.gameObject);
                    }

                    if (result.gameObject == fruitEat)
                    {
                        if (selectedSlot != null)
                        {
                            EatFruit();
                        }

                    }

                    if (result.gameObject == fruitDrop)
                    {
                        if (selectedSlot != null)
                        {
                            DropFruit();
                        }
                    }

                    // Gems

                    if (result.gameObject == swGems)
                    {
                        SwitchGems();
                    }

                    if (result.gameObject.transform.tag == ("CanvasGem"))
                    {
                        Debug.Log("Gem");
                        SelectGem(result.gameObject);
                    }

                    if (result.gameObject == gemDrop)
                    {
                        DropGem();
                    }
                }
            }
        }
    }

    void SwitchSkills()
    {
        skills.SetActive(true);
        swords.SetActive(false);
        fruits.SetActive(false);
        gems.SetActive(false);

        playerManager.UpdateSkillsInPlayerCancas();
    }

    void SwitchSwords()
    {
        skills.SetActive(false);
        fruits.SetActive(false);
        gems.SetActive(false);

        swords.SetActive(true);
        UnselectAllSlots();
        selectedSharpFruit = null; 
        playerManager.UpdateSwordsInCanvas();
    }

    void SwitchFruits()
    {
        skills.SetActive(false);
        swords.SetActive(false);
        fruits.SetActive(true);
        gems.SetActive(false);

        UnselectAllSlots();
        playerManager.UpdateFruitsInPlayerCanvas();
    }
    

    void SwitchGems()
    {
        skills.SetActive(false);
        swords.SetActive(false);
        fruits.SetActive(false);
        gems.SetActive(true);

        UnselectAllSlots();
        playerManager.UpdateGemsInCanvas();
    }

    public void UnselectAllSlots()
    {
        selectedSlot = null;

        GameObject[] swordSlots = GameObject.FindGameObjectsWithTag("CanvasSword");

        foreach (GameObject swordSlot in swordSlots)
        {
            if(swordSlot != null)
            {
                swordSlot.GetComponent<CanvasSword>().UnselectThisSlot();
            }
        }

        GameObject[] fruitSlots = GameObject.FindGameObjectsWithTag("CanvasTreeFruitSlot");

        foreach (GameObject fruitSlot in fruitSlots)
        {
            if (fruitSlot != null)
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

    //Sword
    void SelectSword(GameObject swordSlot)
    {
        selectedSlot = null;

        GameObject[] swordSlots = GameObject.FindGameObjectsWithTag("CanvasSword");
        foreach (GameObject swordSLOT in swordSlots)
        {
            swordSLOT.GetComponent<CanvasSword>().UnselectThisSlot();
        }

        selectedSlot = swordSlot;
        selectedSlot.GetComponent<CanvasSword>().SelectThisSlot();
    }
    
    void HoldSword()
    {
        GameObject[] swordSlots = GameObject.FindGameObjectsWithTag("CanvasSword");
        foreach (GameObject swordSlot in swordSlots)
        {
            swordSlot.GetComponent<CanvasSword>().UnselectThisSlot();
        }

        playerManager.HoldSword(selectedSlot.GetComponent<CanvasSword>().number);
        selectedSlot = null;

        SwitchSwords();
    }

    void DropSword()
    {
        GameObject[] swordSlots = GameObject.FindGameObjectsWithTag("CanvasSword");
        foreach (GameObject swordSlot in swordSlots)
        {
            swordSlot.GetComponent<CanvasSword>().UnselectThisSlot();
        }
        playerManager.RemoveSword(selectedSlot.GetComponent<CanvasSword>().number);

        selectedSlot = null;
        SwitchSwords();
    }

    void SelectSharpFruit(GameObject sharpFruit)
    {
        selectedSharpFruit = null; 

        GameObject[] sharpFruits = GameObject.FindGameObjectsWithTag("CanvasSharpFruit");
        foreach(GameObject sharpFRUIT in sharpFruits)
        {
            sharpFRUIT.GetComponent<CanvasSharpFruit>().UnselectThisSlot();
        }

        selectedSharpFruit = sharpFruit;
        selectedSharpFruit.GetComponent<CanvasSharpFruit>().SelectThisSlot();
    }

    void PolishSword()
    {
        GameObject[] swordSlots = GameObject.FindGameObjectsWithTag("CanvasSword");
        foreach (GameObject swordSlot in swordSlots)
        {
            swordSlot.GetComponent<CanvasSword>().UnselectThisSlot();
        }

        GameObject[] sharpFruits = GameObject.FindGameObjectsWithTag("CanvasSharpFruit");
        foreach (GameObject sharpFRUIT in sharpFruits)
        {
            sharpFRUIT.GetComponent<CanvasSharpFruit>().UnselectThisSlot();
        }

        playerManager.AddPointsFromFruitToSword
            (selectedSharpFruit.GetComponent<CanvasSharpFruit>().fruitPower, selectedSlot.GetComponent<CanvasSword>().number);

        playerManager.RemoveFruit(selectedSharpFruit.GetComponent<CanvasSharpFruit>().fruitNumber);
        
        selectedSlot = null;
        selectedSharpFruit = null;

        playerManager.UpdateSwordsInCanvas();
    }

    

    //Fruit
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
            CanvasPlayerInfo.text = "Food Fruit \n +" +
                selectedSlot.GetComponent<CanvasTreeFruit>().fruitPower + " Hunger Reduce";
        }
        if (selectedSlot.GetComponent<CanvasTreeFruit>().fruitType == "Fat Fruit")
        {
            CanvasPlayerInfo.text = "Fat Fruit \n +" +
                selectedSlot.GetComponent<CanvasTreeFruit>().fruitPower + " Hunger Resistance";
        }
        if (selectedSlot.GetComponent<CanvasTreeFruit>().fruitType == "Health Fruit")
        {
            CanvasPlayerInfo.text = "Health Fruit \n +" +
                selectedSlot.GetComponent<CanvasTreeFruit>().fruitPower + " Current Health";
        }
        if (selectedSlot.GetComponent<CanvasTreeFruit>().fruitType == "Max Health Fruit")
        {
            CanvasPlayerInfo.text = "Max Health Fruit \n +" +
                selectedSlot.GetComponent<CanvasTreeFruit>().fruitPower + " Max Health";
        }
        if (selectedSlot.GetComponent<CanvasTreeFruit>().fruitType == "Recovery Fruit")
        {
            CanvasPlayerInfo.text = "Recovery Fruit \n +" +
                selectedSlot.GetComponent<CanvasTreeFruit>().fruitPower + " Health Recovery";

        }
        if (selectedSlot.GetComponent<CanvasTreeFruit>().fruitType == "Defence Fruit")
        {
            CanvasPlayerInfo.text = "Defence Fruit \n +" +
                selectedSlot.GetComponent<CanvasTreeFruit>().fruitPower + " Defence";
        }
        if (selectedSlot.GetComponent<CanvasTreeFruit>().fruitType == "Energy Fruit")
        {
            CanvasPlayerInfo.text = "Energy Fruit \n +" +
                selectedSlot.GetComponent<CanvasTreeFruit>().fruitPower + " Current Stamina";
        }
        if (selectedSlot.GetComponent<CanvasTreeFruit>().fruitType == "Max Energy Fruit")
        {
            CanvasPlayerInfo.text = "Max Energy Fruit \n +" +
                selectedSlot.GetComponent<CanvasTreeFruit>().fruitPower + " Max Stamina";
        }
        if (selectedSlot.GetComponent<CanvasTreeFruit>().fruitType == "Breath Fruit")
        {
            CanvasPlayerInfo.text = "Breath Fruit \n +" +
                selectedSlot.GetComponent<CanvasTreeFruit>().fruitPower + " Stamina Recovery";
        }
        if (selectedSlot.GetComponent<CanvasTreeFruit>().fruitType == "Light Fruit")
        {
            CanvasPlayerInfo.text = "Light Fruit \n +" +
                selectedSlot.GetComponent<CanvasTreeFruit>().fruitPower + " Speed";
        }
        if (selectedSlot.GetComponent<CanvasTreeFruit>().fruitType == "Sharp Fruit")
        {
            CanvasPlayerInfo.text = "Sharp Fruit \n +" +
                selectedSlot.GetComponent<CanvasTreeFruit>().fruitPower + " Weapons Sharpened";
        }
        if (selectedSlot.GetComponent<CanvasTreeFruit>().fruitType == "Strength Fruit")
        {
            CanvasPlayerInfo.text = "Strength Fruit \n +" +
                selectedSlot.GetComponent<CanvasTreeFruit>().fruitPower + " Strength";
        }
        if (selectedSlot.GetComponent<CanvasTreeFruit>().fruitType == "Wisdom Fruit")
        {
            CanvasPlayerInfo.text = "Wisdom Fruit \n +" +
                selectedSlot.GetComponent<CanvasTreeFruit>().fruitPower + " Experience";
        }
        if (selectedSlot.GetComponent<CanvasTreeFruit>().fruitType == "Sage Fruit")
        {
            CanvasPlayerInfo.text = "Sage Fruit \n +" +
                selectedSlot.GetComponent<CanvasTreeFruit>().fruitPower + " Wisdom";
        }
    }

    void EatFruit()
    {
        GameObject[] fruitSlots = GameObject.FindGameObjectsWithTag("CanvasTreeFruitSlot");

        foreach (GameObject fruitSlot in fruitSlots)
        {
            if (fruitSlot != null)
            {
                fruitSlot.GetComponent<CanvasTreeFruit>().UnselectThisSlot();
            }
        }

        playerManager.EatFruit(selectedSlot.GetComponent<CanvasTreeFruit>().fruitType,
            selectedSlot.GetComponent<CanvasTreeFruit>().fruitPower,
            selectedSlot.GetComponent<CanvasTreeFruit>().fruitNumber);
        selectedSlot = null;

        SwitchFruits();

    }

    void DropFruit()
    {
        playerManager.RemoveFruit(selectedSlot.GetComponent<CanvasTreeFruit>().fruitNumber);
        selectedSlot = null;

        SwitchFruits();
    }

    void SelectGem(GameObject gemSlot)
    {
        selectedSlot = null;

        GameObject[] gemSlots = GameObject.FindGameObjectsWithTag("CanvasGem");
        foreach (GameObject gemSLOT in gemSlots)
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
            CanvasPlayerInfo.text = "Health Gem \n" + "+ " + selectedSlot.GetComponent<CanvasGem>().gemPower + " Current Health";
        }
        if (selectedSlot.GetComponent<CanvasGem>().gemType == "Exp")
        {
            CanvasPlayerInfo.text = "Experience Gem \n" + "+ " + selectedSlot.GetComponent<CanvasGem>().gemPower + " Experience";
        }
        if (selectedSlot.GetComponent<CanvasGem>().gemType == "Fruits")
        {
            CanvasPlayerInfo.text = "Fruits Gem \n" + "+ " + selectedSlot.GetComponent<CanvasGem>().gemPower + " Fruits";
        }
        if (selectedSlot.GetComponent<CanvasGem>().gemType == "Super Health")
        {
            CanvasPlayerInfo.text = "Super Health Gem \n" + "+ " + selectedSlot.GetComponent<CanvasGem>().gemPower + " Max Health";
        }
        if (selectedSlot.GetComponent<CanvasGem>().gemType == "Recovery")
        {
            CanvasPlayerInfo.text = "Recovery Gem \n" + "+ " + selectedSlot.GetComponent<CanvasGem>().gemPower + " Recovery";
        }
        if (selectedSlot.GetComponent<CanvasGem>().gemType == "Super Fruits")
        {
            CanvasPlayerInfo.text = "Super Fruits Gem \n" + "+ " + selectedSlot.GetComponent<CanvasGem>().gemPower + " Fruits Growth";
        }
        if (selectedSlot.GetComponent<CanvasGem>().gemType == "Fruits Power")
        {
            CanvasPlayerInfo.text = "Fruits Power Gem \n" + "+ " + selectedSlot.GetComponent<CanvasGem>().gemPower + " Fruits Power";
        }
        if (selectedSlot.GetComponent<CanvasGem>().gemType == "Wild Fruits")
        {
            CanvasPlayerInfo.text = "Wild Fruits Gem \n" + "+ " + selectedSlot.GetComponent<CanvasGem>().gemPower + " Wild Fruits";
        }
        if (selectedSlot.GetComponent<CanvasGem>().gemType == "Attack")
        {
            CanvasPlayerInfo.text = "Power Gem \n" + "+ " + selectedSlot.GetComponent<CanvasGem>().gemPower + " Attack Power";
        }
        if (selectedSlot.GetComponent<CanvasGem>().gemType == "Attack Quality")
        {
            CanvasPlayerInfo.text = "Attack Quality Gem \n" + "+ " + selectedSlot.GetComponent<CanvasGem>().gemPower + " Attack Quality";
        }
        if (selectedSlot.GetComponent<CanvasGem>().gemType == "Attack Distance")
        {
            CanvasPlayerInfo.text = "Attack Distance \n" + "+ " + selectedSlot.GetComponent<CanvasGem>().gemPower + " Attack Distance";
        }
        if (selectedSlot.GetComponent<CanvasGem>().gemType == "Hydration")
        {
            CanvasPlayerInfo.text = "Hydration Gem \n" + "+ " + selectedSlot.GetComponent<CanvasGem>().gemPower + " Hydration";
        }
        if (selectedSlot.GetComponent<CanvasGem>().gemType == "Skill")
        {
            CanvasPlayerInfo.text = "Skill Gem \n" + "+ " + selectedSlot.GetComponent<CanvasGem>().gemPower + " To Skills";
        }
        if (selectedSlot.GetComponent<CanvasGem>().gemType == "Level")
        {
            CanvasPlayerInfo.text = "Level Gem \n" + "+ " + selectedSlot.GetComponent<CanvasGem>().gemPower + " Level";
        }

        selectedSlot = null;
    }

    void DropGem()
    {
        GameObject[] gemSlots = GameObject.FindGameObjectsWithTag("CanvasGem");
        foreach (GameObject gemSLOT in gemSlots)
        {
            if (gemSLOT != null)
            {
                gemSLOT.GetComponent<CanvasGem>().UnselectThisSlot();
            }
        }

        playerManager.RemoveGem(selectedSlot.GetComponent<CanvasGem>().gemNumber);
        selectedSlot = null;
    }


}
