using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class CanvasMenu : MonoBehaviour
{
    GameObject enable;
    bool isEnabled = false;

    //Save Manager
    SaveManager saveManager;

    //Main Menu
    GameObject panelMenu;

    //Save & Load Menu
    GameObject panelSaveAndLoad;
    public GameObject saveHolder;

    public GameObject selectedSave;

    private void Awake()
    {
        enable = this.gameObject.transform.Find("Enable").gameObject;
        saveManager = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveManager>();

        // Panel Menu
        panelMenu = enable.transform.Find("Frame").transform.Find("Background").transform.Find("PanelMenu").gameObject;

        // Panel SaveAndLoad
        panelSaveAndLoad = enable.transform.Find("Frame").transform.Find("Background").transform.Find("PanelSaveLoad").gameObject;
        saveHolder = enable.transform.Find("Frame").transform.Find("Background").transform.Find("PanelSaveLoad").
            transform.Find("PanelRight").transform.Find("Panel").transform.Find("Background").transform.Find("Holder").gameObject;

        saveManager.saveHolder = saveHolder;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        InteractWithButtons();
    }

    public void Enabled()
    {
        isEnabled = !isEnabled;

        if(isEnabled == true)
        {
            enable.SetActive(true);

            Menu();
        }

        if(isEnabled == false)
        {
            enable.SetActive(false);
        }
    }



    void InteractWithButtons()
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

                    
                    // Panel Menu

                    if(result.gameObject.name == "ResumeGame")
                    {
                        ResumeGame();
                    }

                    if(result.gameObject.name == "SaveAndLoadGame")
                    {
                        SaveAndLoadGame();
                    }

                    if (result.gameObject.name == "QuitGame")
                    {
                        QuitGame();
                    }

                    // Panel Save And Load

                    if(result.gameObject.name == "SLBackMenu")
                    {
                        Menu();
                    }

                    if(result.gameObject.name == "SLNewSave")
                    {
                        SaveGame();
                    }

                    if(result.gameObject.tag == "SaveSlot")
                    {
                        SelectSave(result.gameObject);
                    }

                    if(result.gameObject.name == "SLLoadGame")
                    {
                        if(selectedSave != null)
                        {
                            LoadGame();
                        }
                    }
                    if(result.gameObject.name == "SLDeleteSave")
                    {
                        if(selectedSave != null)
                        {
                            Delete();
                        }
                    }
                }
            }
        }
    }

    // Generic -  might works in all panels

    void Menu()
    {
        //Backs to menu

        panelSaveAndLoad.SetActive(false);

        panelMenu.SetActive(true);
    }

    // Panel Menu
    void ResumeGame()
    {
        Enabled();
    }

    void SaveAndLoadGame()
    {
        //Debug.Log(DateTime.Now);
        panelMenu.SetActive(false);
        panelSaveAndLoad.SetActive(true);
        saveManager.UpdateSaveNames();
    }

    void Options()
    {
        //Autosave?
        //
    }

    void QuitGame()
    {
        saveManager.QuitGame();
    }

    // Panel Save And Load

    void SaveGame()
    {
        saveManager.SaveGame();
    }

    void SelectSave(GameObject saveSlot)
    {
        selectedSave = null;
        GameObject[] saveSlots = GameObject.FindGameObjectsWithTag("SaveSlot");
        foreach(GameObject saveSLOT in saveSlots)
        {
            saveSLOT.GetComponent<SaveSlot>().Unselect();
        }
        selectedSave = saveSlot;
        selectedSave.GetComponent<SaveSlot>().Select();
    }

    void Delete()
    {
        Debug.Log("touched button");
        saveManager.DeleteSave(selectedSave.GetComponent<SaveSlot>().saveNumber);
    }

    void LoadGame()
    {
        saveManager.LoadGame(selectedSave.GetComponent<SaveSlot>().saveNumber.ToString());
        selectedSave = null;
        ResumeGame();
        Enabled();
    }
}
