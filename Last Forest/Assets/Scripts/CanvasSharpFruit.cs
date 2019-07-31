using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSharpFruit : MonoBehaviour
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
        
        this.gameObject.transform.Find("Button").gameObject.GetComponent<Image>().color = new Color32(255, 112, 131, 255);
        
    }

    public void NoImage()
    {
        this.gameObject.transform.Find("Button").gameObject.GetComponent<Image>().sprite = canvasTree.noImage;
        this.gameObject.transform.Find("Button").gameObject.GetComponent<Image>().color = new Color32(190, 190, 190, 0);
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
