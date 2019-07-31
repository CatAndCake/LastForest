using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemsManager : MonoBehaviour
{
    public GameObject gemMesh;
    public string gemType;
    public int gemPower;
    public int gemMeshNumber;

    public void GemType()
    {
        float randomGem = Random.Range(0, 100);

        if (randomGem <= 82)
        {
            int randomGem2 = Random.Range(0, 3);
            if (randomGem2 == 0)
            {
                gemType = ("Health");
            }

            if (randomGem2 == 1)
            {
                gemType = ("Exp");
            }

            if (randomGem2 == 2)
            {
                gemType = ("Fruits");
            }
        }

        if (randomGem > 82 && randomGem < 95)
        {
            int randomGem2 = Random.Range(0, 11);

            if (randomGem2 == 0)
            {
                gemType = ("Super Health");
            }
            if (randomGem2 == 1)
            {
                gemType = ("Recovery");
            }
            if (randomGem2 == 2)
            {
                gemType = ("Super Fruits");

            }
            if (randomGem2 == 3)
            {
                gemType = ("Fruits Power");

            }
            if (randomGem2 == 4)
            {
                gemType = ("Wild Fruits");

            }
            if (randomGem2 == 5)
            {
                gemType = ("Attack");

            }
            if (randomGem2 == 6)
            {
                gemType = ("Attack Quality");

            }
            if (randomGem2 == 7)
            {
                gemType = ("Attack Distance");

            }
            if (randomGem2 == 8)
            {
                gemType = ("Hydration");
            }
            if (randomGem2 == 9)
            {
                gemType = ("Light Branches");
            }
            if (randomGem2 == 10)
            {
                gemType = ("Attraction");
            }
        }

        if(randomGem >= 95 && randomGem <= 100)
        {
            int randomGem2 = Random.Range(0, 4);
            if(randomGem2 <= 2)
            {
                gemType = ("Skill");
            }

            if(randomGem2 == 3)
            {
                gemType = ("Level");
            }
        }
    }

    public void GemPower()
    {
        int randomLevel = Random.Range(0, 101);

        if(randomLevel >= 0 && randomLevel <= 50)
        {
            gemPower = 1;
        }
        if(randomLevel > 50 && randomLevel <= 90)
        {
            gemPower = 2;
        }
        if(randomLevel > 90 && randomLevel <= 95)
        {
            gemPower = 3;
        }
        if(randomLevel > 95 && randomLevel <= 99)
        {
            gemPower = 4;
        }
        if(randomLevel > 99 && randomLevel <= 101)
        {
            gemPower = 5;
        }


    }

    public void SpawnGemMesh(GameObject gem)
    {
       GameObject newMesh = Instantiate(gemMesh, new Vector3(
            gem.transform.position.x,
            gem.transform.position.y,
            gem.transform.position.z),
            Quaternion.identity);

        newMesh.transform.SetParent(gem.transform);
    }

}