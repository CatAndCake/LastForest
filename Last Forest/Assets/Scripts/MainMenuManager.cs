using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public List<SaveNames> saveNames;
    public GameObject iSaveSlot;

    public Transform saveSlotsHolder;

    public void NewGame()
    {
        SceneManager.LoadScene("Buildings");
    }
    

    public void LoadSaves()
    {
        GameObject[] saveSlots = GameObject.FindGameObjectsWithTag("SaveSlot");

        foreach(GameObject saveSlotD in saveSlots)
        {
            Destroy(saveSlotD);
        }

        saveNames = SaveSystem.LoadSaveNames<List<SaveNames>>("SaveNames");

        for(int i = 0; i < saveNames.Count; i++)
        {
            GameObject saveSlot = Instantiate(iSaveSlot);
            saveSlot.transform.SetParent(saveSlotsHolder);

            saveSlot.GetComponent<SaveSlot>().saveNumber = saveNames[i].SaveNumber;
            saveSlot.GetComponent<SaveSlot>().saveTime = saveNames[i].SaveTime;
            saveSlot.GetComponent<SaveSlot>().ShowName();
        }
    }

    public void LoadGame(int loadNumber)
    {
        SaveManager.loadedGame = true;
        SaveManager.saveName = loadNumber.ToString();
        SceneManager.LoadScene("Buildings");
        
    }

    public void LoadLastRound()
    {

    }

    public void DeleteSave(string loadName)
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
