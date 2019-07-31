using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SaveNames
{
    public int SaveNumber;
    public string SaveTime;


    public SaveNames(int saveNumber, string saveTime)
    {
        this.SaveNumber = saveNumber;
        this.SaveTime = saveTime;
    }
}

[System.Serializable]
public class FruitTrees
{
    //tree size
    //when fruitage?

    public string Type;
    public int Level;
    public int Points;
    public int Exp;
    public int ExpToUpgdrade;
    public int HealthCurrent;
    public int HealthMax;
    public int HealthReg;
    public int HealthRegMax;
    public int Fruits;
    public int FruitsMax;
    public int FruitsPower;
    public int FruitsPowerMax;
    public int FruitsWild;
    public int FruitsWildMax;

    public string[] FruitType;
    public int[] FruitPower;
    public int[] FruitNumber;

    public float[] Location;

    public FruitTrees(string type, int level, int points, int exp, int expToUpgrade, int healthCurrent,
        int healthMax, int healthReg, int healthRegMax, int fruits, int fruitsMax, int fruitsPower,
        int fruitPowerMax, int fruitsWild, int fruitsWildMax, string[] fruitType, int[] fruitPower, int[] fruitNumber, float[] location)
    {
        this.Type = type;
        this.Level = level;
        this.Points = points;
        this.Exp = exp;
        this.ExpToUpgdrade = expToUpgrade;
        this.HealthCurrent = healthCurrent;
        this.HealthMax = healthMax;
        this.HealthReg = healthReg;
        this.HealthRegMax = healthRegMax;
        this.Fruits = fruits;
        this.FruitsMax = fruitsMax;
        this.FruitsPowerMax = fruitPowerMax;
        this.FruitsWild = fruitsWild;
        this.FruitsWildMax = fruitsWildMax;
        this.FruitType = fruitType;
        this.FruitPower = fruitPower;
        this.FruitNumber = fruitNumber;
        this.Location = location;
    }
}

[System.Serializable]
public class Dupa1
{
    public int Gowno1;

    public Dupa1(int gowno1)
    {
        this.Gowno1 = gowno1;
    }

}

[System.Serializable]
public class Dupa2
{
    public int Gowno2;

    public Dupa2(int gowno2)
    {
        this.Gowno2 = gowno2;
    }

}

