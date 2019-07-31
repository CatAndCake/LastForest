using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillButton : MonoBehaviour
{
    PlayerStatistics playerStatistics;
    public string text;

    void Start()
    {
        playerStatistics = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatistics>();

        if(text != null)
        {
            this.gameObject.transform.Find("Text").GetComponent<Text>().text = text;
        }
    }
}
