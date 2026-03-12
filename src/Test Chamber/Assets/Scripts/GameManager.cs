using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int playerMoney = 0;

    void Awake()
    {
        instance = this;
    }

    public void AddMoney(int amount)
    {
        playerMoney += amount;
        
        Debug.Log("Money: " + playerMoney);
    }
}
