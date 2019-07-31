using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Sword : MonoBehaviour
{
    SwordsManager swordsManager;

    GameObject swordMesh;
    public int number;
    public int efficiency = 0;
    public int currentEfficiency;
    public int sharpness = 0;
    public int weight = 0;
    public int width = 0;
    public int savedSouls;
    public bool holding = false;

    private void Awake()
    {
        swordsManager = GameObject.FindGameObjectWithTag("SwordsManager").GetComponent<SwordsManager>();
        swordMesh = this.gameObject.transform.Find("Mesh").gameObject;
        swordsManager.nSword = this.gameObject;
        swordsManager.SwordNumber();
        currentEfficiency = efficiency;
    }

    private void Start()
    {
        SetSwordWide();
    }
    void SetSwordWide()
    {
        if(width <= 8)
        {
            swordMesh.transform.localScale = new Vector3(1f + (width * 0.025f), 1f + (width * 0.025f), 1f + (width * 0.025f));
        }
        if(width > 8)
        {
            swordMesh.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        }
    }
}
