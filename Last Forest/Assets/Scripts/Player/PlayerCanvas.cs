using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanvas : MonoBehaviour
{
    GameObject environments;
    bool environmentEnabled;

    private void Awake()
    {
        environments = GameObject.FindGameObjectWithTag("EnvironmentModifications").transform.Find("Environment").gameObject;
    }

    private void Update()
    {
        
    }

    public void EnvironemtEnabled()
    {
        environmentEnabled = !environmentEnabled;

        if(environmentEnabled == true)
        {
            environments.SetActive(true);
        }

        if(environmentEnabled == false)
        {
            environments.SetActive(false);
        }
    }
}
