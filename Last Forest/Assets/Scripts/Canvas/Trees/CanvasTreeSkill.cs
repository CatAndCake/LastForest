using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasTreeSkill : MonoBehaviour
{
    public GameObject tree;
    public string skill;
    public string text;
    
    // Update is called once per frame
    void Update()
    {
        if(text != null)
        {
            this.gameObject.transform.Find("Text").GetComponent<Text>().text = text;
        }
    }
}
