using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SaveManager : MonoBehaviour
{

    
    // All lists
    public List <SaveNames> saveNames;
    public List <FruitTrees> fruitTrees;

    int lastSaveNumber = 0;

    // Others
    CanvasMenu canvasMenu;
    
    public GameObject saveData;

    public GameObject saveHolder;

    
    // from main menu

    public static bool loadedGame = false;
    public static string saveName;

    private void Awake()
    {
        canvasMenu = GameObject.FindGameObjectWithTag("CanvasMenu").GetComponent<CanvasMenu>();
        
    }

    private void Start()
    {
        if(loadedGame == true)
        {
            Debug.Log(saveName);
            
        }
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Z))
        {
            
            //Debug.Log("Save Manager / Update");
            //saveNames = new List <SaveNames>();
            //SaveSystem.SaveName(saveNames, "SaveNames");
            //UpdateSaveNames();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            
            //Debug.Log("Save Manager / Update");
            //saveNames = new List<SaveNames>();
            //SaveSystem.SaveName(saveNames, "SaveNames");
            //UpdateSaveNames();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            
            
            //Debug.Log("Save Manager / Update");
            //saveNames = new List<SaveNames>();
            //saveNames = SaveSystem.LoadSaveNames<List<SaveNames>>("SaveNames");
            //UpdateSaveNames();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            
            
            //Debug.Log("Save Manager / Update");
            //saveNames.Add(new SaveNames(DateTime.Now.ToString("")));
            //UpdateSaveNames();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("Save Manager / Update");
            saveNames = new List<SaveNames>();
            UpdateSaveNames();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            FindAllFruitTrees();
        }


    }

    void FindAllFruitTrees()
    {
        fruitTrees = new List<FruitTrees>();

        GameObject[] fruitTree = GameObject.FindGameObjectsWithTag("TreeFruits");

        for(int i = 0; i < fruitTree.Length; i++)
        {
            TreeFruits treeFruits = fruitTree[i].GetComponent<TreeFruits>();

            fruitTrees.Add(new FruitTrees(
                treeFruits.treeType,
                treeFruits.level,
                treeFruits.points,
                treeFruits.exp,
                treeFruits.expToUpgrade,
                treeFruits.healthCurrent,
                treeFruits.healthMax,
                treeFruits.healthReg,
                treeFruits.healthRegMax,
                treeFruits.fruits,
                treeFruits.fruitsMax,
                treeFruits.fruitsPower,
                treeFruits.fruitsPowerMax,
                treeFruits.fruitsWild,
                treeFruits.fruitsWildMax,
                new string[treeFruits.fruit.Count],
                new int[treeFruits.fruit.Count],
                new int[treeFruits.fruit.Count],
                new float[3] { treeFruits.transform.position.x, treeFruits.transform.position.y, treeFruits.transform.position.z }));

            for(int f = 0; f < treeFruits.fruit.Count; f++)
            {
                // f - which fruit
                fruitTrees[i].FruitType[f] = treeFruits.fruit[f].fruitType;
                fruitTrees[i].FruitPower[f] = treeFruits.fruit[f].fruitPower;
                fruitTrees[i].FruitNumber[f] = treeFruits.fruit[f].fruitNumber;
            } 
        }
    }

    public void UpdateSaveNames()
    {
        // Najpierw musimy załadować listę
        GameObject[] savesSlot = GameObject.FindGameObjectsWithTag("SaveSlot");
        foreach (GameObject saveSLOT in savesSlot)
        {
            Destroy(saveSLOT);
        }

        saveNames = SaveSystem.LoadSaveNames<List<SaveNames>>("SaveNames");



        for (int i = saveNames.Count - 1; i >= 0; i--)
        {
            GameObject saveName = Instantiate(saveData);
            saveName.transform.SetParent(saveHolder.transform);
            saveName.GetComponent<SaveSlot>().saveNumber = saveNames[i].SaveNumber;
            saveName.GetComponent<SaveSlot>().saveTime = saveNames[i].SaveTime;
            saveName.GetComponent<SaveSlot>().ShowName();
            //saveName string save name
        }

        if(saveNames.Count > 0)
        {
            lastSaveNumber = saveNames[saveNames.Count - 1].SaveNumber;
        }
        if(saveNames.Count == 0)
        {
            lastSaveNumber = 0;
        }

        Debug.Log(saveNames.Count);
        Debug.Log(lastSaveNumber);

    }


    public void SaveGame()
    {
        

        string nSaveTime = DateTime.Now.ToString("");
        int nSaveNumber = lastSaveNumber + 1;

        saveNames.Add(new SaveNames(nSaveNumber, nSaveTime));

        SaveSystem.SaveName(saveNames, "SaveNames");

        FindAllFruitTrees();
        // znajdź dane o drzewach atakujacych
        // znajdz dane o drzewach defensywnych
        // znajdz dane o graczu


        SaveSystem.SaveGame(fruitTrees, "fruit trees", nSaveNumber.ToString());
        // zapisz dane o drzewach atakujacych 
        // i tak dalej...


        UpdateSaveNames();
        
    }

    

    public void LoadGame(string saveName)
    {
        fruitTrees = SaveSystem.LoadGame<List<FruitTrees>>("fruit trees", saveName);
    }

    public void DeleteSave(int saveNumber)
    {
        for(int i = 0; i < saveNames.Count; i++)
        {
            if(saveNames[i].SaveNumber == saveNumber)
            {
                saveNames.Remove(saveNames[i]);
            }
        }

        SaveSystem.SaveName(saveNames, "SaveNames");
        
        SaveSystem.Delete(saveNumber.ToString());

        UpdateSaveNames();
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }



   
}
