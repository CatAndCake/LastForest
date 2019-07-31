using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSword : MonoBehaviour
{
    CanvasPlayer canvasPlayer;

    public int number;
    public int efficiency;
    public int currentEfficiency;
    public int sharpness;
    public int weight;
    public int width;
    public int savedSouls;
    public bool holding;

    private void Awake()
    {
        canvasPlayer = GameObject.FindGameObjectWithTag("CanvasPlayer").GetComponent<CanvasPlayer>();
    }


    public void ShowImage()
    {
        this.gameObject.transform.Find("Button").gameObject.GetComponent<Image>().sprite = canvasPlayer.swordImage;
        this.gameObject.transform.Find("Button").gameObject.GetComponent<Image>().color = new Color32(190, 190, 190, 255);

        if(holding == true)
        {
            this.gameObject.transform.Find("HoldingSign").gameObject.SetActive(true);
        }
        if(holding == false)
        {
            this.gameObject.transform.Find("HoldingSign").gameObject.SetActive(false);
        }
    }

    public void NoImage()
    {
        this.gameObject.transform.Find("Button").gameObject.GetComponent<Image>().sprite = null;//canvasPlayer.noImage;
        this.gameObject.transform.Find("Button").gameObject.GetComponent<Image>().color = new Color32(190, 190, 190, 0);
        this.gameObject.transform.Find("HoldingSign").gameObject.SetActive(false);
    }

    public void ShowSavedSouls()
    {
        this.gameObject.transform.Find("SavedSouls").transform.Find("Text").gameObject.GetComponent<Text>().text = "Saved Souls " + savedSouls;
    }

    public void NoSavedSouls()
    {
        this.gameObject.transform.Find("SavedSouls").transform.Find("Text").gameObject.GetComponent<Text>().text = null;
    }

    public void ShowEfficiency()
    {
        this.gameObject.transform.Find("Efficiency").gameObject.SetActive(true);

        float calculatedEffi = ((currentEfficiency * 100f) / efficiency) / 100;

        this.gameObject.transform.Find("Efficiency").transform.Find("EfiBar").
            transform.Find("Background").transform.Find("BarValue").transform.localScale = new Vector3(calculatedEffi, 1, 1);
    }

    public void NoEfficiency()
    {
        this.gameObject.transform.Find("Efficiency").gameObject.SetActive(false);
    }

    public void SelectThisSlot()
    {
        this.gameObject.transform.Find("Image").gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    public void UnselectThisSlot()
    {
        this.gameObject.transform.Find("Image").gameObject.GetComponent<Image>().color = new Color32(190, 190, 190, 255);
    }

    
}
