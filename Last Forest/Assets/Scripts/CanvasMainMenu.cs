using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasMainMenu : MonoBehaviour
{


    MainMenuManager menuManager;

    //Panels
    GameObject mainMenu;
    GameObject loadMenu;

    //Saves
    public GameObject saveSlot;
    GameObject selectedSave;

    private void Awake()
    {
        menuManager = GameObject.FindGameObjectWithTag("MenuManager").GetComponent<MainMenuManager>();
        mainMenu = this.gameObject.transform.Find("MainMenu").gameObject;
        loadMenu = this.gameObject.transform.Find("LoadMenu").gameObject;
        menuManager.saveSlotsHolder = loadMenu.transform.Find("PanelRight").transform.Find("Frame").transform.Find("Background").transform.Find("Holder").gameObject.transform;
    }

    private void Start()
    {
        BackToMenu();
    }

    private void Update()
    {
        InteractWithButtons();
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
                    // Generic

                    if(result.gameObject.name == "Menu")
                    {
                        BackToMenu();
                    }
                    // Main Menu
                    if(result.gameObject.name == "NewGame")
                    {
                        NewGame();
                    }
                    if(result.gameObject.name == "LoadGame")
                    {
                        LoadGame();
                    }
                    if(result.gameObject.name == "Options")
                    {
                        Options();
                    }
                    if(result.gameObject.name == "Quit")
                    {
                        QuitGame();
                    }

                    // Load Menu

                    if(result.gameObject.tag == "SaveSlot")
                    {
                        SelectSaveSlot(result.gameObject);
                    }

                    if (result.gameObject.name == "Load")
                    {
                        if (selectedSave == null)
                        {
                            Debug.Log("First, select save");
                            // stwórzmy okienko, które będzie wyskakiwało
                        }
                        if (selectedSave != null)
                        {
                            Load();
                        }
                    }

                    if(result.gameObject.name == "LoadLast")
                    {
                        LoadLastRound();
                    }

                    if(result.gameObject.name == "Delete")
                    {
                        if (selectedSave == null)
                        {
                            Debug.Log("First, select save");
                            // stwórzmy okienko, które będzie wyskakiwało
                        }
                        if (selectedSave != null)
                        {
                            DeleteSave();
                        }
                    }
                }
            }
        }
    }

    // Generic

    void BackToMenu()
    {
        selectedSave = null; 
        loadMenu.SetActive(false);

        mainMenu.SetActive(true);
    }    

    // Main Menu
    void NewGame()
    {
        Debug.Log("Menu New Game");
        menuManager.NewGame();
    }

    void LoadGame()
    {
        mainMenu.SetActive(false);

        loadMenu.SetActive(true);
        menuManager.LoadSaves();
        // następnie wczytuemy listę z nazwami savów oraz pokazujemy ją w holderze w panelu right
    }

    void Options()
    {

    }

    void QuitGame()
    {
        menuManager.QuitGame();
    }

    // Load Menu 

    void SelectSaveSlot(GameObject nSaveSlot)
    {
        selectedSave = null; 
        GameObject[] saveSlots = GameObject.FindGameObjectsWithTag("SaveSlot");

        foreach(GameObject saveSLOT in saveSlots)
        {
            saveSLOT.GetComponent<SaveSlot>().Unselect();
        }

        selectedSave = nSaveSlot;

        selectedSave.GetComponent<SaveSlot>().Select();
    }

    void Load()
    {

        menuManager.LoadGame(selectedSave.GetComponent<SaveSlot>().saveNumber);

        selectedSave = null;

        // musimy załadować level


        // następnie musimy załadować pozostałe rzeczy na tamtym levelu
        // może uda się to zrobić za pomocą staic? a może dont destroy on load
    }

    void LoadLastRound()
    {
        
    }

    void DeleteSave()
    {
        
    }


}
