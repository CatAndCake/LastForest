using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CanvasGem : MonoBehaviour
{
    //Images for fruit and gem are located in canvas tree.
    //If there will be a sens to move them to different file then files localisation will be changed
    
    CanvasTreeEnable canvasTree;

    public Sprite noImage;

    public string gemType;
    public Sprite gemImage;
    public int gemNumber;
    public int gemPower;

    private void Awake()
    {
        canvasTree = GameObject.FindGameObjectWithTag("CanvasTree").GetComponent<CanvasTreeEnable>();
    }


    public void GemImage()
    {
        this.gameObject.transform.Find("Button").gameObject.GetComponent<Image>().sprite = canvasTree.gemImage;

        if (gemType == "Health")
        {
            this.gameObject.transform.Find("Button").gameObject.GetComponent<Image>().color = new Color32(143, 233, 129, 255);
        }
        if (gemType == "Exp")
        {
            this.gameObject.transform.Find("Button").gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        if (gemType == "Fruits")
        {
            this.gameObject.transform.Find("Button").gameObject.GetComponent<Image>().color = new Color32(253, 245, 137, 255);
        }
        if (gemType == "Super Health")
        {
            this.gameObject.transform.Find("Button").gameObject.GetComponent<Image>().color = new Color32(20, 192, 1, 255);
        }
        if (gemType == "Recovery")
        {
            this.gameObject.transform.Find("Button").gameObject.GetComponent<Image>().color = new Color32(69, 224, 58, 255);
        }
        if (gemType == "Super Fruits")
        {
            this.gameObject.transform.Find("Button").gameObject.GetComponent<Image>().color = new Color32(255, 231, 11, 255);
        }
        if (gemType == "Fruits Power")
        {
            this.gameObject.transform.Find("Button").gameObject.GetComponent<Image>().color = new Color32(255, 159, 0, 255);
        }
        if (gemType == "Wild Fruits")
        {
            this.gameObject.transform.Find("Button").gameObject.GetComponent<Image>().color = new Color32(255, 170, 0, 255);
        }
        if (gemType == "Attack")
        {
            this.gameObject.transform.Find("Button").gameObject.GetComponent<Image>().color = new Color32(119, 112, 255, 255);
        }
        if (gemType == "Attack Quality")
        {
            this.gameObject.transform.Find("Button").gameObject.GetComponent<Image>().color = new Color32(255, 25, 150, 255);
        }
        if (gemType == "Attack Distance")
        {
            this.gameObject.transform.Find("Button").gameObject.GetComponent<Image>().color = new Color32(232, 25, 255, 255);
        }
        if (gemType == "Hydration")
        {
            this.gameObject.transform.Find("Button").gameObject.GetComponent<Image>().color = new Color32(0, 214, 245, 255);
        }
        if(gemType == "Light Branches")
        {
            this.gameObject.transform.Find("Button").gameObject.GetComponent<Image>().color = new Color32(96, 255, 255, 255);
        }
        if(gemType == "Attraction")
        {
            this.gameObject.transform.Find("Button").gameObject.GetComponent<Image>().color = new Color32(61, 255, 217, 255);
        }
        if (gemType == "Skill")
        {
            this.gameObject.transform.Find("Button").gameObject.GetComponent<Image>().color = new Color32(170, 208, 255, 255);
        }
        if (gemType == "Level")
        {
            this.gameObject.transform.Find("Button").gameObject.GetComponent<Image>().color = new Color32(92, 162, 248, 255);

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
