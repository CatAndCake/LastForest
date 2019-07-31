using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasPlayerInfo : MonoBehaviour
{
    public static string text;

    private void Update()
    {
        this.gameObject.GetComponent<Text>().text = text;
    }
}
