using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class SaveData
{
    public float playerX;
    public float playerY;
    public bool isMirrorWorld;
    public string sceneName;

    public SaveData() 
    {
        playerX = 0f;
        playerY = 0f;
        isMirrorWorld = false;
        sceneName = "SampleScene"; 
    }
}