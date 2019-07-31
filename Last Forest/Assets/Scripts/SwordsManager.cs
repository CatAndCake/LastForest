using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordsManager : MonoBehaviour
{
    // How this scripts workds?
    // First after winning a fight with enemy random sword can spawn
    // In attached script to that sword we will set in this script
    // that nSword is exactly that sword, then in attached script we will start all functions
    // at the end we will set nSword to null

    public GameObject nSword;

    public string swordType;
    public int swordsNumbers = 0;
    public int swordSharpness;
    public int swordWeight;
    public int swordWidth;

    
    public void SwordNumber()
    {
        nSword.GetComponent<Sword>().number = swordsNumbers;
        swordsNumbers = swordsNumbers + 1;

        SwordSharpness();
    }

    //Sharpness
    public void SwordSharpness()
    {
        int canMakeSwordSharper = Random.Range(0, 6);

        if(canMakeSwordSharper <= 2)
        {
            SwordWeight();
        }

        if(canMakeSwordSharper > 2)
        {
            swordSharpness = swordSharpness + 1;
            SwordSharpness();
        }
        
    }

    //Weight
    public void SwordWeight()
    {
        int canMakeSwordHeavier = Random.Range(0, 10);

        if(canMakeSwordHeavier <= 1)
        {
            SwordWidth();
        }

        if(canMakeSwordHeavier > 1)
        {
            swordWeight = swordWeight + 1;
            SwordWeight();
        }
    }

    // Width

    public void SwordWidth()
    {
        int canMakeSwordWider = Random.Range(0, 6);

        if (canMakeSwordWider <= 2)
        {
            SetValuesForNewSword();
        }

        if (canMakeSwordWider > 2)
        {
            swordWidth = swordWidth + 1;
            SwordWidth();
        }
    }

    void SetValuesForNewSword()
    {
        Sword sword = nSword.GetComponent<Sword>();

        int swordEfficiency = Random.Range(80, 121);

        sword.efficiency = sword.efficiency + swordEfficiency;

        sword.sharpness = sword.sharpness + swordSharpness;
        sword.weight = sword.weight + swordWeight;
        sword.width = sword.width + swordWidth;

        nSword = null;
        swordType = null;
        swordSharpness = 0;
        swordWeight = 0;
        swordWidth = 0;

    }
}
