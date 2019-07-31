using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInteractable : MonoBehaviour
{

    bool interact;

    //Player
    PlayerManager playerManager;

    //Canvas Touch
    GameObject interactionButton;

    //Game Objects
    GameObject equipmentInformations;

    //Enddar
    
    EnddarManager enddarManager;
    bool interactingWithEnddar;

    //General rules 
    public bool interaction;
    public bool alreadyCollidingWithGO = false;
    public GameObject collidingGO;

    //Tree
    CanvasTreeEnable canvasTree;
    public static bool canvasEnable = false;
    TreesManager treesManager;
    public bool interactingWithTree = false;
    public bool canvasTreeIsEnable;

    //Gem
    public bool interactingWithGem;

    //Sword
    bool interactingWithSword;
    

    private void Awake()
    {
        canvasTree = GameObject.FindGameObjectWithTag("CanvasTree").GetComponent<CanvasTreeEnable>();
        treesManager = GameObject.FindGameObjectWithTag("TreesManager").GetComponent<TreesManager>();

        interactionButton = GameObject.FindGameObjectWithTag("CanvasTouch").transform.Find("PanelNoBattle").transform.Find("Interaction").
            transform.Find("Button").transform.gameObject;

        equipmentInformations = GameObject.FindGameObjectWithTag("EquipmentInformations");
        playerManager = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();
        
    }
    private void Update()
    {
        Interact();
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.tag == ("Gem"))
        {
            if (alreadyCollidingWithGO == false)
            {
                collidingGO = other.gameObject;
                interactingWithGem = true;
                alreadyCollidingWithGO = true;
            }
        }

        if (other.transform.tag == ("TreeFruits") ||
            other.transform.tag == "TreeFighting" ||
            other.transform.tag == "TreeDefending")
        {
            if (alreadyCollidingWithGO == false)
            {
                interactingWithTree = true;
                collidingGO = other.gameObject;
                treesManager.collidingTree = other.gameObject;
                alreadyCollidingWithGO = true;
            }
        }
        if(other.transform.tag == "Sword")
        {
            if(alreadyCollidingWithGO == false)
            {
                collidingGO = other.gameObject;
                interactingWithSword = true;
                alreadyCollidingWithGO = true;
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == ("Gem"))
        {
            if (interactingWithGem == true)
            {
                StopInteractingWithGem();
            }
        }

        if (other.transform.tag == ("TreeFruits") ||
            other.transform.tag == "TreeFighting" ||
            other.transform.tag == "TreeDefending")
        {
            if (interactingWithTree == true)
            {
                StopInteractingWithTree();
            }
        }

        if(other.transform.tag == "Sword")
        {
            if(interactingWithSword == true)
            {
                StopInteractingWithSword();
            }
        }
    }

    void Interact()
    {
        // Show with which game object player is interacting
        if(interactingWithTree == true)
        {
            if (ControlManager.battle == true)
            {
                collidingGO.gameObject.transform.Find("Light").gameObject.GetComponent<Light>().intensity = 0;
            }

            if (ControlManager.battle == false)
            {
                collidingGO.gameObject.transform.Find("Light").gameObject.GetComponent<Light>().color = Color.green;
                collidingGO.gameObject.transform.Find("Light").gameObject.GetComponent<Light>().intensity = 1;
            }
        }

        if(interactingWithGem == true)
        {
            if (ControlManager.battle == true)
            {
                collidingGO.gameObject.transform.Find("Light").gameObject.GetComponent<Light>().intensity = 0;
            }

            if (ControlManager.battle == false)
            {
                collidingGO.gameObject.transform.Find("Light").gameObject.GetComponent<Light>().color = Color.blue;
                collidingGO.gameObject.transform.Find("Light").gameObject.GetComponent<Light>().intensity = 1;
            }
        }

        if(interactingWithSword == true)
        {
            if (ControlManager.battle == true)
            {
                collidingGO.gameObject.transform.Find("Light").gameObject.GetComponent<Light>().intensity = 0;
            }

            if (ControlManager.battle == false)
            {
                collidingGO.gameObject.transform.Find("Light").gameObject.GetComponent<Light>().intensity = 1;
            }
        }

        if (alreadyCollidingWithGO == true)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Ended)
                {
                    Touch touchDelete = Input.GetTouch(i);
                    PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);

                    eventDataCurrentPosition.position = new Vector2(touchDelete.position.x, touchDelete.position.y);

                    List<RaycastResult> results = new List<RaycastResult>();

                    EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

                    foreach (RaycastResult result in results)
                    {
                        if (result.gameObject == interactionButton)
                        {
                            
                            //Interact with game object
                            if (interactingWithTree == true)
                            {
                                InteractingWithTree();
                            }
                            if (interactingWithGem == true)
                            {
                                if(playerManager.playerGems.Count > 16)
                                {
                                    //Write here that no more space for gems
                                }
                                if (playerManager.playerGems.Count <= 16)
                                {
                                    InteractingWithGem();
                                }
                            }
                            if(interactingWithSword == true)
                            {
                                if (playerManager.playerSwords.Count > 3)
                                {
                                    // Write here that player can have only 4 swords
                                }

                                if (playerManager.playerSwords.Count <= 3)
                                {
                                    InteractingWithSword();
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    void InteractingWithTree()
    {
        // Can't interact with tree during battle or when Player canvas is opened!
        
        if(ControlManager.battle == false)
        {
            if (ControlManager.canvasPlayer == false)
            {
                canvasTree.isEnabled = !canvasTree.isEnabled;

                if (canvasTree.isEnabled == true)
                {
                    canvasTree.SwitchSkills();
                }
                if (canvasTree.isEnabled == false)
                {
                    if (canvasTree.skillsOpen == true)
                    {
                        treesManager.DeleteTreeSkills();
                    }
                }
            }
        }
    }

    public void StopInteractingWithTree()
    {
        // We can stop interacting with tree only when we are not close to that tree

        interactingWithTree = false;

        if (collidingGO != null)
        {
            collidingGO.gameObject.transform.Find("Light").gameObject.GetComponent<Light>().intensity = 0;
        }
        alreadyCollidingWithGO = false;
        collidingGO = null;
        treesManager.collidingTree = null;
    }

    void InteractingWithGem()
    {
        //Means taking the gem
        playerManager.GetComponent<PlayerManager>().AddGem(
                    collidingGO.GetComponent<GemInformations>().gemType,
                    collidingGO.GetComponent<GemInformations>().gemImage,
                    collidingGO.GetComponent<GemInformations>().gemPower);


        Destroy(collidingGO);
        StopInteractingWithGem();
    }

    void StopInteractingWithGem()
    {
        // We can stop interacting with gem when we take that gem or when we got away from it
        collidingGO.gameObject.transform.Find("Light").gameObject.GetComponent<Light>().intensity = 0;
        alreadyCollidingWithGO = false;
        interactingWithGem = false;
        collidingGO = null;
    }

    void InteractingWithSword()
    {
        playerManager.GetComponent<PlayerManager>().AddSword(
            collidingGO.GetComponent<Sword>().number,
            collidingGO.GetComponent<Sword>().efficiency,
            collidingGO.GetComponent<Sword>().currentEfficiency,
            collidingGO.GetComponent<Sword>().sharpness,
            collidingGO.GetComponent<Sword>().weight,
            collidingGO.GetComponent<Sword>().width);

        Destroy(collidingGO);
        StopInteractingWithSword();
    }

    void StopInteractingWithSword()
    {
        Debug.Log("Stopped intreacting with sword");
        collidingGO.gameObject.transform.Find("Light").gameObject.GetComponent<Light>().intensity = 0;
        alreadyCollidingWithGO = false;
        interactingWithSword = false;
        collidingGO = null;
    }
}
