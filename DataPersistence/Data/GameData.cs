using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public float poise; 
    public float iFrameTime; 
    public float velo; 
    public float jumpVelo; 
    public bool doubleJump; 
    public float hangTime;
    public float dashCooldownTime;
    public int maxHealth;
    public int currentHealth;
    public PlayerData playerData;
    public SaveSystemSerializableDictionary<string, bool> itemsCollected;
    
    public GameData()
    {
        playerData = new PlayerData();
        itemsCollected = new SaveSystemSerializableDictionary<string, bool>();
        maxHealth = 3;
        currentHealth = maxHealth;
        poise = 1f;
        iFrameTime = .2f;
        velo = 8f;
        jumpVelo = 10f;
        doubleJump = false;
        hangTime = 2f;
        dashCooldownTime = .3f;
    }

    public void PassAllData()
    {
        playerData.SetAllStats(maxHealth, poise, iFrameTime, velo, jumpVelo, doubleJump, hangTime, dashCooldownTime);
    }
}
