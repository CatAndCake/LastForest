using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CanvasInfoText : MonoBehaviour
{
    GameObject information;
    Text text;


    static string ntext;
    static float time;
    static float timer;

    private void Awake()
    {
        text = this.gameObject.transform.Find("Panel").transform.Find("Information").gameObject.GetComponent<Text>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        Text();
    }

    public void Text()
    {
        if(timer <= time)
        {
            if(ntext.Length <= 35)
            {
                text.fontSize = 20;
            }

            if(ntext.Length > 35)
            {
                text.fontSize = 15;
            }

            text.text = ntext;
        }
        if(timer > time)
        {
            text.text = "";
            ntext = "";
            time = 0f;
        }
    }

    public static void SetText(string sText, float sTime)
    {
        timer = 0f;
        ntext = sText;
        time = sTime;
    }
}

