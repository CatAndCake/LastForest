using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CanvasEquipment : MonoBehaviour
{
    EquipmentInformations equipmentInformations;
    GameObject skills;
    GameObject inventory;
    GameObject gems;
    GameObject food;

    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;
    GameObject[] gemSlots;
    public GameObject selectedSlot;
    float timer;

    private void Awake()
    {
        equipmentInformations = GameObject.FindGameObjectWithTag("EquipmentInformations").GetComponent<EquipmentInformations>();
        //skills = this.gameObject.transform.Find("Equipment").transform.Find("Inventory").gameObject;
        inventory = this.gameObject.transform.Find("Equipment").transform.Find("Inventory").gameObject;
        gems = this.gameObject.transform.Find("Equipment").transform.Find("Gems").gameObject;
        food = this.gameObject.transform.Find("Equipment").transform.Find("Food").gameObject;
    }

    private void Start()
    {
        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();
    }

    private void Update()
    {
        SelectSlot();
    }

    public void SwitchSkills()
    {

    }
    public void SwitchInventory()
    {
        gems.SetActive(false);
        food.SetActive(false);
        inventory.SetActive(true);
    }

    public void SwitchGems()
    {
        food.SetActive(false);
        inventory.SetActive(false);
        gems.SetActive(true);
    }

    public void SwitchFood()
    {
        inventory.SetActive(false);
        gems.SetActive(false);
        food.SetActive(true);
    }

    void SelectSlot()
    {
        float m = Input.GetAxisRaw("Fire1") * Time.deltaTime;
        timer += Time.deltaTime;

        if (m > 0)
        {

            gemSlots = GameObject.FindGameObjectsWithTag("CanvasGem");

            m_PointerEventData = new PointerEventData(m_EventSystem);

            m_PointerEventData.position = Input.mousePosition;


            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            m_Raycaster.Raycast(m_PointerEventData, results);


            foreach (RaycastResult result in results)
            {
                if (result.gameObject.transform.tag == ("CanvasGem"))
                {
                    /*if (result.gameObject.GetComponent<CanvasGem>().image != null)
                    {
                        Debug.Log(result.gameObject.name);
                        UnselectGems();
                        selectedSlot = result.gameObject;
                        SelectedGem();
                    }*/
                }

                if (result.gameObject.transform.tag == ("CanvasDrop"))
                {
                    if (selectedSlot != null)
                    {
                        if (timer >= 0.1f)
                        {
                            Debug.Log("Drop");
                            timer = 0f;
                            DropItem();
                        }
                    }
                }

            }
        }
        
    }
    

    void UnselectGems()
    {
        foreach(GameObject gemSlot in gemSlots)
        {
            gemSlot.GetComponent<CanvasGem>().UnselectThisSlot();
        }
    }

    void SelectedGem()
    {
        selectedSlot.GetComponent<CanvasGem>().SelectThisSlot();
        SlotInfo();
    }

    void DropItem()
    {
        /*selectedSlot.GetComponent<Image>().color = new Color32(170, 170, 170, 200);
        equipmentInformations.RemoveGem(selectedSlot.GetComponent<CanvasGem>().nameGem);
        selectedSlot = null;
        SlotInfo();*/
    }

    void SlotInfo()
    {
        /*GameObject slotInfo = this.gameObject.transform.Find("Equipment").gameObject.transform.Find("SlotInfo").gameObject;
        slotInfo.SetActive(true);
        if(selectedSlot != null)
        {
            GameObject slotInfoText = slotInfo.transform.Find("TextContainer").gameObject.transform.Find("Text").gameObject;
            slotInfoText.GetComponent<Text>().text = (selectedSlot.GetComponent<CanvasGem>().nameGem +
                selectedSlot.GetComponent<CanvasGem>().defence);
        }

        if(selectedSlot == null)
        {
            GameObject slotInfoText = slotInfo.transform.Find("TextContainer").gameObject.transform.Find("Text").gameObject;
            slotInfoText.GetComponent<Text>().text = null;
        }*/
        
    }

    
}
