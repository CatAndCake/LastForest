using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemInformations : MonoBehaviour
{
    GemsManager gemsManager;
    public string gemType;
    public Sprite gemImage;
    public int gemPower;
    public int gemMeshNumber;

    private void Awake()
    {
        gemsManager = GameObject.FindGameObjectWithTag("GemsManager").GetComponent<GemsManager>();
    }

    private void Start()
    {
        GemInfo();
    }

    void GemInfo()
    {
        gemsManager.GemType();
        gemType = gemsManager.gemType;
        gemsManager.gemType = null;
        gemMeshNumber = gemsManager.gemMeshNumber;

        gemsManager.GemPower();
        gemPower = gemsManager.gemPower;
        gemsManager.gemPower = 0;

        gemsManager.SpawnGemMesh(this.gameObject);
    }
    
}
