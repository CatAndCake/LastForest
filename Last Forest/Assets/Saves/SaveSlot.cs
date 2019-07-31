using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    public int saveNumber;
    public string saveTime;

    public void ShowName()
    {
        this.gameObject.transform.Find("Background").transform.Find("Text").GetComponent<Text>().text = "S" + saveNumber + "  " + saveTime;
    }

    public void Select()
    {
        this.gameObject.transform.Find("Background").GetComponent<Image>().color = new Color32(250, 250, 250, 250);
    }

    public void Unselect()
    {
        this.gameObject.transform.Find("Background").GetComponent<Image>().color = new Color32(190, 190, 190, 250);
    }
}
