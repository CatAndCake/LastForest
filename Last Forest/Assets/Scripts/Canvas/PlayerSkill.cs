using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    public string text;

    private void Start()
    {
        if(text != null)
        {
            this.gameObject.transform.Find("Text").GetComponent<Text>().text = text;
        }
    }
}
