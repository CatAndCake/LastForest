using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveAndLoadDelete 
{
    public int level;
    public int health;
    public float[] position;

    public SaveAndLoadDelete (InfoToSaveDelete infoToSave)
    {
        level = infoToSave.level;
        health = infoToSave.health;
        position = new float[3];
        position[0] = infoToSave.transform.position.x;
        position[1] = infoToSave.transform.position.y;
        position[2] = infoToSave.transform.position.z;
    }
    
}
