using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasTreeFruit : MonoBehaviour
{
    
    CanvasTreeEnable canvasTree;

    public string fruitType;
    
    public int fruitNumber;
    public int fruitPower;

    private void Awake()
    {
        canvasTree = GameObject.FindGameObjectWithTag("CanvasTree").GetComponent<CanvasTreeEnable>();
    }

    public void ShowImage()
    {
        this.gameObject.transform.Find("Button").gameObject.GetComponent<Image>().sprite = canvasTree.fruitImage;
        this.gameObject.transform.Find("Button").gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);

        if (fruitType == "Food Fruit")
        {
            this.gameObject.transform.Find("Button").gameObject.GetComponent<Image>().color = new Color32(119, 250, 155, 255);
        }
        if (fruitType == "Fat Fruit")
        {
            this.gameObject.transform.Find("Button").gameObject.GetComponent<Image>().color = new Color32(27, 221, 9, 255);
        }
        if (fruitType == "Health Fruit")
        {
            this.gameObject.transform.Find("Button").gameObject.GetComponent<Image>().color = new Color32(241, 250, 100, 255);
        }
        if (fruitType == "Max Health Fruit")
        {
            this.gameObject.transform.Find("Button").gameObject.GetComponent<Image>().color = new Color32(255, 98, 0, 255);
        }
        if (fruitType == "Recovery Fruit")
        {
            this.gameObject.transform.Find("Button").gameObject.GetComponent<Image>().color = new Color32(248, 222, 0, 255);

        }
        if (fruitType == "Defence Fruit")
        {
            this.gameObject.transform.Find("Button").gameObject.GetComponent<Image>().color = new Color32(255, 165, 0, 255);
        }
        if (fruitType == "Energy Fruit")
        {
            this.gameObject.transform.Find("Button").gameObject.GetComponent<Image>().color = new Color32(141, 248, 228, 255);
        }
        if (fruitType == "Max Energy Fruit")
        {
            this.gameObject.transform.Find("Button").gameObject.GetComponent<Image>().color = new Color32(74, 125, 248, 255);
        }
        if (fruitType == "Breath Fruit")
        {
            this.gameObject.transform.Find("Button").gameObject.GetComponent<Image>().color = new Color32(0, 172, 255, 255);
        }
        if (fruitType == "Light Fruit")
        {
            this.gameObject.transform.Find("Button").gameObject.GetComponent<Image>().color = new Color32(126, 234, 250, 255);
        }
        if (fruitType == "Sharp Fruit")
        {
            this.gameObject.transform.Find("Button").gameObject.GetComponent<Image>().color = new Color32(255, 112, 131, 255);
        }
        if (fruitType == "Strength Fruit")
        {
            this.gameObject.transform.Find("Button").gameObject.GetComponent<Image>().color = new Color32(248, 0, 37, 255);
        }
        if (fruitType == "Wisdom Fruit")
        {
            this.gameObject.transform.Find("Button").gameObject.GetComponent<Image>().color = new Color32(239, 137, 253, 255);
        }
        if (fruitType == "Sage Fruit")
        {
            this.gameObject.transform.Find("Button").gameObject.GetComponent<Image>().color = new Color32(212, 16, 255, 255);
        }
    }

    public void NoImage()
    {
        this.gameObject.transform.Find("Button").gameObject.GetComponent<Image>().sprite = canvasTree.noImage;
        this.gameObject.transform.Find("Button").gameObject.GetComponent<Image>().color = new Color32(190, 190, 190, 255);
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
